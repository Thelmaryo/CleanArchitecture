using System.ComponentModel.DataAnnotations;

namespace College.Presenters.CourseContext
{
    public class CourseViewModel
    {
        [Display(Name = "Nome")]
        public string Name { get; set; }
    }
}
