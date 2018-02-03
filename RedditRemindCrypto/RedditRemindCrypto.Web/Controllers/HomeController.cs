using RedditRemindCrypto.Business.Services;
using RedditRemindCrypto.Business.Services.Enums;
using RedditRemindCrypto.Web.Models.Home;
using System.Linq;
using System.Web.Mvc;

namespace RedditRemindCrypto.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICurrencyService currencyService;

        public HomeController(ICurrencyService currencyService)
        {
            this.currencyService = currencyService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HowTo()
        {
            return View();
        }

        public ActionResult SupportedCurrencies()
        {
            var currencies = currencyService.GetAll();
            var model = new SupportedCurrenciesModel
            {
                FiatCurrencies = currencies.Where(x => x.CurrencyType == CurrencyType.Fiat),
                CryptoCurrencies = currencies.Where(x => x.CurrencyType == CurrencyType.Crypto)
            };
            return View(model);
        }

        public ActionResult Developers()
        {
            return View();
        }
    }
}