using College.UseCases.Commands;

namespace College.UseCases.Professor.Inputs
{
    public class ProfessorInputRegister : ICommand
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CPF { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public int Degree { get; set; }
    }
}
