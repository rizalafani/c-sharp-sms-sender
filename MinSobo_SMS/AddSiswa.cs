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
    public partial class AddSiswa : ChildForm
    {
        public AddSiswa()
        {
            InitializeComponent();
        }

        private Konektor koneksi;
        private string query;
        private DataSet ds;
        private DaftarSiswa _siswa;
        public MainForm main;
        
        private void AddSiswa_Load(object sender, EventArgs e)
        {
            try
            {
                koneksi = new Konektor();
                ds = new DataSet();
                ds = koneksi.GetData("select * from classes order by kelas desc");
                comboBox1.Items.Clear();
                for (byte i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i][0].ToString() != "Semua")
                    {
                        comboBox1.Items.Add(ds.Tables[0].Rows[i][0].ToString());
                    }                    
                }
                comboBox1.SelectedIndex = 0;
                btn_bersih.Enabled = false;
                btn_ubah.Enabled = false;
                btn_hapus.Enabled = false;
                btn_cari.Enabled = true;
                btn_simpan.Enabled = true;
                btn_delete_all.Enabled = true;
                txt_no_induk.Enabled = true;
                txt_no_induk.Clear();
                txt_nama.Clear();
                txt_alamat.Clear();
                txt_namawali.Clear();
                txt_hp.Clear();
                textBox1.Enabled = true;
                txt_no_induk.Focus();
                ds.Clear();
                ds = koneksi.GetData("select * from siswa");
                SetDataGridView(ds);
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
                koneksi = new Konektor();
                if (txt_no_induk.Text == "" || txt_nama.Text == "" || txt_namawali.Text == "" || txt_hp.Text == "")
                {
                    MessageBox.Show("Mohon Lengkapi Data Dulu !!", "Warning !!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_no_induk.Focus();
                }
                else
                {
                    ds = new DataSet();
                    ds = koneksi.GetData("select * from siswa where no_induk = '"+txt_no_induk.Text+"'");

                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        query = "insert into siswa values ('" + txt_no_induk.Text + "','" + txt_nama.Text + "','" + txt_alamat.Text + "','" + comboBox1.Text + "','" + txt_namawali.Text + "','" + txt_hp.Text + "');";
                        byte res = (byte)koneksi.ManipulasiData(query);
                        if (res == 1)
                        {
                            MessageBox.Show("Berhasil menambah data !!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        AddSiswa_Load(null, null);
                    }
                    else
                    {
                        MessageBox.Show("No Induk sudah terpakai","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        txt_no_induk.Clear();
                        txt_no_induk.Focus();
                    }                    

                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void vistaButton6_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_no_induk.Text != "")
                {
                    ds = new DataSet();
                    ds = koneksi.GetData("select * from siswa where no_induk = '" + txt_no_induk.Text + "'");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow kolom in ds.Tables[0].Rows)
                        {
                            txt_no_induk.Text = kolom["no_induk"].ToString();
                            txt_nama.Text = kolom["nama"].ToString();
                            txt_alamat.Text = kolom["alamat"].ToString();
                            txt_namawali.Text = kolom["nama_wali"].ToString();
                            txt_hp.Text = kolom["no_hp"].ToString();
                            comboBox1.Text = kolom["kelas"].ToString();
                            txt_no_induk.Enabled = false;
                            btn_cari.Enabled = false;
                            btn_simpan.Enabled = false;
                            btn_bersih.Enabled = true;
                            btn_ubah.Enabled = true;
                            btn_hapus.Enabled = true;
                            btn_delete_all.Enabled = false;
                            textBox1.Enabled = false;
                            dataGridView1.DataSource = ds.Tables[0];
                            SetDataGridView(ds);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Data yang anda cari tidak ada !", "Warning !!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txt_no_induk.Clear();
                        txt_no_induk.Focus();
                    }                    
                }
                else
                {
                    MessageBox.Show("Masukkan nomer induk yang anda cari !!!", "Warning !!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_no_induk.Focus();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void vistaButton7_Click(object sender, EventArgs e)
        {
            try
            {
                AddSiswa_Load(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }            
        }

        private void txt_no_induk_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    vistaButton6_Click(null, null);
                }
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
                    query = "delete from siswa where no_induk = '" +txt_no_induk.Text+ "'";
                    koneksi = new Konektor();
                    byte res = (byte)koneksi.ManipulasiData(query);
                    if (res == 1)
                    {
                        MessageBox.Show("Berhasil Menghapus Data !!!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        AddSiswa_Load(null, null);
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

        private void btn_ubah_Click(object sender, EventArgs e)
        {
            try
            {
                query = "update siswa set nama='"+txt_nama.Text+"',alamat='"+txt_alamat.Text+"',kelas='"+comboBox1.Text+"',nama_wali='"+txt_namawali.Text+"',no_hp='"+txt_hp.Text+"' where no_induk='"+txt_no_induk.Text+"'";
                byte res = (byte)koneksi.ManipulasiData(query);
                if (res == 1)
                {
                    MessageBox.Show("Berhasil mengubah data !!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Gagal mengubah data !!", "Warning !!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                AddSiswa_Load(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void SetDataGridView(DataSet ds)
        {
            try
            {
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].HeaderText = "Nomer Induk";
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
                dataGridView1.Columns[5].Width = 150;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ds = new DataSet();
                ds = koneksi.GetData("select * from siswa where nama like '%"+textBox1.Text+"%';");
                SetDataGridView(ds);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void vistaButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Anda Yakin Menghapus Semua Data ??? \nData akan hilang semua", "Warning !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    query = "delete from siswa";
                    koneksi = new Konektor();
                    byte res = (byte)koneksi.ManipulasiData(query);
                    if (res == 1)
                    {
                        MessageBox.Show("Berhasil Menghapus Data !!!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);                        
                    }
                    AddSiswa_Load(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
