using College.Presenters.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace College.Presenters.CourseContext
{
    public class DeleteDisciplineViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Disciplina")]
        public string Name { get; set; }
        [Display(Name = "Curso")]
        public string Course { get; set; }
        [Display(Name = "Professor")]
        public string Professor { get; set; }
        [Display(Name = "Carga de trabalho semanal")]
        public int WeeklyWorkload { get; set; }
        [Display(Name = "Periodo")]
        public int Period { get; set; }
        public DeleteButton DeleteButton => new DeleteButton();
        public BackButton BackButton => new BackButton();
    }
}
