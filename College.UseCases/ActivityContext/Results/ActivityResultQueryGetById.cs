using College.Entities.ActivityContext.Entities;
using College.UseCases.Shared.Commands;

namespace College.UseCases.ActivityContext.Results
{
    public class ActivityResultQueryGetById : IQueryResult
    {
        public Activity Activity { get; set; }
    }
}
