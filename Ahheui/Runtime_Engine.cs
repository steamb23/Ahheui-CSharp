using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SteamB23.Ahheui.Storages;

namespace SteamB23.Ahheui
{
    public partial class Runtime
    {
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
            var currentStorage = storage[storagePointer];
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
                        DoublePopWork(ref currentStorage, CurrentSyntax.command);
                    }
                    catch (DivideByZeroException)
                    {
                        // 에러핸들러 호출
                        // 아직 에러핸들러에 대한 구현이 이루어지지 않았기 때문에 0을 대입.
                        currentStorage.Push(0);
                    }
                    break;
                case Syntax.Command.Pop:
                    currentStorage.Pop();
                    break;
                case Syntax.Command.Output:
                    console.Output(currentStorage.Pop().ToString());
                    break;
                case Syntax.Command.OutputChar:
                    console.Output(((char)currentStorage.Pop()).ToString());
                    break;
                case Syntax.Command.Push:
                    currentStorage.Push(GetRawValue(CurrentSyntax.index));
                    break;
                case Syntax.Command.Input:
                    currentStorage.Push(double.Parse(console.Input()));
                    break;
                case Syntax.Command.InputChar:
                    currentStorage.Push(char.Parse(console.Input()));
                    break;
                case Syntax.Command.Clone:
                    currentStorage.Push(currentStorage.Peek());
                    break;
                case Syntax.Command.Switch:
                    DoublePopWork(ref currentStorage, Syntax.Command.Switch);
                    break;
                case Syntax.Command.Pick:
                    storagePointer = (int)CurrentSyntax.index;
                    break;
                case Syntax.Command.Move:
                    double temp = currentStorage.Pop();
                    storage[(int)CurrentSyntax.index].Push(temp);
                    break;
            }
        }
        static void DoublePopWork(ref IStorage stack, Syntax.Command command)
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
        static double GetRawValue(Syntax.Index index)
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
