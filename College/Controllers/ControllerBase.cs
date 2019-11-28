using College.Helpers;
using College.UseCases.AccountContext.Inputs;
using College.UseCases.AccountContext.Queries;
using System.Web.Mvc;

namespace College.Controllers
{
    public class ControllerBase : Controller
    {
        private readonly UserQueryHandler _userQuery;

        public ControllerBase(UserQueryHandler userQuery)
        {
            _userQuery = userQuery;
        }

        protected bool UserIsInRole(string role)
        {
            return _userQuery.Handle(new UserInputIsInRole { Role = role, UserId = Authentication.UserId }).IsInRole;
        }
    }
}