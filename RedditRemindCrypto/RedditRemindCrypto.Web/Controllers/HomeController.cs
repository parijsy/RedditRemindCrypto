using RedditRemindCrypto.Business.Services;
using RedditRemindCrypto.Business.Services.Models;
using RedditRemindCrypto.Web.Code;
using RedditRemindCrypto.Web.Models.Home;
using System.Linq;
using System.Web.Mvc;

namespace RedditRemindCrypto.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICurrencyService currencyService;
        private readonly PackagesConfigReader packagesConfigReader;

        public HomeController(ICurrencyService currencyService, PackagesConfigReader packagesConfigReader)
        {
            this.currencyService = currencyService;
            this.packagesConfigReader = packagesConfigReader;
        }

        public ActionResult Index()
        {
            var currencies = currencyService.GetAll();
            var model = new IndexModel
            {
                FiatCurrencies = currencies.Where(x => x.CurrencyType == CurrencyType.Fiat),
                CryptoCurrencies = currencies.Where(x => x.CurrencyType == CurrencyType.Crypto),
                Packages = packagesConfigReader.ReadPackageNames(Server.MapPath(@"~\packages.config"))
            };
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}