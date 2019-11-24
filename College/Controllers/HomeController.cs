using College.Helpers;
using College.Models;
using College.UseCases.AccountContext.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace College.Controllers
{
    public class HomeController : ControllerBase
    {
        public HomeController(UserQueryHandler userQuery) : base(userQuery)
        {
        }

        public ActionResult Index()
        {
            if (!Authentication.UserAuthenticated)
                return RedirectToAction("Login", "Account");
            if (UserIsInRole("Student"))
                return RedirectToAction("Student");
            else if (UserIsInRole("Professor"))
                return RedirectToAction("Professor");
            else if (UserIsInRole("Admin"))
                return View();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Professor()
        {
            if (!UserIsInRole("Professor"))
                return RedirectToAction("Index", "Home");
            Discipline discipline = new Discipline();
            ViewBag.Disciplines = discipline.GetByProfessor(Authentication.UserId);
            return View();
        }

        public ActionResult Student()
        {
            if (!UserIsInRole("Student"))
                return RedirectToAction("Index", "Home");
            Discipline discipline = new Discipline();
            Enrollment enrollment = new Enrollment();
            enrollment.GetCurrent(Authentication.UserId);
            ViewBag.Disciplines = discipline.GetByEnrollment(enrollment.Id);
            ViewBag.StatusEnrollment = enrollment.Status;
            List<Enrollment> enrollments = enrollment.GetByStudent(Authentication.UserId).OrderBy(x => x.Begin).ToList();
            enrollments.RemoveAll(x => x.Id == enrollment.Id);
            ViewBag.Enrollments = enrollments;
            return View();
        }
    }
}