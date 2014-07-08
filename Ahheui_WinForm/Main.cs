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
        public Main()
        {
            runtime = new Runtime("", this, Endded, Endding, Test);
            InitializeComponent();
        }
        #region IConsole구현
        void IConsole.Output(string output)
        {
            if (output != "\r\n" && output == "\n" && output == "\r")
                output = "\r\n";
            txtBox_outputBox.Invoke(new Action(() =>
            {
                txtBox_outputBox.AppendText(output);
            }));
        }

        string IConsole.Input()
        {
            var input = new Input();
            if (input.ShowDialog() == DialogResult.OK)
            {
                return input.txtbox_char.Text;
            }
            else
            {
                throw new Exception();
            }
        }
        #endregion
        void Endding(object sender, EventArgs e)
        {
            MessageBox.Show("실행 완료", "완료");
        }
        void Endded(object sender, EventArgs e)
        {
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
        private void btn_run_Click(object sender, EventArgs e)
        {
            var boxScript = this.txtBox_scriptBox.Text.Replace("\r","");
            if (activatedScript != boxScript)
            {
                activatedScript = boxScript;
                runtime = new Runtime(boxScript, this, Endded, Endding, Test);
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
                    runtime.OneRun();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            this.txtBox_outputBox.Text = "";
            if (runtime != null)
                runtime.Reset();
        }
    }
}
