create database QuanLyQuanCF
go

use QuanLyQuanCF
go

create table NhanVien(
	maNhanVien char(10) primary key,
	hoNhanVien nvarchar(150),
	tenNhanVien nvarchar(50),
	soDienThoai char(10),
	ngaySinh date,
	phai bit,
	cMND char(12),
	thuongTru nvarchar(250),
	tamTru nvarchar(250),
	ngayVaoLam date,
	urlAnh nvarchar(100),
	maLoaiNhanVien char(10)
)
go

create table LoaiNhanVien(
	maLoaiNhanVien char(10) primary key,
	tenLoai nvarchar(150),
	luongCoBan float
)
go

create table SanPham(
	maSanPham char(10) primary key,
	tenSanPham nvarchar(150),
	donViTinh nvarchar(50),
	maLoaiSanPham char(10)
)