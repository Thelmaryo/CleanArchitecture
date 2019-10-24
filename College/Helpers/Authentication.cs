using System;

namespace College.Helpers
{
    public static class Authentication
    {
        public static bool UserAuthenticated { get; set; }
        public static Guid UserId { get; set; }
    }
}