namespace Remote
{
    partial class MainForm
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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Connected Clients", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Disconnected Clients", System.Windows.Forms.HorizontalAlignment.Left);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lv_Clients = new System.Windows.Forms.ListView();
            this.Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SessionID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MachineID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenu_sessionRC = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SendCmdMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SendFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DisconnectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl_main = new System.Windows.Forms.TabControl();
            this.tab_clients = new System.Windows.Forms.TabPage();
            this.tab_fileTransfer = new System.Windows.Forms.TabPage();
            this.lv_FileTransfer = new System.Windows.Forms.ListView();
            this.Empty = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Direction = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PercentComplete = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gb_Log = new System.Windows.Forms.GroupBox();
            this.lbox_log = new System.Windows.Forms.ListBox();
            this.panel_settings = new System.Windows.Forms.Panel();
            this.checkBox_EnforceID = new System.Windows.Forms.CheckBox();
            this.gb_broadcast = new System.Windows.Forms.GroupBox();
            this.combo_command = new System.Windows.Forms.ComboBox();
            this.button_SendCmd = new System.Windows.Forms.Button();
            this.panel_commands = new System.Windows.Forms.Panel();
            this.button_Help = new System.Windows.Forms.Button();
            this.button_start = new System.Windows.Forms.Button();
            this.button_clearLog = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel_status = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.contextMenu_sessionRC.SuspendLayout();
            this.tabControl_main.SuspendLayout();
            this.tab_clients.SuspendLayout();
            this.tab_fileTransfer.SuspendLayout();
            this.gb_Log.SuspendLayout();
            this.panel_settings.SuspendLayout();
            this.gb_broadcast.SuspendLayout();
            this.panel_commands.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lv_Clients
            // 
            this.lv_Clients.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lv_Clients.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Status,
            this.SessionID,
            this.MachineID});
            this.lv_Clients.ContextMenuStrip = this.contextMenu_sessionRC;
            this.lv_Clients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv_Clients.ForeColor = System.Drawing.SystemColors.WindowText;
            listViewGroup1.Header = "Connected Clients";
            listViewGroup1.Name = "ConnectedClients";
            listViewGroup2.Header = "Disconnected Clients";
            listViewGroup2.Name = "DisconnectedClients";
            this.lv_Clients.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
            this.lv_Clients.Location = new System.Drawing.Point(3, 3);
            this.lv_Clients.Name = "lv_Clients";
            this.lv_Clients.Size = new System.Drawing.Size(474, 293);
            this.lv_Clients.TabIndex = 0;
            this.lv_Clients.UseCompatibleStateImageBehavior = false;
            this.lv_Clients.View = System.Windows.Forms.View.Details;
            this.lv_Clients.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lv_Clients_ColumnClick);
            // 
            // Status
            // 
            this.Status.Text = "Status";
            this.Status.Width = 121;
            // 
            // SessionID
            // 
            this.SessionID.Text = "Session ID";
            this.SessionID.Width = 63;
            // 
            // MachineID
            // 
            this.MachineID.Text = "Machine ID";
            this.MachineID.Width = 128;
            // 
            // contextMenu_sessionRC
            // 
            this.contextMenu_sessionRC.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SendCmdMenuItem,
            this.SendFileMenuItem,
            this.DisconnectMenuItem});
            this.contextMenu_sessionRC.Name = "contextMenu_sessionRC";
            this.contextMenu_sessionRC.Size = new System.Drawing.Size(160, 70);
            // 
            // SendCmdMenuItem
            // 
            this.SendCmdMenuItem.Name = "SendCmdMenuItem";
            this.SendCmdMenuItem.Size = new System.Drawing.Size(159, 22);
            this.SendCmdMenuItem.Text = "Wyślij polecenie";
            this.SendCmdMenuItem.Click += new System.EventHandler(this.SendCmdMenuItem_Click);
            // 
            // SendFileMenuItem
            // 
            this.SendFileMenuItem.Name = "SendFileMenuItem";
            this.SendFileMenuItem.Size = new System.Drawing.Size(159, 22);
            this.SendFileMenuItem.Text = "Wyślij plik";
            this.SendFileMenuItem.Click += new System.EventHandler(this.SendFileMenuItem_Click);
            // 
            // DisconnectMenuItem
            // 
            this.DisconnectMenuItem.Name = "DisconnectMenuItem";
            this.DisconnectMenuItem.Size = new System.Drawing.Size(159, 22);
            this.DisconnectMenuItem.Text = "Rozłącz";
            this.DisconnectMenuItem.Click += new System.EventHandler(this.DisconnectMenuItem_Click);
            // 
            // tabControl_main
            // 
            this.tabControl_main.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl_main.Controls.Add(this.tab_clients);
            this.tabControl_main.Controls.Add(this.tab_fileTransfer);
            this.tabControl_main.Location = new System.Drawing.Point(1, 2);
            this.tabControl_main.Name = "tabControl_main";
            this.tabControl_main.SelectedIndex = 0;
            this.tabControl_main.Size = new System.Drawing.Size(488, 325);
            this.tabControl_main.TabIndex = 1;
            // 
            // tab_clients
            // 
            this.tab_clients.Controls.Add(this.lv_Clients);
            this.tab_clients.Location = new System.Drawing.Point(4, 22);
            this.tab_clients.Name = "tab_clients";
            this.tab_clients.Padding = new System.Windows.Forms.Padding(3);
            this.tab_clients.Size = new System.Drawing.Size(480, 299);
            this.tab_clients.TabIndex = 0;
            this.tab_clients.Text = "Połączone urządzenia";
            this.tab_clients.UseVisualStyleBackColor = true;
            // 
            // tab_fileTransfer
            // 
            this.tab_fileTransfer.Controls.Add(this.lv_FileTransfer);
            this.tab_fileTransfer.Location = new System.Drawing.Point(4, 22);
            this.tab_fileTransfer.Name = "tab_fileTransfer";
            this.tab_fileTransfer.Size = new System.Drawing.Size(480, 299);
            this.tab_fileTransfer.TabIndex = 1;
            this.tab_fileTransfer.Text = "Transfer plików";
            this.tab_fileTransfer.UseVisualStyleBackColor = true;
            // 
            // lv_FileTransfer
            // 
            this.lv_FileTransfer.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Empty,
            this.FileName,
            this.Direction,
            this.PercentComplete});
            this.lv_FileTransfer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv_FileTransfer.Location = new System.Drawing.Point(0, 0);
            this.lv_FileTransfer.Name = "lv_FileTransfer";
            this.lv_FileTransfer.Size = new System.Drawing.Size(480, 299);
            this.lv_FileTransfer.TabIndex = 0;
            this.lv_FileTransfer.UseCompatibleStateImageBehavior = false;
            this.lv_FileTransfer.View = System.Windows.Forms.View.Details;
            this.lv_FileTransfer.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lv_FileTransfer_ColumnClick);
            // 
            // Empty
            // 
            this.Empty.Text = "ID";
            this.Empty.Width = 30;
            // 
            // FileName
            // 
            this.FileName.Text = "Nazwa pliku";
            this.FileName.Width = 150;
            // 
            // Direction
            // 
            this.Direction.Text = "Kierunek";
            // 
            // PercentComplete
            // 
            this.PercentComplete.Text = "% Zakończone";
            this.PercentComplete.Width = 90;
            // 
            // gb_Log
            // 
            this.gb_Log.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_Log.Controls.Add(this.lbox_log);
            this.gb_Log.ForeColor = System.Drawing.SystemColors.Info;
            this.gb_Log.Location = new System.Drawing.Point(1, 189);
            this.gb_Log.Name = "gb_Log";
            this.gb_Log.Size = new System.Drawing.Size(484, 222);
            this.gb_Log.TabIndex = 2;
            this.gb_Log.TabStop = false;
            this.gb_Log.Text = "Log serwera";
            // 
            // lbox_log
            // 
            this.lbox_log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbox_log.FormattingEnabled = true;
            this.lbox_log.HorizontalScrollbar = true;
            this.lbox_log.Location = new System.Drawing.Point(3, 16);
            this.lbox_log.Name = "lbox_log";
            this.lbox_log.Size = new System.Drawing.Size(478, 203);
            this.lbox_log.TabIndex = 0;
            // 
            // panel_settings
            // 
            this.panel_settings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_settings.Controls.Add(this.checkBox_EnforceID);
            this.panel_settings.Location = new System.Drawing.Point(4, 418);
            this.panel_settings.Name = "panel_settings";
            this.panel_settings.Size = new System.Drawing.Size(478, 24);
            this.panel_settings.TabIndex = 0;
            // 
            // checkBox_EnforceID
            // 
            this.checkBox_EnforceID.AutoSize = true;
            this.checkBox_EnforceID.ForeColor = System.Drawing.SystemColors.Info;
            this.checkBox_EnforceID.Location = new System.Drawing.Point(8, 3);
            this.checkBox_EnforceID.Name = "checkBox_EnforceID";
            this.checkBox_EnforceID.Size = new System.Drawing.Size(183, 17);
            this.checkBox_EnforceID.TabIndex = 0;
            this.checkBox_EnforceID.Text = "Wymagaj unikalnego Machine ID";
            this.checkBox_EnforceID.UseVisualStyleBackColor = true;
            // 
            // gb_broadcast
            // 
            this.gb_broadcast.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_broadcast.Controls.Add(this.combo_command);
            this.gb_broadcast.Controls.Add(this.button_SendCmd);
            this.gb_broadcast.ForeColor = System.Drawing.SystemColors.Info;
            this.gb_broadcast.Location = new System.Drawing.Point(4, 448);
            this.gb_broadcast.Name = "gb_broadcast";
            this.gb_broadcast.Size = new System.Drawing.Size(478, 76);
            this.gb_broadcast.TabIndex = 3;
            this.gb_broadcast.TabStop = false;
            this.gb_broadcast.Text = "Polecenia grupowe (wysyłane do wszystkich połączonych urządzeń)";
            // 
            // combo_command
            // 
            this.combo_command.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.combo_command.FormattingEnabled = true;
            this.combo_command.Location = new System.Drawing.Point(8, 30);
            this.combo_command.Name = "combo_command";
            this.combo_command.Size = new System.Drawing.Size(383, 21);
            this.combo_command.TabIndex = 2;
            this.combo_command.Click += new System.EventHandler(this.combo_command_Click);
            // 
            // button_SendCmd
            // 
            this.button_SendCmd.ForeColor = System.Drawing.SystemColors.WindowText;
            this.button_SendCmd.Location = new System.Drawing.Point(397, 30);
            this.button_SendCmd.Name = "button_SendCmd";
            this.button_SendCmd.Size = new System.Drawing.Size(75, 23);
            this.button_SendCmd.TabIndex = 1;
            this.button_SendCmd.Text = "Wyślij";
            this.button_SendCmd.UseVisualStyleBackColor = true;
            this.button_SendCmd.Click += new System.EventHandler(this.button_SendCmd_Click);
            // 
            // panel_commands
            // 
            this.panel_commands.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_commands.Controls.Add(this.button_Help);
            this.panel_commands.Controls.Add(this.button_start);
            this.panel_commands.Controls.Add(this.button_clearLog);
            this.panel_commands.Location = new System.Drawing.Point(4, 530);
            this.panel_commands.Name = "panel_commands";
            this.panel_commands.Size = new System.Drawing.Size(478, 65);
            this.panel_commands.TabIndex = 4;
            // 
            // button_Help
            // 
            this.button_Help.Location = new System.Drawing.Point(8, 12);
            this.button_Help.Name = "button_Help";
            this.button_Help.Size = new System.Drawing.Size(150, 30);
            this.button_Help.TabIndex = 2;
            this.button_Help.Text = "Lista komend";
            this.button_Help.UseVisualStyleBackColor = true;
            this.button_Help.Click += new System.EventHandler(this.button_Help_Click);
            // 
            // button_start
            // 
            this.button_start.Location = new System.Drawing.Point(320, 12);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(150, 30);
            this.button_start.TabIndex = 1;
            this.button_start.Text = "Uruchom serwer";
            this.button_start.UseVisualStyleBackColor = true;
            this.button_start.Click += new System.EventHandler(this.button_start_Click);
            // 
            // button_clearLog
            // 
            this.button_clearLog.Location = new System.Drawing.Point(164, 12);
            this.button_clearLog.Name = "button_clearLog";
            this.button_clearLog.Size = new System.Drawing.Size(150, 30);
            this.button_clearLog.TabIndex = 0;
            this.button_clearLog.Text = "Wyczyść logi serwera";
            this.button_clearLog.UseVisualStyleBackColor = true;
            this.button_clearLog.Click += new System.EventHandler(this.button_clearLog_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.statusStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel_status});
            this.statusStrip1.Location = new System.Drawing.Point(0, 594);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(486, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip_status";
            // 
            // StatusLabel_status
            // 
            this.StatusLabel_status.ForeColor = System.Drawing.SystemColors.Info;
            this.StatusLabel_status.Name = "StatusLabel_status";
            this.StatusLabel_status.Size = new System.Drawing.Size(39, 17);
            this.StatusLabel_status.Text = "Status";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HotTrack;
            this.ClientSize = new System.Drawing.Size(486, 616);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel_commands);
            this.Controls.Add(this.gb_broadcast);
            this.Controls.Add(this.panel_settings);
            this.Controls.Add(this.gb_Log);
            this.Controls.Add(this.tabControl_main);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Klient zdalnego sterowania by Marcin Drabek";
            this.contextMenu_sessionRC.ResumeLayout(false);
            this.tabControl_main.ResumeLayout(false);
            this.tab_clients.ResumeLayout(false);
            this.tab_fileTransfer.ResumeLayout(false);
            this.gb_Log.ResumeLayout(false);
            this.panel_settings.ResumeLayout(false);
            this.panel_settings.PerformLayout();
            this.gb_broadcast.ResumeLayout(false);
            this.panel_commands.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ListView lv_Clients;
        internal System.Windows.Forms.ColumnHeader Status;
        internal System.Windows.Forms.ColumnHeader SessionID;
        internal System.Windows.Forms.ColumnHeader MachineID;
        private System.Windows.Forms.TabControl tabControl_main;
        private System.Windows.Forms.TabPage tab_clients;
        private System.Windows.Forms.GroupBox gb_Log;
        private System.Windows.Forms.Panel panel_settings;
        private System.Windows.Forms.ListBox lbox_log;
        private System.Windows.Forms.CheckBox checkBox_EnforceID;
        private System.Windows.Forms.GroupBox gb_broadcast;
        private System.Windows.Forms.Button button_SendCmd;
        private System.Windows.Forms.Panel panel_commands;
        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.Button button_clearLog;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel_status;
        private System.Windows.Forms.ContextMenuStrip contextMenu_sessionRC;
        private System.Windows.Forms.ToolStripMenuItem SendCmdMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SendFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DisconnectMenuItem;
        private System.Windows.Forms.TabPage tab_fileTransfer;
        private System.Windows.Forms.ListView lv_FileTransfer;
        private System.Windows.Forms.ColumnHeader Empty;
        private System.Windows.Forms.ColumnHeader FileName;
        private System.Windows.Forms.ColumnHeader Direction;
        private System.Windows.Forms.ColumnHeader PercentComplete;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button_Help;
        private System.Windows.Forms.ComboBox combo_command;
    }
}

