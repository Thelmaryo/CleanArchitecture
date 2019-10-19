using College.Enumerators;
using College.Helpers;
using College.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace College.Controllers
{
    public class DisciplineController : ControllerBase
    {
        // GET: Discipline
        public ActionResult Index()
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            Course course = new Course();
            Professor professor = new Professor();
            ViewBag.Courses = course.List();
            ViewBag.Professors = professor.List();
            Discipline discipline = new Discipline();
            return View(discipline.List());
        }

        // GET: Discipline/Details/5
        public ActionResult Details(Guid id)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            var discipline = new Discipline();
            discipline.Get(id);
            return View(discipline);
        }

        // GET: Discipline/Create
        public ActionResult Create()
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            Course course = new Course();
            Professor professor = new Professor();
            ViewBag.Courses = course.List();
            ViewBag.Professors = professor.List();
            return View();
        }

        // POST: Discipline/Create
        [HttpPost]
        public ActionResult Create(Discipline discipline)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            try
            {
                var professor = new Professor();
                professor.Get(discipline.ProfessorId);
                int professorWorkload = professor.GetWorkload() + discipline.WeeklyWorkload;
                if (professor.Degree == EDegree.Bachelor)
                {
                    if(professorWorkload > 40)
                    {
                        ModelState.AddModelError("ProfessorId", "A carga horária máxima do professor (40h) não pode ser excedida");
                        return View(discipline);
                    }
                }
                else if(professor.Degree == EDegree.Master)
                {
                    if (professorWorkload > 30)
                    {
                        ModelState.AddModelError("ProfessorId", "A carga horária máxima do professor (30h) não pode ser excedida");
                        return View(discipline);
                    }
                }
                else if (professor.Degree == EDegree.Doctor)
                {
                    if (professorWorkload > 25)
                    {
                        ModelState.AddModelError("ProfessorId", "A carga horária máxima do professor (25h) não pode ser excedida");
                        return View(discipline);
                    }
                }
                discipline.Create();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Discipline/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            Course course = new Course();
            Professor professor = new Professor();
            ViewBag.Courses = course.List();
            ViewBag.Professors = professor.List();
            var discipline = new Discipline();
            discipline.Get(id);
            return View(discipline);
        }

        // POST: Discipline/Edit/5
        [HttpPost]
        public ActionResult Edit(Discipline discipline)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            try
            {
                var professor = new Professor();
                professor.Get(discipline.ProfessorId);
                int professorWorkload = professor.GetWorkload();
                if (professor.Degree == EDegree.Bachelor)
                {
                    if (professorWorkload > 40)
                    {
                        ModelState.AddModelError("ProfessorId", "A carga horária máxima do professor (40h) não pode ser excedida");
                        return View(discipline);
                    }
                }
                else if (professor.Degree == EDegree.Master)
                {
                    if (professorWorkload > 30)
                    {
                        ModelState.AddModelError("ProfessorId", "A carga horária máxima do professor (30h) não pode ser excedida");
                        return View(discipline);
                    }
                }
                else if (professor.Degree == EDegree.Doctor)
                {
                    if (professorWorkload > 25)
                    {
                        ModelState.AddModelError("ProfessorId", "A carga horária máxima do professor (25h) não pode ser excedida");
                        return View(discipline);
                    }
                }
                discipline.Edit();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Discipline/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            var discipline = new Discipline();
            discipline.Get(id);
            return View(discipline);
        }

        // POST: Discipline/Delete/5
        [HttpPost]
        public ActionResult Delete(Discipline discipline)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            try
            {
                // TODO: Add delete logic here
                discipline.Delete();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(discipline);
            }
        }
    }
}
