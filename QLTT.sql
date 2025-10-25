create database QLTinTuc
use QLTinTuc
 create table TinTuc
 (
	MaTinTuc int primary key not null,
	TenTinTuc nvarchar(30)
 )

 create table LoaiTinTuc
 (
	MaLoaiTinTuc int primary key not null,
	TenLoai nvarchar(30),
	MaTinTuc int,
	constraint FK_MATINTUC_LOAITINTUC foreign key (MaTinTuc) references TinTuc(MaTinTuc)
 )

 Insert into TinTuc
 Values (01,N'Đá bóng'),
 (02,N'Giá vàng'),
 (03,N'Bầu cử');

 Insert into LoaiTinTuc
 Values (01,N'Thể thao',01),
 (02,N'Kinh tế',02),
 (03,N'Thế giới',03);