using System;
using System.Collections.Generic;
using System.Text;

namespace College.Presenters.Shared
{
    public class SaveButton : IButton
    {
        public string Text => "Salvar";
        public string Color => "Green";
        public string Font => "Arial";
        public string FontColor => "White";
    }
}
