using System.ComponentModel.DataAnnotations;
using College.Presenters.Shared;
using System.Collections.Generic;

namespace College.Presenters.ProfessorContext
{
    public class CreateProfessorViewModel
    {

        [Display(Name = "Nome")]
        public string FirstName { get; set; }

        [Display(Name = "Sobrenome")]
        public string LastName { get; set; }

        public string CPF { get; set; }

        public string Email { get; set; }

        [Display(Name = "Telefone")]
        public string Phone { get; set; }

        [Display(Name = "Titulação")]
        public IEnumerable<ComboboxItem> Degree { get; set; }
        public SaveButton SaveButton => new SaveButton();
        public BackButton BackButton => new BackButton();
    }
}
