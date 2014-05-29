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
            PrivateObject parser = new PrivateObject(new Parser("아희"));
            SyntaxTree test = (SyntaxTree)parser.Invoke("WordAnalyzer", '고');

            // 방향 판별 검사
            Assert.AreEqual<SyntaxTree>(new SyntaxTree(SyntaxTree.Command.Goto, SyntaxTree.Move.Up, SyntaxTree.Index._), (SyntaxTree)parser.Invoke("WordAnalyzer", '고'));
            Assert.AreEqual<SyntaxTree>(new SyntaxTree(SyntaxTree.Command.Goto, SyntaxTree.Move.Down, SyntaxTree.Index._), (SyntaxTree)parser.Invoke("WordAnalyzer", '구'));
            Assert.AreEqual<SyntaxTree>(new SyntaxTree(SyntaxTree.Command.Goto, SyntaxTree.Move.Right, SyntaxTree.Index._), (SyntaxTree)parser.Invoke("WordAnalyzer", '가'));
            Assert.AreEqual<SyntaxTree>(new SyntaxTree(SyntaxTree.Command.Goto, SyntaxTree.Move.Left, SyntaxTree.Index._), (SyntaxTree)parser.Invoke("WordAnalyzer", '거'));
            Assert.AreEqual<SyntaxTree>(new SyntaxTree(SyntaxTree.Command.Goto, SyntaxTree.Move.UpJump, SyntaxTree.Index._), (SyntaxTree)parser.Invoke("WordAnalyzer", '교'));
            Assert.AreEqual<SyntaxTree>(new SyntaxTree(SyntaxTree.Command.Goto, SyntaxTree.Move.DownJump, SyntaxTree.Index._), (SyntaxTree)parser.Invoke("WordAnalyzer", '규'));
            Assert.AreEqual<SyntaxTree>(new SyntaxTree(SyntaxTree.Command.Goto, SyntaxTree.Move.RightJump, SyntaxTree.Index._), (SyntaxTree)parser.Invoke("WordAnalyzer", '갸'));
            Assert.AreEqual<SyntaxTree>(new SyntaxTree(SyntaxTree.Command.Goto, SyntaxTree.Move.LeftJump, SyntaxTree.Index._), (SyntaxTree)parser.Invoke("WordAnalyzer", '겨'));

            Assert.AreEqual<SyntaxTree>(new SyntaxTree(SyntaxTree.Command.None, SyntaxTree.Move.None, SyntaxTree.Index._), (SyntaxTree)parser.Invoke("WordAnalyzer", '관'));
            Assert.AreEqual<SyntaxTree>(new SyntaxTree(SyntaxTree.Command.Division, SyntaxTree.Move.Right, SyntaxTree.Index.ㄴ), (SyntaxTree)parser.Invoke("WordAnalyzer", '난'));
            Assert.AreEqual<SyntaxTree>(new SyntaxTree(SyntaxTree.Command.Move, SyntaxTree.Move.Right, SyntaxTree.Index.ㅅ), (SyntaxTree)parser.Invoke("WordAnalyzer", '쌋'));
        }
    }
}
