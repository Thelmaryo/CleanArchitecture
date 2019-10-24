using College.UseCases.Shared.Commands;
using College.UseCases.StudentContext.Inputs;
using College.UseCases.StudentContext.Repositories;
using College.UseCases.StudentContext.Result;

namespace College.UseCases.StudentContext.Queries
{
    public class StudentQueryHandler : IQueryHandler<StudentInputGetById, StudentResultQueryGet>
    {
        private readonly IStudentRepository _SREP;

        public StudentQueryHandler(IStudentRepository sREP)
        {
            _SREP = sREP;
        }
        public StudentResultQueryGet Handle(StudentInputGetById command)
        {
            var result = new StudentResultQueryGet();
            var student = _SREP.Get(command.StudentId);
            if (student != null)
            {
                result.Student = student;
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

        public StudentResultQueryGet Handle(StudentInputGetByCPF command)
        {
            var result = new StudentResultQueryGet();
            var student = _SREP.Get(command.StudentCPF);
            if (student != null)
            {
                result.Student = student;
                result.Notifications.Add("Error", "Não foi possivel deletar Discente!");
            }
            else
                result.Notifications.Add("Success", "Discente Deletado");

            return result;
        }

        public StudentResultQueryList Handle(StudentInputListByDiscipline command)
        {
            var result = new StudentResultQueryList();
            result.Student = _SREP.GetByDiscipline(command.DisciplineId);
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
