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


namespace RPS.Server
{
    public partial class ServerForm : Form
    {
        int count = 0;
        int mydraw = 0;
        Random rnd = new Random();
        SimpleTcpServer server;
        string serverIp = "0.0.0.0:3000";
        int numPlayers = 0;
        string portPlayer1;
        string portPlayer2;

        public ServerForm()
        {
            InitializeComponent();
        }

        private void ServerForm_Load(object sender, EventArgs e)
        {
            CreateButton.Enabled = true;
            server = new SimpleTcpServer(serverIp);
            server.Events.ClientConnected += Events_ClientConnected;
            server.Events.ClientDisconnected += Events_ClientDisconnected;
            server.Events.DataReceived += Events_ClientDataReceived;
            textinfo.Text = "Your IP is: " + serverIp + "\n";
            NumOfPlayersLabel.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            server.Start();
            textinfo.Text += "Server has been created.\n";
            CreateButton.Enabled = false;
            NumOfPlayersLabel.Visible = true;
        }

        private void Events_ClientDataReceived(object sender, DataReceivedEventArgs e)       // Send the message
        {
            this.Invoke((MethodInvoker)delegate
            {
                string command = Encoding.UTF8.GetString(e.Data);

                if (server.IsListening)
                {
                    if (e.IpPort == portPlayer1)
                    {
                        server.Send(portPlayer2, command);
                    }
                    else
                    {
                        server.Send(portPlayer1, command);
                    }
                }       
                count++;
                if (count == 2){
                    Thread T = new Thread(trn);
                    T.Start();
                }
                if (command.Contains("Draw"))
                {
                    mydraw++;
                }
                if (mydraw == 2)
                {
                    server.Send(portPlayer2, "letdr");
                    server.Send(portPlayer1, "letdr");
                    mydraw = 0;
                }

            });
        }
        public void trn()
        {
            if (rnd.Next(1, 2) == 1)
            {
                server.Send(portPlayer1, "turn");
                server.Send(portPlayer2, "nottrn");
                
            }
            else
            {
                server.Send(portPlayer2, "turn");
                server.Send(portPlayer1, "nottrn");
            }
        }
        private void Events_ClientDisconnected(object sender, ConnectionEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                if (e.IpPort == portPlayer1)
                    portPlayer1 = null;
                else
                    portPlayer2 = null;

                textinfo.Text += e.IpPort + " Player disconnected.\n";

                numPlayers -= 1;
                NumOfPlayersLabel.Text = "Number Of Players Connected: " + numPlayers.ToString();
            });
        }
        public void start1_func()
        {
            server.Send(portPlayer1, "Start");
        }
        public void start2_func()
        {
            server.Send(portPlayer2, "Start");
        }
        private void Events_ClientConnected(object sender, ConnectionEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                if (numPlayers == 0)
                {
                    portPlayer1 = e.IpPort;
                }
                else if (numPlayers == 1) // Who connected
                {
                    if (portPlayer1 == null)
                        portPlayer1 = e.IpPort;
                    else
                        portPlayer2 = e.IpPort;
                    server.Send(portPlayer1, "Start");
                    server.Send(portPlayer2, "Start");


                }
                else
                {
                    server.Send(e.IpPort, "Full");
                }
                textinfo.Text += e.IpPort + " Player connected.\n";
                numPlayers ++;
                NumOfPlayersLabel.Text = "Number Of Players Connected: " + numPlayers.ToString();
            });
        }
    }
}