using College.Helpers;
using College.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace College.Controllers
{
    public class AccountController : ControllerBase
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            user.Login();
            if (!Authentication.UserAuthenticated)
            {
                ViewBag.Error = "Não foi possivel efetuar o Login.";
                return View(user);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            if(Authentication.UserAuthenticated)
                new User().Logout();
            return RedirectToAction("Login"); 
        }
    }
}