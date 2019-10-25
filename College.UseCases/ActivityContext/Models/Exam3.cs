using College.Entities.ActivityContext.Entities;
using College.Entities.Shared;
using College.UseCases.ActivityContext.Interfaces;
using System;

namespace College.UseCases.ActivityContext.Models
{
    public class Exam3 : IActivity
    {
        public Exam3(Guid disciplineId)
        {
            DisciplineId = disciplineId;
        }

        public Guid DisciplineId { get; private set; }
        public DateTime Date
        {
            get
            {
                int month = DateTime.Now.Month <= 7 ? 6 : 12;
                int year = DateTime.Now.Year;
                return new DateTime(1, month, year);
            }
        }
        public string Description => "Prova 3";
        public decimal Value => 20;
    }
}
