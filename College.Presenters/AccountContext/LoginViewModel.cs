using System.ComponentModel.DataAnnotations;

namespace College.Presenters.AccountContext
{
    public class LoginViewModel
    {
        [Display(Name = "Usuário")]
        public string UserName { get; set; }
        [Display(Name = "Senha")]
        public string Password { get; set; }
        public string Feedback { get; set; }
    }
}
