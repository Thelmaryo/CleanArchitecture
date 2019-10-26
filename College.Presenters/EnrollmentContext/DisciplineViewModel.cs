using System.ComponentModel.DataAnnotations;

namespace College.Presenters.EnrollmentContext
{
    public class DisciplineViewModel
    {
        [Display(Name = "Nome")]
        public string Name { get; set; }
    }
}
