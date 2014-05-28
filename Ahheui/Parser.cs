using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamB23.Ahheui
{
    public class Parser
    {
        char[,] words;
        SyntaxTree[,] ast;
        public Parser(string script)
        {
            words = ScriptCutter(script);
            ast = new SyntaxTree[words.GetLength(0), words.GetLength(1)];
            for (int i = 0; i < words.GetLength(0); i++)
            {
                for (int j = 0; j < words.GetLength(1); j++)
                {
                    ast[i, j] = WordAnalyzer(words[i, j]);
                }
            }
        }

        char[,] ScriptCutter(string script)
        {
            string[] lineSplits = script.Split('\n');
            // totalLength를 구해서 완성된 리스트를 만들기전에 임시로 저장해둘 큐
            List<char[]> wordsList = new List<char[]>();

            int totalLength = 0;
            foreach (var lineSplit in lineSplits)
            {
                var word = lineSplit.ToCharArray();
                if (totalLength < word.Length)
                    totalLength = word.Length;
                wordsList.Add(word);
            }
            // 배열 재조립
            char[,] result = new char[lineSplits.Count(), totalLength];
            for (int i = 0; i < lineSplits.Count(); i++)
            {
                for (int j = 0; j < totalLength; j++)
                {
                    result[i,j] = wordsList[i][j];
                }
            }
            return result;
        }
        public SyntaxTree WordAnalyzer(char word)
        {
            SyntaxTree result = new SyntaxTree();
            int tempWord;
            int choseong;
            int jungseong;
            int jongseong;

            tempWord = word - 0xAC00;

            choseong = tempWord / (21 * 28);
            jungseong = (tempWord % (21 * 28)) / 28;
            jongseong = tempWord / 28;

            switch (choseong)
            {
                case 0:  // ㄱ
                    result = new SyntaxTree(Command.Goto, ToMoveCommand(jungseong), Index._);
                        break;
                //case 2:  // ㄴ

                //    break;
            }
            return result;
        }
        Move ToMoveCommand(int jungseong)
        {
            for (int i = 0; i < ReservedWord.move.Length; i++)
            {
                byte rwmove;
                rwmove = ReservedWord.move[i];
                if (jungseong == rwmove)
                {
                    return (Move)jungseong;
                }
            }
            return Move.None;
        }
    }

}
