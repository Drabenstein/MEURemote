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

        #region methods
        public MainForm()
        {
            InitializeComponent();
            this.StatusLabel_status.Text = "Bezczynny";
            ImageList imgList = new ImageList();
            imgList.Images.Add("connected", Resources.user_available);
            imgList.Images.Add("disconnected", Resources.user_invisible);
            lv_Clients.SmallImageList = imgList;
            List<string> cmds = ServerCommands.GetCommandList();
            foreach (string cmd in cmds)
            {
                combo_command.Items.Add(cmd);
            }
        }

        private void button_start_Click(object sender, EventArgs e)
        {
            if (button_start.Text == "Uruchom serwer")
            {
                _server = new TcpComm.Server(UpdateUI, enforceUniqueMachineId: checkBox_EnforceID.Checked);
                string errMsg = "";
                _server.Start(5010, ref errMsg);
                System.Threading.Thread ftm = new System.Threading.Thread(FileTransferMonitor);
                ftm.Start();
                button_start.Text = "Zatrzymaj serwer";
            }
            else
            {
                if (_server != null)
                {
                    _server.Close();
                    this.lv_Clients.Items.Clear();
                    button_start.Text = "Uruchom serwer";
                }
            }
        }

        public void UpdateUI(byte[] bytes, int sessionID, byte dataChannel)
        {
            if (dataChannel < 251)
            {
                UI(delegate () { this.lbox_log.Items.Add("Session " + sessionID.ToString() + ": " + TcpComm.Utilities.BytesToString(bytes)); });
            }
            else if (dataChannel == 255)
            {
                string tmp = "";
                string msg = TcpComm.Utilities.BytesToString(bytes);
                bool dontReport = false;

                if (msg.Length > 3)
                {
                    tmp = msg.Substring(0, 3);
                }

                if (tmp == "UBS")
                {
                    // Wątpliwa część
                    string[] parts = msg.Split(new string[] { "U", "B", "S", ":" }, StringSplitOptions.None);
                    msg = "Dane zostały wysłane do sesji o ID: " + parts[1];
                }

                if (msg == "Connected.")
                {
                    UI(updateClientsList);
                }

                if (msg.Contains(" MachineID:"))
                {
                    UI(updateClientsList);
                }

                if (msg.Contains("Session Stopped. ("))
                {
                    UI(updateClientsList);
                }

                if (!dontReport)
                {
                    UI(() => this.StatusLabel_status.Text = msg);
                }
            }
        }

        private void UI(Action uiUpdate)
        {
            if (this.InvokeRequired)
            {
                try
                {
                    this.Invoke(new MethodInvoker(uiUpdate));
                }
                catch (Exception)
                {
                }
            }
            else
            {
                try
                {
                    uiUpdate();
                }
                catch (Exception)
                {
                }
            }
        }

        private void updateClientsList()
        {
            List<TcpComm.Server.SessionCommunications> sessionList = _server.GetSessionCollection();
            ListViewItem lvi;
            this.lv_Clients.Items.Clear();

            foreach (TcpComm.Server.SessionCommunications session in sessionList)
            {
                if (session.IsRunning)
                {
                    lvi = new ListViewItem(" Connected", 0, lv_Clients.Groups[0]);
                    lvi.ImageKey = "connected";
                }
                else
                {
                    lvi = new ListViewItem(" Disconnected", 1, lv_Clients.Groups[1]);
                    lvi.ImageKey = "disconnected";
                }

                lvi.SubItems.Add(session.sessionID.ToString());
                lvi.SubItems.Add(session.machineId);
                this.lv_Clients.Items.Add(lvi);
            }
        }

        private void FileTransferMonitor()
        {
            DateTime waitTimeout;
            List<TcpComm.Server.SessionCommunications> sessions;
            bool moveOn = false;

            while (_server.IsRunning)
            {
                waitTimeout = DateTime.Now.AddMilliseconds(100);
                while (DateTime.Now < waitTimeout)
                {
                    System.Threading.Thread.Sleep(1);
                }

                sessions = _server.GetSessionCollection();
                foreach (TcpComm.Server.SessionCommunications session in sessions)
                {
                    updateFileProgress(session);
                }
            }
        }

        private void updateFileProgress(TcpComm.Server.SessionCommunications session)
        {
            bool moveOn = false;
            int tmpId = -1;

            moveOn = false;

            if (session.SendingFile)
            {
                UI(() =>
                {
                    tmpId = -1;
                    foreach (ListViewItem tlvi in lv_FileTransfer.Items)
                    {
                        Int32.TryParse(tlvi.SubItems[0].Text, out tmpId);
                        if (tmpId == session.sessionID && tlvi.SubItems[2].Text.Equals("Wychodzące"))
                        {
                            tlvi.SubItems[3].Text = session.GetPercentOfFileSent().ToString() + "%";
                            break;
                        }
                        else
                        {
                            tmpId = -1;
                        }
                    }

                    if (tmpId == -1)
                    {
                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = session.sessionID.ToString();
                        lvi.SubItems.Add(System.IO.Path.GetFileName(session.OutgingFileName));
                        lvi.SubItems.Add("Wychodzące");
                        lvi.SubItems.Add(session.GetPercentOfFileSent().ToString());
                        lv_FileTransfer.Items.Add(lvi);
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
                    foreach (ListViewItem tlvi in lv_FileTransfer.Items)
                    {
                        Int32.TryParse(tlvi.SubItems[0].Text, out tmpId);
                        if (tmpId == session.sessionID && tlvi.SubItems[2].Text.Equals("Wychodzące"))
                        {
                            lv_FileTransfer.Items.Remove(tlvi);
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
                    tmpId = -1;
                    foreach (ListViewItem tlvi in lv_FileTransfer.Items)
                    {
                        Int32.TryParse(tlvi.Text, out tmpId);
                        if (tmpId == session.sessionID && tlvi.SubItems[2].Text.Equals("Przychodzące"))
                        {
                            tlvi.SubItems[3].Text = session.GetPercentOfFileReceived().ToString() + "%";
                            break;
                        }
                        else
                        {
                            tmpId = -1;
                        }
                    }

                    ListViewItem lvi = new ListViewItem();

                    if (tmpId == -1)
                    {
                        lvi.Text = session.sessionID.ToString();
                        lvi.SubItems.Add(System.IO.Path.GetFileName(session.IncomingFileName));
                        lvi.SubItems.Add("Przychodzące");
                        lvi.SubItems.Add(session.GetPercentOfFileReceived().ToString());
                        lv_FileTransfer.Items.Add(lvi);
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
                    foreach (ListViewItem tlvi in lv_FileTransfer.Items)
                    {
                        Int32.TryParse(tlvi.SubItems[0].Text, out tmpId);
                        if (tmpId == session.sessionID && tlvi.SubItems[2].Text.Equals("Przychodzące"))
                        {
                            lv_FileTransfer.Items.Remove(tlvi);
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

        private void button_SendCmd_Click(object sender, EventArgs e)
        {
            if (this.combo_command.Text.Trim().Length > 0)
            {
                string errMsg = "";
                _server.SendText(this.combo_command.Text.Trim(), errMsg: ref errMsg);
            }
        }
#endregion
        private void SendFileMenuItem_Click(object sender, EventArgs e)
        {
            if (lv_Clients.SelectedItems.Count > 0)
            {
                ListViewItem lvi = lv_Clients.SelectedItems[0];
                TcpComm.Server.SessionCommunications session = _server.GetSession(Convert.ToInt32(lvi.SubItems[1].Text));
                string message;
                string fileName;
                if (session == null)
                {
                    MessageBox.Show("Wybrany klient jest rozłączony.", "Klient rozłączony", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                message = "Wybierz plik do wysłania do: " + lvi.SubItems[2].Text;
                openFileDialog1.Title = message;
                openFileDialog1.FileName = "";
                openFileDialog1.ShowDialog();
                fileName = openFileDialog1.FileName;
                if (fileName.Trim().Equals(""))
                {
                    return;
                }

                if (!_server.SendFile(fileName, session.sessionID))
                {
                    MessageBox.Show("Klient został rozłączony", "Klient rozłączony", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        #region additionalMethods
        private void SendCmdMenuItem_Click(object sender, EventArgs e)
        {
            if (lv_Clients.SelectedItems.Count > 0)
            {
                ListViewItem lvi = lv_Clients.SelectedItems[0];
                TcpComm.Server.SessionCommunications session = _server.GetSession(Convert.ToInt32(lvi.SubItems[1].Text));
                Object ret;

                if (session == null)
                {
                    MessageBox.Show("Klient jest rozłączony.", "Klient rozłączony", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                CommandForm cmdForm = new CommandForm(lvi.SubItems[2].Text);
                cmdForm.ShowDialog();
                ret = cmdForm.Command;
                if (ret.ToString() == "")
                {
                    return;
                }

                if (session != null)
                {
                    string errMsg = "";
                    _server.SendText(ret.ToString(), 1, session.sessionID, ref errMsg);
                }
            }
        }

        private void DisconnectMenuItem_Click(object sender, EventArgs e)
        {
            if (lv_Clients.SelectedItems.Count > 0)
            {
                ListViewItem lvi = lv_Clients.SelectedItems[0];
                TcpComm.Server.SessionCommunications session = _server.GetSession(Convert.ToInt32(lvi.SubItems[1].Text));

                if (session != null)
                {
                    session.Close();
                }
            }
        }

        private void button_Help_Click(object sender, EventArgs e)
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
            if (combo_command.Text.Contains(ServerCommands.SVR_CHECKPROCESS))
            {
                return;
            }
            else if (combo_command.Text.Contains(ServerCommands.SVR_KILLPROCESS))
            {
                return;
            }
            else if (combo_command.Text.Contains(ServerCommands.SVR_RUN))
            {
                return;
            }

            combo_command.DroppedDown = true;

        }

        private void lv_Clients_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column != _clientsSortColumn)
            {
                _clientsSortColumn = e.Column;
                lv_Clients.Sorting = SortOrder.Ascending;
            }
            else
            {
                if (lv_Clients.Sorting == SortOrder.Ascending)
                {
                    lv_Clients.Sorting = SortOrder.Descending;
                }
                else
                {
                    lv_Clients.Sorting = SortOrder.Ascending;
                }
            }

            lv_Clients.Sort();
            lv_Clients.ListViewItemSorter = new ListViewItemComparer(e.Column, lv_Clients.Sorting);
        }
        #endregion

        private void lv_FileTransfer_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column != _filesSortColumn)
            {
                _filesSortColumn = e.Column;
                lv_FileTransfer.Sorting = SortOrder.Ascending;
            }
            else
            {
                if (lv_FileTransfer.Sorting == SortOrder.Ascending)
                {
                    lv_FileTransfer.Sorting = SortOrder.Descending;
                }
                else
                {
                    lv_FileTransfer.Sorting = SortOrder.Ascending;
                }
            }

            lv_FileTransfer.Sort();
            lv_FileTransfer.ListViewItemSorter = new ListViewItemComparer(e.Column, lv_FileTransfer.Sorting);
        }
    }
}
