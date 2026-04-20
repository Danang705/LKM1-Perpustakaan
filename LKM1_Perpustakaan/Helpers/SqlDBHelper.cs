using Npgsql;
using System.Data;

namespace LKM1_Perpustakaan.Helpers
{
    // Class ini berfungsi untuk membuka dan menutup gerbang koneksi ke PostgreSQL
    // Contoh penggunaan: SqlDBHelper db = new SqlDBHelper(connectionString);
    public class SqlDBHelper
    {
        private NpgsqlConnection connection;

        public SqlDBHelper(string pConstr)
        {
            connection = new NpgsqlConnection(pConstr);
        }

        // Method untuk menyiapkan query SQL
        public NpgsqlCommand getNpgsqlCommand(string query)
        {
            connection.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
            return cmd;
        }

        // Method untuk memutus koneksi agar memori tidak bocor
        public void closeConnection()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}