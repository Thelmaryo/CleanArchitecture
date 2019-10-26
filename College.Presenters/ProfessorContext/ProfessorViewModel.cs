using System;
using System.ComponentModel.DataAnnotations;

namespace College.Presenters.ProfessorContext
{
    public class ProfessorViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Nome")]
        public string FirstName { get; set; }

        [Display(Name = "Sobrenome")]
        public string LastName { get; set; }

        public string CPF { get; set; }

        public string Email { get; set; }

        [Display(Name = "Telefone")]
        public string Phone { get; set; }
    }
}
