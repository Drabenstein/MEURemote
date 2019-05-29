using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Remote
{
    public partial class CommandHelpForm : Form
    {
        public CommandHelpForm()
        {
            InitializeComponent();
            Dictionary<string, string> commandsDescriptions = ServerCommands.GetCommandsDescriptions();
            Dictionary<string, string> commandsSyntaxExamples = ServerCommands.GetCommandsSyntaxExamples();
            foreach (var item in commandsDescriptions)
            {
                ListViewItem listViewItem = new ListViewItem(item.Key);
                listViewItem.SubItems.Add(item.Value);
                listViewItem.SubItems.Add(commandsSyntaxExamples.FirstOrDefault(x => x.Key == item.Key).Value);
                lv_commands.Items.Add(listViewItem);
            }
        }

        private void menuItem_copy_Click(object sender, EventArgs e)
        {
            if (lv_commands.SelectedItems.Count > 0)
            {
                Clipboard.SetText(lv_commands.SelectedItems[0].Text);
            }
        }
    }
}
