using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tangent.Helpers;
using Tangent.Models;

namespace Tangent.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            User loggedInUser = DAL.Login(user);
            if (loggedInUser.token != null)
            {
                return RedirectToAction("Index", "Project");
            }
            else
            {
                return View();
            }
        }
    }
}