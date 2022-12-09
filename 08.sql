

/* Người làm: 20120269 - Võ Văn Minh Đoàn */
CREATE DATABASE GIAONHANHANG
GO
USE GIAONHANHANG
GO


CREATE TABLE USERS
(
	Username varchar(20),
	Pass varchar(30),
	RoleName varchar(9),
	TrangThai nvarchar(20),
	PRIMARY KEY(Username)
)

CREATE TABLE NHANVIEN
(
	MaNV varchar(10),
	HoTen nvarchar(30),
	Username varchar(20),
	Primary key(MaNV),
	Foreign key(Username) references USERS(Username)
)

CREATE TABLE QUANTRI
(
	MaQT varchar(10),
	HoTen nvarchar(30),
	Username varchar(20),
	Primary key(MaQT),
	Foreign key(Username) references USERS(Username)
)

CREATE TABLE DOITAC
(
	MaDT varchar(10),
	Email varchar(30),
	NgDaiDien nvarchar(30),
	SLChiNhanh smallint,
	TenQuan nvarchar(30),
	LoaiTP nvarchar(30),
	Username varchar(20),
	Primary key(MaDT),
	Foreign key(Username) references USERS(Username)
)
SELECT MaQT FROM QUANTRI WHERE Username = 'leinea'
CREATE TABLE HOPDONG
(
	MaHD varchar(10),
	NgDaiDien nvarchar(30),
	SLChiNhanh smallint,
	SoTaiKhoan varchar(20),
	NganHang nvarchar(30),
	CNNganHang nvarchar(30),
	MaSoThue varchar(13),
	NgayKy date,
	ThoiHan nvarchar(10),
	NgayHetHan date,
	MaDT varchar(10),
	MaNV varchar(10),
	TrangThai nvarchar(10),
	Primary key(MaHD),
	Foreign key(MaDT) references DOITAC(MaDT),
	Foreign key(MaNV) references NHANVIEN(MaNV)
)

CREATE TABLE CHINHANH
(
	STT int,
	MaDT varchar(10),
	TP nvarchar(30),
	Quan nvarchar(30),
	DiaChiCuThe nvarchar(50),
	SDT	char(10),
	TinhTrang nvarchar(30),
	NgayLap date,
	Primary key(STT,MaDT),
	Foreign key(MaDT) references DOITAC(MaDT)
)

CREATE TABLE THUCPHAM
(
	MaTP varchar(10),
	MaDT varchar(10),
	TenMon nvarchar(30),
	MieuTa nvarchar(50),
	Gia decimal(10,1),
	TinhTrang nvarchar(30),
	TuyChon nvarchar(50),
	Primary key(MaTP,MaDT),
	Foreign key (MaDT) references DOITAC(MaDT)
)

CREATE TABLE KHACHHANG
(
	MaKH varchar(10),
	HoTen nvarchar(30),
	DiaChi nvarchar(100),
	SDT char(10),
	Email varchar(30),
	Username varchar(20),
	Primary key(MaKH),
	Foreign key(Username) references USERS(Username)
)

CREATE TABLE TAIXE
(
	MaTX varchar(10),
	CMND varchar(12),
	HoTen nvarchar(30),
	SDT char(10),
	DiaChi nvarchar(100),
	BienSoXe varchar(10),
	KhuVucHoatDong nvarchar(30),
	Email varchar(30),
	SoTaiKhoan varchar(20),
	NganHang nvarchar(30),
	CNNganHang nvarchar(30),
	Username varchar(20),
	Primary key(MaTX),
	Foreign key(Username) references USERS(Username)
)

CREATE TABLE DONDATHANG
(
	MaDH varchar(10),
	GioDat varchar(6),
	NgayDat date,
	GiaTriDH decimal(10,1),
	TinhTrang nvarchar(30),
	MaKH varchar(10),
	MaTX varchar(10),
	Primary key(MaDH),
	Foreign key(MaKH) references KHACHHANG(MaKH),
	Foreign key(MaTX) references TAIXE(MaTX)
)

CREATE TABLE CHITIETDONDATHANG
(
	MaDH varchar(10),
	MaTP varchar(10),
	MaDT varchar(10),
	SoLuong int,
	DanhGia nvarchar(100),
	Primary key(MaDH,MaTP,MaDT),
	Foreign key(MaDH) references DONDATHANG(MaDH),
	Foreign key(MaTP,MaDT) references THUCPHAM(MaTP,MaDT)
)

