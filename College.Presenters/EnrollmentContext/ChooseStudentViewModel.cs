using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using College.Presenters.Shared;

namespace College.Presenters.EnrollmentContext
{
    public class ChooseStudentViewModel
    {
        [Display(Name = "Cursos")]
        public IEnumerable<ComboboxItem> Courses { get; set; }
        [Display(Name = "Acadêmico (CPF)")]
        public string StudentCPF { get; set; }
        public SaveButton SaveButton => new SaveButton();
        public BackButton BackButton => new BackButton();
        public string Feedback { get; set; }
    }
}
