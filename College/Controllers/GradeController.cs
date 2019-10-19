using College.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace College.Controllers
{
    public class GradeController : ControllerBase
    {
        // GET: Grade
        public ActionResult Index(Guid? enrollmentId)
        {
            if (!User.IsInRole("Student"))
                return RedirectToAction("Index", "Home");
            Enrollment enrollment = new Enrollment();
            if (enrollmentId == null)
                enrollment.GetCurrent(User.Id);
            else
                enrollment.Get((Guid)enrollmentId);
            Discipline discipline = new Discipline();
            var disciplines = discipline.GetByEnrollment(enrollment.Id);
            var grades = new List<Grade>();
            foreach (var d in disciplines)
            {
                Grade grade = new Grade
                {
                    DisciplineId = d.Id,
                    Discipline = d.Name
                };
                Activity activity = new Activity();
                var activities = activity.GetByDiscipline(d.Id, new Semester(enrollment.Begin, enrollment.End));
                var activityGrades = new List<ActivityGrade>();
                foreach (var a in activities)
                {
                    ActivityGrade activityGrade = new ActivityGrade();
                    activityGrade.GetByStudent(User.Id, a.Id);
                    activityGrades.Add(activityGrade);
                }
                grade.Value += activityGrades.Sum(x => x.Grade);
                Exam exam = new Exam();
                exam.GetByStudent(enrollment.Id, d.Id);
                grade.Value += exam.Exam1;
                grade.Value += exam.Exam2;
                grade.Value += exam.Exam3;
                grade.FinalExam = exam.FinalExam;
                grades.Add(grade);
            }
            if (enrollmentId == null)
            {
                grades.ForEach(x => x.Status = "Matriculado");
            }
            else
            {
                foreach(Grade g in grades)
                {
                    if (g.Value >= 60 || (g.Value >= 40 && g.Value < 60 && g.FinalExam >= 60))
                        g.Status = "Aprovado";
                    else
                        g.Status = "Reprovado";
                }
            }
            ViewBag.EnrollmentId = enrollment.Id;
            return View(grades);
        }

        public ActionResult ShowGrades(Guid disciplineId, Guid enrollmentId)
        {
            if (!User.IsInRole("Student"))
                return RedirectToAction("Index", "Home");
            Enrollment enrollment = new Enrollment();
            enrollment.Get(enrollmentId);
            Activity activity = new Activity();
            var activities = activity.GetByDiscipline(disciplineId, new Semester(enrollment.Begin, enrollment.End));
            var activityGrades = new List<ActivityGrade>();
            foreach (var a in activities)
            {
                ActivityGrade activityGrade = new ActivityGrade();
                activityGrade.GetByStudent(User.Id, a.Id);
                if (activityGrade.Id == Guid.Empty)
                    activityGrade.ActivityId = a.Id;
                activityGrades.Add(activityGrade);
            }
            Exam exam = new Exam();
            exam.GetByStudent(enrollment.Id, disciplineId);
            ViewBag.Exam1 = exam.Exam1;
            ViewBag.Exam2 = exam.Exam2;
            ViewBag.Exam3 = exam.Exam3;
            ViewBag.Activities = activities;
            ViewBag.EnrollmentId = enrollment.Id;
            return View(activityGrades);
        }
    }
}