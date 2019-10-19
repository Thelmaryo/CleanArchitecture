using College.Helpers;
using College.Models;
using System.Web.Mvc;

namespace College.Controllers
{
    public class ControllerBase : Controller
    {
        protected User User;
        public ControllerBase()
        {
            User = new User()
            {
                Id = Authentication.UserId
            };
        }
    }
}