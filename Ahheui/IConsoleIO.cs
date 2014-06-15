using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SteamB23.Ahheui
{
    public interface IConsoleIO
    {
        void Output(string output);
        string Input();
    }
}
