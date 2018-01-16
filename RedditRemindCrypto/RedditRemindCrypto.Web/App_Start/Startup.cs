using Autofac;
using Autofac.Integration.Mvc;
using Hangfire;
using Microsoft.Owin;
using Owin;
using RedditRemindCrypto.Business;
using RedditRemindCrypto.Business.Clients.CoinMarketCap;
using RedditRemindCrypto.Business.Clients.CoinMarketCap.Decorators;
using RedditRemindCrypto.Business.Clients.FixerIO;
using RedditRemindCrypto.Business.Clients.FixerIO.Decorators;
using RedditRemindCrypto.Business.Expressions;
using RedditRemindCrypto.Business.Expressions.Converters;
using RedditRemindCrypto.Business.Expressions.Parsers;
using RedditRemindCrypto.Business.Factories;
using RedditRemindCrypto.Business.Services;
using RedditRemindCrypto.Business.Settings;
using RedditRemindCrypto.Web.App_Start;
using RedditRemindCrypto.Web.Code;
using RedditRemindCrypto.Web.Factories;
using RedditRemindCrypto.Web.Settings;
using System;
using System.Web.Mvc;

[assembly: OwinStartup(typeof(Startup))]
namespace RedditRemindCrypto.Web.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = RegisterAutofac();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            ConfigureHangFire(app, container);
        }

        private IContainer RegisterAutofac()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<BotSettings>().As<IBotSettings>();
            builder.RegisterType<ConnectionStringFactory>().As<IConnectionStringFactory>();
            builder.RegisterType<CoinMarketCapClient>().Named<ICoinMarketCapClient>("Client").InstancePerLifetimeScope();
            builder.RegisterDecorator<ICoinMarketCapClient>((c, decoratee) => new CoinMarketCapClientCachingDecorator(decoratee), "Client");
            builder.RegisterType<FixerClient>().Named<IFixerClient>("Client").InstancePerLifetimeScope();
            builder.RegisterDecorator<IFixerClient>((c, decoratee) => new FixerClientCachingDecorator(decoratee), "Client");
            builder.RegisterType<CurrencyConverter>();
            builder.RegisterType<RedditClientFactory>();
            builder.RegisterType<ExpressionEvaluator>();
            builder.RegisterType<CurrencyService>().As<ICurrencyService>();
            builder.RegisterType<RemindRequestService>().As<IRemindRequestService>();
            builder.RegisterType<CurrencyParser>();
            builder.RegisterType<ExpressionOperatorParser>();
            builder.RegisterType<ExpressionReader>();
            builder.RegisterType<ExpressionExtractor>();
            builder.RegisterType<RedditUnreadMessagesReader>();
            builder.RegisterType<RemindRequestHandler>();
            builder.RegisterType<RemindRequestProcessor>();
            builder.RegisterType<AutoPopularCoinAdder>();

            builder.RegisterType<PackagesConfigReader>();

            return builder.Build();
        }

        public void ConfigureHangFire(IAppBuilder app, IContainer container)
        {
            var factory = new ConnectionStringFactory();
            GlobalConfiguration.Configuration.UseSqlServerStorage(factory.Create());
            GlobalConfiguration.Configuration.UseActivator(new ContainerJobActivator(container));

            app.UseHangfireDashboard();
            app.UseHangfireServer();

            RecurringJob.AddOrUpdate<RedditUnreadMessagesReader>("Bot_UnreadMessageReader", x => x.ReadUnreadComments(), Cron.Minutely);
            RecurringJob.AddOrUpdate<RedditUnreadMessagesReader>("Bot_UnreadPrivateMessages", x => x.ReadUnreadPrivateMessages(), Cron.Minutely);
            RecurringJob.AddOrUpdate<RemindRequestProcessor>("Bot_RemindRequestProcessor", x => x.Process(), Cron.MinuteInterval(5));
            RecurringJob.AddOrUpdate<AutoPopularCoinAdder>("AutoAdder_PopularCoins", x => x.AutoAddPopularCoins(200), Cron.Daily(1));
        }

        public class ContainerJobActivator : JobActivator
        {
            private IContainer _container;

            public ContainerJobActivator(IContainer container)
            {
                _container = container;
            }

            public override object ActivateJob(Type type)
            {
                return _container.Resolve(type);
            }
        }
    }
}