create proc ThemUser
	@Username varchar(20),
	@Pass varchar(30),
	@RoleName varchar(9)
as
begin tran ThemUser
	begin try
		if @Username='' or @Pass=''
		begin 
			print N'Thông tin trống'
			select 1 as code
			ROLLBACK TRAN ThemUser
		end
		if exists(select* from USERS where Username = @Username)
		begin
			print N'Username đã tồn tại'
			select 2 as code
			ROLLBACK TRAN ThemUser
		end
		if @RoleName != 'DoiTac' and @RoleName != 'KhachHang' and @RoleName != 'TaiXe' and @RoleName != 'NhanVien' and @RoleName != 'QuanTri'
		begin
			print N'Role name không hợp lệ!'
			select 3 as code
			ROLLBACK TRAN ThemUser
		end
		insert into USERS values(@Username,@Pass,@RoleName,N'Hoạt động')
	end try
	begin catch
		print N'Lỗi hệ thống!'
		select 4 as code
		ROLLBACK TRAN ThemUser
	END CATCH
COMMIT TRAN ThemUser
select 0 as code
GO

CREATE PROC CapNhatUser
	@Username varchar(20),
	@Pass varchar(30),
	@RoleName varchar(9)
AS
BEGIN TRAN CapNhatUser
	BEGIN TRY
		IF NOT EXISTS (SELECT * FROM USERS WHERE Username = @Username)
		BEGIN
			Print N'Username không tồn tại!'
			Select 1
			ROLLBACK TRAN CapNhatUser
		END

		Update USERS
		SET Pass = @Pass, RoleName = @RoleName
		where Username = @Username
	END TRY
	BEGIN CATCH
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN CapNhatUser
	END CATCH
COMMIT TRAN CapNhatUser
Select 0
GO



CREATE PROC LoginUser
	@Username varchar(20),
	@Pass varchar(30)
AS
BEGIN TRAN
	BEGIN TRY
		IF NOT EXISTS (SELECT * FROM USERS WHERE Username = @Username AND Pass = @Pass)
		BEGIN
			Print N'Sai tên đăng nhập hoặc mật khẩu!'
			SELECT 1
			ROLLBACK TRAN
		END
		IF (SELECT TrangThai FROM USERS WHERE Username = @Username) = N'Bị khóa'
		BEGIN
			Print N'Tài khoản bị khóa!'
			SELECT 2
			ROLLBACK TRAN
		END
		
	END TRY
	BEGIN CATCH
		print N'Lỗi hệ thống!'
		SELECT 3
		ROLLBACK TRAN
	END CATCH
COMMIT TRAN
SELECT 0
GO
select * from USERS

CREATE PROC Block_Unblock
	@Username varchar(20)
AS
BEGIN TRAN Block_Unblock
	BEGIN TRY
		IF NOT EXISTS (SELECT * FROM USERS WHERE Username = @Username)
		BEGIN
			Print @Username + N' không tồn tại!'
			SELECT 1
			ROLLBACK TRAN Block_Unblock
		END
		IF (SELECT TrangThai FROM USERS WHERE Username = @Username) = N'Bị khóa'
		BEGIN
			UPDATE USERS
			SET TrangThai = N'Hoạt động'
			WHERE Username = @Username
			SELECT 2
		END
		ELSE IF (SELECT TrangThai FROM USERS WHERE Username = @Username) = N'Hoạt động'
		BEGIN
			UPDATE USERS
			SET TrangThai = N'Bị khóa'
			WHERE Username = @Username
			SELECT 3
		END
	END TRY
	BEGIN CATCH
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN Block_Unblock
	END CATCH
COMMIT TRAN Block_Unblock
GO

CREATE PROC TimKiemUser
	@Username varchar(20)
AS
BEGIN TRAN
	BEGIN TRY
		IF NOT EXISTS (SELECT * FROM USERS WHERE Username = @Username)
		BEGIN
			Print @Username + N' không tồn tại!'
			ROLLBACK TRAN
		END
		Select * from USERS where Username = @Username
	END TRY
	BEGIN CATCH
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
	END CATCH
COMMIT TRAN
GO
select * from USERS

CREATE PROC TimKiemNhanVien
	@Username varchar(20)
AS
BEGIN TRAN TimKiemNhanVien
	BEGIN TRY
		IF NOT EXISTS (SELECT * FROM NHANVIEN WHERE Username = @Username)
		BEGIN
			Print @Username + N' không tồn tại!'
			ROLLBACK TRAN TimKiemNhanVien
		END
		SELECT * FROM NHANVIEN WHERE Username = @Username
	END TRY
	BEGIN CATCH
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN TimKiemNhanVien
	END CATCH
