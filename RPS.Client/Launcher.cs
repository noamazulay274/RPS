using RPS.Client;
using SuperSimpleTcp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace RPS.Client
{
    public partial class Launcher : Form
    {
        
        String ServerIP = "127.0.0.1:3000";
        SimpleTcpClient client;
        

        public Launcher()
        {
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Launcher_Load(object sender, EventArgs e)
        {
            Startbtn.Visible = false;
            //FormBorderStyle = FormBorderStyle.None;
            ExitButton.Location = new Point(ExitButton.Parent.ClientSize.Width - ExitButton.Width, 0);
            ConnectButton.Location = new Point(333 -ConnectButton.Width/2, 300);
            //Startbtn.Visible = false;
            client = new(ServerIP);
            client.Events.Connected += Events_Connected;
            client.Events.DataReceived += Events_ClientDataReceived;
            Threadgame.client = this.client;
        }

        private void Events_ClientDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e is null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            this.Invoke((MethodInvoker)delegate
            {
                string command = Encoding.UTF8.GetString(e.Data);
                if (command.Contains( "Start"))
                {
                    Thread thread1 = new Thread(Threadgame.Dogame);
                    thread1.Start();
                }


            });
        }

        private void Events_Connected(object sender, ConnectionEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {

            });
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectButton.Text = "Connecting...";
                ConnectButton.Enabled = false;
                client.Connect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Game g = new Game(client);
            g.ShowDialog();
        }
    }
    public class Threadgame
    {
        public static SimpleTcpClient client;
        public static void Dogame()
        {
            Game g = new Game(client);
            g.ShowDialog();
        }
    }

}
