using College.UseCases.ActivityContext.Interfaces;
using College.UseCases.Shared.Commands;
using System;

namespace College.UseCases.ActivityContext.Inputs
{
    public class ActivityInputUpdate : ICommand
    {
        public Guid Id { get; set; }
        public IActivity Activity { get; set; }
        public decimal DistributedPoints { get; set; }
    }
}
