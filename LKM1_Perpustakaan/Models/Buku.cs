namespace LKM1_Perpustakaan.Models
{
    // Model ini merepresentasikan tabel 'buku' beserta nama kategorinya
    public class Buku
    {
        public int id_buku { get; set; }
        public string judul { get; set; } = string.Empty;
        public string pengarang { get; set; } = string.Empty;
        public int id_kategori { get; set; }
        public string? nama_kategori { get; set; } // Diambil dari proses JOIN tabel
    }
}