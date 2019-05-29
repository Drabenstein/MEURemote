using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Remote
{
    public partial class CommandForm : Form
    {
        public CommandForm(string id)
        {
            InitializeComponent();
            List<string> commands = ServerCommands.GetCommandsList();
            foreach (var cmd in commands)
            {
                combo_command.Items.Add(cmd);
            }
            this.Text += id;
            label_cmd.Text += id;
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

        public string Command { get; private set; }

        private void button_ok_Click(object sender, EventArgs e)
        {
            List<string> cmds = ServerCommands.GetCommandsList();
            bool isCorrect = cmds.Any(cmd => combo_command.Text.Contains((cmd)));
           
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

        private void button_cancel_Click(object sender, EventArgs e)
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
                button_ok_Click(sender, e);
            }
        }
    }
}
