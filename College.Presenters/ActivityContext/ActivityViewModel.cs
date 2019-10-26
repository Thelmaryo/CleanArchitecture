using System;
using System.ComponentModel.DataAnnotations;

namespace College.Presenters.ActivityContext
{
    public class ActivityViewModel
    {
        // Discipline Name
        [Display(Name = "Disciplina")]
        public string Name { get; set; }
        [Display(Name = "Descrição")]
        public string Description { get; set; }
        [Display(Name = "Nota")]
        public decimal Value { get; set; }
        [Display(Name = "Data")]
        public DateTime Date { get; set; }
    }
}
