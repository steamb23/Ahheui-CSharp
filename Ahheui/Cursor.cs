using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SteamB23.Ahheui
{

    public class Cursor : ICloneable
    {
        // C#의 신이시어 프로퍼티를 대문자로 표기하지 않은 저를 용서하소서.
        Parser parser;
        public Cursor(int i, int j, Parser parser)
        {
            this._i = i;
            this._j = j;
            this.parser = parser;
        }
        int _i;
        int _j;
        public int i
        {
            get
            {
                return _i;
            }
            set
            {
                _i = (value < 0) ? parser.SyntaxField.GetLength(0) : value;
                _i = (value >= parser.SyntaxField.GetLength(0)) ? 0 : value;
            }
        }
        public int j
        {
            get
            {
                return _j;
            }
            set
            {
                _j = (value < 0) ? parser.SyntaxField.GetLength(1) : value;
                _j = (value >= parser.SyntaxField.GetLength(1)) ? 0 : value;
            }
        }
        public object Clone()
        {
            return new Cursor(i, j, parser);
        }
    }
}
