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
        char[,] Words
        {
            get
            {
                return words;
            }
        }
        SyntaxTree[,] Ast
        {
            get
            {
                return ast;
            }
        }
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

            SyntaxTree result = CreateSyntaxTree(SyntaxTree.Command.None, -1, 0);
            if (word >= 0xac00 && word <= 0xD79F)
            {

                int tempWord;
                int firstChar;
                int middleChar;
                int lastChar;

                tempWord = word - 0xac00;

                firstChar = tempWord / (21 * 28);
                middleChar = (tempWord % (21 * 28)) / 28;
                lastChar = (tempWord % (21 * 28)) % 28;
                if (ToMoveCommand(middleChar) != SyntaxTree.Move.None)
                {
                    #region 구문 판별 switch
                    switch (firstChar)
                    {
                        default:
                            result = CreateSyntaxTree(SyntaxTree.Command.None, middleChar, 0);
                            break;
                        case 0:  // ㄱ          Goto와 Return의 구분이 필요함!
                            if (SyntaxTree.Move.None != ToMoveCommand(middleChar))
                                result = CreateSyntaxTree(SyntaxTree.Command.Goto, middleChar, lastChar);
                            else
                                result = CreateSyntaxTree(SyntaxTree.Command.Return, -1, 0);
                            break;
                        case 2:  // ㄴ
                            result = CreateSyntaxTree(SyntaxTree.Command.Division, middleChar, lastChar);
                            break;
                        case 3:  // ㄷ
                            result = CreateSyntaxTree(SyntaxTree.Command.Addition, middleChar, 0);
                            break;
                        case 4:  // ㄸ
                            result = CreateSyntaxTree(SyntaxTree.Command.Multiplication, middleChar, 0);
                            break;
                        case 5:  // ㄹ
                            result = CreateSyntaxTree(SyntaxTree.Command.Remainder, middleChar, lastChar);
                            break;
                        case 6:  // ㅁ
                            if (lastChar == 11)
                                result = CreateSyntaxTree(SyntaxTree.Command.Output, middleChar, lastChar);
                            else if (lastChar == 18)
                                result = CreateSyntaxTree(SyntaxTree.Command.OutputChar, middleChar, lastChar);
                            else
                                result = CreateSyntaxTree(SyntaxTree.Command.Pop, middleChar, 0);
                            break;
                        case 7:  // ㅂ
                            if (lastChar == 11)
                                result = CreateSyntaxTree(SyntaxTree.Command.Input, middleChar, lastChar);
                            else if (lastChar == 18)
                                result = CreateSyntaxTree(SyntaxTree.Command.InputChar, middleChar, lastChar);
                            else
                                result = CreateSyntaxTree(SyntaxTree.Command.Push, middleChar, 0);
                            break;
                        case 8:  // ㅃ
                            result = CreateSyntaxTree(SyntaxTree.Command.Clone, middleChar, 0);
                            break;
                        case 9:  // ㅅ
                            result = CreateSyntaxTree(SyntaxTree.Command.Pick, middleChar, lastChar);
                            break;
                        case 10: // ㅆ
                            result = CreateSyntaxTree(SyntaxTree.Command.Move, middleChar, lastChar);
                            break;
                        case 11: // ㅇ
                            result = CreateSyntaxTree(SyntaxTree.Command.Nothing, middleChar, 0);
                            break;
                        case 12: // ㅈ
                            result = CreateSyntaxTree(SyntaxTree.Command.Compare, middleChar, 0);
                            break;
                        case 14: // ㅊ
                            result = CreateSyntaxTree(SyntaxTree.Command.Condition, middleChar, 0);
                            break;
                        case 16: // ㅌ
                            result = CreateSyntaxTree(SyntaxTree.Command.Subtraction, middleChar, 0);
                            break;
                        case 17: // ㅍ
                            result = CreateSyntaxTree(SyntaxTree.Command.Switch, middleChar, 0);
                            break;
                        case 18: // ㅎ
                            result = CreateSyntaxTree(SyntaxTree.Command.End, -1, 0);
                            break;
                    }
                    #endregion
                }
            }
            return result;
        }
        SyntaxTree.Move ToMoveCommand(int jungseong)
        {
            byte reservedWord;
            for (int i = 0; i < ReservedWord.move.Length; i++)
            {
                reservedWord = ReservedWord.move[i];
                if (jungseong == reservedWord)
                {
                    return (SyntaxTree.Move)jungseong;
                }
            }
            return SyntaxTree.Move.None;
        }
        SyntaxTree CreateSyntaxTree(SyntaxTree.Command command, int jungseong, int jongseong)
        {
            return new SyntaxTree(command, ToMoveCommand(jungseong), (SyntaxTree.Index)jongseong);
        }
    }

}
