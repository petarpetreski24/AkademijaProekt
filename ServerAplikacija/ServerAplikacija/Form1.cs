using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace ServerAplikacija
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.Columns.Add("Ime", 100);
            listView1.Columns.Add("Prezime", 100);
            listView1.Columns.Add("Godini", 100);
            listView1.Columns.Add("MaticenBroj", 100);
            listView1.Columns.Add("Adresa", 100);
            listView1.Columns.Add("Grad", 100);
            listView1.Columns.Add("VidVakcina", 100);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            string connectionString = null;
            SqlConnection con;
            SqlCommand cmd;
            SqlDataReader dataReader;
            string sql;
            connectionString = null;
            connectionString = "Server=COMP193; Database= Licnosti; Integrated Security=True;";
            con = new SqlConnection(connectionString);
            con.Open();
            sql = "Select Ime, Prezime, Godini, MaticenBroj, Adresa, Grad,VidVakcina from UsersLogin";
            cmd = new SqlCommand(sql, con);
            dataReader = cmd.ExecuteReader();
            try
            {
                while (dataReader.Read())
                {
                    listView1.Items.Add(dataReader.GetValue(0).ToString());
                    listView1.Items[listView1.Items.Count - 1].SubItems.Add(dataReader.GetValue(1).ToString());
                    listView1.Items[listView1.Items.Count - 1].SubItems.Add(dataReader.GetValue(2).ToString());
                    listView1.Items[listView1.Items.Count - 1].SubItems.Add(dataReader.GetValue(3).ToString());
                    listView1.Items[listView1.Items.Count - 1].SubItems.Add(dataReader.GetValue(4).ToString());
                    listView1.Items[listView1.Items.Count - 1].SubItems.Add(dataReader.GetValue(5).ToString());
                    listView1.Items[listView1.Items.Count - 1].SubItems.Add(dataReader.GetValue(6).ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            List<string> iminja = new List<string>();
            List<string> preziminja = new List<string>();
            List<string> godini = new List<string>();
            List<string> embg = new List<string>();
            List<string> adresi = new List<string>();
            List<string> gradovi = new List<string>();
            List<string> vakcina = new List<string>();
            string connectionString = null;
            SqlConnection con;
            SqlCommand cmd;
            SqlDataReader dataReader;
            string sql;
            connectionString = null;
            connectionString = "Server=COMP193; Database= Licnosti; Integrated Security=True;";
            con = new SqlConnection(connectionString);
            con.Open();
            sql = "Select Ime, Prezime, Godini, MaticenBroj, Adresa, Grad, VidVakcina from UsersLogin";
            cmd = new SqlCommand(sql, con);
            dataReader = cmd.ExecuteReader();
            try
            {
                while (dataReader.Read())
                {
                    iminja.Add(Regex.Replace(dataReader.GetValue(0).ToString(), @"\s", ""));
                    preziminja.Add(Regex.Replace(dataReader.GetValue(1).ToString(), @"\s", ""));
                    godini.Add(Regex.Replace(dataReader.GetValue(2).ToString(), @"\s", ""));
                    embg.Add(Regex.Replace(dataReader.GetValue(3).ToString(), @"\s", ""));
                    adresi.Add(Regex.Replace(dataReader.GetValue(4).ToString(), @"\s", ""));
                    gradovi.Add(Regex.Replace(dataReader.GetValue(5).ToString(), @"\s", ""));
                    vakcina.Add(Regex.Replace(dataReader.GetValue(6).ToString(), @"\s", ""));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            //IMINJA
            if (radioButton1.Checked == true)
            {
                string sortkey = textBox1.Text;
                int indeks = iminja.IndexOf(sortkey);
                if (indeks > -1)
                {
                    listView1.Items.Clear();
                    for (int i = indeks; i < iminja.Count; i++)
                    {
                        if (iminja[i] == sortkey)
                        {
                            listView1.Items.Add(iminja[i]);
                            listView1.Items[listView1.Items.Count - 1].SubItems.Add(preziminja[i]);
                            listView1.Items[listView1.Items.Count - 1].SubItems.Add(godini[i]);
                            listView1.Items[listView1.Items.Count - 1].SubItems.Add(embg[i]);
                            listView1.Items[listView1.Items.Count - 1].SubItems.Add(adresi[i]);
                            listView1.Items[listView1.Items.Count - 1].SubItems.Add(gradovi[i]);
                            listView1.Items[listView1.Items.Count - 1].SubItems.Add(vakcina[i]);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Ne postoi imeto");
                }
            }
            //Preziminja
            if (radioButton2.Checked == true)
            {
                string sortkeyp = textBox1.Text;
                int indeksp = preziminja.IndexOf(sortkeyp);
                if (indeksp > -1)
                {
                    listView1.Items.Clear();
                    for (int i = indeksp; i < preziminja.Count; i++)
                    {
                        if (preziminja[i] == sortkeyp)
                        {

                            listView1.Items.Add(iminja[i]);
                            listView1.Items[listView1.Items.Count - 1].SubItems.Add(preziminja[i]);
                            listView1.Items[listView1.Items.Count - 1].SubItems.Add(godini[i]);
                            listView1.Items[listView1.Items.Count - 1].SubItems.Add(embg[i]);
                            listView1.Items[listView1.Items.Count - 1].SubItems.Add(adresi[i]);
                            listView1.Items[listView1.Items.Count - 1].SubItems.Add(gradovi[i]);
                            listView1.Items[listView1.Items.Count - 1].SubItems.Add(vakcina[i]);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Ne postoi prezimeto");
                }
            }
            //GODINI
            if (radioButton3.Checked == true)
            {
                string sortkeygo = textBox1.Text;
                int indeksgo = godini.IndexOf(sortkeygo);
                if (indeksgo > -1)
                {
                    listView1.Items.Clear();
                    for (int i = indeksgo; i < godini.Count; i++)
                    {
                        if (godini[i] == sortkeygo)
                        {
                            listView1.Items.Add(iminja[i]);
                            listView1.Items[listView1.Items.Count - 1].SubItems.Add(preziminja[i]);
                            listView1.Items[listView1.Items.Count - 1].SubItems.Add(godini[i]);
                            listView1.Items[listView1.Items.Count - 1].SubItems.Add(embg[i]);
                            listView1.Items[listView1.Items.Count - 1].SubItems.Add(adresi[i]);
                            listView1.Items[listView1.Items.Count - 1].SubItems.Add(gradovi[i]);
                            listView1.Items[listView1.Items.Count - 1].SubItems.Add(vakcina[i]);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Ne postoi godinata");
                }
            }
            //GRADOVI
            if (radioButton4.Checked == true)
            {
                string sortkeyg = textBox1.Text;
                int indeksg = gradovi.IndexOf(sortkeyg);
                if (indeksg > -1)
                {
                    listView1.Items.Clear();
                    for (int i = indeksg; i < gradovi.Count; i++)
                    {
                        if (gradovi[i] == sortkeyg)
                        {
                            listView1.Items.Add(iminja[i]);
                            listView1.Items[listView1.Items.Count - 1].SubItems.Add(preziminja[i]);
                            listView1.Items[listView1.Items.Count - 1].SubItems.Add(godini[i]);
                            listView1.Items[listView1.Items.Count - 1].SubItems.Add(embg[i]);
                            listView1.Items[listView1.Items.Count - 1].SubItems.Add(adresi[i]);
                            listView1.Items[listView1.Items.Count - 1].SubItems.Add(gradovi[i]);
                            listView1.Items[listView1.Items.Count - 1].SubItems.Add(vakcina[i]);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Ne postoi gradot");
                }
            }
            //VAKCINA
            if (radioButton5.Checked == true)
            {
                string sortkeyv = textBox1.Text;
                int indeksv = vakcina.IndexOf(sortkeyv);
                if (indeksv > -1)
                {
                    listView1.Items.Clear();
                    for (int i = indeksv; i < vakcina.Count; i++)
                    {
                        if (vakcina[i] == sortkeyv)
                        {
                            listView1.Items.Add(iminja[i]);
                            listView1.Items[listView1.Items.Count - 1].SubItems.Add(preziminja[i]);
                            listView1.Items[listView1.Items.Count - 1].SubItems.Add(godini[i]);
                            listView1.Items[listView1.Items.Count - 1].SubItems.Add(embg[i]);
                            listView1.Items[listView1.Items.Count - 1].SubItems.Add(adresi[i]);
                            listView1.Items[listView1.Items.Count - 1].SubItems.Add(gradovi[i]);
                            listView1.Items[listView1.Items.Count - 1].SubItems.Add(vakcina[i]);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Greska");
                }
            }
        }
        }
    }

