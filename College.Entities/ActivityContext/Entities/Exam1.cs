using College.Entities.ActivityContext.Interfaces;
using College.Entities.Shared;
using System;

namespace College.Entities.ActivityContext.Entities
{
    public class Exam1 : Entity, IActivity
    {
        public Exam1(Discipline discipline, Guid? id)
        {
            if (id != null) Id = (Guid)id;
            Discipline = discipline;
        }

        public Discipline Discipline { get; private set; }
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
