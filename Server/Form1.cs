using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimpleTCP;

using System.Net;


namespace tcpip
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        SimpleTcpServer server;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            server = new SimpleTcpServer();
            server.Delimiter = 0x13;//enter
            server.StringEncoder = Encoding.UTF8;
            server.DataReceived += Server_DataReceived;
        }

        private void Server_DataReceived(object sender, SimpleTCP.Message e)
        {
            //Update mesage to txtStatus
            txtStatus.Invoke((MethodInvoker)delegate()
            {
                txtStatus.AppendText(Environment.NewLine);
                txtStatus.Text += e.MessageString;
                e.ReplyLine(string.Format(" "));
                e.ReplyLine(string.Format("Succes Send: {0}", e.MessageString));
            });
        }

        private void btnStart_Click(object sender, System.EventArgs e)
        {
            //Start server host
            txtStatus.Text += "Server Ready... ";
            txtStatus.Text += " ";
            System.Net.IPAddress ip = System.Net.IPAddress.Parse(txtHost.Text);
            server.Start(ip, Convert.ToInt32(txtPort.Text));
        }

        private void btnStop_Click(object sender, System.EventArgs e)
        {
            if (server.IsStarted)
                server.Stop();
        }

    }
}
