using College.Presenters.Shared;
using System;

namespace College.Presenters.ProfessorContext
{
    public class ProfessorDetailsViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public LinkButton EditButton => new LinkButton("Editar");
        public BackButton BackButton => new BackButton();
    }
}
