using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SteamB23.Ahheui.Storages;

namespace SteamB23.Ahheui
{
    public partial class Runtime
    {
        public event EventHandler OneRunning;
        public event EventHandler Endding;
        public event EventHandler Stoping;

        IConsole console;

        Parser parser;
        Cursor cursor;
        Storage storage;
        int storagePointer;
        Stack<Cursor> PreviousCursor;
        bool isEnd;
        bool isRun;
        bool isCursorMovePass;
        Syntax.Move previousMove;

        Thread runPlatform;
        readonly object runPlatformLock;
        public Runtime(String script, IConsole console)
        {
            this.console = console;
            this.storage = new Storage();
            this.parser = new Parser(script);
            this.cursor = new Cursor(0, 0, parser);
            this.storagePointer = 0;
            this.PreviousCursor = new Stack<Cursor>();

            this.runPlatformLock = new object();
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
        public int StoragePointer
        {
            get
            {
                return storagePointer;
            }
        }
        public IStorage CurrentStorage
        {
            get
            {
                return storage[storagePointer];
            }
        }
        public bool IsRun
        {
            get
            {
                return isRun;
            }
        }
        public bool IsEnd
        {
            get
            {
                return isEnd;
            }
        }
        /// <summary>
        /// 모든 명령들을 실행합니다.
        /// </summary>
        public void Run()
        {
            isRun = true;
#if DEBUG
            RepeatRun();
#endif
#if RELEASE
            if (runPlatform == null || !runPlatform.IsAlive)
            {
                runPlatform = new Thread(RepeatRun);
                runPlatform.Start();
            }
#endif
        }
        /// <summary>
        /// 실행을 정지하고 사용된 자원을 초기화합니다.
        /// </summary>
        /// <exception cref="System.Threading.ThreadStateException">
        /// 구동 스레드가 시작되지 않은 상태에서 이 메서드가 호출된 경우
        /// </exception>
        /// <exception cref="System.Threading.ThreadInterruptedException">
        /// 구동 스레드가 대기중인 상태에서 이 메서드가 호출된 경우
        /// </exception>
        public void Reset()
        {
            Stop();
            isEnd = false;
            storage.Clear();
            cursor.Clear();
            storagePointer = 0;
        }
        /// <summary>
        /// 실행을 정지합니다.
        /// </summary>
        /// <exception cref="System.Threading.ThreadStateException">
        /// 구동 스레드가 시작되지 않은 상태에서 이 메서드가 호출된 경우
        /// </exception>
        /// <exception cref="System.Threading.ThreadInterruptedException">
        /// 구동 스레드가 대기중인 상태에서 이 메서드가 호출된 경우
        /// </exception>
        public void Stop()
        {
            isRun = false;
        }
        /// <summary>
        /// <c>StorageBackup</c>객체에서 스토리지를 덮어씌웁니다.
        /// </summary>
        /// <param name="backup">스토리지의 데이터가 담겨있는 <c>StorageBackup</c>객체</param>
        public void Restore(StorageBackup backup)
        {
            storage.Restore(backup);
        }
        /// <summary>
        /// 스토리지를 백업합니다.
        /// </summary>
        /// <returns>스토리지의 데이터가 담겨있는 <c>StorageBackup</c>객체</returns>
        public StorageBackup Backup()
        {
            return storage.Backup();
        }
        /// <summary>
        /// 한번만 실행합니다.
        /// </summary>
        public void OneRun()
        {
            isCursorMovePass = false;
            lock (runPlatformLock)
            {
                if (OneRunning != null)
                    OneRunning(this, EventArgs.Empty);
                CommandRun(CurrentSyntax.command);
                if (!isEnd && !isCursorMovePass)
                    CursorMove(CurrentSyntax.move);
            }
        }
        // 스레드에서 돌리기 위한 메서드
        public void RepeatRun()
        {
            while (!isEnd && isRun)
            {
                OneRun();
            }
            if (Stoping != null)
                Stoping(this, EventArgs.Empty);
        }
        public void Wait()
        {
            runPlatform.Join();
        }
    }
}
