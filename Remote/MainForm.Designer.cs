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
            this.lv_clients = new System.Windows.Forms.ListView();
            this.Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SessionID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MachineID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenu_sessionRC = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItem_sendCommand = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_sendFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_disconnect = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl_main = new System.Windows.Forms.TabControl();
            this.tab_clients = new System.Windows.Forms.TabPage();
            this.tab_fileTransfer = new System.Windows.Forms.TabPage();
            this.lv_fileTransfer = new System.Windows.Forms.ListView();
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
            this.button_SendCommand = new System.Windows.Forms.Button();
            this.panel_commands = new System.Windows.Forms.Panel();
            this.button_help = new System.Windows.Forms.Button();
            this.button_startServer = new System.Windows.Forms.Button();
            this.button_clearLog = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel_status = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.contextMenu_sessionRC.SuspendLayout();
            this.tabControl_main.SuspendLayout();
            this.tab_clients.SuspendLayout();
            this.tab_fileTransfer.SuspendLayout();
            this.gb_Log.SuspendLayout();
            this.panel_settings.SuspendLayout();
            this.gb_broadcast.SuspendLayout();
            this.panel_commands.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // lv_clients
            // 
            this.lv_clients.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lv_clients.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Status,
            this.SessionID,
            this.MachineID});
            this.lv_clients.ContextMenuStrip = this.contextMenu_sessionRC;
            this.lv_clients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv_clients.ForeColor = System.Drawing.SystemColors.WindowText;
            listViewGroup1.Header = "Connected Clients";
            listViewGroup1.Name = "ConnectedClients";
            listViewGroup2.Header = "Disconnected Clients";
            listViewGroup2.Name = "DisconnectedClients";
            this.lv_clients.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
            this.lv_clients.Location = new System.Drawing.Point(4, 4);
            this.lv_clients.Margin = new System.Windows.Forms.Padding(4);
            this.lv_clients.Name = "lv_clients";
            this.lv_clients.Size = new System.Drawing.Size(635, 363);
            this.lv_clients.TabIndex = 0;
            this.lv_clients.UseCompatibleStateImageBehavior = false;
            this.lv_clients.View = System.Windows.Forms.View.Details;
            this.lv_clients.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lv_clients_ColumnClick);
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
            this.contextMenu_sessionRC.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenu_sessionRC.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_sendCommand,
            this.menuItem_sendFile,
            this.menuItem_disconnect});
            this.contextMenu_sessionRC.Name = "contextMenu_sessionRC";
            this.contextMenu_sessionRC.Size = new System.Drawing.Size(187, 76);
            // 
            // menuItem_sendCommand
            // 
            this.menuItem_sendCommand.Name = "menuItem_sendCommand";
            this.menuItem_sendCommand.Size = new System.Drawing.Size(186, 24);
            this.menuItem_sendCommand.Text = "Wyślij polecenie";
            this.menuItem_sendCommand.Click += new System.EventHandler(this.menuItem_sendCommand_Click);
            // 
            // menuItem_sendFile
            // 
            this.menuItem_sendFile.Name = "menuItem_sendFile";
            this.menuItem_sendFile.Size = new System.Drawing.Size(186, 24);
            this.menuItem_sendFile.Text = "Wyślij plik";
            this.menuItem_sendFile.Click += new System.EventHandler(this.menuItem_sendFile_Click);
            // 
            // menuItem_disconnect
            // 
            this.menuItem_disconnect.Name = "menuItem_disconnect";
            this.menuItem_disconnect.Size = new System.Drawing.Size(186, 24);
            this.menuItem_disconnect.Text = "Rozłącz";
            this.menuItem_disconnect.Click += new System.EventHandler(this.menuItem_disconnect_Click);
            // 
            // tabControl_main
            // 
            this.tabControl_main.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl_main.Controls.Add(this.tab_clients);
            this.tabControl_main.Controls.Add(this.tab_fileTransfer);
            this.tabControl_main.Location = new System.Drawing.Point(1, 2);
            this.tabControl_main.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl_main.Name = "tabControl_main";
            this.tabControl_main.SelectedIndex = 0;
            this.tabControl_main.Size = new System.Drawing.Size(651, 400);
            this.tabControl_main.TabIndex = 1;
            // 
            // tab_clients
            // 
            this.tab_clients.Controls.Add(this.lv_clients);
            this.tab_clients.Location = new System.Drawing.Point(4, 25);
            this.tab_clients.Margin = new System.Windows.Forms.Padding(4);
            this.tab_clients.Name = "tab_clients";
            this.tab_clients.Padding = new System.Windows.Forms.Padding(4);
            this.tab_clients.Size = new System.Drawing.Size(643, 371);
            this.tab_clients.TabIndex = 0;
            this.tab_clients.Text = "Połączone urządzenia";
            this.tab_clients.UseVisualStyleBackColor = true;
            // 
            // tab_fileTransfer
            // 
            this.tab_fileTransfer.Controls.Add(this.lv_fileTransfer);
            this.tab_fileTransfer.Location = new System.Drawing.Point(4, 25);
            this.tab_fileTransfer.Margin = new System.Windows.Forms.Padding(4);
            this.tab_fileTransfer.Name = "tab_fileTransfer";
            this.tab_fileTransfer.Size = new System.Drawing.Size(643, 371);
            this.tab_fileTransfer.TabIndex = 1;
            this.tab_fileTransfer.Text = "Transfer plików";
            this.tab_fileTransfer.UseVisualStyleBackColor = true;
            // 
            // lv_fileTransfer
            // 
            this.lv_fileTransfer.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Empty,
            this.FileName,
            this.Direction,
            this.PercentComplete});
            this.lv_fileTransfer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv_fileTransfer.Location = new System.Drawing.Point(0, 0);
            this.lv_fileTransfer.Margin = new System.Windows.Forms.Padding(4);
            this.lv_fileTransfer.Name = "lv_fileTransfer";
            this.lv_fileTransfer.Size = new System.Drawing.Size(643, 371);
            this.lv_fileTransfer.TabIndex = 0;
            this.lv_fileTransfer.UseCompatibleStateImageBehavior = false;
            this.lv_fileTransfer.View = System.Windows.Forms.View.Details;
            this.lv_fileTransfer.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lv_fileTransfer_ColumnClick);
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
            this.gb_Log.Location = new System.Drawing.Point(1, 233);
            this.gb_Log.Margin = new System.Windows.Forms.Padding(4);
            this.gb_Log.Name = "gb_Log";
            this.gb_Log.Padding = new System.Windows.Forms.Padding(4);
            this.gb_Log.Size = new System.Drawing.Size(645, 273);
            this.gb_Log.TabIndex = 2;
            this.gb_Log.TabStop = false;
            this.gb_Log.Text = "Log serwera";
            // 
            // lbox_log
            // 
            this.lbox_log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbox_log.FormattingEnabled = true;
            this.lbox_log.HorizontalScrollbar = true;
            this.lbox_log.ItemHeight = 16;
            this.lbox_log.Location = new System.Drawing.Point(4, 19);
            this.lbox_log.Margin = new System.Windows.Forms.Padding(4);
            this.lbox_log.Name = "lbox_log";
            this.lbox_log.Size = new System.Drawing.Size(637, 250);
            this.lbox_log.TabIndex = 0;
            // 
            // panel_settings
            // 
            this.panel_settings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_settings.Controls.Add(this.checkBox_EnforceID);
            this.panel_settings.Location = new System.Drawing.Point(5, 514);
            this.panel_settings.Margin = new System.Windows.Forms.Padding(4);
            this.panel_settings.Name = "panel_settings";
            this.panel_settings.Size = new System.Drawing.Size(637, 30);
            this.panel_settings.TabIndex = 0;
            // 
            // checkBox_EnforceID
            // 
            this.checkBox_EnforceID.AutoSize = true;
            this.checkBox_EnforceID.ForeColor = System.Drawing.SystemColors.Info;
            this.checkBox_EnforceID.Location = new System.Drawing.Point(11, 4);
            this.checkBox_EnforceID.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox_EnforceID.Name = "checkBox_EnforceID";
            this.checkBox_EnforceID.Size = new System.Drawing.Size(235, 21);
            this.checkBox_EnforceID.TabIndex = 0;
            this.checkBox_EnforceID.Text = "Wymagaj unikalnego Machine ID";
            this.checkBox_EnforceID.UseVisualStyleBackColor = true;
            // 
            // gb_broadcast
            // 
            this.gb_broadcast.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_broadcast.Controls.Add(this.combo_command);
            this.gb_broadcast.Controls.Add(this.button_SendCommand);
            this.gb_broadcast.ForeColor = System.Drawing.SystemColors.Info;
            this.gb_broadcast.Location = new System.Drawing.Point(5, 551);
            this.gb_broadcast.Margin = new System.Windows.Forms.Padding(4);
            this.gb_broadcast.Name = "gb_broadcast";
            this.gb_broadcast.Padding = new System.Windows.Forms.Padding(4);
            this.gb_broadcast.Size = new System.Drawing.Size(637, 94);
            this.gb_broadcast.TabIndex = 3;
            this.gb_broadcast.TabStop = false;
            this.gb_broadcast.Text = "Polecenia grupowe (wysyłane do wszystkich połączonych urządzeń)";
            // 
            // combo_command
            // 
            this.combo_command.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.combo_command.FormattingEnabled = true;
            this.combo_command.Location = new System.Drawing.Point(11, 37);
            this.combo_command.Margin = new System.Windows.Forms.Padding(4);
            this.combo_command.Name = "combo_command";
            this.combo_command.Size = new System.Drawing.Size(509, 24);
            this.combo_command.TabIndex = 2;
            this.combo_command.Click += new System.EventHandler(this.combo_command_Click);
            // 
            // button_SendCommand
            // 
            this.button_SendCommand.ForeColor = System.Drawing.SystemColors.WindowText;
            this.button_SendCommand.Location = new System.Drawing.Point(529, 37);
            this.button_SendCommand.Margin = new System.Windows.Forms.Padding(4);
            this.button_SendCommand.Name = "button_SendCommand";
            this.button_SendCommand.Size = new System.Drawing.Size(100, 28);
            this.button_SendCommand.TabIndex = 1;
            this.button_SendCommand.Text = "Wyślij";
            this.button_SendCommand.UseVisualStyleBackColor = true;
            this.button_SendCommand.Click += new System.EventHandler(this.button_sendCommand_Click);
            // 
            // panel_commands
            // 
            this.panel_commands.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_commands.Controls.Add(this.button_help);
            this.panel_commands.Controls.Add(this.button_startServer);
            this.panel_commands.Controls.Add(this.button_clearLog);
            this.panel_commands.Location = new System.Drawing.Point(5, 652);
            this.panel_commands.Margin = new System.Windows.Forms.Padding(4);
            this.panel_commands.Name = "panel_commands";
            this.panel_commands.Size = new System.Drawing.Size(637, 80);
            this.panel_commands.TabIndex = 4;
            // 
            // button_help
            // 
            this.button_help.Location = new System.Drawing.Point(11, 15);
            this.button_help.Margin = new System.Windows.Forms.Padding(4);
            this.button_help.Name = "button_help";
            this.button_help.Size = new System.Drawing.Size(200, 37);
            this.button_help.TabIndex = 2;
            this.button_help.Text = "Lista komend";
            this.button_help.UseVisualStyleBackColor = true;
            this.button_help.Click += new System.EventHandler(this.button_help_Click);
            // 
            // button_startServer
            // 
            this.button_startServer.Location = new System.Drawing.Point(427, 15);
            this.button_startServer.Margin = new System.Windows.Forms.Padding(4);
            this.button_startServer.Name = "button_startServer";
            this.button_startServer.Size = new System.Drawing.Size(200, 37);
            this.button_startServer.TabIndex = 1;
            this.button_startServer.Text = "Uruchom serwer";
            this.button_startServer.UseVisualStyleBackColor = true;
            this.button_startServer.Click += new System.EventHandler(this.button_start_Click);
            // 
            // button_clearLog
            // 
            this.button_clearLog.Location = new System.Drawing.Point(219, 15);
            this.button_clearLog.Margin = new System.Windows.Forms.Padding(4);
            this.button_clearLog.Name = "button_clearLog";
            this.button_clearLog.Size = new System.Drawing.Size(200, 37);
            this.button_clearLog.TabIndex = 0;
            this.button_clearLog.Text = "Wyczyść logi serwera";
            this.button_clearLog.UseVisualStyleBackColor = true;
            this.button_clearLog.Click += new System.EventHandler(this.button_clearLog_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.BackColor = System.Drawing.SystemColors.HotTrack;
            this.statusStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel_status});
            this.statusStrip.Location = new System.Drawing.Point(0, 732);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip.Size = new System.Drawing.Size(648, 26);
            this.statusStrip.TabIndex = 5;
            this.statusStrip.Text = "statusStrip_status";
            // 
            // statusLabel_status
            // 
            this.statusLabel_status.ForeColor = System.Drawing.SystemColors.Info;
            this.statusLabel_status.Name = "statusLabel_status";
            this.statusLabel_status.Size = new System.Drawing.Size(49, 20);
            this.statusLabel_status.Text = "Status";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HotTrack;
            this.ClientSize = new System.Drawing.Size(648, 758);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.panel_commands);
            this.Controls.Add(this.gb_broadcast);
            this.Controls.Add(this.panel_settings);
            this.Controls.Add(this.gb_Log);
            this.Controls.Add(this.tabControl_main);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
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
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ListView lv_clients;
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
        private System.Windows.Forms.Button button_SendCommand;
        private System.Windows.Forms.Panel panel_commands;
        private System.Windows.Forms.Button button_startServer;
        private System.Windows.Forms.Button button_clearLog;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel_status;
        private System.Windows.Forms.ContextMenuStrip contextMenu_sessionRC;
        private System.Windows.Forms.ToolStripMenuItem menuItem_sendCommand;
        private System.Windows.Forms.ToolStripMenuItem menuItem_sendFile;
        private System.Windows.Forms.ToolStripMenuItem menuItem_disconnect;
        private System.Windows.Forms.TabPage tab_fileTransfer;
        private System.Windows.Forms.ListView lv_fileTransfer;
        private System.Windows.Forms.ColumnHeader Empty;
        private System.Windows.Forms.ColumnHeader FileName;
        private System.Windows.Forms.ColumnHeader Direction;
        private System.Windows.Forms.ColumnHeader PercentComplete;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button button_help;
        private System.Windows.Forms.ComboBox combo_command;
    }
}

