using System.Collections.Generic;
using College.Entities.StudentContext.Entities;
using College.UseCases.Shared.Commands;

namespace College.UseCases.StudentContext.Result
{
    public class StudentResultQueryGet : IQueryResult
    {
        public IDictionary<string, string> Notifications { get; set; }

        public Student Student { get; set; }
    }
}
