using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SteamB23.Ahheui;

namespace SteamB23.Ahheui_WinForm
{
    public partial class Main : Form, IConsole
    {
        string activatedScript = "";
        Runtime runtime;
        StringBuilder outputString = new StringBuilder();
        StorageBackup backup;
        public Main()
            : base()
        {
            RuntimeInitialize("");
            InitializeComponent();
        }
        void RuntimeInitialize(string script)
        {
            runtime = new Runtime(script, this);
            runtime.Endding += Endding;
        }
        #region IConsole구현
        void IConsole.Output(string output)
        {
            //if (output != "\r\n" && output == "\n" && output == "\r")
            //    output = "\r\n";
            txtBox_outputBox.Invoke(new Action(() =>
            {
                txtBox_outputBox.AppendText(output);
            }));
        }

        string IConsole.Input(InputType inputType)
        {
            string inputTypeString;
            switch (inputType)
            {
                case InputType.Char:
                    inputTypeString = "문자(Char)";
                    break;
                case InputType.Number:
                    inputTypeString = "정수(Int64)";
                    break;
                default:
                    throw new ArgumentException();
            }
            object temp;
            using (var input = new Input(inputTypeString))
            {
                //components.Add(input);
                temp = this.Invoke(new Func<string>(() =>
                {
                    switch (input.ShowDialog())
                    {
                        case DialogResult.OK:
                            return input.txtbox_char.Text;
                        case DialogResult.Cancel:
                        default:
                            Abort();
                            return "0";
                    }
                }));
                //components.Remove(input);
            }
            return (string)temp;
        }
        #endregion
        #region 아희 이벤트 핸들러
        void Endding(object sender, EventArgs e)
        {
            MessageBox.Show("실행 완료", "완료");
        }
        void Stoping(object sender, EventArgs e)
        {
            runtime.Restore(backup);
        }
        void Test(object sender, EventArgs e)
        {
            string temp = "현재 명령 : " + runtime.CurrentWord + "\n현재 좌표 : " + runtime.Cursor.i + ", " + runtime.Cursor.j;
            try
            {
                temp += "\n현재 스택 : " + runtime.CurrentStorage.Peek();
            }
            catch (Exception)
            {
            }

            //MessageBox.Show(temp);
        }
        #endregion
        void Run()
        {
            if (runtime.IsEnd)
                Clear();
            var boxScript = this.txtBox_scriptBox.Text.Replace("\r", "");
            if (activatedScript != boxScript)
            {
                activatedScript = boxScript;
                RuntimeInitialize(boxScript);
            }
            if (runtime != null)
                runtime.Run();
        }
        void Stop()
        {
            if (runtime != null)
                runtime.Stop();
        }
        void Abort()
        {
            this.backup = runtime.Backup();
            Stop();
        }
        void Clear()
        {
            this.txtBox_outputBox.Text = "";
            if (runtime != null)
                runtime.Reset();
        }
        private void btn_run_Click(object sender, EventArgs e)
        {
            Run();
        }
        private void btn_stop_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void btn_oneRun_Click(object sender, EventArgs e)
        {
            if (runtime != null)
                if (!runtime.IsRun)
                    runtime.OneRun();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
