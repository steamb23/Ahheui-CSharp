using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SteamB23.Ahheui;

namespace Ahheui_Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void WordAnalyzer()
        {
            PrivateObject parser = new PrivateObject(new Parser("아희"));
            SyntaxTree test = (SyntaxTree)parser.Invoke("WordAnalyzer", '고');
            Assert.AreEqual<SyntaxTree>(new SyntaxTree(Command.Goto, Move.Up, Index._), (SyntaxTree)parser.Invoke("WordAnalyzer", '고'));
            Assert.AreEqual<SyntaxTree>(new SyntaxTree(Command.Goto, Move.Down, Index._), (SyntaxTree)parser.Invoke("WordAnalyzer", '구'));
            Assert.AreEqual<SyntaxTree>(new SyntaxTree(Command.Goto, Move.Right, Index._), (SyntaxTree)parser.Invoke("WordAnalyzer", '가'));
            Assert.AreEqual<SyntaxTree>(new SyntaxTree(Command.Goto, Move.Left, Index._), (SyntaxTree)parser.Invoke("WordAnalyzer", '거'));
            Assert.AreEqual<SyntaxTree>(new SyntaxTree(Command.Goto, Move.UpJump, Index._), (SyntaxTree)parser.Invoke("WordAnalyzer", '교'));
            Assert.AreEqual<SyntaxTree>(new SyntaxTree(Command.Goto, Move.DownJump, Index._), (SyntaxTree)parser.Invoke("WordAnalyzer", '규'));
            Assert.AreEqual<SyntaxTree>(new SyntaxTree(Command.Goto, Move.RightJump, Index._), (SyntaxTree)parser.Invoke("WordAnalyzer", '갸'));
            Assert.AreEqual<SyntaxTree>(new SyntaxTree(Command.Goto, Move.LeftJump, Index._), (SyntaxTree)parser.Invoke("WordAnalyzer", '겨'));
            
        }
    }
}
