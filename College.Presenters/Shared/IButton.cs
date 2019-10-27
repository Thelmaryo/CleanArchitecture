using System;
using System.Collections.Generic;
using System.Text;

namespace College.Presenters.Shared
{
    public interface IButton
    {
        string Text { get; }
        string Color { get; }
        string Font { get; }
        string FontColor { get; }
    }
}
