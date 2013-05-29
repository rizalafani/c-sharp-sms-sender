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
    public partial class DaftarSiswa : ChildForm
    {
        public DaftarSiswa()
        {
            InitializeComponent();
        }

        private Konektor_Excel excel;
        private Konektor koneksi;
        private DataSet ds;
        private string query;

        private void DaftarSiswa_Load(object sender, EventArgs e)
        {
           
        }

        private void vistaButton1_Click(object sender, EventArgs e)
        {
            try
            {
                excel = new Konektor_Excel();
                ds = new DataSet();
                openFileDialog1.Filter = "Excel File (*.xlsx)| *.xlsx;";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.Title = "Cari File Excel";
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = openFileDialog1.FileName;
                }

                ds = excel.GetDataExcel(textBox1.Text);
                dataGridView1.DataSource = ds.Tables[0];

                dataGridView1.Columns[0].HeaderText = "No Induk";
                dataGridView1.Columns[0].Width = 150;
                dataGridView1.Columns[1].HeaderText = "Nama Siswa";
                dataGridView1.Columns[1].Width = 250;
                dataGridView1.Columns[2].HeaderText = "Alamat";
                dataGridView1.Columns[2].Width = 180;
                dataGridView1.Columns[3].HeaderText = "Kelas";
                dataGridView1.Columns[3].Width = 80;
                dataGridView1.Columns[4].HeaderText = "Nama Wali";
                dataGridView1.Columns[4].Width = 250;
                dataGridView1.Columns[5].HeaderText = "Nomer HP";
                dataGridView1.Columns[5].Width = 180;
            }
            catch (Exception)
            {
                MessageBox.Show("Buka File Excelnya Dulu !!","Warning !!",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }            
        }

        private void vistaButton3_Click(object sender, EventArgs e)
        {
            try
            {
                koneksi = new Konektor();
                if (dataGridView1.Rows.Count > 0)
                {
                    for (short i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        query = "insert into siswa values ('"+dataGridView1[0,i].Value.ToString()+"',"+
                        "'" + dataGridView1[1, i].Value.ToString() + "',"+
                        "'" + dataGridView1[2, i].Value.ToString() + "',"+
                        "'" + dataGridView1[3, i].Value.ToString() + "',"+
                        "'" + dataGridView1[4, i].Value.ToString() + "',"+
                        "'" + dataGridView1[5, i].Value.ToString() + "');";
                        koneksi.ManipulasiData(query);
                    }

                    MessageBox.Show("Berhasil Menyimpan Data !!","Informasi",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    textBox1.Clear();
                    dataGridView1.DataSource = null;
                }
                else
                {
                    MessageBox.Show("Tidak ada data yang akan disimpan !!", "Warning !!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }

}
