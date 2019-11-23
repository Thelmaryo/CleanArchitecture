using College.UseCases.Shared.Commands;
using College.UseCases.StudentContext.Inputs;
using College.UseCases.StudentContext.Repositories;
using College.UseCases.StudentContext.Result;

namespace College.UseCases.StudentContext.Queries
{
    public class StudentQueryHandler : IQueryHandler<StudentInputGetById, StudentResultQueryGet>
    {
        private readonly IStudentRepository _SREP;

        public StudentQueryHandler(IStudentRepository SREP)
        {
            _SREP = SREP;
        }
        public StudentResultQueryGet Handle(StudentInputGetById command)
        {
            var result = new StudentResultQueryGet();
            result.Student = _SREP.Get(command.StudentId);

            return result;
        }

        public StudentResultQueryList Handle(StudentInputList command)
        {
            var result = new StudentResultQueryList();
            result.Students = _SREP.List();

            return result;
        }

        public StudentResultQueryGet Handle(StudentInputGetByCPF command)
        {
            var result = new StudentResultQueryGet();
            result.Student = _SREP.Get(command.StudentCPF);

            return result;
        }

        public StudentResultQueryList Handle(StudentInputListByDiscipline command)
        {
            var result = new StudentResultQueryList();
            result.Students = _SREP.GetByDiscipline(command.DisciplineId);

            return result;
        }
    }
}
