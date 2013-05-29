using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GsmComm.GsmCommunication;

namespace MinSobo_SMS
{
    public partial class FormKoneksi : Form
    {
        public FormKoneksi()
        {
            InitializeComponent();
        }

        private GsmCommMain comm;
        private MainForm main;

        private void Form1_Load(object sender, EventArgs e)
        {
            for (byte i = 0; i < 50; i++)
            {
                comm = new GsmCommMain((i + 1), 9600, 300);
                try
                {
                    comm.Open();
                    if (comm.IsConnected() == true)
                    {
                        comboBox1.Items.Add((i+1));
                    }
                    comm.Close();
                }
                catch (Exception)
                {
                }
            }

            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Tidak Ada port yang terbuka !!!\n "+
                    "Cek kembali bluetooth Anda !","Warning !!",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void vistaButton1_Click(object sender, EventArgs e)
        {
            try
            {
                comm = new GsmCommMain(int.Parse(comboBox1.Text), 9600, 300);
                comm.Open();
                if (comm.IsConnected() == true)
                {
                    MessageBox.Show("Koneksi Suksess !!", "Information !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Tidak Ada port yang terbuka !!!\n " +
                    "Cek kembali bluetooth Anda !", "Warning !!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                comm.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void vistaButton2_Click(object sender, EventArgs e)
        {
            try
            {
                main = new MainForm();
                comm = new GsmCommMain(int.Parse(comboBox1.Text), 9600, 300);
                comm.Open();
                if (comm.IsConnected() == true)
                {
                    MessageBox.Show("Koneksi Suksess !!", "Information !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    main.Port = comboBox1.Text;
                    main.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Tidak Ada port yang terbuka !!!\n " +
                    "Cek kembali bluetooth Anda !", "Warning !!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                comm.Close();
                main.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