COMMIT TRAN TimKiemNhanVien
GO

create proc ThemNhanVien
	@MaNV varchar(10),
	@HoTen nvarchar(30),
	@Username varchar(20)
as
begin tran ThemNhanVien
	begin try
		if @MaNV='' or @HoTen=''
		begin 
			print N'Thông tin trống'
			Select 1
			rollback tran ThemNhanVien
			
		end
		if exists(select* from NHANVIEN where MaNV=@MaNV)
		begin
			print N'Mã nhân viên đã tồn tại'
			Select 2
			rollback tran ThemNhanVien
			
		end
		insert into NhanVien values(@MaNV,@HoTen,@Username)
	end try
	begin catch
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN ThemNhanVien
	END CATCH
COMMIT TRAN ThemNhanVien
Select 0
GO

Exec ThemNhanVien '0001',N'Lê Long', null

Exec ThemNhanVien '0001',N'Lê Long', null

Exec ThemNhanVien '',N'', null

Exec ThemNhanVien '0002',N'Hoàng Xuân Huấn', null

Exec ThemNhanVien '0004',N'Nguyễn Bách Khá', null

Exec ThemNhanVien '0005',N'Phạm Lê Phú', null

CREATE PROC CapNhatNhanVien
	@MaNV varchar(10),
	@HoTen nvarchar(30),
	@Username varchar(20)
AS
BEGIN TRAN CapNhatNhanVien
	BEGIN TRY
		DECLARE @OldUsername varchar(20)
		SET @OldUsername = (Select Username from NHANVIEN where MaNV = @MaNV)
		IF NOT EXISTS (SELECT * FROM NHANVIEN WHERE MaNV = @MaNV)
		BEGIN
			Print N'Nhân viên không tồn tại!'
			Select 1
			ROLLBACK TRAN CapNhatNhanVien
		END
		IF EXISTS (SELECT * FROM USERS WHERE Username != @OldUsername AND Username = @Username)
		BEGIN
			Print N'Username mới đã tồn tại!'
			Select 2
			ROLLBACK TRAN CapNhatNhanVien
		END
		Update NHANVIEN
		SET Username = null
		where MaNV = @MaNV
		Update USERS
		SET Username = @Username
		where Username = @OldUsername
		Update NHANVIEN
		SET HoTen = @HoTen, Username = @Username
		where MaNV = @MaNV
	END TRY
	BEGIN CATCH
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN CapNhatNhanVien
	END CATCH
COMMIT TRAN CapNhatNhanVien
Select 0
GO

select * from NHANVIEN
CREATE PROC TimKiemQuanTri
	@Username varchar(20)
AS
BEGIN TRAN TimKiemQuanTri
	BEGIN TRY
		IF NOT EXISTS (SELECT * FROM QUANTRI WHERE Username = @Username)
		BEGIN
			Print @Username + N' không tồn tại!'
			ROLLBACK TRAN TimKiemQuanTri
		END
		SELECT * FROM QUANTRI WHERE Username = @Username
	END TRY
	BEGIN CATCH
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN TimKiemQuanTri
	END CATCH
COMMIT TRAN TimKiemQuanTri
GO

create proc ThemQuanTri
	@MaQT varchar(10),
	@HoTen nvarchar(30),
	@Username varchar(20)
as
begin tran ThemQuanTri
	begin try
		if @MaQT='' or @HoTen=''
		begin 
			print N'Thông tin trống'
			Select 1
			rollback tran ThemQuanTri
			
		end
		if exists(select* from QUANTRI where MaQT=@MaQT)
		begin
			print N'Mã quản trị viên đã tồn tại'
			Select 2
			rollback tran ThemQuanTri
			
		end
		insert into QUANTRI values(@MaQT,@HoTen,@Username)
	end try
	begin catch
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN ThemQuanTri
	END CATCH
COMMIT TRAN ThemQuanTri
Select 0
GO

CREATE PROC CapNhatQuanTri
	@MaQT varchar(10),
	@HoTen nvarchar(30),
	@Username varchar(20)
