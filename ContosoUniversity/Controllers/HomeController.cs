using EFCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContosoUniversity.Controllers
{
    public class HomeController : Controller
    {
       
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Welcome()
        {
            ViewBag.Message = "Your Welcome Page";

            return View();
        }
	}
}