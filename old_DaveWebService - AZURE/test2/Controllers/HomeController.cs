using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace daveWebService.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            int currentHour = DateTime.Now.Hour;
            int currentMin = DateTime.Now.Minute;
            ViewBag.TimeMessage01 = String.Format("The time is: {0:00}:{1:00}", currentHour, currentMin);
            return View();
        }

        public ActionResult WebPage2()
        {
            return View();
        }

    }
}
