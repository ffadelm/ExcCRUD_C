CREATE DATABASE Team8PABD1
  On Primary(Name= 'ExercisePABD',
    Filename = 'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\ExercisePABD.mdf',
    Size = 10MB, MaxSize = 20MB, FileGrowth = 5MB),
  FileGroup Exercise097_Secondary(Name = 'Exercise1Team8_Secondary',
    Filename = 'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\ExercisePABD_Secondary.ndf',
    Size = 10MB, MaxSize = 20MB, FileGrowth = 5MB)
  Log On (Name = 'Team8PABD1',
    Filename = 'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\ExercisePABD_log.ldf',
    Size = 10MB, MaxSize = 20MB, FileGrowth = 5MB)
    Go

Use Team8PABD1

Create Table Barang
(
  kd_barang varchar (10) not null Primary key,
  nama_barang varchar (20) not null,
  harga int
)

Create Table Transaksi
(
  
  tanggal date,
  waktu time,
  kd_barang varchar (10) not null Primary key,
  id_transaksi varchar (30) not null ,
  constraint id_transaksi unique (id_transaksi)

)

Drop table Transaksi
Create Table DetailBarang
(
  kd_faktur varchar (10) not null primary key,
  nama_barang varchar (20),
  harga int,
  jumlah int,
  kd_barang varchar (10)foreign key references barang (kd_barang)
)
ALTER TABLE DetailBarang
ADD id_transaksi varchar(30)

ALTER TABLE DetailBarang
ADD FOREIGN KEY (id_transaksi) REFERENCES Transaksi(id_transaksi);

insert into Barang(kd_barang, nama_barang, harga)
VALUES
('BRG_1', 'Teh Pucuk Harum', 4500),
('BRG_2', 'Bite & Bright', 7500),
('BRG_3', 'Sari Roti Kr', 5000),
('BRG_4', 'Kantong Plastik', 200);
  
ALTER TABLE Transaksi ALTER COLUMN waktu VARCHAR(5);

insert into Transaksi (tanggal,waktu,id_transaksi)
VALUES
('02-04-21', '14.41','YOG0102.05.20210402.0172')

insert into DetailBarang (kd_faktur,nama_barang,harga,jumlah)
VALUES
('KD_001','Teh Pucuk Harum','4500','1'),
('KD_002','Bite & Bright','7500','1'),
('KD_003','Sari Roti Kr','5000','1'),
('KD_004','Kantong Plastik','200','1');