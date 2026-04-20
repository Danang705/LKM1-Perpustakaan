# REST API Sistem Manajemen Perpustakaan

## a) Deskripsi Project
Project ini adalah sebuah REST API untuk mengelola sistem perpustakaan sederhana. Domain ini dipilih karena memiliki struktur relasi data yang jelas dan operasi CRUD yang sangat umum. API ini mengelola data Kategori, Buku, dan histori Peminjaman, dilengkapi dengan fitur *Soft Delete* pada data buku.

## b) Teknologi yang Digunakan
* **Bahasa Pemrograman:** C#
* **Framework:** ASP.NET Core Web API (.NET 6/8)
* **Database:** PostgreSQL
* **Library Tambahan:** Npgsql (untuk koneksi database)

## c) Langkah Instalasi & Cara Menjalankan Project
1. Clone repositori ini ke komputer lokal.
2. Buka project menggunakan Visual Studio 2022.
3. Buka file `appsettings.json` dan sesuaikan `ConnectionStrings` dengan konfigurasi PostgreSQL lokal Anda (Host, Port, Database, Username, Password).
4. Tekan tombol `F5` atau klik icon **Run** di Visual Studio untuk menjalankan aplikasi.
5. Swagger UI akan otomatis terbuka di browser.

## d) Cara Import Database
1. Buka pgAdmin dan buat database baru (misal: `lkm1_db`).
2. Buka **Query Tool** pada database tersebut.
3. Buka file `database.sql` yang ada di repositori ini, *copy* seluruh isinya.
4. *Paste* ke Query Tool dan klik **Execute (F5)**. Schema, tabel, relasi, index, dan 5 baris sample data akan otomatis terbuat.

## e) Daftar Endpoint API (Tabel Buku)

| Method | URL | Keterangan |
| :--- | :--- | :--- |
| `GET` | `/api/Buku` | Mengambil seluruh daftar buku yang tersedia (tidak termasuk yang di-soft delete) |
| `GET` | `/api/Buku/{id}` | Mengambil detail satu buku berdasarkan ID |
| `POST` | `/api/Buku` | Menambahkan data buku baru ke database |
| `PUT` | `/api/Buku/{id}` | Memperbarui data buku yang sudah ada berdasarkan ID |
| `DELETE` | `/api/Buku/{id}` | Menghapus buku (Mengimplementasikan Soft Delete dengan mengisi kolom `deleted_at`) |

## f) Link Video Presentasi
[Tonton Video Demonstrasi API di YouTube](LINK_YOUTUBE_KAMU_DISINI)
