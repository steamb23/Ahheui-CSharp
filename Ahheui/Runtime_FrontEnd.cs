using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SteamB23.Ahheui.Storages;

namespace SteamB23.Ahheui
{
    public partial class Runtime
    {
        #region 이벤트
        /// <summary>
        /// 한번 실행될때마다 호출됩니다.
        /// </summary>
        /// <remarks>
        /// <c>OnceRun</c>메서드를 수동으로 호출하였거나, <c>Run</c>메서드, <c>RepeatRun</c>메서드에서 <c>OnceRun</c>메서드를 호출했을 때에도 호출됩니다.
        /// </remarks>
        public event EventHandler OnceRunning;
        /// <summary>
        /// 한번 실행후 호출됩니다.
        /// </summary>
        public event EventHandler OnceRan;
        /// <summary>
        /// 스크립트의 진행이 정상적으로 완전히 종료되었을때 호출됩니다.
        /// </summary>
        public event EventHandler Finishing;
        /// <summary>
        /// 반복 실행이 완전히 종료되었을 때 호출됩니다.
        /// </summary>
        public event EventHandler RepeatRunEndding;
        /// <summary>
        /// <c>Run</c>메서드가 호출될 때 호출됩니다.
        /// </summary>
        public event EventHandler CallRun;
        /// <summary>
        /// <c>Stop</c>메서드가 호출될 때 호출됩니다.
        /// </summary>
        public event EventHandler CallStop;
        /// <summary>
        /// <c>Abort</c>메서드가 호출될 때 호출됩니다.
        /// </summary>
        public event EventHandler CallAbort;
        /// <summary>
        /// <c>Reset</c>메서드가 호출될 때 호출됩니다.
        /// </summary>
        public event EventHandler CallReset;
        void OnOnceRunning()
        {
            if (OnceRunning != null)
                OnceRunning(this, EventArgs.Empty);
        }
        void OnOnceRan()
        {
            if (OnceRan != null)
                OnceRan(this, EventArgs.Empty);
        }
        void OnFinishing()
        {
            if (Finishing != null)
                Finishing(this, EventArgs.Empty);
        }
        void OnRepeatRunEndding()
        {
            if (RepeatRunEndding != null)
                RepeatRunEndding(this, EventArgs.Empty);
        }
        void OnCallRun()
        {
            if (CallRun != null)
                CallRun(this, EventArgs.Empty);
        }
        void OnCallStop()
        {
            if (CallStop != null)
                CallStop(this, EventArgs.Empty);
        }
        void OnCallAbort()
        {
            if (CallAbort != null)
                CallAbort(this, EventArgs.Empty);
        }
        void OnCallReset()
        {
            if (CallReset != null)
                CallReset(this, EventArgs.Empty);
        }
        #endregion
        IConsole console;

        string script;
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
        public Runtime(string script, IConsole console)
        {
            this.script = script;
            this.console = console;
            this.storage = new Storage();
            this.parser = new Parser(script);
            this.cursor = new Cursor(0, 0, parser);
            this.storagePointer = 0;
            this.PreviousCursor = new Stack<Cursor>();

            this.runPlatformLock = new object();
        }
        public string Script
        {
            get
            {
                return script;
            }
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
        public void Reset()
        {
            Stop();
            OnCallReset();
            Parallel.Invoke(() =>
            {
                isEnd = false;
                runPlatform.Join();
                isEnd = false;
                storage.Clear();
                cursor.Clear();
                storagePointer = 0;
            });
        }
        /// <summary>
        /// 실행을 정지합니다.
        /// </summary>
        public void Stop()
        {
            OnCallStop();
            isRun = false;
        }
        /// <summary>
        /// 실행을 정지하고 백업을 반환합니다.
        /// </summary>
        public Backup Abort()
        {
            OnCallAbort();
            Stop();
            isCursorMovePass = true;
            return Backup();
        }
        /// <summary>
        /// <c>StorageBackup</c>객체에서 스토리지를 덮어씌웁니다.
        /// </summary>
        /// <param name="backup">스토리지의 데이터가 담겨있는 <c>StorageBackup</c>객체</param>
        public void Restore(Backup backup)
        {
            storage.Restore(backup);
        }
        /// <summary>
        /// 스토리지를 백업합니다.
        /// </summary>
        /// <returns>스토리지의 데이터가 담겨있는 <c>StorageBackup</c>객체</returns>
        public Backup Backup()
        {
            return storage.Backup();
        }
        /// <summary>
        /// 한번만 실행합니다.
        /// </summary>
        public void OnceRun()
        {
            isCursorMovePass = false;
            lock (runPlatformLock)
            {
                OnOnceRunning();
                CommandRun(CurrentSyntax.command);
                if (!isEnd && !isCursorMovePass)
                    CursorMove(CurrentSyntax.move);
                OnOnceRan();
            }
        }
        // 스레드에서 돌리기 위한 메서드
        public void RepeatRun()
        {
            while (!isEnd && isRun)
            {
                OnceRun();
            }
            //OnRepeatRunEndding();
        }
    }
}
