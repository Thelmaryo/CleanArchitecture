using College.Helpers;
using College.Models;
using System;
using System.Security.Cryptography;
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
            string userName = user.GetSalt(user.UserName);
            if (string.IsNullOrEmpty(userName))
            {
                ViewBag.Error = "O Usuario não existe.";
                return View(user);
            }
            // DESCRIPTOGRAFA
            byte[] SALT = new byte[8];
            int i = 0;
            var salt = userName.Split(',');
            foreach (var stringSalt in salt)
            {
                SALT[i] = Convert.ToByte(stringSalt);
                i++;
            }
            int myIterations = 100000;
            Rfc2898DeriveBytes k = new Rfc2898DeriveBytes(user.Password, SALT, myIterations);
            user.Password = Convert.ToBase64String(k.GetBytes(32));

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
            if (Authentication.UserAuthenticated)
                new User().Logout();
            return RedirectToAction("Login");
        }
    }
}