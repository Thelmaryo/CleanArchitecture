using College.Entities.ActivityContext.Entities;
using College.Entities.EvaluationContext.ValueObjects;
using System;
using System.Collections.Generic;

namespace College.UseCases.ActivityContext.Repositories
{
    public interface IActivityRepository
    {
        public void Create(Activity activity);
        public void Edit(Activity activity);
        public void Delete(Guid id);
        public Activity Get(Guid id);
        public IEnumerable<Activity> GetByDiscipline(Guid id, Semester semester);
    }
}
