using College.Entities.ActivityContext.Interfaces;
using College.Entities.EvaluationContext.ValueObjects;
using System;
using System.Collections.Generic;

namespace College.UseCases.ActivityContext.Repositories
{
    public interface IActivityRepository
    {
        public void Create(IActivity activity);
        public void Update(IActivity activity);
        public void Delete(Guid id);
        public IActivity Get(Guid id);
        public IEnumerable<IActivity> GetByDiscipline(Guid id, Semester semester);
    }
}
