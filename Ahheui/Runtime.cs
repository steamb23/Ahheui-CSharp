using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SteamB23.Ahheui
{
    public class Runtime
    {
        Parser parser;
        Cursor cursor;
        public Runtime(String script)
        {
            this.parser = new Parser(script);
        }
    }
    public struct Cursor
    {
        int i;
        int j;
    }
}
