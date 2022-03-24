using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleMenuExample
{
    class ConsoleMenuElement
    {
        public string Label { get; set; }
        public Action ActionToRun { get; set; }
        public ConsoleMenuElement(string label, Action action)
        {
            Label = label;
            ActionToRun = action;
        }
        public override string ToString()
        {
            return Label;
        }
    }
}
