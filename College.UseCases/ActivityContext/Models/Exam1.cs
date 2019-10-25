using College.Entities.ActivityContext.Entities;
using College.Entities.Shared;
using College.UseCases.ActivityContext.Interfaces;
using System;

namespace College.UseCases.ActivityContext.Models
{
    public class Exam1 : IActivity
    {
        public Exam1(Guid disciplineId)
        {
            DisciplineId = disciplineId;
        }

        public Guid DisciplineId { get; private set; }
        public DateTime Date
        {
            get
            {
                int month = DateTime.Now.Month <= 7 ? 4 : 10;
                int year = DateTime.Now.Year;
                return new DateTime(1, month, year);
            }
        }
        public string Description => "Prova 1";
        public decimal Value => 20;
    }
}