AS
BEGIN TRAN CapNhatQuanTri
	BEGIN TRY
		DECLARE @OldUsername varchar(20)
		SET @OldUsername = (Select Username from QUANTRI where MaQT = @MaQT)
		IF NOT EXISTS (SELECT * FROM QUANTRI WHERE MaQT = @MaQT)
		BEGIN
			Print N'Quản trị viên không tồn tại!'
			Select 1
			ROLLBACK TRAN CapNhatQuanTri
		END
		IF EXISTS (SELECT * FROM USERS WHERE Username != @OldUsername AND Username = @Username)
		BEGIN
			Print N'Username mới đã tồn tại!'
			Select 2
			ROLLBACK TRAN CapNhatQuanTri
		END
		Update QUANTRI
		SET Username = null
		where MaQT = @MaQT
		Update USERS
		SET Username = @Username
		where Username = @OldUsername
		Update QUANTRI
		SET HoTen = @HoTen, Username = @Username
		where MaQT = @MaQT
	END TRY
	BEGIN CATCH
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN CapNhatQuanTri
	END CATCH
COMMIT TRAN CapNhatQuanTri
Select 0
GO


CREATE PROC DoiMatKhau
	@Username varchar(20),
	@OldPass varchar(30),
	@NewPass varchar(30)
AS
BEGIN TRAN DoiMatKhau
	BEGIN TRY
		IF NOT EXISTS (SELECT * FROM USERS WHERE Username = @Username)
		BEGIN
			Print N'Username không tồn tại!'
			Select 1
			ROLLBACK TRAN DoiMatKhau
		END
		IF EXISTS (SELECT * FROM USERS WHERE Username = @Username AND Pass != @OldPass)
		BEGIN
			Print N'Sai mật khẩu!'
			Select 2
			ROLLBACK TRAN DoiMatKhau
		END
		Update USERS
		SET Pass = @NewPass
		where Username = @Username
		
	END TRY
	BEGIN CATCH
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN DoiMatKhau
	END CATCH
COMMIT TRAN DoiMatKhau
Select 0
GO

CREATE PROC DuyetHopDong
	@MaHD varchar(10),
	@ThoiHan nvarchar(10),
	@NgayHetHan date
AS
BEGIN TRAN DuyetHopDong
	BEGIN TRY
		IF NOT EXISTS (SELECT * FROM HOPDONG WHERE MaHD = @MaHD)
		BEGIN
			Print @MaHD + N' không tồn tại!'
			select 1
			ROLLBACK TRAN DuyetHopDong
		END
		IF (SELECT TrangThai FROM HOPDONG WHERE MaHD = @MaHD) = N'Đã duyệt'
		BEGIN
			Print @MaHD + N' đã được duyệt!'
			select 2
			ROLLBACK TRAN DuyetHopDong
		END
		update HOPDONG
		set TrangThai = N'Đã duyệt', NgayKy = GETDATE(), ThoiHan = @ThoiHan, NgayHetHan = @NgayHetHan
		where MaHD = @MaHD
	END TRY
	BEGIN CATCH
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN DuyetHopDong
	END CATCH
COMMIT TRAN DuyetHopDong
select 0
GO


CREATE PROC TimKiemDoiTac
	@MaDT varchar(10)
AS
BEGIN TRAN
	BEGIN TRY
		IF NOT EXISTS (SELECT * FROM DOITAC WHERE MaDT = @MaDT)
		BEGIN
			Print @MaDT + N' không tồn tại!'
			ROLLBACK TRAN
		END
		SELECT * FROM DOITAC WHERE MaDT = @MaDT
	END TRY
	BEGIN CATCH
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
	END CATCH
COMMIT TRAN
GO

CREATE PROC TimKiemDoiTac_Username
	@Username varchar(20)
AS
BEGIN TRAN
	BEGIN TRY
		IF NOT EXISTS (SELECT * FROM DOITAC WHERE Username = @Username)
		BEGIN
			Print N'Username không tồn tại!'
			ROLLBACK TRAN
		END
		SELECT * FROM DOITAC WHERE Username = @Username
	END TRY
	BEGIN CATCH
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
	END CATCH
COMMIT TRAN
GO

select * from DOITAC
exec TimKiemDoiTac '0002'

CREATE PROC TimKiemHopDong
	@MaHD varchar(10)
AS
BEGIN TRAN TimKiemHopDong
	BEGIN TRY
		IF NOT EXISTS (SELECT * FROM HOPDONG WHERE MaHD = @MaHD)
		BEGIN
			Print @MaHD + N' không tồn tại!'
			ROLLBACK TRAN TimKiemHopDong
		END
		SELECT * FROM HOPDONG WHERE MaHD = @MaHD
	END TRY
	BEGIN CATCH
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN TimKiemHopDong
	END CATCH
