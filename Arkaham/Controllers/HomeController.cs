using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Arkaham.Models;

namespace Arkaham.Controllers
{
    //[Authorize]
    public class HomeController : Controller

    {
        //[AuthorizeArkaham("Laboratory")]

        [Authorize]
        public ActionResult SecureMethod()
        {
            return View();
        }

        public ActionResult NonSecureMethod()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
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