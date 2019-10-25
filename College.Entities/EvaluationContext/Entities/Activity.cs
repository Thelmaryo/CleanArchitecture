using College.Entities.Shared;
using System;

namespace College.Entities.EvaluationContext.Entities
{
    public class Activity : Entity
    {
        public Activity(Guid id, Student student, string description, decimal grade, decimal value)
        {
            if (grade > value)
                Notifications.Add("Grade", $"A nota deve estar entre 0 e {value} pontos.");
            Id = id;
            Student = student;
            Description = description;
            Grade = grade;
            Value = value;
        }

        public Activity(Guid id, Guid studentId, decimal grade, decimal value)
        {
            if (grade > value)
                Notifications.Add("Grade", $"A nota deve estar entre 0 e {value} pontos.");
            Id = id;
            Student = new Student(studentId);
            Grade = grade;
            Value = value;
        }

        public Student Student { get; private set; }
        public string Description { get; private set; }
        public decimal Grade { get; private set; }
        public decimal Value { get; private set; }
    }
}
