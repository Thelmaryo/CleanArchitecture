using College.Presenters.Shared;

namespace College.Presenters.AccountContext
{
    public class LoginViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public LinkButton SubmitButton => new LinkButton("Entrar");
        public string Feedback { get; set; }
    }
}
