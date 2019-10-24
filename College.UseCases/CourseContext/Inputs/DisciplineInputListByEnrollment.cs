﻿using College.UseCases.Shared.Commands;
using System;

namespace College.UseCases.CourseContext.Inputs
{
    public class DisciplineInputListByEnrollment : ICommand
    {
        public Guid EnrollmentId { get; set; }
    }
}
