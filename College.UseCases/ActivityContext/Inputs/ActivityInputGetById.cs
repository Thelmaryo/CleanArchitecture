using College.UseCases.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace College.UseCases.ActivityContext.Inputs
{
    public class ActivityInputGetById : ICommand
    {
        public Guid ActivityId { get; set; }
    }
}
