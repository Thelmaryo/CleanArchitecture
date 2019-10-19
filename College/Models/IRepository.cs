using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace College.Models
{
    public interface IRepository
    {
        void Create();
        void Edit();
        void Delete();
    }
}