-- Buat database jika belum ada
CREATE DATABASE IF NOT EXISTS db_tukutempeh;
use db_tukutempeh;
drop table `customer`;
CREATE TABLE IF NOT EXISTS `customer` (
  `id_customer` varchar(6) NOT NULL,
  `nama_customer` varchar(50) NOT NULL,
  `no_telpon` varchar(13) NOT NULL,
  `email` varchar(50) NOT NULL,
  `status_del` varchar(1) not null default 'F',
  PRIMARY KEY (`id_customer`)
);

INSERT INTO customer (id_customer, nama_customer, no_telpon, email, status_del)
VALUES 
  ('C00001', 'John Doe', '0821235679876', 'johndoe@gmail.com', 'F'),
  ('C00002', 'Jane Alice', '0877564329754', 'janealice@gmail.com', 'F'),
  ('C00003', 'Bob Smith', '0812346798435', 'bobsmith@gmail.com', 'F'),
  ('C00004', 'Michael Davis', '0843332222198', 'michael@example.com', 'F'),
  ('C00005', 'Samantha Lee', '0813748907346', 'samantha@example.com', 'F'),
  ('C00006', 'UMUM', '00000000', 'umum@mail.com', 'F');



drop table `barang`;
CREATE TABLE IF NOT EXISTS`barang` (
  `id_barang` varchar(6) NOT NULL,
  `nama_barang` varchar(50) NOT NULL,
  `harga_beli` int NOT NULL,
  `harga_jual` int NOT NULL,
  `stok` int NOT NULL,
  `status_del` varchar(1) not null default 'F',
  PRIMARY KEY (`id_barang`)
);
INSERT INTO barang (id_barang, nama_barang, harga_beli, harga_jual, stok, status_del)
VALUES
 ('T00001', 'Tempeh Original 70g', 10000, 15000, 40, 'F'),
 ('T00002', 'Tempeh Original 250g', 20000, 25000, 40, 'F'),
 ('T00003', 'Tempeh Spicy Balado 70g', 10000, 15000, 24, 'F'),
 ('T00004', 'Tempeh Spicy Balado 250g', 20000, 25000, 22, 'F');
 
drop table `penjualan`;
CREATE TABLE IF NOT EXISTS `penjualan` (
  `id_penjualan` varchar(6) NOT NULL,
   `id_customer` varchar(6) REFERENCES `customer`(`id_customer`),
  `tanggal_penjualan` timestamp,
  `total_penjualan` int NOT NULL,
  `status_del` varchar(1) not null default 'F',
 
  primary key (`id_penjualan`)
);
INSERT INTO penjualan (id_penjualan, id_customer, tanggal_penjualan, total_penjualan, status_del)
VALUES
('P00001', 'C00001', '2023-10-10 10:15:50', 55000, 'F'),
('P00002', 'C00002', '2023-10-15 11:20:30', 50000, 'F'),
('P00003', 'C00003', '2023-10-18 11:32:20', 55000, 'F'),
('P00004', 'C00004', '2023-10-20 12:14:25', 15000, 'F'),
('P00005', 'C00005', '2023-10-24 12:34:56', 95000, 'F');


drop table `detail_penjualan`;
CREATE TABLE IF NOT EXISTS `detail_penjualan` (
`id_penjualan` varchar(6) REFERENCES `penjualan`(`id_penjualan`),
`id_barang` varchar(6) REFERENCES `barang`(`id_barang`),
  `jumlah_barangjual` int,
  `harga_jual` int NOT NULL,
  `subtotal` int not Null,
`status_del` varchar(1) not null default 'F'


);
INSERT INTO detail_penjualan (id_penjualan, id_barang, jumlah_barangjual, harga_jual, subtotal, status_del)
VALUES 
('P00001', 'T00001', 2, 15000, 30000, 'F'),
('P00001', 'T00004', 1, 25000, 25000, 'F'),
('P00002', 'T00002', 2, 25000, 50000, 'F'),
('P00003', 'T00001', 2, 15000, 30000, 'F'),
('P00003', 'T00003', 1, 25000, 25000, 'F'),
('P00004', 'T00001', 1, 15000, 15000, 'F'),
('P00005', 'T00002', 3, 15000, 45000, 'F'),
('P00005', 'T00004', 2, 25000, 50000, 'F');

drop table `pembelian`;
CREATE TABLE IF NOT EXISTS `pembelian` (
  `id_pembelian` varchar(6) NOT NULL,
  `tanggal_pembelian` timestamp,
  `total_pembelian` int NOT NULL,
  `status_del` varchar(1) not null default 'F',
  primary key (`id_pembelian`)
);

INSERT INTO pembelian (id_pembelian, tanggal_pembelian, total_pembelian, status_del)
VALUES 
('PB0001', '2023-09-25 14:30:00', 1500000, 'F'),
('PB0002', '2023-10-20 12:21:05', 400000, 'F');

drop table `detail_pembelian`;
CREATE TABLE IF NOT EXISTS `detail_pembelian` (
  `id_pembelian` varchar(6) REFERENCES `pembelian`(`id_pembelian`),
  `id_barang` varchar(6) REFERENCES `barang`(`id_barang`),
  `jumlah_barangbeli` int not null,
  `harga_beli` int not null,
  `subtotal` int not Null,
`status_del` varchar(1) not null default 'F'
);
INSERT INTO detail_pembelian (id_pembelian, id_barang, jumlah_barangbeli, harga_beli, subtotal, status_del)
VALUES 
('PB0001', 'T00001', 25, 10000, 250000, 'F'),
('PB0001', 'T00002', 25, 10000, 250000, 'F'),
('PB0001', 'T00003', 25, 20000, 500000, 'F'),
('PB0001', 'T00004', 25, 20000, 500000, 'F'),
('PB0002', 'T00001', 20, 10000, 200000, 'F'),
('PB0002', 'T00002', 20, 10000, 200000, 'F');
drop table `login`;
CREATE TABLE IF NOT EXISTS `login` (
`username` varchar (20) not null,
`pass` varchar (20) not null
);
INSERT INTO login
VALUES 
('Owner01','matakuadadua'),
('Kasir01','halohalobandung');


