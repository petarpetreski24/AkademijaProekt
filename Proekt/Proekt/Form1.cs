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
    public partial class Form1 : Form
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

                MessageBox.Show(ex.ToString());
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
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Register reg = new Register();
            reg.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoginSuccess lg = new LoginSuccess();
            List<string> loginitems = new List<string>();
            loginitems.Add(textBox1.Text);
            loginitems.Add(textBox2.Text);
            Send("login");
            foreach (string str in loginitems)
            {
                Send(str);
            }
            string message = Accept();
            if(message=="success")
            {
                MessageBox.Show("Uspesno se najavivte");
                lg.Show();
                Hide();
                
            }
            else if(message=="failure")
            {
                MessageBox.Show("Neuspesna najava. Pogresna lozinka!");
                
            }
            else if(message=="noregister")
            {
                MessageBox.Show("Toa korisnicko ime ne e registrirano!");
                
            }

        }

        private void label4_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
