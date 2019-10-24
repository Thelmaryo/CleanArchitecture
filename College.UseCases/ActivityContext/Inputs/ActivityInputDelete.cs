using College.UseCases.Shared.Commands;
using System;

namespace College.UseCases.ActivityContext.Inputs
{
    public class ActivityInputDelete : ICommand
    {
        public Guid Id { get; set; }
    }
}
