﻿using College.UseCases.Shared.Commands;
using System;

namespace College.UseCases.ProfessorContext.Inputs
{
    public class ProfessorInputList : ICommand
    {
        public Guid ProfessorId { get; set; }
    }
}
