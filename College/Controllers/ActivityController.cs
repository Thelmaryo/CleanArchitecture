using College.Models;
using College.UseCases.AccountContext.Queries;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace College.Controllers
{
    public class ActivityController : ControllerBase
    {
        public ActivityController(UserQueryHandler userQuery) : base(userQuery)
        {
        }

        // GET: Activity
        public ActionResult Index(Guid disciplineId)
        {
            if (!UserIsInRole("Professor"))
                return RedirectToAction("Index", "Home");
            Discipline discipline = new Discipline();
            discipline.Get(disciplineId);
            ViewBag.DisciplineId = disciplineId;
            ViewBag.Discipline = discipline.Name;
            var activity = new Activity();
            return View(activity.GetByDiscipline(disciplineId, new Semester()));
        }

        public ActionResult Grades(Guid activityId)
        {
            if (!UserIsInRole("Professor"))
                return RedirectToAction("Index", "Home");
            Activity activity = new Activity();
            activity.Get(activityId);
            Student student = new Student();
            var students = student.GetByDiscipline(activity.DisciplineId);
            List<ActivityGrade> grades = new List<ActivityGrade>();
            foreach (var s in students)
            {
                var grade = new ActivityGrade();
                grade.GetByStudent(s.Id, activityId);
                grade.ActivityId = activityId;
                grade.StudentId = s.Id;
                grades.Add(grade);
            }
            ViewBag.Students = students;
            ViewBag.Activity = activity;
            return View(grades);
        }

        public ActionResult GiveGrade(Guid studentId, Guid activityId)
        {
            if (!UserIsInRole("Professor"))
                return RedirectToAction("Index", "Home");
            Student student = new Student();
            student.Get(studentId);
            ViewBag.Student = student.FirstName + " " + student.LastName;
            var grade = new ActivityGrade();
            grade.GetByStudent(studentId, activityId);
            if (grade.Id == Guid.Empty)
                return View(new ActivityGrade { ActivityId = activityId, StudentId = studentId });
            return View(grade);
        }

        [HttpPost]
        public ActionResult GiveGrade(ActivityGrade activityGrade)
        {
            if (!UserIsInRole("Professor"))
                return RedirectToAction("Index", "Home");
            Activity activity = new Activity();
            activity.Get(activityGrade.ActivityId);
            if (activityGrade.Grade > activity.Value)
            {
                ModelState.AddModelError("Grade", $"A atividade vale {activity.Value} pontos");
                return View(activityGrade);
            }
            if (activityGrade.Id == Guid.Empty)
                activityGrade.Create();
            else
                activityGrade.Edit();
            return RedirectToAction("Grades", new { activityId = activityGrade.ActivityId });
        }

        // GET: Activity/Details/5
        public ActionResult Details(Guid id)
        {
            if (!UserIsInRole("Docente"))
                return RedirectToAction("Index", "Home");
            var activity = new Activity();
            activity.Get(id);
            return View(activity);
        }

        // GET: Activity/Create
        public ActionResult Create(Guid disciplineId)
        {
            if (!UserIsInRole("Professor"))
                return RedirectToAction("Index", "Home");
            return View(new Activity { DisciplineId = disciplineId, Date = DateTime.Now.Date });
        }

        // POST: Activity/Create
        [HttpPost]
        public ActionResult Create(Activity activity)
        {
            if (!UserIsInRole("Professor"))
                return RedirectToAction("Index", "Home");
            try
            {
                // TODO: Add insert logic here
                activity.Create();
                return RedirectToAction("Index", new { disciplineId = activity.DisciplineId });
            }
            catch (Exception e)
            {
                return View(activity);
            }
        }

        // GET: Activity/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (!UserIsInRole("Professor"))
                return RedirectToAction("Index", "Home");
            var activity = new Activity();
            activity.Get(id);
            return View(activity);
        }

        // POST: Activity/Edit/5
        [HttpPost]
        public ActionResult Edit(Activity activity)
        {
            if (!UserIsInRole("Professor"))
                return RedirectToAction("Index", "Home");
            try
            {
                // TODO: Add update logic here
                activity.Edit();
                return RedirectToAction("Index", new { disciplineId = activity.DisciplineId });
            }
            catch
            {
                return View(activity);
            }
        }

        // GET: Activity/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (!UserIsInRole("Professor"))
                return RedirectToAction("Index", "Home");
            var activity = new Activity();
            activity.Get(id);
            return View(activity);
        }

        // POST: Activity/Delete/5
        [HttpPost]
        public ActionResult Delete(Activity activity)
        {
            if (!UserIsInRole("Professor"))
                return RedirectToAction("Index", "Home");
            try
            {
                // TODO: Add delete logic here
                activity.Delete();
                return RedirectToAction("Index", new { disciplineId = activity.DisciplineId });
            }
            catch
            {
                return View(activity);
            }
        }
    }
}