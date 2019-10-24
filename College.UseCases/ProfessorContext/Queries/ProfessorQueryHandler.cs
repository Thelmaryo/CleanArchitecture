using College.UseCases.ProfessorContext.Inputs;
using College.UseCases.Shared.Commands;
using College.UseCases.ProfessorContext.Result;
using College.UseCases.ProfessorContext.Repositories;
using System;

namespace College.UseCases.ProfessorContext.Queries
{
    public class ProfessorQueryHandler : IQueryHandler<ProfessorInputGet, ProfessorResultQueryGet>
    {
        private readonly IProfessorRepository _PREP;

        public ProfessorQueryHandler(IProfessorRepository PREP)
        {
            _PREP = PREP;
        }
        public ProfessorResultQueryGet Handle(ProfessorInputGet command)
        {
            var result = new ProfessorResultQueryGet();

            if (_PREP.Get(command.ProfessorId) != null)
            {
                result.Professor = _PREP.Get(command.ProfessorId);
                result.Notifications.Add("Error", "Não foi possivel deletar Discente!");
            }
            else
                result.Notifications.Add("Success", "Discente Deletado");

            return result;
            throw new System.NotImplementedException();
        }

        public ProfessorResultQueryList Handle(ProfessorInputList command)
        {
            var result = new ProfessorResultQueryList();
            result.Professor = _PREP.List();
            if (result.Professor != null)
            {
                result.Notifications.Add("Success", "Lista Criada com sucesso");
            }
            else
                result.Notifications.Add("Error", "Erro na criação da Lista");

            return result;
        }

        public ProfessorResultQueryGetWorkload Handle(ProfessorInputGetWorkload command)
        {
            var result = new ProfessorResultQueryGetWorkload();
            result.Workload = _PREP.GetWorkload(command.ProfessorId);
            // if (result.Workload != null)
            // {
            //     result.Notifications.Add("Error", "Não foi possivel deletar Discente!");
            // }
            // else
            //     result.Notifications.Add("Success", "Discente Deletado");

            return result;
        }
    }
}
