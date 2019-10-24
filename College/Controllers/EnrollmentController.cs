using College.Helpers;
using College.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace College.Controllers
{
    public class EnrollmentController : ControllerBase
    {
        public ActionResult Index()
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            Enrollment enrollment = new Enrollment();
            var enrollments = enrollment.GetPreEnrollments();
            Student student = new Student();
            ViewBag.Students = student.List();
            return View(enrollments);
        }

        public ActionResult ChooseStudent()
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            Course course = new Course();
            ViewBag.Courses = new SelectList(course.List().Select(x => new ComboboxItem(x.Name, x.Id.ToString())), "Value", "Text");
            if (TempData["Error"] != null)
                ViewBag.Error = TempData["Error"];
            return View();
        }

        // GET: Enrollment/Create
        public ActionResult Create(string CPF, Guid courseId)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            Student student = new Student();
            student.Get(CPF);
            if (student.Id == Guid.Empty)
            {
                TempData["Error"] = "CPF Inválido";
                return RedirectToAction("ChooseStudent");
            }
            Discipline discipline = new Discipline();
            var allDisciplines = discipline.GetByCourse(courseId);
            var concludedDisciplines = discipline.GetConcluded(student.Id);
            DisciplinePortfolio portfolio = new DisciplinePortfolio(allDisciplines.Where(x => !concludedDisciplines.Any(y => y.Id == x.Id)));
            ViewBag.StudentId = student.Id;
            return View(portfolio);
        }

        // POST: Enrollment/Create
        [HttpPost]
        public ActionResult Create(DisciplinePortfolio disciplines, Guid studentId)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            // TODO: Add insert logic here
            if (!disciplines.Options.Any(x => x.Checked))
                return View(disciplines);
            Enrollment enrollment = new Enrollment()
            {
                StudentId = studentId
            };
            if (DateTime.Now.Month <= 6)
            {
                enrollment.Begin = new DateTime(DateTime.Now.Year, 1, 1);
                enrollment.End = new DateTime(DateTime.Now.Year, 6, 30);
            }
            else
            {
                enrollment.Begin = new DateTime(DateTime.Now.Year, 7, 1);
                enrollment.End = new DateTime(DateTime.Now.Year, 12, 30);
            }
            enrollment.Create(disciplines.Options.Where(x => x.Checked));
            return RedirectToAction("Index");
        }

        public ActionResult Confirm(Guid id)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            Enrollment enrollment = new Enrollment
            {
                Id = id
            };
            enrollment.Confirm();
            Exam exam = new Exam
            {
                EnrollmentId = id
            };
            Discipline discipline = new Discipline();
            foreach (var d in discipline.GetByEnrollment(id))
            {
                exam.DisciplineId = d.Id;
                exam.Create();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Deny(Guid id)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            Enrollment enrollment = new Enrollment
            {
                Id = id
            };
            enrollment.Cancel();
            return RedirectToAction("Index");
        }
    }
}
