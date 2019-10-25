using College.UseCases.Shared.Commands;
using College.UseCases.Shared.Enumerators;
using System;

namespace College.UseCases.EvaluationContext.Inputs
{
    public class ActivityInputUpdateGrade : ICommand
    {
        public Guid StudentId { get; set; }
        public Guid ActivityId { get; set; }
        public decimal Value { get; set; }
        public decimal Grade { get; set; }
    }
}
