using College.UseCases.ActivityContext.Interfaces;
using College.UseCases.Shared.Commands;
using College.UseCases.Shared.Enumerators;
using System;

namespace College.UseCases.ActivityContext.Inputs
{
    public class ActivityInputRegister : ICommand
    {
        public IActivity Activity { get; set; }
        public decimal DistributedPoints { get; set; }
    }
}
