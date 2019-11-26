using College.Entities.ActivityContext.Entities;
using College.UseCases.Shared.Commands;
using System;
using System.Collections.Generic;

namespace College.UseCases.ActivityContext.Interfaces
{
    public interface IActivity
    {
        string Description { get; }
        Guid DisciplineId { get; }
        decimal Value { get; }
        decimal MaxValue { get; }
        DateTime Date { get; }
    }
}
