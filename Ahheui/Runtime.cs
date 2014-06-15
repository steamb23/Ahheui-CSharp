using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SteamB23.Ahheui
{
    public class Runtime
    {
        IConsoleIO app;

        Parser parser;
        Cursor cursor;
        Stack<double>[] stacks;
        Queue<double> queue;
        int StoragePointer;
        Stack<Cursor> PreviousCursor;
        bool isEnd;

        public Runtime(String script)
        {
            this.cursor = new Cursor(0, 0, parser);
            this.stacks = new Stack<double>[27];
            for (int i = 0; i < stacks.Length; i++)
            {
                stacks[i] = new Stack<double>();
            }
            this.queue = new Queue<double>();
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
                        DoublePopWork(ref stacks[StoragePointer], CurrentSyntax.command);
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
                case Syntax.Command.Push:
                    stacks[StoragePointer].Push(GetRawValue(CurrentSyntax.index));
                    break;
                case Syntax.Command.Input:
                    stacks[StoragePointer].Push(double.Parse(app.Input()));
                    break;
                case Syntax.Command.InputChar:
                    stacks[StoragePointer].Push(char.Parse(app.Input()));
                    break;
                case Syntax.Command.Clone:
                    stacks[StoragePointer].Push(stacks[StoragePointer].Peek());
                    break;
                case Syntax.Command.Switch:
                    DoublePopWork(ref stacks[StoragePointer], Syntax.Command.Switch);
                    break;
            }
        }
        static void DoublePopWork(ref Stack<double> stack, Syntax.Command command)
        {
            double t1 = stack.Pop();
            double t2 = stack.Pop();
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
                case Syntax.Command.Switch:
                    stack.Push(t1);
                    stack.Push(t2);
                    break;
            }
        }
        public static double GetRawValue(Syntax.Index index)
        {
            switch (index)
            {
                case Syntax.Index.ㄱ:
                case Syntax.Index.ㄴ:
                    return 2;
                case Syntax.Index.ㄷ:
                case Syntax.Index.ㅈ:
                case Syntax.Index.ㅋ:
                    return 3;
                case Syntax.Index.ㅁ:
                case Syntax.Index.ㅂ:
                case Syntax.Index.ㅅ:
                case Syntax.Index.ㅊ:
                case Syntax.Index.ㅌ:
                case Syntax.Index.ㅍ:
                case Syntax.Index.ㄲ:
                case Syntax.Index.ㄳ:
                case Syntax.Index.ㅆ:
                    return 4;
                case Syntax.Index.ㄹ:
                case Syntax.Index.ㄵ:
                case Syntax.Index.ㄶ:
                    return 5;
                case Syntax.Index.ㅄ:
                    return 6;
                case Syntax.Index.ㄺ:
                case Syntax.Index.ㄽ:
                    return 7;

                case Syntax.Index.ㅀ:
                    return 8;
                case Syntax.Index.ㄻ:
                case Syntax.Index.ㄼ:
                case Syntax.Index.ㄾ:
                case Syntax.Index.ㄿ:
                    return 9;
                default:
                    return 0;
            }
        }
    }
}
