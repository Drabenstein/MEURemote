using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Remote
{
    public partial class CommandForm : Form
    {
        private string _commandToReturn;

        public CommandForm(string id)
        {
            InitializeComponent();
            List<string> cmds = ServerCommands.GetCommandList();
            foreach (var cmd in cmds)
            {
                combo_command.Items.Add(cmd);
            }
            this.Text += id;
            label_cmd.Text += id;
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

        public string Command
        {
            get
            {
                return _commandToReturn;
            }

            private set
            {
                _commandToReturn = value;
            }
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            List<string> cmds = ServerCommands.GetCommandList();
            bool isCorrect = false;
            foreach (var cmd in cmds)
            {
                if(combo_command.Text.Contains(cmd))
                {
                    isCorrect = true;
                    break;
                }
            }

            if (isCorrect)
            {
                Command = combo_command.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Niepoprawna komenda");
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Command = "";
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void combo_command_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if ENTER was pressed
            if(e.KeyChar == (char)13)
            {
                btn_ok_Click(sender, e);
            }
        }
    }
}
