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
    public partial class FormSMS : ChildForm
    {
        public FormSMS()
        {
            InitializeComponent();
        }

        private Konektor koneksi;
        private string query;
        private DataSet ds;
        private CheckBox ckbox = new CheckBox();
        private SMS sms_sender;

        private void FormSMS_Load(object sender, EventArgs e)
        {
            try
            {
                koneksi = new Konektor();
                ds = new DataSet();
                ds = koneksi.GetData("select * from classes order by kelas desc");
                comboBox1.Items.Add("Semua");
                for (byte i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    comboBox1.Items.Add(ds.Tables[0].Rows[i][0].ToString());
                }
                comboBox1.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text == "Semua")
                {
                    query = "select * from siswa";
                }
                else
                {
                    query = "select * from siswa where kelas = '"+comboBox1.Text+"'";
                }

                EventLoadCheckbox(query);
                textBox1.Focus();
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
                label3.Text = textBox1.Text.Length.ToString() + " Character";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }            
        }

        #region Checkbox

        private int TotalCheckBoxes = 0;
        private int TotalCheckedCheckBoxes = 0;
        private CheckBox HeaderCheckBox = null;
        private bool IsHeaderCheckBoxClicked = false;

        private void EventLoadCheckbox(string query)
        {
            AddHeaderCheckBox();

            HeaderCheckBox.KeyUp += new KeyEventHandler(HeaderCheckBox_KeyUp);
            HeaderCheckBox.MouseClick += new MouseEventHandler(HeaderCheckBox_MouseClick);
            dataGridView1.CellValueChanged += new DataGridViewCellEventHandler(dgvSelectAll_CellValueChanged);
            dataGridView1.CurrentCellDirtyStateChanged += new EventHandler(dgvSelectAll_CurrentCellDirtyStateChanged);
            dataGridView1.CellPainting += new DataGridViewCellPaintingEventHandler(dgvSelectAll_CellPainting);

            BindGridView(query);
        }

        private void BindGridView(string query)
        {
            dataGridView1.DataSource = GetDataSource(query);

            TotalCheckBoxes = dataGridView1.RowCount;
            TotalCheckedCheckBoxes = 0;

            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = true;
            dataGridView1.Columns[4].ReadOnly = true;
            dataGridView1.Columns[5].ReadOnly = true;
            dataGridView1.Columns[6].ReadOnly = true;
        }

        private DataTable GetDataSource(string query)
        {
            DataTable dTable = new DataTable();

            DataRow dRow = null;
            DateTime dTime;
            Random rnd = new Random();

            dTable.Columns.Add("_Check", System.Type.GetType("System.Boolean"));
            dTable.Columns.Add("_no_induk");
            dTable.Columns.Add("_nama");
            dTable.Columns.Add("_alamat");
            dTable.Columns.Add("_kelas");
            dTable.Columns.Add("_nama_wali");
            dTable.Columns.Add("_nomer_hp");

            ds = new DataSet();
            ds = koneksi.GetData(query);

            for (int n = 0; n<ds.Tables[0].Rows.Count; ++n)
            {
                dRow = dTable.NewRow();
                dTime = DateTime.Now;

                dRow["_Check"] = "true";
                dRow["_no_induk"] = ds.Tables[0].Rows[n][0].ToString();
                dRow["_nama"] = ds.Tables[0].Rows[n][1].ToString();
                dRow["_alamat"] = ds.Tables[0].Rows[n][2].ToString();
                dRow["_kelas"] = ds.Tables[0].Rows[n][3].ToString();
                dRow["_nama_wali"] = ds.Tables[0].Rows[n][4].ToString();
                dRow["_nomer_hp"] = ds.Tables[0].Rows[n][5].ToString();

                dTable.Rows.Add(dRow);
                dTable.AcceptChanges();
            }

            return dTable;
        }        

        private void dgvSelectAll_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!IsHeaderCheckBoxClicked)
                RowCheckBoxClick((DataGridViewCheckBoxCell)dataGridView1[e.ColumnIndex, e.RowIndex]);
        }

        private void dgvSelectAll_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell is DataGridViewCheckBoxCell)
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void HeaderCheckBox_MouseClick(object sender, MouseEventArgs e)
        {
            HeaderCheckBoxClick((CheckBox)sender);
        }

        private void HeaderCheckBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                HeaderCheckBoxClick((CheckBox)sender);
        }

        private void dgvSelectAll_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex == 0)
                ResetHeaderCheckBoxLocation(e.ColumnIndex, e.RowIndex);
        }

        private void AddHeaderCheckBox()
        {
            HeaderCheckBox = new CheckBox();

            HeaderCheckBox.Size = new Size(15, 15);

            //Add the CheckBox into the DataGridView
            this.dataGridView1.Controls.Add(HeaderCheckBox);
            HeaderCheckBox.Checked = true;
        }

        private void ResetHeaderCheckBoxLocation(int ColumnIndex, int RowIndex)
        {
            //Get the column header cell bounds
            Rectangle oRectangle = this.dataGridView1.GetCellDisplayRectangle(ColumnIndex, RowIndex, true);

            Point oPoint = new Point();

            oPoint.X = oRectangle.Location.X + (oRectangle.Width - HeaderCheckBox.Width) / 2 + 1;
            oPoint.Y = oRectangle.Location.Y + (oRectangle.Height - HeaderCheckBox.Height) / 2 + 1;

            //Change the location of the CheckBox to make it stay on the header
            HeaderCheckBox.Location = oPoint;
        }

        private void HeaderCheckBoxClick(CheckBox HCheckBox)
        {
            IsHeaderCheckBoxClicked = true;

            foreach (DataGridViewRow Row in dataGridView1.Rows)
                ((DataGridViewCheckBoxCell)Row.Cells["check"]).Value = HCheckBox.Checked;

            dataGridView1.RefreshEdit();

            TotalCheckedCheckBoxes = HCheckBox.Checked ? TotalCheckBoxes : 0;

            IsHeaderCheckBoxClicked = false;
        }

        private void RowCheckBoxClick(DataGridViewCheckBoxCell RCheckBox)
        {
            if (RCheckBox != null)
            {
                //Modifiy Counter;            
                if ((bool)RCheckBox.Value && TotalCheckedCheckBoxes < TotalCheckBoxes)
                    TotalCheckedCheckBoxes++;
                else if (TotalCheckedCheckBoxes > 0)
                    TotalCheckedCheckBoxes--;

                //Change state of the header CheckBox.
                if (TotalCheckedCheckBoxes < TotalCheckBoxes)
                    HeaderCheckBox.Checked = false;
                else if (TotalCheckedCheckBoxes == TotalCheckBoxes)
                    HeaderCheckBox.Checked = true;
            }
        }
#endregion

        private void vistaButton1_Click(object sender, EventArgs e)
        {
            try
            {
                sms_sender = new SMS();
                listBox1.Items.Clear();
                for (short i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1[0, i].Value.ToString() == "True")
                    {
                        sms_sender.SendSMS(textBox2.Text, dataGridView1[6, i].Value.ToString(),textBox1.Text);
                        listBox1.Items.Add(dataGridView1[2, i].Value.ToString()+" Terkirim");
                    }
                }
                MessageBox.Show("Semua sms telah dikirim !!!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
