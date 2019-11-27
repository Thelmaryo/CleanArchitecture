using College.Entities.Shared;
using System;

namespace College.Entities.EvaluationContext.Entities
{
    public class Activity : Entity
    {
        public Activity()
        {
                
        }
        public Activity(Guid id, Student student, string description, DateTime date, decimal grade, decimal value)
        {
            if (grade > value)
                Notifications.Add("Grade", $"A nota deve estar entre 0 e {value} pontos.");
            Id = id;
            Student = student;
            Description = description;
            Grade = grade;
            Value = value;
            Date = date;
        }

        public Activity(Guid id, Student student, decimal grade, decimal value)
        {
            if (grade > value)
                Notifications.Add("Grade", $"A nota deve estar entre 0 e {value} pontos.");
            Id = id;
            Student = student;
            Grade = grade;
            Value = value;
        }
        // System.Guid Id, System.Decimal Grade, System.Decimal Value
        public Activity(Guid id, Decimal grade, Decimal value)
        {
            if (grade > value)
                Notifications.Add("Grade", $"A nota deve estar entre 0 e {value} pontos.");
            Id = id;
            Grade = grade;
            Value = value;
        }
        public void UpdateStudent(Student student)
        {
            Student = student;
        }

        public Student Student { get; private set; }
        public string Description { get; private set; }
        public decimal Grade { get; private set; }
        public decimal Value { get; private set; }
        public DateTime Date { get; private set; }
    }
}
