using Npgsql;
using LKM1_Perpustakaan.Helpers;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LKM1_Perpustakaan.Models
{
    public class BukuContext
    {
        private string __constr;
        public BukuContext(string pConstr) { __constr = pConstr; }

        public List<Buku> GetAll()
        {
            List<Buku> list = new List<Buku>();
            SqlDBHelper db = new SqlDBHelper(__constr);
            NpgsqlCommand cmd = db.getNpgsqlCommand("SELECT b.id_buku, b.judul, b.pengarang, b.id_kategori, k.nama_kategori FROM perpustakaan.buku b JOIN perpustakaan.kategori k ON b.id_kategori = k.id_kategori WHERE b.deleted_at IS NULL;");
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) list.Add(new Buku { id_buku = Convert.ToInt32(reader["id_buku"]), judul = reader["judul"].ToString()!, pengarang = reader["pengarang"].ToString()!, id_kategori = Convert.ToInt32(reader["id_kategori"]), nama_kategori = reader["nama_kategori"].ToString() });
            cmd.Dispose(); db.closeConnection(); return list;
        }

        public Buku? GetById(int id)
        {
            Buku? buku = null; SqlDBHelper db = new SqlDBHelper(__constr);
            NpgsqlCommand cmd = db.getNpgsqlCommand("SELECT b.id_buku, b.judul, b.pengarang, b.id_kategori, k.nama_kategori FROM perpustakaan.buku b JOIN perpustakaan.kategori k ON b.id_kategori = k.id_kategori WHERE b.id_buku = @id AND b.deleted_at IS NULL;");
            cmd.Parameters.AddWithValue("@id", id);
            NpgsqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read()) buku = new Buku { id_buku = Convert.ToInt32(reader["id_buku"]), judul = reader["judul"].ToString()!, pengarang = reader["pengarang"].ToString()!, id_kategori = Convert.ToInt32(reader["id_kategori"]), nama_kategori = reader["nama_kategori"].ToString() };
            cmd.Dispose(); db.closeConnection(); return buku;
        }

        public void Add(Buku b) { SqlDBHelper db = new SqlDBHelper(__constr); NpgsqlCommand cmd = db.getNpgsqlCommand("INSERT INTO perpustakaan.buku (judul, pengarang, id_kategori) VALUES (@j, @p, @k);"); cmd.Parameters.AddWithValue("@j", b.judul); cmd.Parameters.AddWithValue("@p", b.pengarang); cmd.Parameters.AddWithValue("@k", b.id_kategori); cmd.ExecuteNonQuery(); cmd.Dispose(); db.closeConnection(); }
        public void Update(int id, Buku b) { SqlDBHelper db = new SqlDBHelper(__constr); NpgsqlCommand cmd = db.getNpgsqlCommand("UPDATE perpustakaan.buku SET judul=@j, pengarang=@p, id_kategori=@k, updated_at=CURRENT_TIMESTAMP WHERE id_buku=@id AND deleted_at IS NULL;"); cmd.Parameters.AddWithValue("@id", id); cmd.Parameters.AddWithValue("@j", b.judul); cmd.Parameters.AddWithValue("@p", b.pengarang); cmd.Parameters.AddWithValue("@k", b.id_kategori); cmd.ExecuteNonQuery(); cmd.Dispose(); db.closeConnection(); }
        public void Delete(int id) { SqlDBHelper db = new SqlDBHelper(__constr); NpgsqlCommand cmd = db.getNpgsqlCommand("UPDATE perpustakaan.buku SET deleted_at=CURRENT_TIMESTAMP WHERE id_buku=@id;"); cmd.Parameters.AddWithValue("@id", id); cmd.ExecuteNonQuery(); cmd.Dispose(); db.closeConnection(); }
    }

    public class PeminjamanContext
    {
        private string __constr; public PeminjamanContext(string pConstr) { __constr = pConstr; }
        public List<Peminjaman> GetAll()
        {
            List<Peminjaman> list = new List<Peminjaman>(); SqlDBHelper db = new SqlDBHelper(__constr);
            NpgsqlCommand cmd = db.getNpgsqlCommand("SELECT p.id_peminjaman, p.id_buku, b.judul, p.nama_peminjam, p.status FROM perpustakaan.peminjaman p JOIN perpustakaan.buku b ON p.id_buku = b.id_buku ORDER BY p.id_peminjaman DESC;");
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) list.Add(new Peminjaman { id_peminjaman = Convert.ToInt32(reader["id_peminjaman"]), id_buku = Convert.ToInt32(reader["id_buku"]), judul_buku = reader["judul"].ToString(), nama_peminjam = reader["nama_peminjam"].ToString()!, status = reader["status"].ToString()! });
            cmd.Dispose(); db.closeConnection(); return list;
        }
        public void Add(Peminjaman p) { SqlDBHelper db = new SqlDBHelper(__constr); NpgsqlCommand cmd = db.getNpgsqlCommand("INSERT INTO perpustakaan.peminjaman (id_buku, nama_peminjam, status) VALUES (@b, @n, 'Dipinjam');"); cmd.Parameters.AddWithValue("@b", p.id_buku); cmd.Parameters.AddWithValue("@n", p.nama_peminjam); cmd.ExecuteNonQuery(); cmd.Dispose(); db.closeConnection(); }
        public void Kembalikan(int id) { SqlDBHelper db = new SqlDBHelper(__constr); NpgsqlCommand cmd = db.getNpgsqlCommand("UPDATE perpustakaan.peminjaman SET status='Dikembalikan', updated_at=CURRENT_TIMESTAMP WHERE id_peminjaman=@id;"); cmd.Parameters.AddWithValue("@id", id); cmd.ExecuteNonQuery(); cmd.Dispose(); db.closeConnection(); }
    }

    public class AuthContext
    {
        private string __constr; public AuthContext(string pConstr) { __constr = pConstr; }
        public void Register(UserRegister u) { SqlDBHelper db = new SqlDBHelper(__constr); NpgsqlCommand cmd = db.getNpgsqlCommand("INSERT INTO perpustakaan.users (nama, username, password) VALUES (@n, @u, @p);"); cmd.Parameters.AddWithValue("@n", u.nama); cmd.Parameters.AddWithValue("@u", u.username); cmd.Parameters.AddWithValue("@p", u.password); cmd.ExecuteNonQuery(); cmd.Dispose(); db.closeConnection(); }
        public string? Login(UserLogin u, IConfiguration cfg)
        {
            string? token = null; SqlDBHelper db = new SqlDBHelper(__constr);
            NpgsqlCommand cmd = db.getNpgsqlCommand("SELECT username, nama FROM perpustakaan.users WHERE username=@u AND password=@p;");
            cmd.Parameters.AddWithValue("@u", u.username); cmd.Parameters.AddWithValue("@p", u.password);
            NpgsqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                var creds = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(cfg["Jwt:Key"]!)), SecurityAlgorithms.HmacSha256);
                var claims = new[] { new Claim(ClaimTypes.NameIdentifier, u.username), new Claim(ClaimTypes.Name, reader["nama"].ToString()!) };
                token = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(cfg["Jwt:Issuer"], cfg["Jwt:Audience"], claims, expires: DateTime.Now.AddMinutes(60), signingCredentials: creds));
            }
            cmd.Dispose(); db.closeConnection(); return token;
        }
    }
}