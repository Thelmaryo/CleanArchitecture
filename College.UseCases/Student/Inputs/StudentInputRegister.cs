using College.UseCases.Commands;
using System;

namespace College.UseCases.Student.Inputs
{
    public class StudentInputRegister : ICommand
    {
        // Id Course
        public Guid CourseId { get; set; }
        // Name Course
        public string Name { get; set; }

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
