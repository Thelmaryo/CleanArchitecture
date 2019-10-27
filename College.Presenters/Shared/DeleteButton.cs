using System;
using System.Collections.Generic;
using System.Text;

namespace College.Presenters.Shared
{
    public class DeleteButton : IButton
    {
        public string Text => "Excluir";
        public string Color => "Red";
        public string Font => "Arial";
        public string FontColor => "White";
    }
}
