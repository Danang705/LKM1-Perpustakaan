-- 1. Buat Schema
CREATE SCHEMA IF NOT EXISTS perpustakaan;

-- 2. DDL (CREATE TABLE)
CREATE TABLE IF NOT EXISTS perpustakaan.kategori (
    id_kategori SERIAL PRIMARY KEY,
    nama_kategori VARCHAR(50) NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS perpustakaan.buku (
    id_buku SERIAL PRIMARY KEY,
    judul VARCHAR(150) NOT NULL,
    pengarang VARCHAR(100) NOT NULL,
    id_kategori INT REFERENCES perpustakaan.kategori(id_kategori),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    deleted_at TIMESTAMP NULL
);

CREATE TABLE IF NOT EXISTS perpustakaan.peminjaman (
    id_peminjaman SERIAL PRIMARY KEY,
    id_buku INT REFERENCES perpustakaan.buku(id_buku),
    nama_peminjam VARCHAR(100) NOT NULL,
    status VARCHAR(20) DEFAULT 'Dipinjam',
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS perpustakaan.users (
    id_user SERIAL PRIMARY KEY,
    nama VARCHAR(100) NOT NULL,
    username VARCHAR(50) UNIQUE NOT NULL,
    password VARCHAR(100) NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- 3. INDEXES (Mempercepat Pencarian)
CREATE INDEX idx_buku_judul ON perpustakaan.buku(judul);
CREATE INDEX idx_peminjaman_status ON perpustakaan.peminjaman(status);

-- 4. INSERT SAMPLE DATA (Minimal 5 baris)
INSERT INTO perpustakaan.kategori (nama_kategori) VALUES ('Fiksi'), ('Sains'), ('Sejarah'), ('Teknologi'), ('Biografi');

INSERT INTO perpustakaan.buku (judul, pengarang, id_kategori) VALUES 
('Laskar Pelangi', 'Andrea Hirata', 1), ('Kosmos', 'Carl Sagan', 2),
('Sapiens', 'Yuval Noah Harari', 3), ('Clean Code', 'Robert C. Martin', 4),
('Steve Jobs', 'Walter Isaacson', 5);

INSERT INTO perpustakaan.peminjaman (id_buku, nama_peminjam, status) VALUES 
(1, 'Danang', 'Dipinjam'), (2, 'Farell', 'Dikembalikan'),
(3, 'Tunggul', 'Dipinjam'), (4, 'Zein', 'Dikembalikan'), (5, 'Falah', 'Dipinjam');

-- Insert 1 Admin User (password: admin123)
INSERT INTO perpustakaan.users (nama, username, password) VALUES ('Admin Perpustakaan', 'admin', 'admin123');
