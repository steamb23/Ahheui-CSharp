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
            this.cursor = new Cursor(0, 0);
        }
        public Cursor Cursor
        {
            get
            {
                return cursor;
            }
        }
        public char CurrentWord
        {
            get
            {
                return parser.Words[cursor.i, cursor.j];
            }
        }
    }
    public struct Cursor
    {
        public Cursor(int i, int j)
        {
            this.i = i;
            this.j = j;
        }
        public int i;
        public int j;
    }
}
