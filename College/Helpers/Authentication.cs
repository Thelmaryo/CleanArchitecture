using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace College.Helpers
{
    public static class Authentication
    {
        public static bool UserAuthenticated { get; set; }
        public static Guid UserId { get; set; }
    }
}