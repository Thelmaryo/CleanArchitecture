﻿using College.UseCases.Shared.Commands;
using System;

namespace College.UseCases.EnrollmentContext.Inputs
{
    public class EnrollmentInputRegister : ICommand
    {
        public Guid StudentId { get; set; }
        public Guid[] Dicipline { get; set; }
    }
}
