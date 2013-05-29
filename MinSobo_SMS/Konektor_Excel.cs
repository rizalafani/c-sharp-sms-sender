using System;
using System.Data;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinSobo_SMS
{
    class Konektor_Excel
    {
        public Konektor_Excel()
        {

        }

        private OleDbConnection koneksi;
        private OleDbCommand perintah;
        private OleDbDataAdapter adapter;
        private DataSet ds;

        public DataSet GetDataExcel(string patch)
        {
            try
            {
                ds = new DataSet();
                koneksi = new OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + patch.Replace("\\","/")+ "';Extended Properties=Excel 8.0");
                koneksi.Open();
                perintah = new OleDbCommand("select * from [sheet1$]", koneksi);
                adapter = new OleDbDataAdapter(perintah);
                perintah.ExecuteNonQuery();
                adapter.Fill(ds);
                koneksi.Close();
                return ds;
            }
            catch (Exception)
            {
                return null;
            }
            
        }
    }
}
