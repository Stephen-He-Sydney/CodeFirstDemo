using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UBTTest.Data.Domain;
using UBTTest.Web.Cache;

namespace UBTTest.Web.Controllers.MVC
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Admin()
        {
            ViewBag.Title = "Amin Page";

            return View();
        }
    }
}
