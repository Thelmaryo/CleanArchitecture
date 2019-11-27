using College.Entities.EvaluationContext.Entities;
using System;
using System.Collections.Generic;

namespace College.UseCases.EvaluationContext.Repositories
{
    public interface IActivityRepository
    {
        void Create(Activity activity);
        void Update(Activity activity);
        Activity GetByStudent(Guid studentId, Guid activityId);
        IEnumerable<Activity> GetByDiscipline(Guid studentId, Guid disciplineId);
    }
}
