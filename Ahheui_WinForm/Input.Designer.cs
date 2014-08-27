namespace SteamB23.Ahheui_WinForm
{
    partial class Input
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
            this.txtbox_char = new System.Windows.Forms.TextBox();
            this.btn_ok = new System.Windows.Forms.Button();
            this.btn_abort = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtbox_char
            // 
            this.txtbox_char.Location = new System.Drawing.Point(12, 14);
            this.txtbox_char.MaxLength = 10000;
            this.txtbox_char.Name = "txtbox_char";
            this.txtbox_char.Size = new System.Drawing.Size(156, 21);
            this.txtbox_char.TabIndex = 0;
            // 
            // btn_ok
            // 
            this.btn_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_ok.Location = new System.Drawing.Point(12, 41);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(75, 23);
            this.btn_ok.TabIndex = 1;
            this.btn_ok.Text = "확인";
            this.btn_ok.UseVisualStyleBackColor = true;
            // 
            // btn_abort
            // 
            this.btn_abort.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.btn_abort.Location = new System.Drawing.Point(93, 41);
            this.btn_abort.Name = "btn_abort";
            this.btn_abort.Size = new System.Drawing.Size(75, 23);
            this.btn_abort.TabIndex = 2;
            this.btn_abort.Text = "정지";
            this.btn_abort.UseVisualStyleBackColor = true;
            // 
            // Input
            // 
            this.AcceptButton = this.btn_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_abort;
            this.ClientSize = new System.Drawing.Size(180, 76);
            this.ControlBox = false;
            this.Controls.Add(this.btn_abort);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.txtbox_char);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Input";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "입력";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_ok;
        public System.Windows.Forms.TextBox txtbox_char;
        private System.Windows.Forms.Button btn_abort;
    }
}