using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputerIPChecker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(tb_ip, "Kliknij dwa razy, aby skopiować do schowka");
        }

        private void btn_checkIP_Click(object sender, EventArgs e)
        {
            TcpComm.Client client = new TcpComm.Client((f, s) => { }, true, 30);
            tb_ip.Text = client.GetLocalIpAddress().ToString();
            btn_checkIP.Visible = false;
            tb_ip.Visible = true;
        }

        private void tb_ip_DoubleClick(object sender, EventArgs e)
        {
            Clipboard.SetText(tb_ip.Text);
        }
    }
}
