using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace MinSobo_SMS
{
    class Konektor
    {
        public Konektor()
        {
            alamat = "Provider=Microsoft.ace.Oledb.12.0;Data Source=minsobo.accdb";
            koneksi = new OleDbConnection(alamat);
        }

        private string alamat;
        private OleDbConnection koneksi;
        private OleDbCommand perintah;
        private OleDbDataAdapter adapter;
        private DataSet ds;

        public DataSet GetData(string query)
        {
            try
            {
                ds = new DataSet();
                koneksi.Open();
                perintah = new OleDbCommand(query, koneksi);
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

        public int ManipulasiData(string query)
        {
            try
            {
                int res = 0;
                koneksi.Open();
                perintah = new OleDbCommand(query, koneksi);
                adapter = new OleDbDataAdapter(perintah);
                res = perintah.ExecuteNonQuery();
                koneksi.Close();
                return res;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
