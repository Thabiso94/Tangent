using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tangent.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Error(string message)
        {
            ViewBag.message = message;
            return View();
        }
    }
}