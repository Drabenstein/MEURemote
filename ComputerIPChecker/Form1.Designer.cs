namespace ComputerIPChecker
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.btn_checkIP = new System.Windows.Forms.Button();
            this.tb_ip = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // btn_checkIP
            // 
            this.btn_checkIP.Location = new System.Drawing.Point(12, 12);
            this.btn_checkIP.Name = "btn_checkIP";
            this.btn_checkIP.Size = new System.Drawing.Size(312, 51);
            this.btn_checkIP.TabIndex = 0;
            this.btn_checkIP.Text = "Sprawdź IP";
            this.btn_checkIP.UseVisualStyleBackColor = true;
            this.btn_checkIP.Click += new System.EventHandler(this.btn_checkIP_Click);
            // 
            // tb_ip
            // 
            this.tb_ip.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tb_ip.Location = new System.Drawing.Point(12, 23);
            this.tb_ip.Name = "tb_ip";
            this.tb_ip.Size = new System.Drawing.Size(312, 26);
            this.tb_ip.TabIndex = 1;
            this.tb_ip.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_ip.Visible = false;
            this.tb_ip.DoubleClick += new System.EventHandler(this.tb_ip_DoubleClick);
            // 
            // toolTip1
            // 
            this.toolTip1.ShowAlways = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 75);
            this.Controls.Add(this.tb_ip);
            this.Controls.Add(this.btn_checkIP);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "Sprawdzanie adresu IP";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_checkIP;
        private System.Windows.Forms.TextBox tb_ip;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

