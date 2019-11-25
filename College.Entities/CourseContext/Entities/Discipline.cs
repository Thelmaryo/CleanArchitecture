using College.Entities.CourseContext.Dictionaries;
using College.Entities.Shared;
using System;

namespace College.Entities.CourseContext.Entities
{
    public class Discipline : Entity
    {
        public Discipline() { }
        // Create
        public Discipline(string name, Course course, Professor professor, int weeklyWorkload, int period, int professorWorkload)
        {
            Name = name;
            Course = course;
            Professor = professor;
            WeeklyWorkload = weeklyWorkload;
            Period = period;
            var maxWorkload = new ProfessorMaxWorkLoadDictionary().Get(Professor.Degree);
            if (professorWorkload + WeeklyWorkload > maxWorkload)
                Notifications.Add("WeeklyWorkload", $"A carga horária máxima do professor {maxWorkload} não pode ser excedida");
        }

        public string Name { get; private set; }
        public Course Course { get; private set; }
        public Professor Professor { get; private set; }
        public int WeeklyWorkload { get; private set; }
        public int Period { get; private set; }

        // Edit
        public Discipline(string name, Course course, Professor professor, int weeklyWorkload, int period, Guid id, int professorWorkload = 0)
        {
            Id = id;
            Name = name;
            Course = course;
            Professor = professor;
            WeeklyWorkload = weeklyWorkload;
            Period = period;
            var maxWorkload = new ProfessorMaxWorkLoadDictionary().Get(Professor.Degree);
            if (professorWorkload != 0 && professorWorkload > maxWorkload)
                Notifications.Add("WeeklyWorkload", $"A carga horária máxima do professor {maxWorkload} não pode ser excedida");
        }
    }
}
