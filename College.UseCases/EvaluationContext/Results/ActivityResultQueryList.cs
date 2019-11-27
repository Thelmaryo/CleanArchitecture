using College.Entities.EvaluationContext.Entities;
using College.UseCases.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace College.UseCases.EvaluationContext.Results
{
    public class ActivityResultQueryList : IQueryResult
    {
        public IEnumerable<Activity> Activities { get; set; }
        public decimal TotalGrade => Activities.Sum(x => x.Grade);
    }
}
