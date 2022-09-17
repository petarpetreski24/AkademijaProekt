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

namespace Proekt
{
    public partial class Register : Form
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public Register()
        {
            InitializeComponent();
        }

        private void Username_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void RegisterSend_Click(object sender, EventArgs e)
        {
            List<string> registeritems = new List<string>();
            registeritems.Add(textBox1.Text);
            registeritems.Add(textBox2.Text);
            registeritems.Add(textBox3.Text);
            registeritems.Add(textBox4.Text);
            registeritems.Add(textBox5.Text);
            registeritems.Add(textBox6.Text);
            registeritems.Add(textBox7.Text);
            registeritems.Add(textBox8.Text);
            Send("register");
            foreach(string str in registeritems)
            {
                Send(str);
            }
            string message = Accept();
            if (message == "success")
            {
                MessageBox.Show("Uspesno se registriravte");
                Close();
            }
            else
                MessageBox.Show("Username-ot veke postoi. Ve molime napravete promena");

        }

        private void Register_Load(object sender, EventArgs e)
        {

        }
    }
}
