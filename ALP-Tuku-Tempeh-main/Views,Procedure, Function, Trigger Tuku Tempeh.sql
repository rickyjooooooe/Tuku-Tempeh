select id_pembelian, tanggal_pembelian, total_pembelian
from pembelian
where status_del = 'F';

drop view vlappembelian;
create view vlappembelian as 
select id_pembelian as 'Id Beli', tanggal_pembelian as 'Tanggal', total_pembelian as 'Subtotal'from pembelian where status_del = 'F';
select * from vlappembelian;


drop view vlappenjualan;
create view vlappenjualan as 
select p.id_penjualan as 'ID Penjualan', c.nama_customer as 'Nama Cust', p.tanggal_penjualan as 'Tanggal', p.total_penjualan as 'Total' from penjualan p, customer c where c.status_del='F' and c.id_customer = p.id_customer;


select * from vlappenjualan;

drop view vCustomer;
create view vCustomer as 
select id_customer as 'id customer', nama_customer as 'nama'
from customer
where status_del = 'F';

select * from vCustomer;

drop view vdetailbarang;
create view vdetailbarang as 
select id_barang as 'id', nama_barang as 'nama barang', stok
from barang
where status_del = 'F';
select * from vdetailbarang;

select id_pembelian as 'id beli', tanggal_pembelian as 'tanggal', total_pembelian as 'total beli'from pembelian where status_del = 'F';

select d.id_barang as "Kode Barang", b.nama_barang as "Nama barang", d.jumlah_barangbeli as "Jumlah", d.harga_beli as "Harga", d.subtotal as "Subtotal"
from detail_pembelian d, barang b
where d.id_pembelian='PB0001' and d.status_del='F' and d.id_barang = b.id_barang and d.status_del = b.status_del;

select * from barang;
select * from pembelian;
select * from customer;
select p.id_penjualan as 'ID Penjualan', c.nama_customer as 'Nama Cust', p.tanggal_penjualan as 'Tanggal', p.total_penjualan as 'Total' from penjualan p, customer c where c.status_del='F' and c.id_customer = p.id_customer;

delimiter ++
create procedure pcostumer()
begin
select nama_customer from customer where status_del='F';
end ++
delimiter ;
call pcostumer();
drop procedure pcostumer;

select id_pembelian as 'Id Beli', tanggal_pembelian as 'Tanggal', total_pembelian as 'Subtotal' from pembelian where status_del = 'F' and tanggal_pembelian between @d1 and @d2;

-- procedure laporan penjualan berdasarkan tanggal
DROP PROCEDURE IF EXISTS plaporanpenjualanberdasarkantglaja;
Delimiter //
CREATE PROCEDURE plaporanpenjualanberdasarkantglaja (
    IN partglmulai DATE,
    IN partglakhir DATE
)
BEGIN
   select p.id_penjualan as 'ID Penjualan', c.nama_customer as 'Nama Cust', p.tanggal_penjualan as 'Tanggal', p.total_penjualan as 'Total' 
   from penjualan p, customer c 
   where c.status_del = 'F' and c.id_customer = p.id_customer and tanggal_penjualan between partglmulai and partglakhir;
END; //
DELIMITER ;
call plaporanpenjualanberdasarkantglaja ('2023-10-01', '2023-10-20');

-- laporan penjualan berdasarkan id penjualan
DROP PROCEDURE IF EXISTS plaporanpenjualanberdasarkanidpenjualan;
Delimiter //
CREATE PROCEDURE plaporanpenjualanberdasarkanidpenjualan (
    IN partglmulai DATE,
    IN partglakhir DATE,
    IN parID varchar(50)
)
BEGIN
    select p.id_penjualan as 'ID Penjualan', c.nama_customer as 'Nama Cust', p.tanggal_penjualan as 'Tanggal', p.total_penjualan as 'Total' 
from penjualan p, customer c 
where c.status_del='F' and c.id_customer = p.id_customer and tanggal_penjualan between partglmulai and partglakhir and p.id_penjualan= parID;
END; //
DELIMITER ;
call plaporanpenjualanberdasarkanidpenjualan ('2023-09-01', '2023-12-31','P00002');

DROP PROCEDURE IF EXISTS plaporanpenjualantgldancostumer;
Delimiter //
CREATE PROCEDURE plaporanpenjualantgldancostumer (
    IN partglmulai DATE,
    IN partglakhir DATE,
    IN parnamacustomer varchar(50)
)
BEGIN
    select p.id_penjualan as 'ID Penjualan', c.nama_customer as 'Nama Cust', p.tanggal_penjualan as 'Tanggal', p.total_penjualan as 'Total' 
from penjualan p, customer c 
where c.status_del='F' and c.id_customer = p.id_customer and tanggal_penjualan between partglmulai and partglakhir and c.nama_customer=parnamacustomer; 
END; //
DELIMITER ;
call plaporanpenjualantgldancostumer ('2023-09-01', '2023-12-31','John Doe');

