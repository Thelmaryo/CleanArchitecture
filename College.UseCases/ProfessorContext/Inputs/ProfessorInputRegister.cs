using College.Entities.ProfessorContext.Enumerators;
using College.UseCases.Shared.Commands;

namespace College.UseCases.ProfessorContext.Inputs
{
    public class ProfessorInputRegister : ICommand
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CPF { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public EDegree Degree { get; set; }
    }
}
