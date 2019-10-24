using System;

namespace College.Entities.EvaluationContext.Entities
{
    public class Activity : ActivityBase
    {
        public Activity(Student student, string description, decimal value, decimal grade, Guid? id)
        {
            if (grade > value)
                Notifications.Add("Grade", $"A atividade vale {value} pontos");
            if (id != null) Id = (Guid)id;
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
