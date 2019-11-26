using College.Presenters.EnrollmentContext;
using College.Presenters.Shared;
using College.UseCases.AccountContext.Queries;
using College.UseCases.CourseContext.Inputs;
using College.UseCases.CourseContext.Queries;
using College.UseCases.EnrollmentContext.Handlers;
using College.UseCases.EnrollmentContext.Inputs;
using College.UseCases.EnrollmentContext.Queries;
using College.UseCases.StudentContext.Inputs;
using College.UseCases.StudentContext.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EnrollmentDisciplineQueryHandler = College.UseCases.EnrollmentContext.Queries.DisciplineQueryHandler;

namespace College.Controllers
{
    public class EnrollmentController : ControllerBase
    {
        private readonly EnrollmentQueryHandler _enrollmentQuery;
        private readonly EnrollmentCommandHandler _enrollmentCommand;
        private readonly CourseQueryHandler _courseQuery;
        private readonly StudentQueryHandler _studentQuery;
        private readonly EnrollmentDisciplineQueryHandler _disciplineQuery;
        public EnrollmentController(EnrollmentQueryHandler enrollmentQuery, EnrollmentCommandHandler enrollmentCommand, CourseQueryHandler courseQuery,
            StudentQueryHandler studentQuery, EnrollmentDisciplineQueryHandler disciplineQuery, UserQueryHandler userQuery) : base(userQuery)
        {
            _enrollmentQuery = enrollmentQuery;
            _enrollmentCommand = enrollmentCommand;
            _courseQuery = courseQuery;
            _studentQuery = studentQuery;
            _disciplineQuery = disciplineQuery;
        }

        public ActionResult Index()
        {
            if (!UserIsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            var result = _enrollmentQuery.Handle(new EnrollmentInputGetPreEnrollments()).Enrollment;
            var enrollments = new EnrollmentListViewModel
            {
                Students = result.Select(x=> new EnrollmentListItem { 
                    Id = x.Id,
                    Student = x.Student.Name
                })
            };
            return View(enrollments);
        }

        public ActionResult ChooseStudent()
        {
            if (!UserIsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            var student = new ChooseStudentViewModel() { 
                Courses = GetComboboxCourse()
            };
            return View(student);
        }

        // GET: Enrollment/Create
        public ActionResult Create(ChooseStudentViewModel student)
        {
            if (!UserIsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            var result = _studentQuery.Handle(new StudentInputGetByCPF { StudentCPF = student.StudentCPF }).Student;
            if (result == null)
            {
                ModelState.AddModelError("StudentCPF", "CPF Inválido");
                student.Courses = GetComboboxCourse();
                return View("ChooseStudent", student);
            }
            var disciplines = _disciplineQuery.Handle(new DisciplineInputGetNotConcluded { CourseId = student.SelectedCourse, StudentId = result.Id }).Disciplines;
            var portfolio = new CreateEnrollmentViewModel {
                Disciplines = disciplines.Select(x=> new Checkbox { Text = x.Name, Value = x.Id.ToString() }),
                StudentId = result.Id
            };
            return View(portfolio);
        }

        // POST: Enrollment/Create
        [HttpPost]
        public ActionResult Create(CreateEnrollmentViewModel enrollment)
        {
            if (!UserIsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            var result = _enrollmentCommand.Handle(new EnrollmentInputRegister {
                StudentId = enrollment.StudentId,
                Disciplines = enrollment.Disciplines.Where(x => x.Checked).Select(x => Guid.Parse(x.Value)).ToArray()
            });
            if (!result.IsValid)
            {
                foreach (var n in result.Notifications)
                    ModelState.AddModelError(n.Key, n.Value);
                return View(enrollment);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Confirm(Guid id)
        {
            if (!UserIsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            _enrollmentCommand.Handle(new EnrollmentInputConfirm { EnrollmentId = id });
            return RedirectToAction("Index");
        }
        public ActionResult Deny(Guid id)
        {
            if (!UserIsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            _enrollmentCommand.Handle(new EnrollmentInputDeny { EnrollmentId = id });
            return RedirectToAction("Index");
        }

        private IEnumerable<ComboboxItem> GetComboboxCourse()
        {
            var combobox = _courseQuery.Handle(new CourseInputList()).Courses.Select(x => new ComboboxItem(x.Name, x.Id.ToString()));
            return combobox;
        }
    }
}
