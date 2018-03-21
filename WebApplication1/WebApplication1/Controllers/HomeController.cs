using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Roles = "Admin,Customer")]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin, Customer")]
        public ActionResult About()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}