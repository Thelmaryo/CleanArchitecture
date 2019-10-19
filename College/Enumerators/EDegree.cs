using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace College.Enumerators
{
    public enum EDegree
    {
        [Description("Especialista")]
        Bachelor,
        [Description("Mestre")]
        Master,
        [Description("Doutor")]
        Doctor
    }
}