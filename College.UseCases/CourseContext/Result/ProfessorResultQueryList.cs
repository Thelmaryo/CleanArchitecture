using College.Entities.CourseContext.Entities;
using College.UseCases.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace College.UseCases.CourseContext.Result
{
    public class ProfessorResultQueryList : IQueryResult
    {
        public IEnumerable<Professor> Professors { get; set; }
    }
}
