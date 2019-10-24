using System.Collections.Generic;
using College.Entities.StudentContext.Entities;
using College.UseCases.Shared.Commands;

namespace College.UseCases.StudentContext.Result
{
    public class StudentResultQueryList : ICommandResult
    {
        public IDictionary<string, string> Notifications { get; set; }

        public IEnumerable<Student> Student { get; set; }
    }
}
