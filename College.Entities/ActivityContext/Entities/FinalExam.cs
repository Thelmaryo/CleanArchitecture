using College.Entities.ActivityContext.Interfaces;
using College.Entities.Shared;
using System;

namespace College.Entities.ActivityContext.Entities
{
    public class FinalExam : Entity, IActivity
    {
        public FinalExam(Discipline discipline, Guid? id)
        {
            if (id != null) Id = (Guid)id;
            Discipline = discipline;
        }

        public Discipline Discipline { get; private set; }
        public DateTime Date
        {
            get
            {
                int month = DateTime.Now.Month <= 7 ? 6 : 12;
                int year = DateTime.Now.Year;
                return new DateTime(8, month, year);
            }
        }
        public string Description => "Exame Final";
        public decimal Value => 100;
    }
}