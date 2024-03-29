﻿using System;
using System.Collections.Generic;
using System.Text;

namespace College.Presenters.Shared
{
    public class ComboboxItem
    {
        public ComboboxItem(string text, string value)
        {
            Text = text;
            Value = value;
        }

        public string Text { get; set; }
        public string Value { get; set; }
    }
}
