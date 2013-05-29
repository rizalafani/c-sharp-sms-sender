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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private FormSMS _sms;
        private FormSMSPribadi _smsPribadi;
        private DaftarSiswa _siswa;
        private AddSiswa _addSiswa;
        private Guru _guru;
        private FormSMSGuru _guru_sms;
        private Kelas kelas;
        private string port = "19";

        public string Port
        {
            get { return port; }
            set { port = value; }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void CloseMdiForm()
        {
            foreach (Form f in this.MdiChildren)
            {
                f.Close();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ribbonButton1_Click(null, null);
        }

        private void ribbonButton6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Application Created By Ahmad Rizal Afani\n SMS Sender V 1.0", "About me", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ribbonButton1_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f is FormSMS)
                {
                    //MessageBox.Show("Form2 is already opened.");
                    return;
                }
            }

            CloseMdiForm();
            _sms = new FormSMS();
            _sms.MdiParent = this;
            _sms.textBox2.Text = port;
            _sms.Show();
        }

        private void ribbonButton2_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f is DaftarSiswa)
                {
                    //MessageBox.Show("Form2 is already opened.");
                    return;
                }
            }

            CloseMdiForm();
            _siswa = new DaftarSiswa();
            _siswa.MdiParent = this;
            _siswa.Show();
        }

        private void ribbonButton3_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f is AddSiswa)
                {
                    //MessageBox.Show("Form2 is already opened.");
                    return;
                }
            }

            CloseMdiForm();
            _addSiswa = new AddSiswa();
            _addSiswa.MdiParent = this;
            _addSiswa.groupBox1.Text = "Tambah Siswa";
            _addSiswa.main = this;
            _addSiswa.Show();
        }

        private void ribbonButton7_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f is FormSMSPribadi)
                {
                    //MessageBox.Show("Form2 is already opened.");
                    return;
                }
            }

            CloseMdiForm();
            _smsPribadi = new FormSMSPribadi();
            _smsPribadi.MdiParent = this;
            _smsPribadi.textBox3.Text = port;
            _smsPribadi.Show();
        }

        private void ribbonButton5_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f is Guru)
                {
                    //MessageBox.Show("Form2 is already opened.");
                    return;
                }
            }

            CloseMdiForm();
            _guru = new Guru();
            _guru.MdiParent = this;
            _guru.Show();
        }

        private void ribbonButton4_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f is FormSMSGuru)
                {
                    //MessageBox.Show("Form2 is already opened.");
                    return;
                }
            }

            CloseMdiForm();
            _guru_sms = new FormSMSGuru();
            _guru_sms.MdiParent = this;
            _guru_sms.textBox2.Text = port;
            _guru_sms.Show();
        }

        private void ribbonButton8_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f is Kelas)
                {
                    //MessageBox.Show("Form2 is already opened.");
                    return;
                }
            }

            CloseMdiForm();
            kelas = new Kelas();
            kelas.MdiParent = this;
            kelas.Show();
        }


    }
}
