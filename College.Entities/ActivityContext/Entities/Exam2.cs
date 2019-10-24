﻿using College.Entities.ActivityContext.Interfaces;
using College.Entities.Shared;
using System;

namespace College.Entities.ActivityContext.Entities
{
    public class Exam2 : Entity, IActivity
    {
        public Exam2(Discipline discipline, Guid? id)
        {
            if (id != null) Id = (Guid)id;
            Discipline = discipline;
        }

        public Discipline Discipline { get; private set; }
        public DateTime Date
        {
            get
            {
                int month = DateTime.Now.Month <= 7 ? 5 : 11;
                int year = DateTime.Now.Year;
                return new DateTime(1, month, year);
            }
        }
        public string Description => "Prova 2";
        public decimal Value => 20;
    }
}
