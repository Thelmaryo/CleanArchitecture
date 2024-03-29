﻿using College.Entities.EnrollmentContext.Enumerators;
using College.Entities.Shared;
using System;
using System.Collections.Generic;

namespace College.Entities.EnrollmentContext.Entities
{
    public class Enrollment : Entity
    {
        public Enrollment(Student student, DateTime begin, DateTime end, EStatusEnrollment status)
        {
            Student = student;
            Begin = begin;
            End = end;
            Status = status;
            _Disciplines = new List<Discipline>();
        }
        public Enrollment(Student student, DateTime begin, DateTime end, EStatusEnrollment status, Guid id)
        {
            Id = id;
            Student = student;
            Begin = begin;
            End = end;
            Status = status;
            _Disciplines = new List<Discipline>();
        }
        public Enrollment(DateTime begin, DateTime end, EStatusEnrollment status)
        {
            Begin = begin;
            End = end;
            Status = status;
            _Disciplines = new List<Discipline>();
        }
        public Enrollment()
        {
            _Disciplines = new List<Discipline>();
        }

        public Student Student { get; private set; }
        private List<Discipline> _Disciplines { get; set; }
        public IReadOnlyList<Discipline> Disciplines { get => _Disciplines; }
        public DateTime Begin { get; private set; }
        public DateTime End { get; private set; }
        public EStatusEnrollment Status { get; private set; }

        public void AddDiscipline(Discipline discipline)
        {
            _Disciplines.Add(discipline);
        }

        public void AddDisciplines(IEnumerable<Discipline> disciplines)
        {
            _Disciplines.AddRange(disciplines);
        }
    }
}
