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
        Runtime runtime;
        StringBuilder outputString = new StringBuilder();
        Backup backup = null;
        bool isReset = false;
        public Main()
            : base()
        {
            RuntimeInitialize("");
            InitializeComponent();
        }
        void RuntimeInitialize(string script)
        {
            runtime = new Runtime(script, this);
            runtime.Finishing += Endding;
            runtime.CallRun += Run;
            runtime.CallReset += Reset;
        }
        #region IConsole구현
        void IConsole.Output(string output)
        {
            if (output == "\n" || output == "\r")
                output = System.Environment.NewLine;
            if (!(isReset||this.IsDisposed))
            {
                txtBox_outputBox.Invoke(new Action(() =>
                {
                    txtBox_outputBox.AppendText(output);
                }));
            }
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
                            backup = runtime.Abort();
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
        // 실행시 backup이 있으면 복구후 삭제
        void Run(object sender, EventArgs e)
        {
            if (backup != null)
            {
                runtime.Restore(backup);
                backup = null;
            }
            
        }
        void Stop(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        void Abort(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        void Reset(object sender, EventArgs e)
        {
            txtBox_outputBox.Invoke(new Action(() =>
            {
                txtBox_outputBox.Text = "";
            }));
        }
        #endregion
        private void btn_run_Click(object sender, EventArgs e)
        {
            this.isReset = false;
            if (runtime.IsEnd)
                runtime.Reset();
            var boxScript = this.txtBox_scriptBox.Text.Replace("\r", "");
            if (runtime.Script != boxScript)
            {
                this.txtBox_outputBox.Text = "";
                RuntimeInitialize(boxScript);
            }
            if (runtime != null)
                runtime.Run();
        }
        private void btn_stop_Click(object sender, EventArgs e)
        {
            if (runtime != null)
                runtime.Stop();
        }

        private void btn_oneRun_Click(object sender, EventArgs e)
        {
            if (runtime != null)
                if (!runtime.IsRun)
                    runtime.OnceRun();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            runtime.Reset();
        }

        private void txtBox_scriptBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.T:
                        (sender as TextBox).Cut();
                        break;
                    case Keys.C:
                        (sender as TextBox).Copy();
                        break;
                    case Keys.A:
                        (sender as TextBox).SelectAll();
                        break;
                    case Keys.D:
                        if (!(sender as TextBox).ReadOnly)
                            (sender as TextBox).Text = "";
                        break;
                }
            }
        }
    }
}
