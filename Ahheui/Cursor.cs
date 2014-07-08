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
                if (value < 0)
                    _i = parser.SyntaxField.GetLength(0)-1;
                else if (value >= parser.SyntaxField.GetLength(0))
                    _i = 0;
                else
                    _i = value;
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
                if (value < 0)
                    _j = parser.SyntaxField.GetLength(1)-1;
                else if (value >= parser.SyntaxField.GetLength(1))
                    _j = 0;
                else
                    _j = value;
            }
        }
        public void Clear()
        {
            _i = 0;
            _j = 0;
        }
        public object Clone()
        {
            return new Cursor(i, j, parser);
        }
    }
}
