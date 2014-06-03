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
        Stack<Cursor> PreviousCursor;
        bool isEnd;

        public Runtime(String script)
        {
            this.cursor = new Cursor(0, 0, parser);
            this.stacks = new Stack<int>[27];
            for (int i = 0; i < stacks.Length; i++)
            {
                stacks[i] = new Stack<int>();
            }
            this.queue = new Queue<int>();
            this.parser = new Parser(script);
            this.StoragePointer = 0;
            this.PreviousCursor = new Stack<Cursor>();
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
        void CursorMove()
        {
            switch (CurrentSyntax.move)
            {
                case Syntax.Move.Up:
                    cursor.i--;
                    break;
                case Syntax.Move.Down:
                    cursor.i++;
                    break;
                case Syntax.Move.Right:
                    cursor.j++;
                    break;
                case Syntax.Move.Left:
                    cursor.j--;
                    break;
                case Syntax.Move.UpJump:
                    cursor.i--;
                    cursor.i--;
                    break;
                case Syntax.Move.DownJump:
                    cursor.i++;
                    cursor.i++;
                    break;
                case Syntax.Move.RightJump:
                    cursor.j++;
                    cursor.j++;
                    break;
                case Syntax.Move.LeftJump:
                    cursor.j--;
                    cursor.j--;
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
}
