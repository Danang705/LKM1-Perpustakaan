namespace LKM1_Perpustakaan.Models
{
    public class Buku
    {
        public int id_buku { get; set; }
        public string judul { get; set; } = string.Empty;
        public string pengarang { get; set; } = string.Empty;
        public int id_kategori { get; set; }
        public string? nama_kategori { get; set; }
    }

    public class Peminjaman
    {
        public int id_peminjaman { get; set; }
        public int id_buku { get; set; }
        public string? judul_buku { get; set; }
        public string nama_peminjam { get; set; } = string.Empty;
        public string status { get; set; } = "Dipinjam";
    }

    public class UserRegister { public string nama { get; set; } = ""; public string username { get; set; } = ""; public string password { get; set; } = ""; }
    public class UserLogin { public string username { get; set; } = ""; public string password { get; set; } = ""; }
}