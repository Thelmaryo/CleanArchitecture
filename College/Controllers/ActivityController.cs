using College.Presenters.ActivityContext;
using College.Presenters.EvaluationContext;
using College.UseCases.AccountContext.Queries;
using College.UseCases.ActivityContext.Handlers;
using College.UseCases.ActivityContext.Inputs;
using College.UseCases.ActivityContext.Queries;
using College.UseCases.CourseContext.Inputs;
using College.UseCases.CourseContext.Queries;
using College.UseCases.EvaluationContext.Inputs;
using College.UseCases.Shared.Commands;
using College.UseCases.StudentContext.Inputs;
using College.UseCases.StudentContext.Queries;
using System;
using System.Linq;
using System.Web.Mvc;

namespace College.Controllers
{
    public class ActivityController : ControllerBase
    {
        private readonly ActivityCommandHandler _activityCommand;
        private readonly ActivityQueryHandler _activityQuery;
        private readonly UseCases.EvaluationContext.Handlers.ActivityCommandHandler _activityEvaluationCommad;
        private readonly UseCases.EvaluationContext.Queries.ActivityQueryHandler _activityEvaluationQuery;
        private readonly DisciplineQueryHandler _disciplineQuery;
        private readonly StudentQueryHandler _studentQuery;
        public ActivityController(ActivityCommandHandler activityCommand, ActivityQueryHandler activityQuery, DisciplineQueryHandler disciplineQuery,
            StudentQueryHandler studentQuery, UseCases.EvaluationContext.Queries.ActivityQueryHandler activityEvaluationQuery, 
            UseCases.EvaluationContext.Handlers.ActivityCommandHandler activityEvaluationCommad, UserQueryHandler userQuery) : base(userQuery)
        {
            _activityCommand = activityCommand;
            _activityQuery = activityQuery;
            _disciplineQuery = disciplineQuery;
            _studentQuery = studentQuery;
            _activityEvaluationQuery = activityEvaluationQuery;
            _activityEvaluationCommad = activityEvaluationCommad;
        }

        // GET: Activity
        public ActionResult Index(Guid disciplineId)
        {
            if (!UserIsInRole("Professor"))
                return RedirectToAction("Index", "Home");
            var discipline = _disciplineQuery.Handle(new DisciplineInputGet { DisciplineId = disciplineId });
            var result = _activityQuery.Handle(new UseCases.ActivityContext.Inputs.ActivityInputGetByDiscipline { DisciplineId = disciplineId, Semester = new UseCases.Shared.Semester() });
            var activities = new ActivityListViewModel {
                DisciplineId = disciplineId,
                DisciplineName = discipline.Discipline.Name,
                Activities = result.Activities.Select(x=> new ActivityListItem { 
                    Date = x.Date.ToShortDateString(),
                    Id = x.Id,
                    Name = x.Description,
                    Value = x.Value
                })
            };
            return View(activities);
        }

        public ActionResult Grades(Guid activityId)
        {
            if (!UserIsInRole("Professor"))
                return RedirectToAction("Index", "Home");
            var activity = _activityQuery.Handle(new ActivityInputGetById { ActivityId = activityId }).Activity;
            var students = _studentQuery.Handle(new StudentInputListByDiscipline { DisciplineId = activity.Discipline.Id }).Students;
            var grades = new GiveGradeListViewModel
            {
                DisciplineId = activity.Discipline.Id,
                ActivityName = activity.Description,
                ActivityValue = activity.Value,
            };
            foreach (var student in students)
            {
                var grade = _activityEvaluationQuery.Handle(new ActivityInputGetByStudent { ActivityId = activityId, StudentId = student.Id }).Activity;
                grades.Students.Add(new GiveGradeListItem {
                    ActivityId = activity.Id,
                    Student = $"{student.FirstName} {student.LastName}",
                    Grade = grade == null ? "0" : grade.Grade.ToString(),
                    StudentId = student.Id
                });
            }
            return View(grades);
        }

        public ActionResult GiveGrade(Guid studentId, Guid activityId)
        {
            if (!UserIsInRole("Professor"))
                return RedirectToAction("Index", "Home");
            var activity = _activityEvaluationQuery.Handle(new ActivityInputGetByStudent { ActivityId = activityId, StudentId = studentId }).Activity;
            var grade = new EditGradeViewModel
            {
                ActivityId = activityId,
                StudentId = studentId 
            };
            if (activity == null)
            {
                grade.Value = _activityQuery.Handle(new ActivityInputGetById { ActivityId = activityId }).Activity.Value;
                var student = _studentQuery.Handle(new StudentInputGetById { StudentId = studentId }).Student;
                grade.Student = $"{student.FirstName} {student.LastName}";
            }
            else
            {
                grade.Grade = activity.Grade;
                grade.Student = activity.Student.Name;
                grade.Value = activity.Value;
            }
            return View(grade);
        }

