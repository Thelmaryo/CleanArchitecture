using College.Models;
using College.Presenters.Shared;
using College.Presenters.StudentContext;
using College.UseCases.CourseContext.Queries;
using College.UseCases.StudentContext.Handlers;
using College.UseCases.StudentContext.Inputs;
using College.UseCases.StudentContext.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace College.Controllers
{
    public class StudentController : ControllerBase
    {
        private readonly StudentQueryHandler _studentQuery;
        private readonly StudentCommandHandler _studentCommand;
        private readonly CourseQueryHandler _courseQuery;

        public StudentController(StudentQueryHandler studentQuery, StudentCommandHandler studentCommand, CourseQueryHandler courseQuery)
        {
            _studentQuery = studentQuery;
            _studentCommand = studentCommand;
            _courseQuery = courseQuery;
        }


        // GET: Student
        public ActionResult Index()
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            var result = _studentQuery.Handle(new StudentInputList());
            StudentListViewModel students = new StudentListViewModel()
            {
                Students = result.Students.ToList().Select(x => new StudentListItem
                {
                    City = x.City,
                    Course = x.Course.Name,
                    CPF = x.CPF.Number,
                    Email = x.Email.Address,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Id = x.Id.ToString()
                })
            };
            return View(students);
        }

        // GET: Student/Details/5
        public ActionResult Details(Guid id)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            var result = _studentQuery.Handle(new StudentInputGetById { StudentId = id });
            var student = new StudentDetailsViewModel
            {
                Id = result.Student.Id,
                CPF = result.Student.CPF.Number,
                Email = result.Student.Email.Address,
                FirstName = result.Student.FirstName,
                LastName = result.Student.LastName,
                Phone = result.Student.Phone,
                Address = result.Student.Address,
                City = result.Student.City,
                Birthdate = result.Student.Birthdate,
                Country = result.Student.Country,
                Course = result.Student.Course.Name,
                Gender = result.Student.Gender
            };
            return View(student);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            var student = new CreateStudentViewModel
            {
                Courses = GetComboboxCourse()
            };
            return View(student);
        }

        // POST: Student/Create
        [HttpPost]
        public ActionResult Create(CreateStudentViewModel student)
        {
            var input = new StudentInputRegister
            {
                Address = student.Address,
                Birthdate = student.Birthdate,
                City = student.City,
                Country = student.Country,
                CourseId = student.SelectedCourse,
                CPF = student.CPF,
                Email = student.Email,
                FirstName = student.FirstName,
                Gender = student.Gender,
                LastName = student.LastName,
                Phone = student.Phone
            };
            var result = _studentCommand.Handle(input);
            if (!result.IsValid)
            {
                foreach (var n in result.Notifications)
                    ModelState.AddModelError(n.Key, n.Value);
                student.Courses = GetComboboxCourse();
                return View(student);
            }
            return RedirectToAction("Index");
        }

        // GET: Student/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            var result = _studentQuery.Handle(new StudentInputGetById { StudentId = id });
            var student = new EditStudentViewModel
            {
                Id = result.Student.Id,
                Email = result.Student.Email.Address,
                FirstName = result.Student.FirstName,
                LastName = result.Student.LastName,
                Phone = result.Student.Phone,
                Address = result.Student.Address,
                City = result.Student.City,
                Birthdate = result.Student.Birthdate,
                Country = result.Student.Country,
                SelectedCourse = result.Student.Course.CourseId,
                Gender = result.Student.Gender,
                Courses = GetComboboxCourse()
            };
            return View(student);
        }

        // POST: Student/Edit/5
        [HttpPost]
        public ActionResult Edit(EditStudentViewModel student)
        {
            var input = new StudentInputUpdate
            {
                Address = student.Address,
                Birthdate = student.Birthdate,
                City = student.City,
                Country = student.Country,
                CourseId = student.SelectedCourse,
                Email = student.Email,
                FirstName = student.FirstName,
                Gender = student.Gender,
                LastName = student.LastName,
                Phone = student.Phone,
                StudentId = student.Id
            };
            var result = _studentCommand.Handle(input);
            if (!result.IsValid)
            {
                foreach (var n in result.Notifications)
                    ModelState.AddModelError(n.Key, n.Value);
                student.Courses = GetComboboxCourse();
                return View(student);
            }
            return RedirectToAction("Index");
        }

        // GET: Student/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            var result = _studentQuery.Handle(new StudentInputGetById { StudentId = id });
            var student = new DeleteStudentViewModel
            {
                Id = result.Student.Id,
                FirstName = result.Student.FirstName
            };
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost]
        public ActionResult Delete(DeleteStudentViewModel student)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            _studentCommand.Handle(new StudentInputDelete { StudentId = student.Id });
            return RedirectToAction("Index");
        }
        private IEnumerable<ComboboxItem> GetComboboxCourse()
        {
            var combobox = _courseQuery.Handle().Courses.Select(x => new ComboboxItem(x.Name, x.Id.ToString()));
            return combobox;
        }
    }
}
