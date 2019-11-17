using College.Presenters.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace College.Presenters.ActivityContext
{
    public class ActivityDetailsViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Disciplina")]
        public string Disciplina { get; set; }

        [Display(Name = "Activity")]
        public string Description { get; set; }
        [Display(Name = "Valor")]
        public decimal Value { get; set; }
        [Display(Name = "Data")]
        public string Date { get; set; }
        public BackButton SaveButton => new BackButton();
        public LinkButton EditButton => new LinkButton("Editar");
    }
}