COMMIT TRAN TimKiemHopDong
GO

create proc XoaUser
	@Username varchar(10)
as
begin tran
	set tran isolation level serializable
	-- Kiểm tra thông tin nhập không được rỗng.
	if (@Username = '')
	begin
		print N'Thông tin nhập không được rỗng'
		rollback tran
		return 1
	end
	-- Kiểm tra tồn tại
	if not exists(select * from DOITAC where MaDT = @MaDT)
	begin
		print N'Không thể xoá, đối tác không tồn tại'
		rollback tran
		return 1
	end
	delete from DOITAC where MaDT = @MaDT
commit tran
return 0
go


--Conversion Deadlock
--Trường hợp lỗi
--T1
create proc capNhatDoiTac
	@MaDT varchar(10),
	@Email varchar(30),
	@NgDaiDien nvarchar(30),
	@SLChiNhanh smallint,
	@TenQuan nvarchar(30),
	@LoaiTP nvarchar(30)
as
begin tran capNhatDoiTac
	set tran isolation level serializable
	-- Kiểm tra thông tin rỗng
	if (@MaDT='' or @Email='' or @NgDaiDien=''  or @TenQuan='' or @LoaiTP='')
	begin 
		print N'Thông tin trống'
		rollback tran capNhatDoiTac
		select 1
	end
	-- Kiểm tra mã đối tác đã tồn tại chưa
	if not exists(select* from DoiTac where MaDT=@MaDT)
	begin
		print N'Mã đối tác chưa tồn tại'
		rollback tran capNhatDoiTac
		select 2
	end
	waitfor delay '0:0:10'
	update DOITAC
	set Email = @Email,NgDaiDien = @NgDaiDien,SLChiNhanh = @SLChiNhanh,TenQuan = @TenQuan,LoaiTP = @LoaiTP
	where MaDT = @MaDT
commit tran capNhatDoiTac
select 0
go
select * from DOITAC

--T2
create proc xoaDoiTac
	@MaDT varchar(10)
as
begin tran xoaDoiTac
	set tran isolation level serializable
	-- Kiểm tra thông tin nhập không được rỗng.
	if (@MaDT = '')
	begin
		print N'Thông tin nhập không được rỗng'
		rollback tran xoaDoiTac
		select 1
	end
	-- Kiểm tra tồn tại
	if not exists(select * from DOITAC where MaDT = @MaDT)
	begin
		print N'Không thể xoá, đối tác không tồn tại'
		rollback tran xoaDoiTac
		select 2
	end
	delete from DOITAC where MaDT = @MaDT
commit tran xoaDoiTac
select 0
go

drop proc capNhatDoiTac
drop proc xoaDoiTac
--Sửa lỗi
--T1
create proc capNhatDoiTac
	@MaDT varchar(10),
	@Email varchar(30),
	@NgDaiDien nvarchar(30),
	@SLChiNhanh smallint,
	@TenQuan nvarchar(30),
	@LoaiTP nvarchar(30)
as
begin tran capNhatDoiTac
	set tran isolation level serializable
	-- Kiểm tra thông tin rỗng
	if (@MaDT='' or @Email='' or @NgDaiDien=''  or @TenQuan='' or @LoaiTP='')
	begin 
		print N'Thông tin trống'
		rollback tran capNhatDoiTac
		return 1
	end
	-- Kiểm tra mã đối tác đã tồn tại chưa
	-- Thiết lập update lock thay vì shared lock khi đọc
	if not exists(select* from DoiTac with (updlock) where MaDT=@MaDT)
	begin
		print N'Mã đối tác chưa tồn tại'
		rollback tran capNhatDoiTac
		return 1
	end
	waitfor delay '0:0:10'
	update DOITAC
	set Email = @Email,NgDaiDien = @NgDaiDien,SLChiNhanh = @SLChiNhanh,TenQuan = @TenQuan,LoaiTP = @LoaiTP
	where MaDT = @MaDT
commit tran capNhatDoiTac
return 0
go

--T2
create proc xoaDoiTac
	@MaDT varchar(10)
