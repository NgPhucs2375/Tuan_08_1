create database CSDL_QL_NhanSuN
use CSDL_QL_NhanSuN
create table Deparment(
	Deptid int primary key not null,
	NameDept nvarchar(20)
)
create table Employee(
	Id int primary key not null,
	NameEmpl nvarchar(40),
	Gender nvarchar(5),
	City nvarchar(20),
	Deptid int ,
	constraint FK_Empl foreign key(Deptid) references Deparment(Deptid)
)

insert into Deparment
Values(1,N'Khoa CNTT'),
(2, N'Khoa Toán'),
(3, N'Khoa Vật Lý'),
(4, N'Khoa Hóa'),
(5, N'Khoa Sinh');

insert into Employee
Values (1,N'Nguyen Hai Yen',N'Nam',N'TP.HCM',1),
(2, N'Tran Thi Lan', N'Nữ', N'Hà Nội', 2),
(3, N'Le Van A', N'Nam', N'Da Nang', 3),
(4, N'Pham Thi B', N'Nữ', N'TP.HCM', 1),
(5, N'Nguyen Van C', N'Nam', N'Hải Phòng', 4),
(6, N'Hoang Thi D', N'Nữ', N'Can Tho', 2),
(7, N'Tran Van E', N'Nam', N'Bình Dương', 5),
(8, N'Le Thi F', N'Nữ', N'Hà Nội', 1),
(9, N'Pham Van G', N'Nam', N'Da Nang', 3),
(10, N'Nguyen Thi H', N'Nữ', N'TP.HCM', 4),
(11, N'Tran Van I', N'Nam', N'Hải Phòng', 2),
(12, N'Le Thi J', N'Nữ', N'Can Tho', 5),
(13, N'Pham Van K', N'Nam', N'Hà Nội', 1),
(14, N'Nguyen Thi L', N'Nữ', N'TP.HCM', 2),
(15, N'Tran Van M', N'Nam', N'Da Nang', 3),
(16, N'Le Thi N', N'Nữ', N'Hải Phòng', 4),
(17, N'Pham Van O', N'Nam', N'Can Tho', 5);