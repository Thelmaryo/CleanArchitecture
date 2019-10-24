using College.UseCases.Shared.Commands;
using College.UseCases.StudentContext.Inputs;
using College.UseCases.StudentContext.Repositories;
using College.UseCases.StudentContext.Result;

namespace College.UseCases.StudentContext.Queries
{
    public class StudentQueryHandler : IQueryHandler<StudentInputGet, StudentResultQueryGet>
    {
        private readonly IStudentRepository _SREP;

        public StudentQueryHandler(IStudentRepository sREP)
        {
            _SREP = sREP;
        }
        public StudentResultQueryGet Handle(StudentInputGet command)
        {
            var result = new StudentResultQueryGet();
            
            if (_SREP.Get(command.StudentId) != null)
            {
                result.Student = _SREP.Get(command.StudentId);
                result.Notifications.Add("Error", "Não foi possivel deletar Discente!");
            }
            else
                result.Notifications.Add("Success", "Discente Deletado");

            return result;
        }

        public StudentResultQueryList Handle(StudentInputList command)
        {
            var result = new StudentResultQueryList();
            result.Student = _SREP.List();
            if (result.Student != null)
            {
                result.Notifications.Add("Success", "Lista Criada com sucesso");
            }
            else
                result.Notifications.Add("Error", "Erro na criação da Lista");

            return result;
        }
    }
}
