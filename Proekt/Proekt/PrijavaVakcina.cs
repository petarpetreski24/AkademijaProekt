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
    public partial class PrijavaVakcina : Form
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
        public PrijavaVakcina()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime prijavaDen = monthCalendar1.SelectionStart;
            if(prijavaDen<=monthCalendar1.TodayDate)
            {
                MessageBox.Show("Ne mozete da se prijavite za vakcija");
            }
            else
            {
                if (radioButton1.Checked == true)
                {
                    Send("vakcina");
                    Send("Pfizer");
                    string datum = monthCalendar1.SelectionStart.ToString();
                    Send(datum);
                    int milliseconds = 100;
                    Thread.Sleep(milliseconds);
                    string message = Accept();
                    if (message == "success")
                    {
                        MessageBox.Show("Uspesno se prijavivte za Pfizer vakcinata protiv COVID-19");
                    }
                    else
                    {
                        MessageBox.Show("Problem pri prijavuvanjeto");
                    }

                }
                if (radioButton2.Checked == true)
                {
                    Send("vakcina");
                    Send("Moderna");
                    string datum = monthCalendar1.SelectionStart.ToString();
                    Send(datum);
                    int milliseconds = 100;
                    Thread.Sleep(milliseconds);
                    string message = Accept();
                    if (message == "success")
                    {
                        MessageBox.Show("Uspesno se prijavivte za Moderna vakcinata protiv COVID-19");
                    }
                    else
                    {
                        MessageBox.Show("Problem pri prijavuvanjeto");
                    }
                }
                if (radioButton3.Checked == true)
                {
                    Send("vakcina");
                    Send("Johnson & Johnson");
                    string datum = monthCalendar1.SelectionStart.ToString();
                    Send(datum);
                    int milliseconds = 100;
                    Thread.Sleep(milliseconds);
                    string message = Accept();
                    if (message == "success")
                    {
                        MessageBox.Show("Uspesno se prijavivte za Johnson & Johnson vakcinata protiv COVID-19");
                    }
                    else
                    {
                        MessageBox.Show("Problem pri prijavuvanjeto");
                    }
                }
                if (radioButton4.Checked == true)
                {
                    Send("vakcina");
                    Send("Sputnik V");
                    string datum = monthCalendar1.SelectionStart.ToString();
                    Send(datum);
                    int milliseconds = 100;
                    Thread.Sleep(milliseconds);
                    string message = Accept();
                    if (message == "success")
                    {
                        MessageBox.Show("Uspesno se prijavivte za Sputnik V vakcinata protiv COVID-19");
                    }
                    else
                    {
                        MessageBox.Show("Problem pri prijavuvanjeto");
                    }
                }
                if (radioButton5.Checked == true)
                {
                    Send("vakcina");
                    Send("Sinopharm");
                    string datum = monthCalendar1.SelectionStart.ToString();
                    Send(datum);
                    int milliseconds = 100;
                    Thread.Sleep(milliseconds);
                    string message = Accept();
                    if (message == "success")
                    {
                        MessageBox.Show("Uspesno se prijavivte za Sinopharm vakcinata protiv COVID-19");
                    }
                    else
                    {
                        MessageBox.Show("Problem pri prijavuvanjeto");
                    }
                }
                if (radioButton6.Checked == true)
                {
                    
                    Send("vakcina");
                    Send("Astra Zeneca");
                    string datum = monthCalendar1.SelectionStart.ToString();
                    Send(datum);
                    int milliseconds = 100;
                    Thread.Sleep(milliseconds);
                    string message = Accept();
                    if (message == "success")
                    {
                        MessageBox.Show("Uspesno se prijavivte za Astra Zeneca vakcinata protiv COVID-19");
                    }
                    else
                    {
                        MessageBox.Show("Problem pri prijavuvanjeto");
                    }
                }
                
            }
            

        }
    }
}
