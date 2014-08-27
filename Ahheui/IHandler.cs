using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SteamB23.Ahheui
{
    public interface IConsole
    {
        void Output(string output);
        string Input(InputType inputType);
    }
    public enum InputType
    {
        Number,
        Char
    }
}
