using System.ComponentModel.DataAnnotations;

namespace College.Entities.Shared
{
    public class CPF
    {
        [Required(ErrorMessage = "O {0} é  um campo Obrigatorio")]
        [RegularExpression(@"([0-9]{2}[\.]?[0-9]{3}[\.]?[0-9]{3}[\/]?[0-9]{4}[-]?[0-9]{2})|([0-9]{3}[\.]?[0-9]{3}[\.]?[0-9]{3}[-]?[0-9]{2})",
            ErrorMessage = "O {0} é invalido")]
        public string Number { get; private set; }
    }
}
