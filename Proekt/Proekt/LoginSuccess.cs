using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text.RegularExpressions;

namespace Proekt
{
    public partial class LoginSuccess : Form
    {
        public static string Accept()
        {
            IPAddress ip = Dns.GetHostEntry("localhost").AddressList[0];
            TcpListener server = new TcpListener(ip, 8085);
            TcpClient client = default(TcpClient);
            try
            {
                server.Start();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
            client = server.AcceptTcpClient();
            byte[] receivedBuffer = new byte[1024];
            NetworkStream stream = client.GetStream();
            stream.Read(receivedBuffer, 0, receivedBuffer.Length);
            int count = Array.IndexOf<byte>(receivedBuffer, 0, 0);

            string msg = Encoding.ASCII.GetString(receivedBuffer, 0, count);
            byte[] sendData = Encoding.ASCII.GetBytes(msg);
            int b = sendData.Length;
            server.Stop();
            return msg;
        }
        public void Send(string msg)
        {
            try
            {
                IPAddress ip = Dns.GetHostEntry("localhost").AddressList[0];
                TcpClient client = new TcpClient("localhost", 8085);
                // Console.WriteLine("Vnesete string koj sakte da bide ispraten");
                string str = msg;
                int byteCount = Encoding.ASCII.GetByteCount(str);
                byte[] sendData = Encoding.ASCII.GetBytes(str);

                //sendData = new byte[byteCount];

                int broj = sendData.Length;
                NetworkStream stream = client.GetStream();
                stream.Write(sendData, 0, sendData.Length);
                client.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public LoginSuccess()
        {
            InitializeComponent();
        }

        private void LoginSuccess_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Send("dashboard");
            textBoxIme.Text = Regex.Replace(Accept(), @"\s", "");
            textBoxPrezime.Text = Regex.Replace(Accept(), @"\s", "");
            textBoxMB.Text = Regex.Replace(Accept(), @"\s", "");
            textBoxAdresa.Text = Regex.Replace(Accept(), @"\s", "");
            textBoxGrad.Text = Regex.Replace(Accept(), @"\s", "");
            textBoxGodini.Text = Regex.Replace(Accept(), @"\s", "");
            label10.Text = Accept();
            label11.Text = Accept();
        }

        private void button2_Click(object sender, EventArgs e)
        {
                PrijavaVakcina obj = new PrijavaVakcina();
                obj.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            PrijavaKarantin obj = new PrijavaKarantin();
            obj.Show();
        }
    }
}
