﻿using College.Entities.Shared;

namespace College.Entities.AccountContext.Entities
{
    public class User : Entity
    {
        public User(){}
        public User(string userName)
        {
            UserName = userName;
        }
        public User(string userName, string password)
        {

            UserName = userName;
            Password = password;
        }
        public User(string userName, string password, string salt, bool active)
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

        public string UserName { get; protected set; }
        public string Password { get; protected set; }
        public bool Active { get; protected set; }
        public string Salt { get; protected set; }
    }
}
