using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace ServerProekt
{
    class Program
    {
        public static void Send(string msg)
        {
            try
            {
                IPAddress ip = Dns.GetHostEntry("localhost").AddressList[0];
                TcpClient client = new TcpClient("localhost", 8085);
                string str = msg;
                int byteCount = Encoding.ASCII.GetByteCount(str);
                byte[] sendData = Encoding.ASCII.GetBytes(str);

                int broj = sendData.Length;
                NetworkStream stream = client.GetStream();
                stream.Write(sendData, 0, sendData.Length);
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
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
        static void Main(string[] args)
        {
            string usernamekey = null;
            string connectionString = null;
            SqlConnection con;
            SqlCommand cmd;
            string sql;
            try
            {
                connectionString = null;
                connectionString = "Server= DESKTOP-KT8CCEC\\SQLEXPRESS; Database= Licnosti; Integrated Security=True;";
                con = new SqlConnection(connectionString);
                con.Open();
                sql = "CREATE TABLE UsersLogin" +
 "(UserName CHAR(255) PRIMARY KEY,Password CHAR(255),Ime CHAR(255),Prezime CHAR(255),Godini CHAR(255),Adresa CHAR(255),"
                +"MaticenBroj CHAR(255),Grad CHAR(255),PrijavenVakcina CHAR(255),VidVakcina CHAR(255),DatumZaVakcina DATETIME,"
                +"DatumZaVtoraDoza DATETIME,PrijavenKarantin CHAR(255),DatumKarantin DATETIME)";
                cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();

            }
            catch 
            {
            }
            connectionString = "Server= DESKTOP-KT8CCEC\\SQLEXPRESS; Database= Licnosti; Integrated Security=True;";
            con = new SqlConnection(connectionString);
            con.Open();
            while (true)
            {
                string poraka = Accept();
                if(poraka=="register")
                {
                    string username = Accept();
                    Console.WriteLine(username);
                    string password = Accept();
                    Console.WriteLine(password);
                    string ime = Accept();
                    Console.WriteLine(ime);
                    string prezime = Accept();
                    Console.WriteLine(prezime);
                    string godini = Accept();
                    Console.WriteLine(godini);
                    string adresa = Accept();
                    Console.WriteLine(adresa);
                    string maticenbroj = Accept();
                    Console.WriteLine(maticenbroj);
                    string grad = Accept();
                    Console.WriteLine(grad);

                    //proverka dali postoi tabelata ako ne postoi se kreira tabela
                    sql = "SELECT count(*) as IsExists FROM dbo.sysobjects where UserName = object_UserName('UsersLogin')";
                        cmd = new SqlCommand(sql, con);
                    try
                    {
                        cmd.CommandText = @"INSERT INTO UsersLogin (UserName, Password, Ime, Prezime, Godini, Adresa, MaticenBroj, Grad) VALUES (@username,@password,@ime,@prezime,@godini,@adresa,@maticenbr,@grad)";
                       
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);
                        cmd.Parameters.AddWithValue("@ime", ime);
                        cmd.Parameters.AddWithValue("@prezime", prezime);
                        cmd.Parameters.AddWithValue("@godini", godini);
                        cmd.Parameters.AddWithValue("@adresa", adresa);
                        cmd.Parameters.AddWithValue("@maticenbr", maticenbroj);
                        cmd.Parameters.AddWithValue("@grad", grad);
                        cmd.ExecuteNonQuery();
                        Send("success");
                    }
                    catch(Exception ex)
                    {
                       Console.WriteLine(ex.ToString());
                       Send("failure");
                        
                    }
                    }
                if(poraka=="login")
                {
                    List<string> usernames = new List<string>();
                    List<string> passwords = new List<string>();
                    SqlDataReader dataReader;
                    sql = "SELECT UserName, Password FROM UsersLogin";
                    cmd = new SqlCommand(sql, con);
                    dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        string usr = dataReader.GetValue(0).ToString();
                        string psw = dataReader.GetValue(1).ToString();
                        usernames.Add(Regex.Replace( usr, @"\s", "" ));
                        passwords.Add(Regex.Replace(psw, @"\s", ""));

                    }
                    dataReader.Close();
                    string username = Accept();
                    usernamekey = username;
                    username = Regex.Replace(username, @"\s", "");
                    string password = Accept();
                    password = Regex.Replace(password, @"\s", "");
                    if (usernames.IndexOf(username) > -1)
                    {
                        if (usernames.IndexOf(username) == passwords.IndexOf(password))
                        {
                            Send("success");
                        }
                        else
                            Send("failure");
                    }
                    else
                        Send("noregister");
                }
                if(poraka=="dashboard")
                {
                   
                    SqlDataReader dataReader;
                    sql = "SELECT UserName, Ime, Prezime, MaticenBroj, Adresa, Grad, Godini, DatumZaVakcina, DatumZaVtoraDoza FROM UsersLogin ";
                    cmd = new SqlCommand(sql, con);
                    dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        string key=Regex.Replace(dataReader.GetValue(0).ToString(), @"\s", "");
                        if(key==usernamekey)
                        {
                            Send(dataReader.GetValue(1).ToString());
                            Send(dataReader.GetValue(2).ToString());
                            Send(dataReader.GetValue(3).ToString());
                            Send(dataReader.GetValue(4).ToString());
                            Send(dataReader.GetValue(5).ToString());
                            Send(dataReader.GetValue(6).ToString());
                            Send(dataReader.GetValue(7).ToString());
                            Send(dataReader.GetValue(8).ToString());

                        }
                    }

                    dataReader.Close();
                }
                if(poraka=="vakcina")
                {
                    string pv = "DA";
                    string vidvakcina = Regex.Replace(Accept(), @"\s", "");
                    Console.WriteLine(vidvakcina);
                    string datum = Accept();
                    DateTime dt;
                    Console.WriteLine(datum);
                    DateTime vtoradoza= new DateTime();
                    DateTime.TryParse(datum, out dt);
                    switch (vidvakcina)
                    {
                        case "AstraZeneca":
                            Console.WriteLine("pomina");
                            vtoradoza = dt.AddDays(84);
                            break;
                        case "Pfizer":
                            Console.WriteLine("pomina");
                            vtoradoza = dt.AddDays(21);
                            break;
                        case "Sinopharm":
                            vtoradoza = dt.AddDays(21);
                            break;
                        case "SputnikV":
                            vtoradoza = dt.AddDays(56);
                            break;
                        case "Moderna":
                            vtoradoza = dt.AddDays(56);
                            break;
                        case "Johnson&Johnson":
                            vtoradoza = dt.AddDays(0);
                            break;
                        default:
                            break;
                    }
                    Console.WriteLine(vtoradoza);
                    
                    sql = "SELECT count(*) as IsExists FROM dbo.sysobjects where UserName = object_UserName('UsersLogin')";
                        cmd = new SqlCommand(sql, con);

                    try
                    {
                        cmd.CommandText = @"UPDATE UsersLogin SET VidVakcina=@vidvakcina,PrijavenVakcina=@pv,DatumZaVakcina=@datumzv,DatumZaVtoraDoza=@vtoradoza WHERE UserName = @usernamekey";
                        cmd.Parameters.AddWithValue(@"vtoradoza", vtoradoza);
                        cmd.Parameters.AddWithValue(@"datumzv",dt);
                        cmd.Parameters.AddWithValue("@vidvakcina", vidvakcina);
                        cmd.Parameters.AddWithValue("@usernamekey", usernamekey);
                        cmd.Parameters.AddWithValue("@pv", pv);
                        cmd.ExecuteNonQuery();
                        Send("success");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        Send("failure");

                    }
                    
                }
                if(poraka=="karantin")
                {
                    string porakas = Accept();
                    if(porakas=="success")
                    {
                    string pk = "DA";
                    string datum = Accept();
                    DateTime dt;
                    Console.WriteLine(datum);
                    DateTime.TryParse(datum, out dt);
                    
                    sql = "SELECT count(*) as IsExists FROM dbo.sysobjects where UserName = object_UserName('UsersLogin')";
                    cmd = new SqlCommand(sql, con);

                    try
                    {
                        cmd.CommandText = @"UPDATE UsersLogin SET PrijavenKarantin=@pk,DatumKarantin=@datumk WHERE UserName = @usernamekey";
                        cmd.Parameters.AddWithValue(@"pk", pk);
                        cmd.Parameters.AddWithValue(@"datumk", dt);
                        cmd.Parameters.AddWithValue("@usernamekey", usernamekey);
                        cmd.ExecuteNonQuery();
                        Send("success");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        Send("failure");

                    }
                }
                }
            }
         }
     }
}
    
