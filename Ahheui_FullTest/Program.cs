using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SteamB23.Ahheui;

namespace SteamB23.Ahheui.FullTest
{
    class Program : IConsole
    {
        string temp;
        static void Main(string[] args)
        {
            new Program().Run();
        }
        void Run()
        {
            Console.WriteLine("[헬로월드]");
            Console.WriteLine("Runtime의 인스턴스 생성...");
            var ahheui = new Runtime("밤밣따빠밣밟따뿌\n빠맣파빨받밤뚜뭏\n돋밬탕빠맣붏두붇\n볻뫃박발뚷투뭏붖\n뫃도뫃희멓뭏뭏붘\n뫃봌토범더벌뿌뚜\n뽑뽀멓멓더벓뻐뚠\n뽀덩벐멓뻐덕더벅", this);
            Console.WriteLine("OneRunEnd 이벤트 등록...");
            ahheui.OneRunEnd += OnOneRunEnd;
            Console.WriteLine("Run");
            ahheui.Run();
            Console.WriteLine("============================================================================");
            Console.WriteLine("                                             출력 결과 : {0}", temp);
        }

        void IConsole.Output(string output)
        {
            Console.WriteLine("                                             출력 : {0}", output);
            temp += output;
        }

        string IConsole.Input()
        {
            Console.Write("                                             입력 : ");
            var temp = Console.ReadLine();
            return temp;
        }
        public void OnOneRunEnd(Runtime sender, EventArgs e)
        {

            Console.WriteLine("============================================================================");
            Console.WriteLine("커서 위치 : {0}, {1}", sender.Cursor.i, sender.Cursor.j);
            Console.WriteLine("현재 스토리지 : {0}", sender.StoragePointer);
            Console.WriteLine("현재 단어 : {0}", sender.CurrentWord);
            Console.WriteLine("현재 명령 : {0}", sender.CurrentSyntax.command);
        }
    }
}
