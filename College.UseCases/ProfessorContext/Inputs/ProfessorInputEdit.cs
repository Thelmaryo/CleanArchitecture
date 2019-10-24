using College.UseCases.Shared.Commands;

namespace College.UseCases.ProfessorContext.Inputs
{
    public class ProfessorInputEdit : ICommand
    {
        public Guid ProfessorId { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public int Degree { get; set; }
    }
}
