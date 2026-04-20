# REST API Sistem Manajemen Perpustakaan Plus (LKM 1 PAA)

## a) Deskripsi Project
Project ini adalah implementasi REST API untuk Sistem Manajemen Perpustakaan. Domain ini dipilih untuk mensimulasikan alur kerja perpustakaan modern yang mencakup pengelolaan katalog buku, kategori, serta transaksi peminjaman oleh petugas. API ini telah dilengkapi dengan sistem keamanan JWT (JSON Web Token) untuk membatasi akses fitur CRUD hanya kepada petugas/admin yang terdaftar.

## b) Teknologi yang Digunakan
* **Bahasa Pemrograman:** C#
* **Framework:** ASP.NET Core Web API (.NET 6/8)
* **Database:** PostgreSQL (Relasional dengan 4 Tabel)
* **Keamanan:** JWT Authentication & Parameterized Queries (Anti-SQL Injection)
* **Dokumentasi API:** Swagger UI dengan skema Otorisasi Bearer

## c) Langkah Instalasi & Cara Menjalankan Project
1. **Clone Repository:** Clone repositori ini ke direktori lokal Anda.
2. **Setup Database:** Pastikan PostgreSQL sudah berjalan, buat database baru, dan eksekusi file `database.sql` (instruksi ada di poin d).
3. **Konfigurasi:** Buka file `appsettings.json`, sesuaikan `ConnectionStrings` dan pastikan port pada bagian `Jwt` sesuai dengan `launchSettings.json` Anda.
4. **Build & Run:** Buka file `.sln` di Visual Studio 2022, lalu tekan `F5`.
5. **Akses Swagger:** Browser akan otomatis membuka halaman Swagger UI untuk pengujian API.

## d) Cara Import Database
1. Buka **pgAdmin** atau tool database PostgreSQL lainnya.
2. Buat database baru bernama `lkm1_perpustakaan` (atau nama lain).
3. Buka **Query Tool** pada database tersebut.
4. Cari file `database.sql` di root folder project ini.
5. Salin dan tempel seluruh isinya ke Query Tool, lalu klik **Execute (F5)**.
6. Database siap digunakan dengan 4 tabel (`kategori`, `buku`, `peminjaman`, `users`) dan data sampel.

## e) Daftar Endpoint Lengkap

### 🔐 Authentication (Public)
| Method | URL | Keterangan |
| :--- | :--- | :--- |
| `POST` | `/api/Auth/register` | Mendaftarkan akun petugas/admin baru |
| `POST` | `/api/Auth/login` | Login petugas untuk mendapatkan Token JWT |

### 📚 Manajemen Buku (Protected - Require Token)
| Method | URL | Keterangan |
| :--- | :--- | :--- |
| `GET` | `/api/Buku` | Mengambil seluruh daftar buku (Non-Deleted) |
| `GET` | `/api/Buku/{id}` | Mengambil detail satu buku berdasarkan ID |
| `POST` | `/api/Buku` | Menambahkan data buku baru |
| `PUT` | `/api/Buku/{id}` | Memperbarui informasi buku |
| `DELETE` | `/api/Buku/{id}` | Menghapus buku (Implementasi Soft Delete) |

### 🤝 Transaksi Peminjaman (Protected - Require Token)
| Method | URL | Keterangan |
| :--- | :--- | :--- |
| `GET` | `/api/Peminjaman` | Melihat histori seluruh peminjaman |
| `POST` | `/api/Peminjaman` | Mencatat transaksi peminjaman buku baru |
| `PUT` | `/api/Peminjaman/Kembalikan/{id}` | Mengupdate status buku menjadi "Dikembalikan" |

## f) Link Video Presentasi
[Klik di sini untuk menonton Video Presentasi LKM 1 di YouTube](https://youtu.be/h2HqCnl-O0s)
