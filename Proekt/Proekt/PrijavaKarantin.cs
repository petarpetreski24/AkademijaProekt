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
    public partial class PrijavaKarantin : Form
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
        public PrijavaKarantin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Send("karantin");
            string datum = monthCalendar1.SelectionStart.ToString();
            DateTime granicenden = monthCalendar1.TodayDate.Subtract(TimeSpan.FromDays(14));
            DateTime poslkont = monthCalendar1.SelectionStart;
            if(poslkont>granicenden && poslkont<=monthCalendar1.TodayDate)
            {
                Send("success");
                Send(datum);
                int milliseconds = 1000;
                Thread.Sleep(milliseconds);
                string message = Accept();
                if (message == "success")
                {
                    MessageBox.Show("Prijaveni ste vo karantin. Ve molime ostanete doma i pocituvajte gi merkite");
                    Close();
                }
                else
                {
                    MessageBox.Show("Problem pri prijavuvanjeto");
                }
               
            }
            else
            {
                MessageBox.Show("Vasiot kontakt bil pred 14 dena i nema potreba da bidite vo samoizolacija ili selektiraniot datum e ponapred od denes");
            }
            
        }
    }
}
