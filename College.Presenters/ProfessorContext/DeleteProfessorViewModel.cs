using System;
using System.ComponentModel.DataAnnotations;
using College.Presenters.Shared;

namespace College.Presenters.ProfessorContext
{
    public class DeleteProfessorViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Nome")]
        public string FirstName { get; set; }

        [Display(Name = "Sobrenome")]
        public string LastName { get; set; }

        public string CPF { get; set; }

        public string Email { get; set; }

        [Display(Name = "Telefone")]
        public string Phone { get; set; }

        [Display(Name = "Titulação")]
        public string Degree { get; set; }
        public BackButton BackButton => new BackButton();
        public DeleteButton DeleteButton => new DeleteButton();
    }
}
