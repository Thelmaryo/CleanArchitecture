using College.Entities.Shared;
using System;

namespace College.Entities.Evaluation.Entities
{
    public class Activity : Entity
    {
        public Activity(Student student, string description, decimal value, decimal grade)
        {
            if (grade > value)
                Notifications.Add("Grade", $"A atividade vale {value} pontos");
            Student = student;
            Description = description;
            Value = value;
            Grade = grade;
        }
        public Activity()
        {

        }
        public Student Student { get; private set; }
        public string Description { get; private set; }
        public decimal Value { get; private set; }
        public decimal Grade { get; private set; }
    }
}
