using College.Entities.Shared;
using System;

namespace College.Entities.Evaluation.Entities
{
    public class Activity : Entity
    {
        public Activity(Discipline discipline, string description, decimal value, DateTime date)
        {
            Discipline = discipline;
            Description = description;
            Value = value;
            Date = date;
        }

        public Discipline Discipline { get; private set; }
        public string Description { get; private set; }
        public decimal Value { get; private set; }
        public DateTime Date { get; private set; }
    }
}
