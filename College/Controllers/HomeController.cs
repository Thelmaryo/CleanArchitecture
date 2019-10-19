using College.Helpers;
using College.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace College.Controllers
{
    public class HomeController : ControllerBase
    {
        public ActionResult Index()
        {
            if (!Authentication.UserAuthenticated)
                return RedirectToAction("Login", "Account");
            if (User.IsInRole("Student"))
                return RedirectToAction("Student");
            else if (User.IsInRole("Professor"))
                return RedirectToAction("Professor");
            else if (User.IsInRole("Admin"))
                return View();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Professor()
        {
            if (!User.IsInRole("Professor"))
                return RedirectToAction("Index", "Home");
            Discipline discipline = new Discipline();
            ViewBag.Disciplines = discipline.GetByProfessor(User.Id);
            return View();
        }

        public ActionResult Student()
        {
            if (!User.IsInRole("Student"))
                return RedirectToAction("Index", "Home");
            Discipline discipline = new Discipline();
            Enrollment enrollment = new Enrollment();
            enrollment.GetCurrent(User.Id);
            ViewBag.Disciplines = discipline.GetByEnrollment(enrollment.Id);
            ViewBag.StatusEnrollment = enrollment.Status;
            List<Enrollment> enrollments = enrollment.GetByStudent(User.Id).OrderBy(x => x.Begin).ToList();
            enrollments.RemoveAll(x=>x.Id == enrollment.Id);
            ViewBag.Enrollments = enrollments;
            return View();
        }
    }
}