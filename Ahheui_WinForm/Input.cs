using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SteamB23.Ahheui_WinForm
{
    public partial class Input : Form
    {
        string inputType;
        public Input(string inputType)
        {
            this.inputType = inputType;
            InitializeComponent();
            this.Text = string.Format("{0} - {1}", this.Text, inputType);
        }
    }
}
