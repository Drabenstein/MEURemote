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
    public partial class CommandHelpForm : Form
    {
        public CommandHelpForm()
        {
            InitializeComponent();
            Dictionary<string, string> cmdsWithDesc = ServerCommands.GetCommandsWithDescription();
            Dictionary<string, string> cmdsWithSyntax = ServerCommands.GetCommandsWithSyntax();
            foreach (var item in cmdsWithDesc)
            {
                ListViewItem lvi = new ListViewItem(item.Key);
                lvi.SubItems.Add(item.Value);
                lvi.SubItems.Add(cmdsWithSyntax.FirstOrDefault(x => x.Key == item.Key).Value);
                lv_commands.Items.Add(lvi);
            }
        }

        private void kopiujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lv_commands.SelectedItems.Count > 0)
            {
                Clipboard.SetText(lv_commands.SelectedItems[0].Text);
            }
        }
    }
}
