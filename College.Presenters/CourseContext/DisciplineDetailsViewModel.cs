using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using College.Presenters.Shared;
using System;

namespace College.Presenters.CourseContext
{
    public class DisciplineDetailsViewModel
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
        public LinkButton EditButton => new LinkButton("Editar");
        public BackButton BackButton => new BackButton();
    }
}
