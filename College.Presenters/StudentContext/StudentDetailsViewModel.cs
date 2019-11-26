using College.Presenters.Shared;
using System;

namespace College.Presenters.StudentContext
{
    public class StudentDetailsViewModel
    {
        public Guid Id { get; set; }
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
        public string Course { get; set; }
        public LinkButton EditButton => new LinkButton("Editar");
        public BackButton BackButton => new BackButton();
    }
}
