using Npgsql;
using LKM1_Perpustakaan.Helpers;
using System.Data;

namespace LKM1_Perpustakaan.Models
{
    // Class ini berisi logika CRUD penuh untuk tabel Buku
    // Contoh penggunaan di Controller: BukuContext context = new BukuContext(config); context.GetAllBuku();
    public class BukuContext
    {
        private string __constr;

        public BukuContext(string pConstr)
        {
            __constr = pConstr;
        }

        // 1. READ: Mengambil semua data buku yang belum dihapus (Soft Delete)
        public List<Buku> GetAllBuku()
        {
            List<Buku> listBuku = new List<Buku>();
            // Query JOIN dengan tabel kategori
            string query = @"SELECT b.id_buku, b.judul, b.pengarang, b.id_kategori, k.nama_kategori 
                             FROM perpustakaan.buku b 
                             JOIN perpustakaan.kategori k ON b.id_kategori = k.id_kategori 
                             WHERE b.deleted_at IS NULL;";

            SqlDBHelper db = new SqlDBHelper(this.__constr);
            NpgsqlCommand cmd = db.getNpgsqlCommand(query);
            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                listBuku.Add(new Buku()
                {
                    id_buku = Convert.ToInt32(reader["id_buku"]),
                    judul = reader["judul"].ToString()!,
                    pengarang = reader["pengarang"].ToString()!,
                    id_kategori = Convert.ToInt32(reader["id_kategori"]),
                    nama_kategori = reader["nama_kategori"].ToString()
                });
            }
            cmd.Dispose();
            db.closeConnection();
            return listBuku;
        }

        // 2. READ: Mengambil satu buku berdasarkan ID
        public Buku? GetBukuById(int id)
        {
            Buku? buku = null;
            // Menggunakan parameter @id_buku untuk keamanan
            string query = @"SELECT b.id_buku, b.judul, b.pengarang, b.id_kategori, k.nama_kategori 
                             FROM perpustakaan.buku b 
                             JOIN perpustakaan.kategori k ON b.id_kategori = k.id_kategori 
                             WHERE b.id_buku = @id_buku AND b.deleted_at IS NULL;";

            SqlDBHelper db = new SqlDBHelper(this.__constr);
            NpgsqlCommand cmd = db.getNpgsqlCommand(query);
            cmd.Parameters.AddWithValue("@id_buku", id); // Bind parameter

            NpgsqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                buku = new Buku()
                {
                    id_buku = Convert.ToInt32(reader["id_buku"]),
                    judul = reader["judul"].ToString()!,
                    pengarang = reader["pengarang"].ToString()!,
                    id_kategori = Convert.ToInt32(reader["id_kategori"]),
                    nama_kategori = reader["nama_kategori"].ToString()
                };
            }
            cmd.Dispose();
            db.closeConnection();
            return buku;
        }

        // 3. CREATE: Menambahkan buku baru menggunakan Parameterized Query
        public void AddBuku(Buku buku)
        {
            string query = @"INSERT INTO perpustakaan.buku (judul, pengarang, id_kategori) 
                             VALUES (@judul, @pengarang, @id_kategori);";

            SqlDBHelper db = new SqlDBHelper(this.__constr);
            NpgsqlCommand cmd = db.getNpgsqlCommand(query);

            // Menyuntikkan data dengan aman (Mencegah SQL Injection)
            cmd.Parameters.AddWithValue("@judul", buku.judul);
            cmd.Parameters.AddWithValue("@pengarang", buku.pengarang);
            cmd.Parameters.AddWithValue("@id_kategori", buku.id_kategori);

            cmd.ExecuteNonQuery();
            cmd.Dispose();
            db.closeConnection();
        }

        // 4. UPDATE: Mengubah data buku
        public void UpdateBuku(int id, Buku buku)
        {
            string query = @"UPDATE perpustakaan.buku 
                             SET judul = @judul, pengarang = @pengarang, id_kategori = @id_kategori, updated_at = CURRENT_TIMESTAMP 
                             WHERE id_buku = @id_buku AND deleted_at IS NULL;";

            SqlDBHelper db = new SqlDBHelper(this.__constr);
            NpgsqlCommand cmd = db.getNpgsqlCommand(query);

            cmd.Parameters.AddWithValue("@id_buku", id);
            cmd.Parameters.AddWithValue("@judul", buku.judul);
            cmd.Parameters.AddWithValue("@pengarang", buku.pengarang);
            cmd.Parameters.AddWithValue("@id_kategori", buku.id_kategori);

            cmd.ExecuteNonQuery();
            cmd.Dispose();
            db.closeConnection();
        }

        public void DeleteBuku(int id)
        {
            // Tidak menggunakan DELETE FROM, melainkan mengisi kolom deleted_at
            string query = @"UPDATE perpustakaan.buku 
                             SET deleted_at = CURRENT_TIMESTAMP 
                             WHERE id_buku = @id_buku;";

            SqlDBHelper db = new SqlDBHelper(this.__constr);
            NpgsqlCommand cmd = db.getNpgsqlCommand(query);
            cmd.Parameters.AddWithValue("@id_buku", id);

            cmd.ExecuteNonQuery();
            cmd.Dispose();
            db.closeConnection();
        }
    }
}