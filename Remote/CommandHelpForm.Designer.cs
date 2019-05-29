namespace Remote
{
    partial class CommandHelpForm
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
            this.lv_commands = new System.Windows.Forms.ListView();
            this.column_Command = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_Desc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_Syntax = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItem_copy = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // lv_commands
            // 
            this.lv_commands.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.column_Command,
            this.column_Desc,
            this.column_Syntax});
            this.lv_commands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv_commands.Location = new System.Drawing.Point(0, 0);
            this.lv_commands.Margin = new System.Windows.Forms.Padding(4);
            this.lv_commands.Name = "lv_commands";
            this.lv_commands.Size = new System.Drawing.Size(791, 321);
            this.lv_commands.TabIndex = 0;
            this.lv_commands.UseCompatibleStateImageBehavior = false;
            this.lv_commands.View = System.Windows.Forms.View.Details;
            this.lv_commands.DoubleClick += new System.EventHandler(this.menuItem_copy_Click);
            // 
            // column_Command
            // 
            this.column_Command.Text = "Komenda";
            this.column_Command.Width = 90;
            // 
            // column_Desc
            // 
            this.column_Desc.Text = "Opis";
            this.column_Desc.Width = 250;
            // 
            // column_Syntax
            // 
            this.column_Syntax.Text = "Przykład";
            this.column_Syntax.Width = 200;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_copy});
            this.contextMenuStrip.Name = "contextMenuStrip1";
            this.contextMenuStrip.Size = new System.Drawing.Size(122, 28);
            // 
            // menuItem_copy
            // 
            this.menuItem_copy.Name = "menuItem_copy";
            this.menuItem_copy.Size = new System.Drawing.Size(121, 24);
            this.menuItem_copy.Text = "Kopiuj";
            this.menuItem_copy.Click += new System.EventHandler(this.menuItem_copy_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip.Location = new System.Drawing.Point(0, 295);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip.Size = new System.Drawing.Size(791, 26);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(299, 20);
            this.toolStripStatusLabel1.Text = "Kliknij dwa razy, aby skopiować do schowka";
            // 
            // CommandHelpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(791, 321);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.lv_commands);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CommandHelpForm";
            this.Text = "Lista poleceń";
            this.contextMenuStrip.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lv_commands;
        private System.Windows.Forms.ColumnHeader column_Command;
        private System.Windows.Forms.ColumnHeader column_Desc;
        private System.Windows.Forms.ColumnHeader column_Syntax;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuItem_copy;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}