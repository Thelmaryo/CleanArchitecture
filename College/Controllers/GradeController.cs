using College.Helpers;
using College.UseCases.AccountContext.Queries;
using College.UseCases.EnrollmentContext.Queries;
using Evaluation =College.UseCases.EvaluationContext.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using College.UseCases.EnrollmentContext.Result;
using College.UseCases.EnrollmentContext.Inputs;
using College.UseCases.EvaluationContext.Inputs;
using College.Entities.EnrollmentContext.Entities;
using College.Presenters.EvaluationContext;

namespace College.Controllers
{
    public class GradeController : ControllerBase
    {
        private readonly Evaluation.DisciplineQueryHandler _disciplineQuery;
        private readonly EnrollmentQueryHandler _enrollmentQuery;
        public GradeController(Evaluation.DisciplineQueryHandler disciplineQuery, EnrollmentQueryHandler enrollmentQuery, UserQueryHandler userQuery) : base(userQuery)
        {
            _disciplineQuery = disciplineQuery;
            _enrollmentQuery = enrollmentQuery;
        }

        // GET: Grade
        public ActionResult Index(Guid? enrollmentId)
        {
            if (!UserIsInRole("Student"))
                return RedirectToAction("Index", "Home");
            Enrollment enrollment;
            if (enrollmentId == null)
                enrollment = _enrollmentQuery.Handle(new EnrollmentInputGetByStudent { StudentId = Authentication.UserId }).Enrollment;
            else
                enrollment = _enrollmentQuery.Handle(new EnrollmentInputGet { EnrollmentId = (Guid)enrollmentId }).Enrollment;
            var disciplines = _disciplineQuery.Handle(new DisciplineInputListByEnrollment { EnrollmentId = enrollment.Id, StudentId = Authentication.UserId, SemesterBegin = enrollment.Begin, SemesterEnd = enrollment.End }).Disciplines;
            var grades = new GradeListViewModel() { EnrollmentId = (Guid)enrollment.Id };
            grades.Students = disciplines.Select(x=> new GradeListItem
            {
                Grade = x.Activities.Sum(y=>y.Grade),
                FinalExam = x.FinalExamGrade,
                Discipline = x.Name,
                DisciplineId = x.Id,
                Status = DictionaryStudentStatus.Get(x.StudentStatus, out string color),
                StatusColor = color
            });
            return View(grades);
        }

        //public ActionResult ShowGrades(Guid disciplineId, Guid enrollmentId)
        //{
        //    if (!UserIsInRole("Student"))
        //        return RedirectToAction("Index", "Home");
        //    Enrollment enrollment = new Enrollment();
        //    enrollment.Get(enrollmentId);
        //    Activity activity = new Activity();
        //    var activities = activity.GetByDiscipline(disciplineId, new Semester(enrollment.Begin, enrollment.End));
        //    var activityGrades = new List<ActivityGrade>();
        //    foreach (var a in activities)
        //    {
        //        ActivityGrade activityGrade = new ActivityGrade();
        //        activityGrade.GetByStudent(Authentication.UserId, a.Id);
        //        if (activityGrade.Id == Guid.Empty)
        //            activityGrade.ActivityId = a.Id;
        //        activityGrades.Add(activityGrade);
        //    }
        //    Exam exam = new Exam();
        //    exam.GetByStudent(enrollment.Id, disciplineId);
        //    ViewBag.Exam1 = exam.Exam1;
        //    ViewBag.Exam2 = exam.Exam2;
        //    ViewBag.Exam3 = exam.Exam3;
        //    ViewBag.Activities = activities;
        //    ViewBag.EnrollmentId = enrollment.Id;
        //    return View(activityGrades);
        //}
    }
}