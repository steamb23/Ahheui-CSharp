using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SteamB23.Ahheui
{
    public class Runtime
    {
        IApplication app;

        Parser parser;
        Cursor cursor;
        Stack<int>[] stacks;
        Queue<int> queue;
        int StoragePointer;
        bool isEnd;

        public Runtime(String script)
        {
            this.cursor = new Cursor(0, 0);
            this.stacks = new Stack<int>[27];
            for (int i = 0; i < stacks.Length; i++)
            {
                stacks[i] = new Stack<int>();
            }
            this.queue = new Queue<int>();
            this.parser = new Parser(script);
            this.StoragePointer = 0;
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
        void CommandRun()
        {
            switch (CurrentSyntax.command)
            {
                case Syntax.Command.Nothing:
                    break;
                case Syntax.Command.End:
                    isEnd = true;
                    break;
                case Syntax.Command.Goto:
                    //                       텍스트 검색을 해야한다.. 미친..
                    break;
                case Syntax.Command.Return:
                    //                       Goto문이 호출됬던 위치로 이동한다.
                    //                       이는 Stack<Cursor>변수를 이용하여 Goto문이 여러번 호출된 상태를 대비한다.
                    break;
                case Syntax.Command.Addition:
                case Syntax.Command.Subtraction:
                case Syntax.Command.Multiplication:
                case Syntax.Command.Division:
                case Syntax.Command.Remainder:
                    try
                    {
                        Operator.Calculator(ref stacks[StoragePointer], CurrentSyntax.command);
                    }
                    catch (DivideByZeroException)
                    {
                        // 에러핸들러 호출
                    }
                    break;
                case Syntax.Command.Pop:
                    stacks[StoragePointer].Pop();
                    break;
                case Syntax.Command.Output:
                    app.Output(stacks[StoragePointer].Pop().ToString());
                    break;
                case Syntax.Command.OutputChar:
                    app.Output(((char)stacks[StoragePointer].Pop()).ToString());
                    break;
            }
        }
        public static class Operator
        {
            public static void Calculator(ref Stack<int> stack, Syntax.Command command)
            {
                int t1 = stack.Pop();
                int t2 = stack.Pop();
                switch (command)
                {
                    case Syntax.Command.Addition:
                        stack.Push(t2 + t1);
                        break;
                    case Syntax.Command.Subtraction:
                        stack.Push(t2 - t1);
                        break;
                    case Syntax.Command.Multiplication:
                        stack.Push(t2 * t1);
                        break;
                    case Syntax.Command.Division:
                        stack.Push(t2 / t1);
                        break;
                    case Syntax.Command.Remainder:
                        stack.Push(t2 % t1);
                        break;
                }
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
