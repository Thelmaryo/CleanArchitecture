using College.Entities.ProfessorContext.Enumerators;
using College.UseCases.Shared.Commands;
using System;

namespace College.UseCases.ProfessorContext.Inputs
{
    public class ProfessorInputUpdate : ICommand
    {
        public Guid ProfessorId { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public EDegree Degree { get; set; }
    }
}
