using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Remote
{
    public partial class MainForm : Form
    {
        private TcpComm.Server _server;
        private CommandHelpForm _helpForm;
        private bool _isHelpFormActive = false;
        private int _clientsSortColumn = -1;
        private int _filesSortColumn = -1;

        public MainForm()
        {
            InitializeComponent();
            this.statusLabel_status.Text = "Bezczynny";
            ImageList imgList = new ImageList();
            imgList.Images.Add("connected", Resources.user_available);
            imgList.Images.Add("disconnected", Resources.user_invisible);
            lv_clients.SmallImageList = imgList;
            List<string> commands = ServerCommands.GetCommandsList();
            foreach (string cmd in commands)
            {
                combo_command.Items.Add(cmd);
            }
        }

        public void UpdateUi(byte[] bytes, int sessionID, byte dataChannel)
        {
            if (dataChannel < 251)
            {
                UI(delegate () { this.lbox_log.Items.Add("Session " + sessionID + ": " + TcpComm.Utilities.BytesToString(bytes)); });
            }
            else if (dataChannel == 255)
            {
                string tmp = "";
                string message = TcpComm.Utilities.BytesToString(bytes);

                if (message.Length > 3)
                {
                    tmp = message.Substring(0, 3);
                }

                if (tmp == "UBS")
                {
                    string[] parts = message.Split(new string[] { "U", "B", "S", ":" }, StringSplitOptions.None);
                    message = "Dane zostały wysłane do sesji o ID: " + parts[1];
                }

                if (message == "Connected.")
                {
                    UI(UpdateClientsList);
                }

                if (message.Contains(" MachineID:"))
                {
                    UI(UpdateClientsList);
                }

                if (message.Contains("Session Stopped. ("))
                {
                    UI(UpdateClientsList);
                }

                UI(() => this.statusLabel_status.Text = message);
            }
        }

        private void UI(Action uiUpdate)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(uiUpdate));
                }
                else
                {
                    uiUpdate();
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void button_start_Click(object sender, EventArgs e)
        {
            if (button_startServer.Text == "Uruchom serwer")
            {
                _server = new TcpComm.Server(UpdateUi, enforceUniqueMachineId: checkBox_EnforceID.Checked);
                string errMsg = "";
                _server.Start(5010, ref errMsg);
                System.Threading.Thread ftm = new System.Threading.Thread(FileTransferMonitor);
                ftm.Start();
                button_startServer.Text = "Zatrzymaj serwer";
            }
            else
            {
                if (_server != null)
                {
                    _server.Close();
                    this.lv_clients.Items.Clear();
                    button_startServer.Text = "Uruchom serwer";
                }
            }
        }

        private void UpdateClientsList()
        {
            List<TcpComm.Server.SessionCommunications> sessionList = _server.GetSessionCollection();
            lv_clients.Items.Clear();

            foreach (TcpComm.Server.SessionCommunications session in sessionList)
            {
                ListViewItem clientListItem;
                if (session.IsRunning)
                {
                    clientListItem = new ListViewItem(" Connected", 0, lv_clients.Groups[0]);
                    clientListItem.ImageKey = "connected";
                }
                else
                {
                    clientListItem = new ListViewItem(" Disconnected", 1, lv_clients.Groups[1]);
                    clientListItem.ImageKey = "disconnected";
                }

                clientListItem.SubItems.Add(session.sessionID.ToString());
                clientListItem.SubItems.Add(session.machineId);
                this.lv_clients.Items.Add(clientListItem);
            }
        }

        private void FileTransferMonitor()
        {
            while (_server.IsRunning)
            {
                DateTime waitTimeout = DateTime.Now.AddMilliseconds(100);
                while (DateTime.Now < waitTimeout)
                {
                    System.Threading.Thread.Sleep(1);
                }

                List<TcpComm.Server.SessionCommunications> sessions = _server.GetSessionCollection();
                foreach (TcpComm.Server.SessionCommunications session in sessions)
                {
                    UpdateFileProgress(session);
                }
            }
        }

        private void UpdateFileProgress(TcpComm.Server.SessionCommunications session)
        {
            bool moveOn = false;

            if (session.SendingFile)
            {
                UI(() =>
                {
                    int itemSessionId = -1;
                    foreach (ListViewItem fileTransferListItem in lv_fileTransfer.Items)
                    {
                        Int32.TryParse(fileTransferListItem.SubItems[0].Text, out itemSessionId);
                        if (itemSessionId == session.sessionID && fileTransferListItem.SubItems[2].Text.Equals("Wychodzące"))
                        {
                            fileTransferListItem.SubItems[3].Text = session.GetPercentOfFileSent().ToString() + "%";
                            break;
                        }
                        else
                        {
                            itemSessionId = -1;
                        }
                    }

                    if (itemSessionId == -1)
                    {
                        ListViewItem progressListItem = new ListViewItem();
                        progressListItem.Text = session.sessionID.ToString();
                        progressListItem.SubItems.Add(System.IO.Path.GetFileName(session.OutgingFileName));
                        progressListItem.SubItems.Add("Wychodzące");
                        progressListItem.SubItems.Add(session.GetPercentOfFileSent().ToString());
                        lv_fileTransfer.Items.Add(progressListItem);
                    }

                    moveOn = true;
                });

                while (!moveOn)
                {
                    System.Threading.Thread.Sleep(100);
                }
            }
            else
            {
                UI(() =>
                {
                    foreach (ListViewItem fileTransferListItem in lv_fileTransfer.Items)
                    {
                        Int32.TryParse(fileTransferListItem.SubItems[0].Text, out int currentSessionId);
                        if (currentSessionId == session.sessionID && fileTransferListItem.SubItems[2].Text.Equals("Wychodzące"))
                        {
                            lv_fileTransfer.Items.Remove(fileTransferListItem);
                            break;
                        }
                    }
                });
            }
            
            if (session.ReceivingFile)
            {
                moveOn = false;
                UI(() =>
                {
                    int currentSessionId = -1;
                    foreach (ListViewItem transferListItem in lv_fileTransfer.Items)
                    {
                        Int32.TryParse(transferListItem.Text, out currentSessionId);
                        if (currentSessionId == session.sessionID && transferListItem.SubItems[2].Text.Equals("Przychodzące"))
                        {
                            transferListItem.SubItems[3].Text = session.GetPercentOfFileReceived().ToString() + "%";
                            break;
                        }
                        else
                        {
                            currentSessionId = -1;
                        }
                    }

                    ListViewItem transferStatusListItem = new ListViewItem();

                    if (currentSessionId == -1)
                    {
                        transferStatusListItem.Text = session.sessionID.ToString();
                        transferStatusListItem.SubItems.Add(System.IO.Path.GetFileName(session.IncomingFileName));
                        transferStatusListItem.SubItems.Add("Przychodzące");
                        transferStatusListItem.SubItems.Add(session.GetPercentOfFileReceived().ToString());
                        lv_fileTransfer.Items.Add(transferStatusListItem);
                    }

                    moveOn = true;
                });

                while (!moveOn)
                {
                    System.Threading.Thread.Sleep(1);
                }
            }
            else
            {
                UI(() =>
                {
                    foreach (ListViewItem tlvi in lv_fileTransfer.Items)
                    {
                        Int32.TryParse(tlvi.SubItems[0].Text, out int currentSessionId);
                        if (currentSessionId == session.sessionID && tlvi.SubItems[2].Text.Equals("Przychodzące"))
                        {
                            lv_fileTransfer.Items.Remove(tlvi);
                            break;
                        }
                    }
                });
            }
        }

        private void button_clearLog_Click(object sender, EventArgs e)
        {
            this.lbox_log.Items.Clear();
        }

        private void button_sendCommand_Click(object sender, EventArgs e)
        {
            if (this.combo_command.Text.Trim().Length > 0)
            {
                string errMsg = "";
                _server.SendText(this.combo_command.Text.Trim(), errMsg: ref errMsg);
            }
        }

        private void menuItem_sendFile_Click(object sender, EventArgs e)
        {
            if (lv_clients.SelectedItems.Count <= 0)
            {
                return;
            }

            ListViewItem selectedItem = lv_clients.SelectedItems[0];
            TcpComm.Server.SessionCommunications session = _server?.GetSession(Convert.ToInt32(selectedItem.SubItems[1].Text));
            if (session == null)
            {
                MessageBox.Show("Wybrany klient jest rozłączony.", "Klient rozłączony", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string message = "Wybierz plik do wysłania do: " + selectedItem.SubItems[2].Text;
            openFileDialog.Title = message;
            openFileDialog.FileName = "";
            openFileDialog.ShowDialog();
            string fileName = openFileDialog.FileName;

            if (fileName.Trim().Equals(""))
            {
                return;
            }

            if (!_server.SendFile(fileName, session.sessionID))
            {
                MessageBox.Show("Klient został rozłączony", "Klient rozłączony", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void menuItem_sendCommand_Click(object sender, EventArgs e)
        {
            if (lv_clients.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = lv_clients.SelectedItems[0];
                TcpComm.Server.SessionCommunications session = _server?.GetSession(Convert.ToInt32(selectedItem.SubItems[1].Text));

                if (session == null)
                {
                    MessageBox.Show("Klient jest rozłączony.", "Klient rozłączony", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                CommandForm cmdForm = new CommandForm(selectedItem.SubItems[2].Text);
                cmdForm.ShowDialog();
                string ret = cmdForm.Command;

                if (string.IsNullOrEmpty(ret))
                {
                    return;
                }

                string errMsg = string.Empty;
                _server.SendText(ret.ToString(), 1, session.sessionID, ref errMsg);
            }
        }

        private void menuItem_disconnect_Click(object sender, EventArgs e)
        {
            if (lv_clients.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = lv_clients.SelectedItems[0];
                TcpComm.Server.SessionCommunications session = _server?.GetSession(Convert.ToInt32(selectedItem.SubItems[1].Text));
                session?.Close();
            }
        }

        private void button_help_Click(object sender, EventArgs e)
        {
            if (_isHelpFormActive)
            {
                _helpForm.Close();
                _helpForm = null;
            }

            _helpForm = new CommandHelpForm();
            _helpForm.Show();
            _isHelpFormActive = true;
        }

        private void combo_command_Click(object sender, EventArgs e)
        {
            if (combo_command.Text.Contains(ServerCommands.SvrCheckprocess))
            {
                return;
            }
            else if (combo_command.Text.Contains(ServerCommands.SvrKillprocess))
            {
                return;
            }
            else if (combo_command.Text.Contains(ServerCommands.SvrRun))
            {
                return;
            }

            combo_command.DroppedDown = true;

        }

        private void lv_clients_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column != _clientsSortColumn)
            {
                _clientsSortColumn = e.Column;
                lv_clients.Sorting = SortOrder.Ascending;
            }
            else
            {
                lv_clients.Sorting = lv_clients.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            }

            lv_clients.ListViewItemSorter = new ListViewItemComparer(e.Column, lv_clients.Sorting);
            lv_clients.Sort();
        }

        private void lv_fileTransfer_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column != _filesSortColumn)
            {
                _filesSortColumn = e.Column;
                lv_fileTransfer.Sorting = SortOrder.Ascending;
            }
            else
            {
                lv_fileTransfer.Sorting = lv_fileTransfer.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            }

            lv_fileTransfer.Sort();
            lv_fileTransfer.ListViewItemSorter = new ListViewItemComparer(e.Column, lv_fileTransfer.Sorting);
        }
    }
}