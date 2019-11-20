using College.Entities.ActivityContext.Entities;
using College.UseCases.Shared;
using System;
using System.Collections.Generic;

namespace College.UseCases.ActivityContext.Repositories
{
    public interface IActivityRepository
    {
        void Create(Activity activity);
        void Update(Activity activity);
        void Delete(Guid id);
        Activity Get(Guid id);
        IEnumerable<Activity> GetByDiscipline(Guid id, Semester semester);
    }
}
