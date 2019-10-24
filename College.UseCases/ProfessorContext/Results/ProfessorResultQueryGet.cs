using College.Entities.ProfessorContext.Entities;
using College.UseCases.Shared.Commands;

namespace College.UseCases.ProfessorContext.Result
{
    public class ProfessorResultQueryGet : IQueryResult
    {
        public Professor Professor { get; set; }
    }
}
