using College.UseCases.Shared.Commands;
using System;

namespace College.UseCases.EvaluationContext.Inputs
{
    public class ActivityInputGiveGrade : ICommand
    {
        public Guid StudentId { get; set; }
        public Guid ActivityId { get; set; }
        public decimal Value { get; set; }
        public decimal Grade { get; set; }
    }
}
