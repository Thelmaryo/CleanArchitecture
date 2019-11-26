using College.Presenters.EvaluationContext;
using College.UseCases.AccountContext.Queries;
using College.UseCases.ActivityContext.Handlers;
using College.UseCases.ActivityContext.Inputs;
using College.UseCases.ActivityContext.Models;
using College.UseCases.ActivityContext.Queries;
using College.UseCases.EvaluationContext.Inputs;
using College.UseCases.StudentContext.Inputs;
using College.UseCases.StudentContext.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Evaluation = College.UseCases.EvaluationContext;

namespace College.Controllers
{
    public class ExamController : ControllerBase
    {
        private readonly ActivityQueryHandler _activityQuery;
        private readonly Evaluation.Queries.ActivityQueryHandler _activityEvaluationQuery;
        private readonly Evaluation.Handlers.ActivityCommandHandler _activityEvaluationCommand;
        private readonly ActivityCommandHandler _activityCommand;
        private readonly StudentQueryHandler _studentQuery;
        public ExamController(ActivityQueryHandler activityQuery, StudentQueryHandler studentQuery, ActivityCommandHandler activityCommand, Evaluation.Queries.ActivityQueryHandler activityEvaluationQuery,
            Evaluation.Handlers.ActivityCommandHandler activityEvaluationCommand, UserQueryHandler userQuery) : base(userQuery)
        {
            _activityQuery = activityQuery;
            _activityCommand = activityCommand;
            _studentQuery = studentQuery;
            _activityEvaluationQuery = activityEvaluationQuery;
            _activityEvaluationCommand = activityEvaluationCommand;
        }

        public ActionResult Index(Guid disciplineId)
        {
            if (!UserIsInRole("Professor"))
                return RedirectToAction("Index", "Home");
            var students = _studentQuery.Handle(new StudentInputListByDiscipline { DisciplineId = disciplineId }).Students;
            var exams = _activityQuery.Handle(new ExamInputGetByDiscipline { DisciplineId = disciplineId, Semester = new UseCases.Shared.Semester() }).Activities;
            if(exams.Count() == 0)
            {
                CreateExams(disciplineId);
                exams = _activityQuery.Handle(new ExamInputGetByDiscipline { DisciplineId = disciplineId, Semester = new UseCases.Shared.Semester() }).Activities;
            }
            var grades = new ExamListViewModel { 
                DisciplineId = disciplineId,
                DisciplineName = exams.First().Discipline.Name
            };
            foreach (var student in students)
            {
                var studentGrades = new Dictionary<string, decimal>();
                foreach (var exam in exams)
                {
                    var result = _activityEvaluationQuery.Handle(new ActivityInputGetByStudent { ActivityId = exam.Id, StudentId = student.Id }).Activity;
                    decimal grade = 0;
                    if (result == null)
                        _activityEvaluationCommand.Handle(new ActivityInputGiveGrade { ActivityId = exam.Id, Grade = 0, StudentId = student.Id, Value = exam.Value });
                    else
                        grade = result.Grade;
                    studentGrades.Add(exam.Description, grade);
                }
                var item = new ExamListItem
                {
                    Student = $"{student.FirstName} {student.LastName}",
                    StudentId = student.Id,
                    Exam1 = studentGrades.Single(x=>x.Key == new Exam1(Guid.Empty).Description).Value,
                    Exam2 = studentGrades.Single(x => x.Key == new Exam2(Guid.Empty).Description).Value,
                    Exam3 = studentGrades.Single(x => x.Key == new Exam3(Guid.Empty).Description).Value,
                    FinalExam = studentGrades.Single(x => x.Key == new FinalExam(Guid.Empty).Description).Value
                };
                grades.Students.Add(item);
            }
            return View(grades);
        }


        public ActionResult GiveGrade(ExamListItem exam, Guid disciplineId)
        {
            if (!UserIsInRole("Professor"))
                return RedirectToAction("Index", "Home");
            var student = _studentQuery.Handle(new StudentInputGetById { StudentId = exam.StudentId }).Student;
            var _exam = new EditExamViewModel
            {
                DisciplineId = disciplineId,
                StudentId = exam.StudentId,
                Student = $"{student.FirstName} {student.LastName}",
                Exam1 = exam.Exam1.ToString().Replace(".", ","),
                Exam2 = exam.Exam2.ToString().Replace(".", ","),
                Exam3 = exam.Exam3.ToString().Replace(".", ","),
                FinalExam = exam.FinalExam.ToString().Replace(".", ",")
            };
            return View(_exam);
        }

        [HttpPost]
        public ActionResult GiveGrade(EditExamViewModel exam)
        {
            if (!UserIsInRole("Professor"))
                return RedirectToAction("Index", "Home");
            var exams = _activityQuery.Handle(new ExamInputGetByDiscipline { DisciplineId = exam.DisciplineId, Semester = new UseCases.Shared.Semester() }).Activities;
            var result1 = _activityEvaluationCommand.Handle(new ActivityInputUpdateGrade { Grade = Convert.ToDecimal(exam.Exam1.Replace(".", ",")), StudentId = exam.StudentId, Value = exams.Single(x => x.Description == new Exam1(Guid.Empty).Description).Value, ActivityId = exams.Single(x => x.Description == new Exam1(Guid.Empty).Description).Id });
            var result2 = _activityEvaluationCommand.Handle(new ActivityInputUpdateGrade { Grade = Convert.ToDecimal(exam.Exam2.Replace(".", ",")), StudentId = exam.StudentId, Value = exams.Single(x => x.Description == new Exam2(Guid.Empty).Description).Value, ActivityId = exams.Single(x => x.Description == new Exam2(Guid.Empty).Description).Id });
            var result3 = _activityEvaluationCommand.Handle(new ActivityInputUpdateGrade { Grade = Convert.ToDecimal(exam.Exam3.Replace(".", ",")), StudentId = exam.StudentId, Value = exams.Single(x => x.Description == new Exam3(Guid.Empty).Description).Value, ActivityId = exams.Single(x => x.Description == new Exam3(Guid.Empty).Description).Id });
            var result4 = _activityEvaluationCommand.Handle(new ActivityInputUpdateGrade { Grade = Convert.ToDecimal(exam.FinalExam.Replace(".", ",")), StudentId = exam.StudentId, Value = exams.Single(x => x.Description == new FinalExam(Guid.Empty).Description).Value, ActivityId = exams.Single(x => x.Description == new FinalExam(Guid.Empty).Description).Id });
            if (!(result1.IsValid && result2.IsValid && result3.IsValid && result4.IsValid))
            {
                foreach (var n in result1.Notifications)
                    ModelState.AddModelError("Exam1", n.Value);
                foreach (var n in result2.Notifications)
                    ModelState.AddModelError("Exam2", n.Value);
                foreach (var n in result3.Notifications)
                    ModelState.AddModelError("Exam3", n.Value);
                foreach (var n in result4.Notifications)
                    ModelState.AddModelError("FinalExam", n.Value);
                return View(exam);
            }
            return RedirectToAction("Index", new { disciplineId = exam.DisciplineId });
        }

        private void CreateExams(Guid disciplineId)
        {
            _activityCommand.Handle(new ActivityInputRegister { Activity = new Exam1(disciplineId) });
            _activityCommand.Handle(new ActivityInputRegister { Activity = new Exam2(disciplineId) });
            _activityCommand.Handle(new ActivityInputRegister { Activity = new Exam3(disciplineId) });
            _activityCommand.Handle(new ActivityInputRegister { Activity = new FinalExam(disciplineId), ValidateTotalGrade = false });
        }
    }
}