using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace College.Entities.Shared
{
    public class CPF : ValueObject
    {
        public CPF()
        {

        }
        public CPF(string number)
        {
            Regex rg = new Regex(@"([0-9]{2}[\.]?[0-9]{3}[\.]?[0-9]{3}[\/]?[0-9]{4}[-]?[0-9]{2})|([0-9]{3}[\.]?[0-9]{3}[\.]?[0-9]{3}[-]?[0-9]{2})");

            if (string.IsNullOrEmpty(number) || !rg.IsMatch(number))
            {
                Notification = "CPF Inválido!";
            }
            Number = number;
        }
        [Required(ErrorMessage = "O {0} é  um campo Obrigatorio")]
        [RegularExpression(@"([0-9]{2}[\.]?[0-9]{3}[\.]?[0-9]{3}[\/]?[0-9]{4}[-]?[0-9]{2})|([0-9]{3}[\.]?[0-9]{3}[\.]?[0-9]{3}[-]?[0-9]{2})",
            ErrorMessage = "O {0} é invalido")]
        public string Number { get; private set; }
    }
}
