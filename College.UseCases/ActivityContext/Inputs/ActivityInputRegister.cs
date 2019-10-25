﻿using College.UseCases.ActivityContext.Interfaces;
using College.UseCases.Shared.Commands;

namespace College.UseCases.ActivityContext.Inputs
{
    public class ActivityInputRegister : ICommand
    {
        public IActivity Activity { get; set; }
        public decimal DistributedPoints { get; set; }
    }
}