as
begin tran xoaDoiTac
	set tran isolation level serializable
	-- Kiểm tra thông tin nhập không được rỗng.
	if (@MaDT = '')
	begin
		print N'Thông tin nhập không được rỗng'
		rollback tran xoaDoiTac
		return 1
	end
	-- Kiểm tra tồn tại
	-- Thiết lập update lock thay vì shared lock khi đọc
	if not exists(select * from DOITAC with (updlock) where MaDT = @MaDT)
	begin
		print N'Không thể xoá, đối tác không tồn tại'
		rollback tran xoaDoiTac
		return 1
	end
	delete from DOITAC where MaDT = @MaDT
commit tran xoaDoiTac
return 0
go

--Dirty Read
--Trường hợp lỗi
--T1
create proc themKhachHang
	@MaKH varchar(10),
	@HoTen nvarchar(30),
	@DiaChi nvarchar(100),
	@SDT char(10),
	@Email varchar(30),
	@Username VARCHAR(20)
as
begin tran themKhachHang
	set tran isolation level read uncommitted
	-- Kiểm tra thông tin rỗng
	if (@MaKH='' or @HoTen='' or @DiaChi='' or @SDT='' or @Email='' OR @Username='')
	begin 
		print N'Thông tin trống'
		SELECT 1
		rollback tran themKhachHang
	end
	-- Kiểm tra mã khách hàng đã tồn tại chưa
	if exists(select* from KHACHHANG where MaKH=@MaKH)
	begin 
		print N'Mã khách hàng đã tồn tại'
		SELECT 2
		rollback tran themKhachHang
	end

	-- Kiểm tra số điện thoại bị trùng
	if exists(select * from KHACHHANG where SDT = @SDT and MaKH != @MaKH)
	begin
		print N'Số điện thoại bị trùng!'
		SELECT 3
		rollback tran themKhachHang
	END
    	insert into KHACHHANG values(@MaKH,@HoTen,@DiaChi,@SDT,@Email,@Username)
	waitfor delay '0:0:10'
COMMIT TRAN themKhachHang
SELECT 0
GO

--T2
CREATE PROC timKiemKhachHang
	@MaKH varchar(10)
AS
BEGIN TRAN timKiemKhachHang
	set tran isolation level read uncommitted
	IF NOT EXISTS (SELECT * FROM KHACHHANG WHERE MaKH = @MaKH)
	BEGIN
		Print @MaKH + N' không tồn tại!'
		ROLLBACK TRAN timKiemKhachHang
		Select 1
	END
	SELECT * FROM KHACHHANG WHERE MaKH = @MaKH
COMMIT TRAN timKiemKhachHang
Select 0
GO
drop proc timKiemKhachHang
drop proc themKhachHang
--Sửa lỗi
--T1
create proc themKhachHang
	@MaKH varchar(10),
	@HoTen nvarchar(30),
	@DiaChi nvarchar(100),
	@SDT char(10),
	@Email varchar(30),
	@Username VARCHAR(20)
as
begin tran themKhachHang
	set tran isolation level read committed
	-- Kiểm tra thông tin rỗng
	if (@MaKH='' or @HoTen='' or @DiaChi='' or @SDT='' or @Email='' OR @Username='')
	begin 
		print N'Thông tin trống'
		SELECT 1
		rollback tran themKhachHang
	end
	-- Kiểm tra mã khách hàng đã tồn tại chưa
	if exists(select* from KHACHHANG where MaKH=@MaKH)
	begin 
		print N'Mã khách hàng đã tồn tại'
		SELECT 2
		rollback tran themKhachHang
	end

	-- Kiểm tra số điện thoại bị trùng
	if exists(select * from KHACHHANG where SDT = @SDT and MaKH != @MaKH)
	begin
		print N'Số điện thoại bị trùng!'
		SELECT 3
		rollback tran themKhachHang
	END
    	insert into KHACHHANG values(@MaKH,@HoTen,@DiaChi,@SDT,@Email,@Username)
	waitfor delay '0:0:10'
COMMIT TRAN themKhachHang
SELECT 0
GO

--T2
CREATE PROC timKiemKhachHang
	@MaKH varchar(10)
AS
BEGIN TRAN timKiemKhachHang
	set tran isolation level read committed
	IF NOT EXISTS (SELECT * FROM KHACHHANG WHERE MaKH = @MaKH)
	BEGIN
		Print @MaKH + N' không tồn tại!'
		ROLLBACK TRAN timKiemKhachHang
		Select 1
	END
	SELECT * FROM KHACHHANG WHERE MaKH = @MaKH
COMMIT TRAN timKiemKhachHang
Select 0
GO
--END--

select * from KHACHHANG
