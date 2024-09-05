using System.Web.Mvc;

namespace AppWithGacDependency.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var combinedGreeting = $"{v1Consumer.Consumer.Greet(null)}\n{v2Consumer.Consumer.Greet(null)}";
            return View((object)combinedGreeting);
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