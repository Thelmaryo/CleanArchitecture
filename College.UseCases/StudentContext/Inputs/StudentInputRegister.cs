using College.UseCases.Shared.Commands;
using System;

namespace College.UseCases.StudentContext.Inputs
{
    public class StudentInputRegister : ICommand
    {
        public Guid CourseId { get; set; }

        public DateTime Birthdate { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CPF { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Gender { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Address { get; set; }
    }
}
