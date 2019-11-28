using College.Entities.ProfessorContext.Enumerators;
using College.Presenters.ProfessorContext;
using College.Presenters.Shared;
using College.UseCases.AccountContext.Queries;
using College.UseCases.ProfessorContext.Handlers;
using College.UseCases.ProfessorContext.Inputs;
using College.UseCases.ProfessorContext.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace College.Controllers
{
    public class ProfessorController : ControllerBase
    {
        private ProfessorQueryHandler _professorQuery;
        private ProfessorCommandHandler _professorCommand;

        public ProfessorController(ProfessorQueryHandler professorQuery, ProfessorCommandHandler professorCommand, UserQueryHandler userQuery) : base(userQuery)
        {
            _professorQuery = professorQuery;
            _professorCommand = professorCommand;
        }

        // GET: Professor
        public ActionResult Index()
        {
            if (!UserIsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            var professors = _professorQuery.Handle().Professors.Select(x =>
                new ProfessorListItem
                {
                    CPF = x.CPF.Number,
                    Email = x.Email.Address,
                    FirstName = x.FirstName,
                    Id = x.Id,
                    LastName = x.LastName,
                    Phone = x.Phone
                });
            var professorsVM = new ProfessorListViewModel
            {
                Professors = professors
            };
            return View(professorsVM);
        }

        // GET: Professor/Details/5
        public ActionResult Details(Guid id)
        {
            if (!UserIsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            var result = _professorQuery.Handle(new ProfessorInputGet { ProfessorId = id });
            var professor = new ProfessorDetailsViewModel
            {
                Id = result.Professor.Id,
                CPF = result.Professor.CPF.Number,
                Email = result.Professor.Email.Address,
                FirstName = result.Professor.FirstName,
                LastName = result.Professor.LastName,
                Phone = result.Professor.Phone
            };
            return View(professor);
        }

        // GET: Professor/Create
        public ActionResult Create()
        {
            if (!UserIsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            var professor = new CreateProfessorViewModel
            {
                Degrees = GetComboboxDegree()
            };
            return View(professor);
        }

        // POST: Professor/Create
        [HttpPost]
        public ActionResult Create(CreateProfessorViewModel professor)
        {
            if (!UserIsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            var professorInput = new ProfessorInputRegister
            {
                CPF = professor.CPF,
                Degree = professor.SelectedDegree,
                Email = professor.Email,
                FirstName = professor.FirstName,
                LastName = professor.LastName,
                Phone = professor.Phone
            };
            var result = _professorCommand.Handle(professorInput);
            if (!result.IsValid)
            {
                foreach (var n in result.Notifications)
                    ModelState.AddModelError(n.Key, n.Value);
                professor.Degrees = GetComboboxDegree();
                return View(professor);
            }
            return RedirectToAction("Index");
        }

        // GET: Professor/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (!UserIsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            var result = _professorQuery.Handle(new ProfessorInputGet { ProfessorId = id });
            var professor = new EditProfessorViewModel
            {
                Id = result.Professor.Id.ToString(),
                CPF = result.Professor.CPF.Number,
                Email = result.Professor.Email.Address,
                FirstName = result.Professor.FirstName,
                LastName = result.Professor.LastName,
                Phone = result.Professor.Phone,
                Degrees = GetComboboxDegree(),
                SelectedDegree = result.Professor.Degree
            };
            return View(professor);
        }

        // POST: Professor/Edit/5
        [HttpPost]
        public ActionResult Edit(EditProfessorViewModel professor)
        {
            if (!UserIsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            var professorInput = new ProfessorInputUpdate
            {
                Degree = professor.SelectedDegree,
                Email = professor.Email,
                FirstName = professor.FirstName,
                LastName = professor.LastName,
                Phone = professor.Phone,
                ProfessorId = Guid.Parse(professor.Id)
            };
            var result = _professorCommand.Handle(professorInput);
            if (!result.IsValid)
            {
                foreach (var n in result.Notifications)
                    ModelState.AddModelError(n.Key, n.Value);
                professor.Degrees = GetComboboxDegree();
                return View(professor);
            }
            return RedirectToAction("Index");
        }

        // GET: Professor/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (!UserIsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            var result = _professorQuery.Handle(new ProfessorInputGet { ProfessorId = id });
            var professor = new DeleteProfessorViewModel
            {
                Id = result.Professor.Id.ToString(),
                Name = result.Professor.Name
            };
            return View(professor);
        }

        // POST: Professor/Delete/5
        [HttpPost]
        public ActionResult Delete(DeleteProfessorViewModel professor)
        {
            if (!UserIsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            _professorCommand.Handle(new ProfessorInputDelete { ProfessorId = Guid.Parse(professor.Id) });
            return RedirectToAction("Index");
        }

        private List<ComboboxItem> GetComboboxDegree()
        {
            var names = Enum.GetNames(typeof(EDegree));
            var values = Enum.GetValues(typeof(EDegree));
            var combobox = new List<ComboboxItem>();
            for (int index = 0; index < names.Count(); index++)
            {
                var value = values.GetValue(index);
                combobox.Add(new ComboboxItem(names[index], value.ToString()));
            }
            return combobox;
        }
    }
}
