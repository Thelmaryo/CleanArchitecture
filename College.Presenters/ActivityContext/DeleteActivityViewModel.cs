using College.Presenters.Shared;
using System;
using System.ComponentModel.DataAnnotations;

namespace College.Presenters.ActivityContext
{
    public class DeleteActivityViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Disciplina")]
        public string Discipline { get; set; }
        public Guid DisciplineId { get; set; }
        [Display(Name = "Activity")]
        public string Description { get; set; }
        [Display(Name = "Valor")]
        public decimal Value { get; set; }
        [Display(Name = "Data")]
        public string Date { get; set; }
        public BackButton BackButton => new BackButton();
        public DeleteButton DeleteButton => new DeleteButton();
    }
}
