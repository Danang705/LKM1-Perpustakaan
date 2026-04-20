-- Buat Schema
CREATE SCHEMA IF NOT EXISTS perpustakaan;

-- Tabel 1: Kategori Buku
CREATE TABLE IF NOT EXISTS perpustakaan.kategori (
    id_kategori SERIAL PRIMARY KEY,
    nama_kategori VARCHAR(50) NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Tabel 2: Buku (Bereselasi dengan kategori)
CREATE TABLE IF NOT EXISTS perpustakaan.buku (
    id_buku SERIAL PRIMARY KEY,
    judul VARCHAR(150) NOT NULL,
    pengarang VARCHAR(100) NOT NULL,
    id_kategori INT REFERENCES perpustakaan.kategori(id_kategori),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    deleted_at TIMESTAMP NULL -- Memenuhi syarat rubrik "Soft Delete"
);

-- Tabel 3: Peminjaman (Berelasi dengan buku)
CREATE TABLE IF NOT EXISTS perpustakaan.peminjaman (
    id_peminjaman SERIAL PRIMARY KEY,
    id_buku INT REFERENCES perpustakaan.buku(id_buku),
    nama_peminjam VARCHAR(100) NOT NULL,
    status VARCHAR(20) DEFAULT 'Dipinjam',
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Mempercepat pencarian API jika user mencari buku berdasarkan judul
CREATE INDEX idx_buku_judul ON perpustakaan.buku(judul);

-- Mempercepat pencarian API jika admin ingin melihat siapa saja yang statusnya masih 'Dipinjam'
CREATE INDEX idx_peminjaman_status ON perpustakaan.peminjaman(status);


-- Insert Kategori (5 baris)
INSERT INTO perpustakaan.kategori (nama_kategori) VALUES 
('Fiksi'), 
('Sains'), 
('Sejarah'), 
('Teknologi'), 
('Biografi');

-- Insert Buku (5 baris)
INSERT INTO perpustakaan.buku (judul, pengarang, id_kategori) VALUES 
('Laskar Pelangi', 'Andrea Hirata', 1),
('Kosmos', 'Carl Sagan', 2),
('Sapiens', 'Yuval Noah Harari', 3),
('Clean Code', 'Robert C. Martin', 4),
('Steve Jobs', 'Walter Isaacson', 5);

-- Insert Peminjaman (5 baris)
INSERT INTO perpustakaan.peminjaman (id_buku, nama_peminjam, status) VALUES 
(1, 'Danang', 'Dipinjam'),
(2, 'Farell', 'Dikembalikan'),
(3, 'Tunggul', 'Dipinjam'),
(4, 'Zein', 'Dikembalikan'),
(5, 'Falah', 'Dipinjam');
