using College.Entities.Shared;

namespace College.Entities.Account.Entities
{
    public class User : Entity
    {
        public User(string userName, string password)
        {

            UserName = userName;
            Password = password;
        }
        public User(string userName, string password, bool active, string salt)
        {

            UserName = userName;
            Password = password;
            Active = active;
            Salt = salt;
        }
        public void Activate()
        {
            Active = true;
        }
        public void Disable()
        {
            Active = false;
        }

        public string UserName { get; private set; }
        public string Password { get; private set; }
        public bool Active { get; private set; }
        public string Salt { get; private set; }
    }
}