DROP PROCEDURE IF EXISTS plaporanpenjualanberdasarkansemua;
Delimiter //
CREATE PROCEDURE plaporanpenjualanberdasarkansemua (
    IN partglmulai DATE,
    IN partglakhir DATE,
    IN parnamacustomer varchar(50),
    IN parID varchar(50)
)
BEGIN
select p.id_penjualan as 'ID Penjualan', c.nama_customer as 'Nama Cust', p.tanggal_penjualan as 'Tanggal', p.total_penjualan as 'Total' 
from penjualan p, customer c 
where c.status_del='F' and c.id_customer = p.id_customer and tanggal_penjualan between partglmulai and partglakhir and c.nama_customer=parnamacustomer and p.id_penjualan=parID;
END; //
DELIMITER ;
call plaporanpenjualanberdasarkansemua ('2023-09-01', '2023-12-31','John Doe','P00001');

-- laporan pembelian berdasarkan tanggal
DROP PROCEDURE IF EXISTS plaporanpembelianberdasarkantgl;
DELIMITER //

CREATE PROCEDURE plaporanpembelianberdasarkantgl (
    IN partglmulai DATE,
    IN partglakhir DATE
)
BEGIN
    SELECT p.id_pembelian AS 'Id Beli', p.tanggal_pembelian AS 'Tanggal', p.total_pembelian AS 'Subtotal'
    FROM pembelian p
    WHERE status_del = 'F' AND tanggal_pembelian BETWEEN partglmulai AND partglakhir;
END; //
DELIMITER ;
CALL plaporanpembelianberdasarkantgl('2023-09-01', '2023-12-31');

DROP PROCEDURE IF EXISTS plaporanpembelianberdasarkansemua;
Delimiter //
CREATE PROCEDURE plaporanpembelianberdasarkansemua (
    IN partglmulai DATE,
    IN partglakhir DATE,
    IN paridpembelian varchar(50)
)
BEGIN
    SELECT p.id_pembelian AS 'Id Beli', p.tanggal_pembelian AS 'Tanggal', p.total_pembelian AS 'Subtotal'
    FROM pembelian p
    WHERE status_del = 'F' AND id_pembelian = paridpembelian AND tanggal_pembelian BETWEEN partglmulai AND partglakhir;
END; //
DELIMITER ;
call plaporanpembelianberdasarkansemua ('2023-09-01', '2023-12-31','PB0002');

drop function if exists fGenIdcustomer;
delimiter //
create function fGenIdcustomer()
returns varchar(6)
deterministic
begin
DECLARE idbaru varCHAR(6);
select CONCAT("C", lpad(ifnull(convert(max(substr(id_customer, 6,1)), unsigned), 0) + 1, 5, '0')) into idbaru 
from customer;
return idbaru;
end //
delimiter ;
select fGenIdcustomer();

drop function if exists fGenIdpembelian;
delimiter //
create function fGenIdpembelian()
returns varchar(6)
deterministic
begin
DECLARE idbaru varCHAR(6);
select CONCAT("PB", lpad(ifnull(convert(max(substr(id_pembelian, 6,1)), unsigned), 0) + 1, 4, '0')) into idbaru 
from pembelian;
return idbaru;
end //
delimiter ;
select fGenIdpembelian();


drop function if exists fGenIdpenjualan;
delimiter //
create function fGenIdpenjualan()
returns varchar(6)
deterministic
begin
DECLARE idbaru varCHAR(6);
select CONCAT("P", lpad(ifnull(convert(max(substr(id_penjualan, 6,1)), unsigned), 0) + 1, 5, '0')) into idbaru 
from penjualan;
return idbaru;
end //
delimiter ;
select fGenIdpenjualan();

drop function if exists fGenIdbarang;
delimiter //
create function fGenIdbarang()
returns varchar(6)
deterministic
begin
DECLARE idbaru varCHAR(6);
select CONCAT("T", lpad(ifnull(convert(max(substr(id_barang, 6,1)), unsigned), 0) + 1, 5, '0')) into idbaru 
from barang;
return idbaru;
end //
delimiter ;
select fGenIdbarang();

drop trigger if exists tUpdstokpembelian;
delimiter $$
create trigger tUpdstokpembelian
after insert
on detail_pembelian
for each row
begin
update barang
set stok = stok + new.jumlah_barangbeli
where id_barang = new.id_barang;
end $$

drop trigger if exists tUpdstokpenjualan;
delimiter $$
create trigger tUpdstokpenjualan
after insert
on detail_pembelian
for each row
begin
update barang
set stok = stok + new.jumlah_barangbeli
where id_barang = new.id_barang;
end $$

drop trigger if exists tInsertCustomer;
delimiter //
create trigger tInsertCustomer
before insert
on customer
for each row
begin
	set new.id_customer = fGenIdcustomer();
    set new.nama_customer = new.nama_customer;
    set new.no_telpon = new.no_telpon;
    set new.email = new.email;
end //
delimiter ;
insert into customer (nama_customer,no_telpon, email,status_del) values('Joko',1,'S003');

