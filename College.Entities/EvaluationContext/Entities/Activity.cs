using College.Entities.EvaluationContext.Interfaces;
using College.Entities.Shared;
using System;

namespace College.Entities.EvaluationContext.Entities
{
    public class Activity : ActivityBase
    {
        public Activity(Student student, string description, decimal value, decimal grade)
        {
            if (grade > value)
                Notifications.Add("Grade", $"A atividade vale {value} pontos");
            Student = student;
            Description = description;
            _Value = value;
            Grade = grade;
        }
        public Activity()
        {

        }
        private decimal _Value { get; set; }
        protected override decimal GetValue() => _Value;
        
    }
}
