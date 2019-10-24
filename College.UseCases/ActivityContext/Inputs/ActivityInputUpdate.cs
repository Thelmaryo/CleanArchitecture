using College.UseCases.Shared.Commands;
using College.UseCases.Shared.Enumerators;
using System;

namespace College.UseCases.ActivityContext.Inputs
{
    public class ActivityInputUpdate : ICommand
    {
        public Guid Id { get; set; }
        public Guid DisciplineId { get; set; }
        public string DisciplineName { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public decimal DistributedPoints { get; set; }
        public DateTime Date { get; set; }
        public EActivityType ActivityType { get; set; }
    }
}
