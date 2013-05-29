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
    public partial class FormSMSPribadi : ChildForm
    {
        public FormSMSPribadi()
        {
            InitializeComponent();
        }

        private SMS sms_sender;

        private void FormSMSPribadi_Load(object sender, EventArgs e)
        {
            try
            {
                textBox2.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                label3.Text = textBox1.Text.Length.ToString() + " Character";
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
                sms_sender = new SMS();
                sms_sender.SendSMS(textBox3.Text,textBox2.Text,textBox1.Text);
                MessageBox.Show("SMS Telah dikirim !!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Clear();
                textBox2.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
