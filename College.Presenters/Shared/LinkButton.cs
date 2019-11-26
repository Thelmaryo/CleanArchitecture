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
            _color = "Blue";
        }

        public LinkButton(string text, string color)
        {
            Text = text;
            _color = color;
        }

        public string Text { get; private set; }
        private string _color { get; set; }
        public string Color => _color;
        public string Font => "Arial";
        public string FontColor => "White";
    }
}
