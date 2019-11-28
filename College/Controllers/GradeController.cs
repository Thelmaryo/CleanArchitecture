using College.Entities.EnrollmentContext.Entities;
using College.Helpers;
using College.Presenters.EvaluationContext;
using College.UseCases.AccountContext.Queries;
using College.UseCases.ActivityContext.Models;
using College.UseCases.EnrollmentContext.Inputs;
using College.UseCases.EnrollmentContext.Queries;
using College.UseCases.EvaluationContext.Inputs;
using College.UseCases.EvaluationContext.Queries;
using System;
using System.Linq;
using System.Web.Mvc;
using Evaluation = College.UseCases.EvaluationContext.Queries;

namespace College.Controllers
{
    public class GradeController : ControllerBase
    {
        private readonly Evaluation.DisciplineQueryHandler _disciplineQuery;
        private readonly EnrollmentQueryHandler _enrollmentQuery;
        private readonly ActivityQueryHandler _activityQuery;
        public GradeController(Evaluation.DisciplineQueryHandler disciplineQuery, EnrollmentQueryHandler enrollmentQuery, ActivityQueryHandler activityQuery, UserQueryHandler userQuery) : base(userQuery)
        {
            _disciplineQuery = disciplineQuery;
            _enrollmentQuery = enrollmentQuery;
            _activityQuery = activityQuery;
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

        public ActionResult ShowGrades(Guid disciplineId, Guid enrollmentId)
        {
            if (!UserIsInRole("Student"))
                return RedirectToAction("Index", "Home");
            var enrollment = _enrollmentQuery.Handle(new EnrollmentInputGet { EnrollmentId = enrollmentId }).Enrollment;
            var activities = _activityQuery.Handle(new ActivityInputGetByDiscipline { DisciplineId = disciplineId, StudentId = Authentication.UserId, SemesterBegin = enrollment.Begin, SemesterEnd = enrollment.End }).Activities.ToList();
            var exam1 = activities.SingleOrDefault(x => x.Description == new Exam1(Guid.Empty).Description);
            var exam2 = activities.SingleOrDefault(x => x.Description == new Exam2(Guid.Empty).Description);
            var exam3 = activities.SingleOrDefault(x => x.Description == new Exam3(Guid.Empty).Description);
            activities.Remove(exam1);
            activities.Remove(exam2);
            activities.Remove(exam3);
            activities.RemoveAll(x => x.Value == 100);
            var activityGrades = new ShowGradesViewModel { 
                EnrollmentId = enrollmentId,
                Exam1 = exam1 == null ? 0 : exam1.Grade,
                Exam2 = exam2 == null ? 0 : exam2.Grade,
                Exam3 = exam3 == null ? 0 : exam3.Grade,
                Activities = activities.Select(x=>new ShowGradesItem { 
                    Activity = x.Description,
                    Grade = x.Grade,
                    Value = x.Value
                })
            };
            return View(activityGrades);
        }
    }
}