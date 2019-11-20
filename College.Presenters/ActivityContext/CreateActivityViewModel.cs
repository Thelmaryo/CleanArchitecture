﻿using College.Presenters.Shared;
using System.ComponentModel.DataAnnotations;

namespace College.Presenters.ActivityContext
{
    public class CreateActivityViewModel
    {
        [Display(Name = "Activity")]
        public string Description { get; set; }
        [Display(Name = "Valor")]
        public decimal Value { get; set; }
        [Display(Name = "Data")]
        public string Date { get; set; }
        public SaveButton SaveButton => new SaveButton();
    }
}