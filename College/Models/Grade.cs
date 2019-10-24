﻿using System;
using System.ComponentModel.DataAnnotations;

namespace College.Models
{
    public class Grade
    {
        public Guid DisciplineId { get; set; }
        [Display(Name = "Disciplina")]
        public string Discipline { get; set; }
        [Display(Name = "Valor")]
        public decimal Value { get; set; }
        [Display(Name = "Exame Final")]
        public decimal FinalExam { get; set; }
        public string Status { get; set; }
    }
}