        [HttpPost]
        public ActionResult GiveGrade(EditGradeViewModel activityGrade)
        {
            if (!UserIsInRole("Professor"))
                return RedirectToAction("Index", "Home");
            ICommandResult result;
            var activity = _activityEvaluationQuery.Handle(new ActivityInputGetByStudent { ActivityId = activityGrade.ActivityId, StudentId = activityGrade.StudentId }).Activity;
            if (activity == null)
            {
                result = _activityEvaluationCommad.Handle(new ActivityInputGiveGrade
                {
                    ActivityId= activityGrade.ActivityId,
                    Grade = activityGrade.Grade,
                    StudentId = activityGrade.StudentId,
                    Value = activityGrade.Value
                });
            }
            else
            {
                result = _activityEvaluationCommad.Handle(new ActivityInputUpdateGrade
                {
                    ActivityId = activityGrade.ActivityId,
                    Grade = activityGrade.Grade,
                    StudentId = activityGrade.StudentId,
                    Value = activityGrade.Value
                });
            }
            if (!result.IsValid)
            {
                foreach (var n in result.Notifications)
                    ModelState.AddModelError(n.Key, n.Value);
                return View(activityGrade);
            }
            return RedirectToAction("Grades", new { activityId = activityGrade.ActivityId });
        }

        // GET: Activity/Details/5
        public ActionResult Details(Guid id)
        {
            if (!UserIsInRole("Professor"))
                return RedirectToAction("Index", "Home");
            var result = _activityQuery.Handle(new ActivityInputGetById { ActivityId = id }).Activity;
            var activity = new ActivityDetailsViewModel
            {
                Id = result.Id,
                Date = result.Date.ToShortDateString(),
                Description = result.Description,
                Value = result.Value,
                Discipline = result.Discipline.Name,
                DisciplineId = result.Discipline.Id
            };
            return View(activity);
        }

        // GET: Activity/Create
        public ActionResult Create(Guid disciplineId)
        {
            if (!UserIsInRole("Professor"))
                return RedirectToAction("Index", "Home");
            return View(new CreateActivityViewModel { DisciplineId = disciplineId, Date = DateTime.Now.Date.ToShortDateString() });
        }

        // POST: Activity/Create
        [HttpPost]
        public ActionResult Create(CreateActivityViewModel activity)
        {
            if (!UserIsInRole("Professor"))
                return RedirectToAction("Index", "Home");
            var result = _activityCommand.Handle(new ActivityInputRegister {
               Activity = new UseCases.ActivityContext.Models.Activity(activity.Description, activity.DisciplineId, activity.Value, Convert.ToDateTime(activity.Date))
            });
            if (!result.IsValid)
            {
                foreach (var n in result.Notifications)
                    ModelState.AddModelError(n.Key, n.Value);
                return View(activity);
            }
            return RedirectToAction("Index", new { disciplineId = activity.DisciplineId });
        }

        // GET: Activity/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (!UserIsInRole("Professor"))
                return RedirectToAction("Index", "Home");
            var result = _activityQuery.Handle(new ActivityInputGetById { ActivityId = id }).Activity;
            var activity = new EditActivityViewModel
            {
                Id = result.Id,
                Date = result.Date.ToShortDateString(),
                Description = result.Description,
                Value = result.Value,
                DisciplineId = result.Discipline.Id
            };
            return View(activity);
        }

        // POST: Activity/Edit/5
        [HttpPost]
        public ActionResult Edit(EditActivityViewModel activity)
        {
            if (!UserIsInRole("Professor"))
                return RedirectToAction("Index", "Home");
            var result = _activityCommand.Handle(new ActivityInputUpdate
            {
                Id = activity.Id,
                Activity = new UseCases.ActivityContext.Models.Activity(activity.Description, activity.DisciplineId, activity.Value, Convert.ToDateTime(activity.Date))
            });
            if (!result.IsValid)
            {
                foreach (var n in result.Notifications)
                    ModelState.AddModelError(n.Key, n.Value);
                return View(activity);
            }
            return RedirectToAction("Index", new { disciplineId = activity.DisciplineId });
        }

        // GET: Activity/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (!UserIsInRole("Professor"))
                return RedirectToAction("Index", "Home");
            var result = _activityQuery.Handle(new ActivityInputGetById { ActivityId = id }).Activity;
            var activity = new DeleteActivityViewModel
            {
                Id = result.Id,
                Date = result.Date.ToShortDateString(),
                Description = result.Description,
                Value = result.Value,
                Discipline = result.Discipline.Name,
                DisciplineId = result.Discipline.Id
            };
            return View(activity);
        }

        // POST: Activity/Delete/5
        [HttpPost]
        public ActionResult Delete(DeleteActivityViewModel activity)
        {
            if (!UserIsInRole("Professor"))
                return RedirectToAction("Index", "Home");
            _activityCommand.Handle(new ActivityInputDelete { Id = activity.Id });
            return RedirectToAction("Index", new { disciplineId = activity.DisciplineId });
        }
    }
}