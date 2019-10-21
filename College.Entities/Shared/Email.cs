using System.ComponentModel.DataAnnotations;

namespace College.Entities.Shared
{
    public class Email
    {
        [Required(ErrorMessage = "O {0} é  um campo Obrigatorio")]
        [RegularExpression(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~ \w])/*)/(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA- Z]//{2,6}))$",
            ErrorMessage = "O {0} é invalido")]
        public string Address { get; private set; }
    }
}
