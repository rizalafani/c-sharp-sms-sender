using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MinSobo_SMS
{
    public partial class Kelas : ChildForm
    {
        public Kelas()
        {
            InitializeComponent();
        }

        private Konektor koneksi;
        private DataSet ds;
        private string query;

        private void Kelas_Load(object sender, EventArgs e)
        {
            try
            {
                SetDataGridView("select * from classes order by kelas");
                textBox1.Clear();
                textBox1.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void SetDataGridView(string query)
        {
            try
            {
                koneksi = new Konektor();
                dataGridView1.DataSource = koneksi.GetData(query).Tables[0];
                dataGridView1.Columns[0].HeaderText = "Kelas";
                dataGridView1.Columns[0].Width = 200;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btn_simpan_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "")
                {
                    koneksi = new Konektor();
                    ds = new DataSet();

                    query = "select * from classes where kelas = '"+textBox1.Text+"';";

                    ds = koneksi.GetData(query);
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        query = "insert into Classes values ('" + textBox1.Text + "');";
                        byte res = (byte)koneksi.ManipulasiData(query);
                        if (res == 1)
                        {
                            MessageBox.Show("Berhasil menambah data");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Kelas sudah terdaftar !!", "Warning !!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }                    
                }
                else
                {
                    MessageBox.Show("Mohon Lengkapi Data Dulu !!", "Warning !!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
                Kelas_Load(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btn_hapus_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Anda Yakin Menghapus Data ini ??? ", "Warning !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    query = "delete from classes where kelas = '" + dataGridView1[0,dataGridView1.CurrentRow.Index].Value.ToString() + "'";
                    koneksi = new Konektor();
                    byte res = (byte)koneksi.ManipulasiData(query);
                    if (res == 1)
                    {
                        MessageBox.Show("Berhasil Menghapus Data !!!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Kelas_Load(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Gagal Menghapus Data !!!", "Warning !!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btn_delete_all_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Anda Yakin Menghapus Semua Data ??? \nData akan hilang semua", "Warning !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    query = "delete from classes";
                    koneksi = new Konektor();
                    byte res = (byte)koneksi.ManipulasiData(query);
                    if (res == 1)
                    {
                        MessageBox.Show("Berhasil Menghapus Data !!!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    Kelas_Load(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btn_simpan_Click(null, null);
            }
        }
    }
}
