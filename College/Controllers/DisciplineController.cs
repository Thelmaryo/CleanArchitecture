using College.Enumerators;
using College.Models;
using College.Presenters.CourseContext;
using College.Presenters.Shared;
using College.UseCases.AccountContext.Queries;
using College.UseCases.CourseContext.Handlers;
using College.UseCases.CourseContext.Inputs;
using College.UseCases.CourseContext.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace College.Controllers
{
    public class DisciplineController : ControllerBase
    {
        private readonly DisciplineCommandHandler _disciplineCommand;
        private readonly DisciplineQueryHandler _disciplineQuery;
        private readonly CourseQueryHandler _courseQuery;
        private readonly ProfessorQueryHandler _professorQuery;

        public DisciplineController(DisciplineCommandHandler disciplineCommand, DisciplineQueryHandler disciplineQuery, CourseQueryHandler courseQuery,
            ProfessorQueryHandler professorQuery, UserQueryHandler userQuery) : base(userQuery)
        {
            _disciplineCommand = disciplineCommand;
            _disciplineQuery = disciplineQuery;
            _courseQuery = courseQuery;
            _professorQuery = professorQuery;
        }

        // GET: Discipline
        public ActionResult Index()
        {
            if (!UserIsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            var result = _disciplineQuery.Handle(new DisciplineInputList()).Disciplines;
            var disciplines = new DisciplineListViewModel { 
                Disciplines = result.Select(x=> new DisciplineListItem { 
                    Course = x.Course.Name,
                    Id = x.Id,
                    Name = x.Name,
                    Period = x.Period,
                    Professor = x.Professor.Name,
                    WeeklyWorkload = x.WeeklyWorkload
                })
            };
            return View(disciplines);
        }

        // GET: Discipline/Details/5
        public ActionResult Details(Guid id)
        {
            if (!UserIsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            var result = _disciplineQuery.Handle(new DisciplineInputGet { DisciplineId = id }).Discipline;
            var discipline = new DisciplineDetailsViewModel
            {
                Id = result.Id,
                Course = result.Course.Name,
                Name = result.Name,
                Period = result.Period,
                Professor = result.Professor.Name,
                WeeklyWorkload = result.WeeklyWorkload
            };
            return View(discipline);
        }

        // GET: Discipline/Create
        public ActionResult Create()
        {
            if (!UserIsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            var discipline = new CreateDisciplineViewModel
            {
                Courses = GetComboboxCourse(),
                Professors = GetComboboxProfessor()
            };
            return View(discipline);
        }

        // POST: Discipline/Create
        [HttpPost]
        public ActionResult Create(CreateDisciplineViewModel discipline)
        {
            if (!UserIsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            var input = new DisciplineInputRegister { 
                CourseId = discipline.SelectedCourse,
                Name = discipline.Name,
                Period = discipline.Period,
                ProfessorId = discipline.SelectedProfessor,
                WeeklyWorkload = discipline.WeeklyWorkload
            };
            var result = _disciplineCommand.Handle(input);
            if (!result.IsValid)
            {
                foreach (var n in result.Notifications)
                    ModelState.AddModelError(n.Key, n.Value);
                discipline.Courses = GetComboboxCourse();
                discipline.Professors = GetComboboxProfessor();
                return View(discipline);
            }
            return RedirectToAction("Index");
        }

        // GET: Discipline/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (!UserIsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            var result = _disciplineQuery.Handle(new DisciplineInputGet { DisciplineId = id }).Discipline;
            var discipline = new EditDisciplineViewModel
            {
                Id = result.Id,
                SelectedCourse = result.Course.Id,
                Name = result.Name,
                Period = result.Period,
                SelectedProfessor = result.Professor.Id,
                WeeklyWorkload = result.WeeklyWorkload,
                Professors = GetComboboxProfessor(),
                Courses = GetComboboxCourse()
            };
            return View(discipline);
        }

        // POST: Discipline/Edit/5
        [HttpPost]
        public ActionResult Edit(EditDisciplineViewModel discipline)
        {
            if (!UserIsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            var input = new DisciplineInputUpdate
            {
                DisciplineId = discipline.Id,
                CourseId = discipline.SelectedCourse,
                Name = discipline.Name,
                Period = discipline.Period,
                ProfessorId = discipline.SelectedProfessor,
                WeeklyWorkload = discipline.WeeklyWorkload
            };
            var result = _disciplineCommand.Handle(input);
            if (!result.IsValid)
            {
                foreach (var n in result.Notifications)
                    ModelState.AddModelError(n.Key, n.Value);
                discipline.Courses = GetComboboxCourse();
                discipline.Professors = GetComboboxProfessor();
                return View(discipline);
            }
            return RedirectToAction("Index");
        }

        // GET: Discipline/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (!UserIsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            var result = _disciplineQuery.Handle(new DisciplineInputGet { DisciplineId = id }).Discipline;
            var discipline = new DeleteDisciplineViewModel
            {
                Id = result.Id,
                Course = result.Course.Name,
                Name = result.Name,
                Period = result.Period,
                Professor = result.Professor.Name,
                WeeklyWorkload = result.WeeklyWorkload
            };
            return View(discipline);
        }

        // POST: Discipline/Delete/5
        [HttpPost]
        public ActionResult Delete(DeleteDisciplineViewModel discipline)
        {
            if (!UserIsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            _disciplineCommand.Handle(new DisciplineInputDelete { DisciplineId = discipline.Id });
            return RedirectToAction("Index");
        }

        private IEnumerable<ComboboxItem> GetComboboxCourse()
        {
            var combobox = _courseQuery.Handle(new CourseInputList()).Courses.Select(x => new ComboboxItem(x.Name, x.Id.ToString()));
            return combobox;
        }

        private IEnumerable<ComboboxItem> GetComboboxProfessor()
        {
            var combobox = _professorQuery.Handle(new ProfessorInputList()).Professors.Select(x => new ComboboxItem(x.Name, x.Id.ToString()));
            return combobox;
        }
    }
}
