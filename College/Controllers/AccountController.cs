using College.Helpers;
using College.Models;
using College.UseCases.AccountContext.Inputs;
using College.UseCases.AccountContext.Queries;
using System;
using System.Security.Cryptography;
using System.Web.Mvc;

namespace College.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly UserQueryHandler _userQuery;

        public AccountController(UserQueryHandler userQuery) : base (userQuery)
        {
            _userQuery = userQuery;
        }

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            var result = _userQuery.Handle(new UserInputLogin { Password = user.Password, UserName = user.UserName });
            if(result.UserId != Guid.Empty)
            {
                Authentication.UserAuthenticated = true;
                Authentication.UserId = result.UserId;
            }
            else
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