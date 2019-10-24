using College.UseCases.Shared.Commands;
using College.UseCases.Shared.Enumerators;
using System;

namespace College.UseCases.EvaluationContext.Inputs
{
    public class ActivityInputGiveGrade : ICommand
    {
        public Guid StudentId { get; set; }
        public string StudentName { get; set; }
        public Guid ActivityId { get; set; }
        public string ActivityName { get; set; }
        public EActivityType ActivityType { get; set; }
        public decimal Value { get; set; }
        public decimal Grade { get; set; }
    }
}
