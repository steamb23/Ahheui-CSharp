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
                return parser.WordField[cursor.i, cursor.j];
            }
        }
        public Syntax CurrentSyntax
        {
            get
            {
                return parser.SyntaxField[cursor.i, cursor.j];
            }
        }
        public void CheckAwayCursor()
        {
            if (cursor.i < 0)
                cursor.i = parser.SyntaxField.GetLength(0);
            if (cursor.i >= parser.SyntaxField.GetLength(0))
                cursor.i = 0;

            if (cursor.j < 0)
                cursor.j = parser.SyntaxField.GetLength(1);
            if (cursor.j >= parser.SyntaxField.GetLength(1))
                cursor.j = 0;
        }
        void CursorMove()
        {
            switch (CurrentSyntax.move)
            {
                case Syntax.Move.Up:
                    cursor.i--;
                    CheckAwayCursor();
                    break;
                case Syntax.Move.Down:
                    cursor.i++;
                    CheckAwayCursor();
                    break;
                case Syntax.Move.Right:
                    cursor.j++;
                    CheckAwayCursor();
                    break;
                case Syntax.Move.Left:
                    cursor.j--;
                    CheckAwayCursor();
                    break;
                case Syntax.Move.UpJump:
                    cursor.i--;
                    CheckAwayCursor();
                    cursor.i--;
                    CheckAwayCursor();
                    break;
                case Syntax.Move.DownJump:
                    cursor.i++;
                    CheckAwayCursor();
                    cursor.i++;
                    CheckAwayCursor();
                    break;
                case Syntax.Move.RightJump:
                    cursor.j++;
                    CheckAwayCursor();
                    cursor.j++;
                    CheckAwayCursor();
                    break;
                case Syntax.Move.LeftJump:
                    cursor.j--;
                    CheckAwayCursor();
                    cursor.j--;
                    CheckAwayCursor();
                    break;
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
