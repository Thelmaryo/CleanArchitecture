namespace College.UseCases.Account.Inputs
{
    public class UserInputResult
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public string Salt { get; set; }
    }
}
