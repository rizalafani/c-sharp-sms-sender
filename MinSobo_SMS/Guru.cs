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
    public partial class Guru : ChildForm
    {
        public Guru()
        {
            InitializeComponent();
        }

        private Konektor koneksi;
        private string query;
        private DataSet ds;

        private void Guru_Load(object sender, EventArgs e)
        {
            try
            {
                koneksi = new Konektor();
                ds = new DataSet();
                btn_bersih.Enabled = false;
                btn_ubah.Enabled = false;
                btn_hapus.Enabled = false;
                btn_cari.Enabled = true;
                btn_simpan.Enabled = true;
                btn_delete_all.Enabled = true;
                txt_kodeguru.Enabled = true;
                txt_kodeguru.Clear();
                txt_namaGuru.Clear();
                txt_Alamat.Clear();
                txt_hp.Clear();
                txt_jabatan.Clear();
                textBox5.Enabled = true;
                txt_kodeguru.Focus();
                ds.Clear();
                ds = koneksi.GetData("select * from guru");
                SetDataGridView(ds);                
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
                dataGridView1.Columns[0].HeaderText = "Kode Guru";
                dataGridView1.Columns[0].Width = 100;
                dataGridView1.Columns[1].HeaderText = "Nama Guru";
                dataGridView1.Columns[1].Width = 250;
                dataGridView1.Columns[2].HeaderText = "Alamat";
                dataGridView1.Columns[2].Width = 200;
                dataGridView1.Columns[3].HeaderText = "Nomer HP";
                dataGridView1.Columns[3].Width = 150;
                dataGridView1.Columns[4].HeaderText = "Jabatan";
                dataGridView1.Columns[4].Width = 250;

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
                koneksi = new Konektor();
                if (txt_kodeguru.Text == "" || txt_namaGuru.Text == "" || txt_Alamat.Text == "" || txt_hp.Text == "")
                {
                    MessageBox.Show("Mohon Lengkapi Data Dulu !!", "Warning !!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_kodeguru.Focus();
                }
                else
                {
                    ds = new DataSet();
                    ds = koneksi.GetData("select * from guru where no_guru = '" + txt_kodeguru.Text + "'");

                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        query = "insert into guru values ('" + txt_kodeguru.Text + "','" + txt_namaGuru.Text + "','" + txt_Alamat.Text + "','" + txt_hp.Text + "','" + txt_jabatan.Text + "');";
                        byte res = (byte)koneksi.ManipulasiData(query);
                        if (res == 1)
                        {
                            MessageBox.Show("Berhasil menambah data !!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        Guru_Load(null, null);
                    }
                    else
                    {
                        MessageBox.Show("No Induk sudah terpakai", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txt_kodeguru.Clear();
                        txt_kodeguru.Focus();
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btn_cari_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_kodeguru.Text != "")
                {
                    ds = new DataSet();
                    ds = koneksi.GetData("select * from guru where no_guru = '" + txt_kodeguru.Text + "'");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow kolom in ds.Tables[0].Rows)
                        {
                            txt_kodeguru.Text = kolom["no_guru"].ToString();
                            txt_namaGuru.Text = kolom["nama_guru"].ToString();
                            txt_Alamat.Text = kolom["alamat"].ToString();
                            txt_hp.Text = kolom["no_hp"].ToString();
                            txt_jabatan.Text = kolom["jabatan"].ToString();
                            txt_kodeguru.Enabled = false;
                            btn_cari.Enabled = false;
                            btn_simpan.Enabled = false;
                            btn_bersih.Enabled = true;
                            btn_ubah.Enabled = true;
                            btn_hapus.Enabled = true;
                            btn_delete_all.Enabled = false;
                            textBox5.Enabled = false;
                            dataGridView1.DataSource = ds.Tables[0];
                            SetDataGridView(ds);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Data yang anda cari tidak ada !", "Warning !!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txt_kodeguru.Clear();
                        txt_kodeguru.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Masukkan kode yang anda cari !!!", "Warning !!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_kodeguru.Focus();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txt_kodeguru_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btn_cari_Click(null, null);
            }
        }

        private void btn_bersih_Click(object sender, EventArgs e)
        {
            try
            {
                Guru_Load(null, null);
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
                query = "update guru set nama_guru='" + txt_namaGuru.Text + "',alamat='" + txt_Alamat.Text + "',jabatan='" + txt_jabatan.Text + "',no_hp='" + txt_hp.Text + "' where no_guru='" + txt_kodeguru.Text + "'";
                byte res = (byte)koneksi.ManipulasiData(query);
                if (res == 1)
                {
                    MessageBox.Show("Berhasil mengubah data !!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Gagal mengubah data !!", "Warning !!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                Guru_Load(null, null);
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
                    query = "delete from guru where no_guru = '" + txt_kodeguru.Text + "'";
                    koneksi = new Konektor();
                    byte res = (byte)koneksi.ManipulasiData(query);
                    if (res == 1)
                    {
                        MessageBox.Show("Berhasil Menghapus Data !!!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Guru_Load(null, null);
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
                    query = "delete from guru";
                    koneksi = new Konektor();
                    byte res = (byte)koneksi.ManipulasiData(query);
                    if (res == 1)
                    {
                        MessageBox.Show("Berhasil Menghapus Data !!!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    Guru_Load(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ds = new DataSet();
                ds = koneksi.GetData("select * from guru where nama_guru like '%" + textBox5.Text + "%';");
                SetDataGridView(ds);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
