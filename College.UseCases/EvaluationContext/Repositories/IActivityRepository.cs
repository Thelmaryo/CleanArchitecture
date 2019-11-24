using College.Entities.EvaluationContext.Entities;
using System;

namespace College.UseCases.EvaluationContext.Repositories
{
    public interface IActivityRepository
    {
        void Create(Activity activity);
        void Update(Activity activity);
        Activity GetByStudent(Guid studentId, Guid activityId);
    }
}
