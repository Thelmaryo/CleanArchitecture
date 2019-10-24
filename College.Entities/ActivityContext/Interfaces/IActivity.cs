using College.Entities.ActivityContext.Entities;
using System;
using System.Collections.Generic;

namespace College.Entities.ActivityContext.Interfaces
{
    public interface IActivity
    {
        string Description { get; }
        Discipline Discipline { get; }
        decimal Value { get; }
        DateTime Date { get; }
        IDictionary<string, string> Notifications { get; }
    }
}
