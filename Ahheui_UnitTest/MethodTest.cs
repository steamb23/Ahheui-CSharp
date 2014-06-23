using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SteamB23.Ahheui;

namespace Ahheui_Test
{
    [TestClass]
    public class MethodTest
    {
        [TestMethod]
        public void WordAnalyzer()
        {
            PrivateObject parser = new PrivateObject(new Parser(""));
            Syntax test = (Syntax)parser.Invoke("WordAnalyzer", '고');

            // 방향 판별 검사
            Assert.AreEqual<Syntax>(new Syntax(Syntax.Command.Goto, Syntax.Move.Up, Syntax.Index._), (Syntax)parser.Invoke("WordAnalyzer", '고'));
            Assert.AreEqual<Syntax>(new Syntax(Syntax.Command.Goto, Syntax.Move.Down, Syntax.Index._), (Syntax)parser.Invoke("WordAnalyzer", '구'));
            Assert.AreEqual<Syntax>(new Syntax(Syntax.Command.Goto, Syntax.Move.Right, Syntax.Index._), (Syntax)parser.Invoke("WordAnalyzer", '가'));
            Assert.AreEqual<Syntax>(new Syntax(Syntax.Command.Goto, Syntax.Move.Left, Syntax.Index._), (Syntax)parser.Invoke("WordAnalyzer", '거'));
            Assert.AreEqual<Syntax>(new Syntax(Syntax.Command.Goto, Syntax.Move.UpJump, Syntax.Index._), (Syntax)parser.Invoke("WordAnalyzer", '교'));
            Assert.AreEqual<Syntax>(new Syntax(Syntax.Command.Goto, Syntax.Move.DownJump, Syntax.Index._), (Syntax)parser.Invoke("WordAnalyzer", '규'));
            Assert.AreEqual<Syntax>(new Syntax(Syntax.Command.Goto, Syntax.Move.RightJump, Syntax.Index._), (Syntax)parser.Invoke("WordAnalyzer", '갸'));
            Assert.AreEqual<Syntax>(new Syntax(Syntax.Command.Goto, Syntax.Move.LeftJump, Syntax.Index._), (Syntax)parser.Invoke("WordAnalyzer", '겨'));
            // 상황 검사
            Assert.AreEqual<Syntax>(new Syntax(Syntax.Command.Return, Syntax.Move.None, Syntax.Index._), (Syntax)parser.Invoke("WordAnalyzer", '관'));
            Assert.AreEqual<Syntax>(new Syntax(Syntax.Command.Division, Syntax.Move.Right, Syntax.Index.ㄴ), (Syntax)parser.Invoke("WordAnalyzer", '난'));
            Assert.AreEqual<Syntax>(new Syntax(Syntax.Command.Move, Syntax.Move.Right, Syntax.Index.ㅅ), (Syntax)parser.Invoke("WordAnalyzer", '쌋'));
        }
        /// <summary>
        /// Parser의 생성자에 기존에 돌아다니던 Hello, world!예제 프로그램을 매개변수로 넣어서 무슨결과가 나왔는지 보는 테스트입니다.
        /// </summary>
        [TestMethod]
        public void ParserInitialize()
        {
            Parser parser = new Parser("밤밣따빠밣밟따뿌\n빠맣파빨받밤뚜뭏\n돋밬탕빠맣붏두붇\n볻뫃박발뚷투뭏붖\n뫃도뫃희멓뭏뭏붘\n뫃봌토범더벌뿌뚜\n뽑뽀멓멓더벓뻐뚠\n뽀덩벐멓뻐덕더벅");
        }
    }
}
