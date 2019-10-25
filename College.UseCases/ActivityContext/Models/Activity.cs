using College.Entities.ActivityContext.Entities;
using College.UseCases.ActivityContext.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace College.UseCases.ActivityContext.Models
{
    public class Activity : IActivity
    {
        public Activity(string description, Guid disciplineId, decimal value, DateTime date)
        {
            Description = description;
            DisciplineId = disciplineId;
            Value = value;
            Date = date;
        }

        public string Description { get; private set; }
        public Guid DisciplineId { get; private set; }
        public decimal Value { get; private set; }
        public DateTime Date { get; private set; }
    }
}
