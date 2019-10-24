using College.UseCases.Shared.Commands;

namespace College.UseCases.ProfessorContext.Result
{
    public class ProfessorResultQueryGetWorkload : IQueryResult
    {
        public int Workload { get; set; }
    }
}
