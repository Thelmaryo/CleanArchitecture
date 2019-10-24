﻿using College.UseCases.Shared.Commands;
using System;

namespace College.UseCases.CourseContext.Inputs
{
    public class DisciplineInputRegister : ICommand
    {
        public string Name { get; set; }
        public Guid CourseId { get; set; }
        public Guid ProfessorId { get; set; }
        public int WeeklyWorkload { get; set; }
        public int Period { get; set; }
    }
}
