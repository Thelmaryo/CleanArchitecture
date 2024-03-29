﻿using College.UseCases.ProfessorContext.Inputs;
using College.UseCases.ProfessorContext.Repositories;
using College.UseCases.ProfessorContext.Result;
using College.UseCases.Shared.Commands;

namespace College.UseCases.ProfessorContext.Queries
{
    public class ProfessorQueryHandler : IQueryHandler<ProfessorInputGet, ProfessorResultQueryGet>
    {
        private IProfessorRepository _PREP;

        public ProfessorQueryHandler(IProfessorRepository PREP)
        {
            _PREP = PREP;
        }
        public ProfessorResultQueryGet Handle(ProfessorInputGet command)
        {
            var result = new ProfessorResultQueryGet();
            result.Professor = _PREP.Get(command.ProfessorId);

            return result;
        }

        public ProfessorResultQueryList Handle()
        {
            var result = new ProfessorResultQueryList();
            result.Professors = _PREP.List();

            return result;
        }
    }
}
