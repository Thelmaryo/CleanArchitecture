using System;
using System.Collections.Generic;
using System.Text;

namespace College.Presenters.Shared
{
    public class BackButton : IButton
    {
        public string Text => "Voltar";
        public string Color => "White";
        public string Font => "Arial";
        public string FontColor => "Black";
    }
}
