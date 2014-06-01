using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamB23.Ahheui
{
    public class Parser
    {
        char[,] wordField;
        Syntax[,] syntaxField;
        public char[,] WordField
        {
            get
            {
                return wordField;
            }
        }
        public Syntax[,] SyntaxField
        {
            get
            {
                return syntaxField;
            }
        }
        public Parser(string script)
        {
            wordField = ScriptCutter(script);
            syntaxField = new Syntax[wordField.GetLength(0), wordField.GetLength(1)];
            for (int i = 0; i < wordField.GetLength(0); i++)
            {
                for (int j = 0; j < wordField.GetLength(1); j++)
                {
                    syntaxField[i, j] = WordAnalyzer(wordField[i, j]);
                }
            }
        }

        char[,] ScriptCutter(string script)
        {
            string[] lineSplits = script.Split('\n');
            // totalLength를 구해서 완성된 리스트를 만들기전에 임시로 저장해둘 큐
            List<char[]> words = new List<char[]>();

            int totalLength = 0;
            foreach (var lineSplit in lineSplits)
            {
                var word = lineSplit.ToCharArray();
                if (totalLength < word.Length)
                    totalLength = word.Length;
                words.Add(word);
            }
            // 배열 재조립
            char[,] result = new char[lineSplits.Count(), totalLength];
            for (int i = 0; i < lineSplits.Count(); i++)
            {
                for (int j = 0; j < totalLength; j++)
                {
                    result[i, j] = words[i][j];
                }
            }
            return result;
        }
        public Syntax WordAnalyzer(char word)
        {

            Syntax result = CreateSyntaxTree(Syntax.Command.None, -1, 0);
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
                #region 구문 판별 switch
                switch (firstChar)
                {
                    default:
                        result = CreateSyntaxTree(Syntax.Command.None, middleChar, 0);
                        break;
                    case 0:  // ㄱ          Goto와 Return의 구분이 필요함!
                        if (Syntax.Move.None != ToMoveCommand(middleChar))
                            result = CreateSyntaxTree(Syntax.Command.Goto, middleChar, lastChar);
                        else
                            result = CreateSyntaxTree(Syntax.Command.Return, -1, 0);
                        break;
                    case 2:  // ㄴ
                        result = CreateSyntaxTree(Syntax.Command.Division, middleChar, lastChar);
                        break;
                    case 3:  // ㄷ
                        result = CreateSyntaxTree(Syntax.Command.Addition, middleChar, 0);
                        break;
                    case 4:  // ㄸ
                        result = CreateSyntaxTree(Syntax.Command.Multiplication, middleChar, 0);
                        break;
                    case 5:  // ㄹ
                        result = CreateSyntaxTree(Syntax.Command.Remainder, middleChar, lastChar);
                        break;
                    case 6:  // ㅁ
                        if (lastChar == 11)
                            result = CreateSyntaxTree(Syntax.Command.Output, middleChar, 0);
                        else if (lastChar == 18)
                            result = CreateSyntaxTree(Syntax.Command.OutputChar, middleChar, 0);
                        else
                            result = CreateSyntaxTree(Syntax.Command.Pop, middleChar, 0);
                        break;
                    case 7:  // ㅂ
                        if (lastChar == 11)
                            result = CreateSyntaxTree(Syntax.Command.Input, middleChar, 0);
                        else if (lastChar == 18)
                            result = CreateSyntaxTree(Syntax.Command.InputChar, middleChar, 0);
                        else
                            result = CreateSyntaxTree(Syntax.Command.Push, middleChar, 0);
                        break;
                    case 8:  // ㅃ
                        result = CreateSyntaxTree(Syntax.Command.Clone, middleChar, 0);
                        break;
                    case 9:  // ㅅ
                        result = CreateSyntaxTree(Syntax.Command.Pick, middleChar, lastChar);
                        break;
                    case 10: // ㅆ
                        result = CreateSyntaxTree(Syntax.Command.Move, middleChar, lastChar);
                        break;
                    case 11: // ㅇ
                        result = CreateSyntaxTree(Syntax.Command.Nothing, middleChar, 0);
                        break;
                    case 12: // ㅈ
                        result = CreateSyntaxTree(Syntax.Command.Compare, middleChar, 0);
                        break;
                    case 14: // ㅊ
                        result = CreateSyntaxTree(Syntax.Command.Condition, middleChar, 0);
                        break;
                    case 16: // ㅌ
                        result = CreateSyntaxTree(Syntax.Command.Subtraction, middleChar, 0);
                        break;
                    case 17: // ㅍ
                        result = CreateSyntaxTree(Syntax.Command.Switch, middleChar, 0);
                        break;
                    case 18: // ㅎ
                        result = CreateSyntaxTree(Syntax.Command.End, -1, 0);
                        break;
                }
                #endregion
            }
            return result;
        }
        Syntax.Move ToMoveCommand(int middleChar)
        {
            byte reservedWord;
            for (int i = 0; i < ReservedWord.move.Length; i++)
            {
                reservedWord = ReservedWord.move[i];
                if (middleChar == reservedWord)
                {
                    return (Syntax.Move)middleChar;
                }
            }
            return Syntax.Move.None;
        }
        Syntax.Index ToIndex(int lastChar)
        {
            if (lastChar > 20)
            {
                switch (lastChar)
                {
                    case 21:
                        return Syntax.Index.ㅇ;
                    case 27:
                        return Syntax.Index.ㅎ;
                    default:
                        return (Syntax.Index)(lastChar - 1);
                }
            }
            else
                return (Syntax.Index)lastChar;
        }
        Syntax CreateSyntaxTree(Syntax.Command command, int middleChar, int lastChar)
        {
            return new Syntax(command, ToMoveCommand(middleChar), (Syntax.Index)lastChar);
        }
    }

}
