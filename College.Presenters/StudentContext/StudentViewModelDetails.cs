using System;
using System.ComponentModel.DataAnnotations;

namespace College.Presenters.StudentContext
{
    public class StudentViewModelDetails
    {
        public Guid Id { get; set; }
        [Display(Name = "Data de Nascimento")]
        public DateTime Birthdate { get; set; }
        [Display(Name = "Nome")]
        public string FirstName { get; set; }
        [Display(Name = "Sobrenome")]
        public string LastName { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        [Display(Name = "Telefone")]
        public string Phone { get; set; }
        [Display(Name = "Gênero")]
        public string Gender { get; set; }
        [Display(Name = "País")]
        public string Country { get; set; }
        [Display(Name = "Cidade")]
        public string City { get; set; }
        [Display(Name = "Endereço")]
        public string Address { get; set; }
        [Display(Name = "Curso")]
        public string Course { get; set; }
    }
}
