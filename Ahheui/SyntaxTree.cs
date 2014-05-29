using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SteamB23.Ahheui
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// 워낙에 간단한 구조의 언어이기 때문에 이런 형태를 띕니다.
    /// </remarks>
    public struct SyntaxTree
    {
        public SyntaxTree(Command command, Move move, Index index)
        {
            this.command = command;
            this.move = move;
            this.index = index;
        }
        Command command;
        Move move;
        Index index;
        public enum Command
        {
            None,
            /// <summary>
            /// 아무것도 하지 않음 - ㅇ+이동 모음
            /// </summary>
            Nothing,
            /// <summary>
            /// 종료 - ㅎ
            /// </summary>
            End,

            /// <summary>
            /// 점프 - ㄱ+이동 모음+오류 받침
            /// </summary>
            Goto,
            /// <summary>
            /// 반환 - ㄱ+비이동 모음+오류 받침
            /// </summary>
            Return,

            /// <summary>
            /// 덧셈 - ㄷ+이동 모음
            /// </summary>
            Addition,
            /// <summary>
            /// 뺄셈 - ㅌ+이동 모음
            /// </summary>
            Subtraction,
            /// <summary>
            /// 곱셈 - ㄸ+이동 모음
            /// </summary>
            Multiplication,
            /// <summary>
            /// 나눗셈 - ㄴ+이동 모음+오류 받침
            /// </summary>
            Division,
            /// <summary>
            /// 나머지 - ㄹ+이동 모음+오류 받침
            /// </summary>
            Remainder,

            /// <summary>
            /// 뽑기 - ㅁ+이동 모음
            /// </summary>
            Pop,
            /// <summary>
            /// 출력 - ㅁ+이동 모음+ㅇ받침
            /// </summary>
            Output,
            /// <summary>
            /// 유니코드 출력 - ㅁ+이동 모음+ㅎ받침
            /// </summary>
            OutputChar,
            /// <summary>
            /// 넣기 - ㅂ+이동 모음
            /// </summary>
            Push,
            /// <summary>
            /// 입력 - ㅂ+이동 모음+ㅇ받침
            /// </summary>
            Input,
            /// <summary>
            /// 입력 - ㅂ+이동 모음+ㅎ받침
            /// </summary>
            InputChar,
            /// <summary>
            /// 되넣기 - ㅃ+이동 모음
            /// </summary>
            Clone,
            /// <summary>
            /// 뒤집기 - ㅍ+이동 모음
            /// </summary>
            /// <remarks>
            /// 스택 맨위의 값과 그 아래의 값을 바꿔치기합니다.
            /// </remarks>
            Switch,

            /// <summary>
            /// 선택 - ㅅ+이동 모음+아무 받침
            /// </summary>
            Pick,
            /// <summary>
            /// 이동 - ㅆ+이동 모음+아무 받침
            /// </summary>
            Move,
            /// <summary>
            /// 비교 - ㅈ+이동 모음
            /// </summary>
            Compare,
            /// <summary>
            /// 조건 - ㅊ+이동 모음
            /// </summary>
            Condition
        }
        public enum Move
        {
            None = -1,
            Up = 8,
            Down = 13,
            Right = 0,
            Left = 4,
            UpJump = 12,
            DownJump = 17,
            RightJump = 2,
            LeftJump = 6
        }
        public enum Index
        {
             _ = 0,
            ㄱ = 1,
            ㄲ = 2,
            ㄳ = 3,
            ㄴ = 4,
            ㄵ = 5,
            ㄶ = 6,
            ㄷ = 7,
            ㄹ = 8,
            ㄺ = 9,
            ㄻ = 10,
            ㄼ = 11,
            ㄽ = 12,
            ㄾ = 13,
            ㄿ = 14,
            ㅀ = 15,
            ㅁ = 16,
            ㅂ = 17,
            ㅄ = 18,
            ㅅ = 19,
            ㅆ = 20,
            ㅇ = 21, // 경우에 따라 안쓰임
            ㅈ = 22,
            ㅊ = 23,
            ㅋ = 24,
            ㅌ = 25,
            ㅍ = 26,
            ㅎ = 27  // 경우에 따라 안쓰임
        }
    }
    public static class ReservedWord
    {
        public static readonly byte[] command = new byte[] { 0, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 14, 16, 17, 18 };
        public static readonly byte[] move = new byte[] { 8, 13, 0, 4, 12, 17, 2, 6 };
        public static readonly byte[] index = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27 };
        // 받침은 모두 예약어
    }
}
