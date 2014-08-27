namespace SteamB23.Ahheui_WinForm
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_clear = new System.Windows.Forms.Button();
            this.chkBox_debug = new System.Windows.Forms.CheckBox();
            this.btn_run = new System.Windows.Forms.Button();
            this.btn_oneRun = new System.Windows.Forms.Button();
            this.btn_stop = new System.Windows.Forms.Button();
            this.txtBox_scriptBox = new System.Windows.Forms.TextBox();
            this.txtBox_outputBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_clear
            // 
            this.btn_clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_clear.Location = new System.Drawing.Point(254, 330);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(75, 23);
            this.btn_clear.TabIndex = 1;
            this.btn_clear.Text = "초기화";
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // chkBox_debug
            // 
            this.chkBox_debug.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkBox_debug.AutoSize = true;
            this.chkBox_debug.Location = new System.Drawing.Point(12, 333);
            this.chkBox_debug.Name = "chkBox_debug";
            this.chkBox_debug.Size = new System.Drawing.Size(62, 19);
            this.chkBox_debug.TabIndex = 2;
            this.chkBox_debug.Text = "디버그";
            this.chkBox_debug.UseVisualStyleBackColor = true;
            // 
            // btn_run
            // 
            this.btn_run.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_run.Location = new System.Drawing.Point(335, 330);
            this.btn_run.Name = "btn_run";
            this.btn_run.Size = new System.Drawing.Size(75, 23);
            this.btn_run.TabIndex = 3;
            this.btn_run.Text = "실행";
            this.btn_run.UseVisualStyleBackColor = true;
            this.btn_run.Click += new System.EventHandler(this.btn_run_Click);
            // 
            // btn_oneRun
            // 
            this.btn_oneRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_oneRun.Location = new System.Drawing.Point(416, 330);
            this.btn_oneRun.Name = "btn_oneRun";
            this.btn_oneRun.Size = new System.Drawing.Size(75, 23);
            this.btn_oneRun.TabIndex = 4;
            this.btn_oneRun.Text = "한번 실행";
            this.btn_oneRun.UseVisualStyleBackColor = true;
            this.btn_oneRun.Click += new System.EventHandler(this.btn_oneRun_Click);
            // 
            // btn_stop
            // 
            this.btn_stop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_stop.Location = new System.Drawing.Point(497, 330);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(75, 23);
            this.btn_stop.TabIndex = 5;
            this.btn_stop.Text = "중지";
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // txtBox_scriptBox
            // 
            this.txtBox_scriptBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBox_scriptBox.Font = new System.Drawing.Font("굴림체", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtBox_scriptBox.Location = new System.Drawing.Point(12, 12);
            this.txtBox_scriptBox.Multiline = true;
            this.txtBox_scriptBox.Name = "txtBox_scriptBox";
            this.txtBox_scriptBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtBox_scriptBox.Size = new System.Drawing.Size(560, 312);
            this.txtBox_scriptBox.TabIndex = 6;
            this.txtBox_scriptBox.WordWrap = false;
            // 
            // txtBox_outputBox
            // 
            this.txtBox_outputBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBox_outputBox.Location = new System.Drawing.Point(12, 358);
            this.txtBox_outputBox.Multiline = true;
            this.txtBox_outputBox.Name = "txtBox_outputBox";
            this.txtBox_outputBox.ReadOnly = true;
            this.txtBox_outputBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtBox_outputBox.Size = new System.Drawing.Size(560, 114);
            this.txtBox_outputBox.TabIndex = 7;
            // 
            // Main
            // 
            this.AcceptButton = this.btn_run;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 484);
            this.Controls.Add(this.txtBox_outputBox);
            this.Controls.Add(this.txtBox_scriptBox);
            this.Controls.Add(this.btn_stop);
            this.Controls.Add(this.btn_oneRun);
            this.Controls.Add(this.btn_run);
            this.Controls.Add(this.chkBox_debug);
            this.Controls.Add(this.btn_clear);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "Main";
            this.Text = "아희희 인터프리터";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.CheckBox chkBox_debug;
        private System.Windows.Forms.Button btn_run;
        private System.Windows.Forms.Button btn_oneRun;
        private System.Windows.Forms.Button btn_stop;
        private System.Windows.Forms.TextBox txtBox_scriptBox;
        private System.Windows.Forms.TextBox txtBox_outputBox;
    }
}