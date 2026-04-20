using Npgsql;
using System.Data;

namespace LKM1_Perpustakaan.Helpers
{
    public class SqlDBHelper
    {
        private NpgsqlConnection connection;
        public SqlDBHelper(string pConstr) { connection = new NpgsqlConnection(pConstr); }
        public NpgsqlCommand getNpgsqlCommand(string query) { connection.Open(); return new NpgsqlCommand(query, connection); }
        public void closeConnection() { if (connection.State == ConnectionState.Open) connection.Close(); }
    }
}