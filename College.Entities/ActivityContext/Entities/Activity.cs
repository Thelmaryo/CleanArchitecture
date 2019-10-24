using College.Entities.ActivityContext.Interfaces;
using College.Entities.Shared;
using System;

namespace College.Entities.ActivityContext.Entities
{
    public class Activity : Entity, IActivity
    {
        public Activity(Discipline discipline, string description, DateTime date, decimal value, decimal pointsAlreadyDistributed, Guid? id)
        {
            if (pointsAlreadyDistributed + value > 100)
                Notifications.Add("Grade", $"A nota total não pode ultrapassar 100 pontos. Já foram distribuídos {pointsAlreadyDistributed} pontos.");
            if (id != null) Id = (Guid)id;
            Discipline = discipline;
            Description = description;
            Value = value;
            Date = date;
        }
        public Activity()
        {

        }

        public Discipline Discipline { get; private set; }
        public string Description { get; private set; }
        public decimal Value { get; private set; }
        public DateTime Date { get; private set; }
    }
}
