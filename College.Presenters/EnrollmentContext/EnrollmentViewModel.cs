using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace College.Presenters.EnrollmentContext
{
    public class EnrollmentViewModel
    {
        // Student Name
        [Display(Name = "Nome")]
        public string Name { get; set; }

        public List<DisciplineViewModel> _Disciplines { get; set; }

        public IReadOnlyList<DisciplineViewModel> Disciplines { get => _Disciplines; }

        [Display(Name = "Ininio")]
        public DateTime Begin { get; set; }

        [Display(Name = "Final")]
        public DateTime End { get; set; }

        public int Status { get; set; }
    }
}
