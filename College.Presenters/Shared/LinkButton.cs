using System;
using System.Collections.Generic;
using System.Text;

namespace College.Presenters.Shared
{
    public class LinkButton : IButton
    {
        public LinkButton(string text)
        {
            Text = text;
        }

        public string Text { get; private set; }
        public string Color => "Blue";
        public string Font => "Arial";
        public string FontColor => "White";
    }
}
