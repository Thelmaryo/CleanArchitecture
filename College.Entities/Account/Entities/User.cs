using College.Entities.Shared;
using System.ComponentModel.DataAnnotations;

namespace College.Entities.Account.Entities
{
    public class User : Entity
    {
        [Display(Name = "Usuário")]
        public string UserName { get; private set; }
        [Display(Name = "Senha")]
        public string Password { get; private set; }
        public bool Active { get; private set; }
        public string Salt { get; private set; }
    }
}
