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
    public partial class ChildForm : Form
    {
        public ChildForm()
        {
            InitializeComponent();
            this.Load += new EventHandler(this.MdiChildForm_Load);
        }

        private void MdiChildForm_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.WindowState = FormWindowState.Maximized;
            this.BringToFront();
        }

        private void ChildForm_Load(object sender, EventArgs e)
        {

        }
    }
}
