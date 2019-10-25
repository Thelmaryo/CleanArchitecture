using College.Entities.ActivityContext.Entities;
using College.UseCases.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace College.UseCases.ActivityContext.Results
{
    public class ActivityResultQueryList : IQueryResult
    {
        public IEnumerable<Activity> Activities { get; set; }
        public ActivityResultQueryList()
        {
            Activities = new List<Activity>();
        }
    }
}
