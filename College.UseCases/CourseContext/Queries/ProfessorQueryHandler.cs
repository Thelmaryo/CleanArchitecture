using College.UseCases.CourseContext.Inputs;
using College.UseCases.CourseContext.Repositories;
using College.UseCases.CourseContext.Result;
using College.UseCases.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace College.UseCases.CourseContext.Queries
{
    public class ProfessorQueryHandler : IQueryHandler<ProfessorInputList, ProfessorResultQueryList>
    {
        private IProfessorRepository _PREP;
        public ProfessorQueryHandler(IProfessorRepository PREP)
        {
            _PREP = PREP;
        }
        public ProfessorResultQueryList Handle(ProfessorInputList command)
        {
            var result = new ProfessorResultQueryList();
            result.Professors = _PREP.List();
            return result;
        }
    }
}
