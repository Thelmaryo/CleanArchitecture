using College.Helpers;
using College.UseCases.AccountContext.Queries;
using College.UseCases.CourseContext.Inputs;
using College.UseCases.EnrollmentContext.Inputs;
using College.UseCases.EnrollmentContext.Queries;
using College.UseCases.EvaluationContext.Inputs;
using System.Linq;
using System.Web.Mvc;
using Course = College.UseCases.CourseContext.Queries;
using Evaluation = College.UseCases.EvaluationContext.Queries;

namespace College.Controllers
{
    public class HomeController : ControllerBase
    {
        private readonly Evaluation.DisciplineQueryHandler _disciplineEvaluationQuery;
        private readonly Course.DisciplineQueryHandler _disciplineQuery;
        private readonly EnrollmentQueryHandler _enrollmentQuery;
        public HomeController(Course.DisciplineQueryHandler disciplineQuery, EnrollmentQueryHandler enrollmentQuery, Evaluation.DisciplineQueryHandler disciplineEvaluationQuery,
            UserQueryHandler userQuery) : base(userQuery)
        {
            _disciplineQuery = disciplineQuery;
            _enrollmentQuery = enrollmentQuery;
            _disciplineEvaluationQuery = disciplineEvaluationQuery;
        }

        public ActionResult Index()
        {
            if (!Authentication.UserAuthenticated)
                return RedirectToAction("Login", "Account");
            if (UserIsInRole("Student"))
                return RedirectToAction("Student");
            else if (UserIsInRole("Professor"))
                return RedirectToAction("Professor");
            else if (UserIsInRole("Admin"))
                return View();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Professor()
        {
            if (!UserIsInRole("Professor"))
                return RedirectToAction("Index", "Home");
            ;
            ViewBag.Disciplines = _disciplineQuery.Handle(new DisciplineInputGetByProfessor { ProfessorId = Authentication.UserId }).Disciplines;
            return View();
        }

        public ActionResult Student()
        {
            if (!UserIsInRole("Student"))
                return RedirectToAction("Index", "Home");
            var enrollment = _enrollmentQuery.Handle(new EnrollmentInputGetByStudent { StudentId = Authentication.UserId }).Enrollment;
            ViewBag.Disciplines = _disciplineEvaluationQuery.Handle(new DisciplineInputListByEnrollment {
                EnrollmentId = enrollment.Id,
                StudentId = Authentication.UserId,
                SemesterBegin = enrollment.Begin,
                SemesterEnd = enrollment.End
            }).Disciplines;
            ViewBag.StatusEnrollment = enrollment.Status;
            var enrollments = _enrollmentQuery.Handle(new EnrollmentInputListByStudent { StudentId = Authentication.UserId }).Enrollment.OrderBy(x => x.Begin).ToList(); 
            enrollments.RemoveAll(x => x.Id == enrollment.Id);
            ViewBag.Enrollments = enrollments;
            return View();
        }
    }
}