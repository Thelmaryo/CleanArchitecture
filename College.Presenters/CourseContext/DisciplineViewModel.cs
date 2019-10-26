using System;
using System.ComponentModel.DataAnnotations;

namespace College.Presenters.CourseContext
{
    public class DisciplineViewModel
    {
        [Display(Name = "Nome")]
        public string Name { get; set; }
        [Display(Name = "Course")]
        public Guid CourseId { get; set; }
        [Display(Name = "Professor")]
        public Guid ProfessorId { get; set; }
        [Display(Name = "Carga de trabalho semanal")]
        public int WeeklyWorkload { get; set; }
        [Display(Name = "Periodo")]
        public int Period { get; set; }
    }
}
