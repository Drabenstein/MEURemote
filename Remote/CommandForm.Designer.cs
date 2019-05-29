namespace Remote
{
    partial class CommandForm
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
            this.label_cmd = new System.Windows.Forms.Label();
            this.combo_command = new System.Windows.Forms.ComboBox();
            this.button_ok = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label_cmd
            // 
            this.label_cmd.AutoSize = true;
            this.label_cmd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label_cmd.Location = new System.Drawing.Point(16, 11);
            this.label_cmd.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_cmd.Name = "label_cmd";
            this.label_cmd.Size = new System.Drawing.Size(315, 18);
            this.label_cmd.TabIndex = 0;
            this.label_cmd.Text = "Wpisz polecenie do przesłania do klienta o ID: ";
            // 
            // combo_command
            // 
            this.combo_command.FormattingEnabled = true;
            this.combo_command.Location = new System.Drawing.Point(20, 92);
            this.combo_command.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.combo_command.Name = "combo_command";
            this.combo_command.Size = new System.Drawing.Size(395, 24);
            this.combo_command.TabIndex = 1;
            this.combo_command.Click += new System.EventHandler(this.combo_command_Click);
            this.combo_command.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.combo_command_KeyPress);
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(208, 142);
            this.button_ok.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(100, 28);
            this.button_ok.TabIndex = 2;
            this.button_ok.Text = "OK";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(316, 142);
            this.button_cancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(100, 28);
            this.button_cancel.TabIndex = 3;
            this.button_cancel.Text = "Anuluj";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // CommandForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 191);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.combo_command);
            this.Controls.Add(this.label_cmd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "CommandForm";
            this.Text = "Komenda do wysłania do: ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_cmd;
        private System.Windows.Forms.ComboBox combo_command;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Button button_cancel;
    }
}