using College.Models;
using College.UseCases.AccountContext.Queries;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace College.Controllers
{
    public class ExamController : ControllerBase
    {
        public ExamController(UserQueryHandler userQuery) : base(userQuery)
        {
        }

        public ActionResult Index(Guid disciplineId)
        {
            if (!UserIsInRole("Professor"))
                return RedirectToAction("Index", "Home");
            var exam = new Exam();
            IEnumerable<Exam> exams = exam.GetByDiscipline(disciplineId);
            Dictionary<Guid, string> studentsDictionary = new Dictionary<Guid, string>();
            foreach (var e in exams)
            {
                Enrollment enrollment = new Enrollment();
                enrollment.Get(e.EnrollmentId);
                Student student = new Student();
                student.Get(enrollment.StudentId);
                studentsDictionary.Add(enrollment.Id, student.FirstName + " " + student.LastName);
            }
            Discipline discipline = new Discipline();
            discipline.Get(disciplineId);
            ViewBag.Students = studentsDictionary;
            ViewBag.Discipline = discipline.Name;

            return View(exams);
        }

        // GET: Exam/Details/5
        public ActionResult Details(Guid id)
        {
            if (!UserIsInRole("Professor"))
                return RedirectToAction("Index", "Home");
            var exam = new Exam();
            exam.Get(id);
            return View(exam);
        }

        public ActionResult GiveGrade(Guid id)
        {
            if (!UserIsInRole("Professor"))
                return RedirectToAction("Index", "Home");
            var exam = new Exam();
            exam.Get(id);
            Enrollment enrollment = new Enrollment();
            enrollment.Get(exam.EnrollmentId);
            Student student = new Student();
            student.Get(enrollment.StudentId);
            ViewBag.Student = student.FirstName + student.LastName;
            return View(exam);
        }

        [HttpPost]
        public ActionResult GiveGrade(Exam exam)
        {
            if (!UserIsInRole("Professor"))
                return RedirectToAction("Index", "Home");
            if (exam.Exam1 > 20)
                ModelState.AddModelError("Exam1", "A Prova 1 vale 20 pontos.");
            if (exam.Exam2 > 20)
                ModelState.AddModelError("Exam2", "A Prova 2 vale 20 pontos.");
            if (exam.Exam3 > 20)
                ModelState.AddModelError("Exam3", "A Prova 3 vale 20 pontos.");
            if (exam.FinalExam > 100)
                ModelState.AddModelError("FinalExam", "O exame final vale 100 pontos.");
            if (!ModelState.IsValid)
                return View(exam);
            exam.Edit();
            return RedirectToAction("Index", new { disciplineId = exam.DisciplineId });
        }
    }
}