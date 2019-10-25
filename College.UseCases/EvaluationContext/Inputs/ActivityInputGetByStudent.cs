using College.UseCases.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace College.UseCases.EvaluationContext.Inputs
{
    public class ActivityInputGetByStudent : ICommand
    {
        public Guid StudentId { get; set; }
        public Guid ActivityId { get; set; }
    }
}
