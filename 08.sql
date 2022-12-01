/*		Tạo CSDL
Người làm: 20120269 - Võ Văn Minh Đoàn */
CREATE DATABASE GIAONHANHANG
GO
USE GIAONHANHANG
GO


CREATE TABLE USERS
(
	Username varchar(20),
	Pass varchar(30),
	RoleName varchar(9)
	PRIMARY KEY(Username)
)

CREATE TABLE NHANVIEN
(
	MaNV varchar(10),
	HoTen nvarchar(30),
	Primary key(MaNV)
)

CREATE TABLE DOITAC
(
	MaDT varchar(10),
	Email varchar(30),
	NgDaiDien nvarchar(30),
	SLChiNhanh smallint,
	TenQuan nvarchar(30),
	LoaiTP nvarchar(30),
	Primary key(MaDT)
)

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
	MaNV varchar(10)
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
	Primary key(MaKH)
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
	Primary key(MaTX)
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

CREATE PROC LoginUser
	@Username varchar(20),
	@Pass varchar(30)
AS
BEGIN TRAN
	BEGIN TRY
		IF NOT EXISTS (SELECT * FROM USERS WHERE Username = @Username AND Pass = @Pass)
		BEGIN
			Print N'Sai tên đăng nhập hoặc mật khẩu!'
			SELECT 1 AS code
			ROLLBACK TRAN
		END
		
	END TRY
	BEGIN CATCH
		print N'Lỗi hệ thống!'
		SELECT 1 AS code
		ROLLBACK TRAN
	END CATCH
COMMIT TRAN
SELECT 0 AS code
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
		SELECT * FROM USERS WHERE Username = @Username
	END TRY
	BEGIN CATCH
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
	END CATCH
COMMIT TRAN
GO

CREATE PROC TimKiemNhanVien
	@MaNV varchar(10)
AS
BEGIN TRAN
	BEGIN TRY
		IF NOT EXISTS (SELECT * FROM NHANVIEN WHERE MaNV = @MaNV)
		BEGIN
			Print @MaNV + N' không tồn tại!'
			ROLLBACK TRAN
		END
		SELECT * FROM NHANVIEN WHERE MaNV = @MaNV
	END TRY
	BEGIN CATCH
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
	END CATCH
COMMIT TRAN
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

CREATE PROC TimKiemHopDong
	@MaHD varchar(10)
AS
BEGIN TRAN
	BEGIN TRY
		IF NOT EXISTS (SELECT * FROM HOPDONG WHERE MaHD = @MaHD)
		BEGIN
			Print @MaHD + N' không tồn tại!'
			ROLLBACK TRAN
		END
		SELECT * FROM HOPDONG WHERE MaHD = @MaHD
	END TRY
	BEGIN CATCH
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
	END CATCH
COMMIT TRAN
GO

CREATE PROC TimKiemChiNhanh
	@STT int,
	@MaDT varchar(10)
AS
BEGIN TRAN
	BEGIN TRY
		IF NOT EXISTS (SELECT * FROM CHINHANH WHERE STT = @STT AND MaDT = @MaDT)
		BEGIN
			Print N'STT: ' + @STT + N', MaDT: ' + @MaDT + N' không tồn tại!'
			ROLLBACK TRAN
		END
		SELECT * FROM CHINHANH WHERE STT = @STT AND MaDT = @MaDT
	END TRY
	BEGIN CATCH
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
	END CATCH
COMMIT TRAN
GO

CREATE PROC TimKiemThucPham
	@MaTP varchar(10),
	@MaDT varchar(10)
AS
BEGIN TRAN
	BEGIN TRY
		IF NOT EXISTS (SELECT * FROM THUCPHAM WHERE MaTP = @MaTP AND MaDT = @MaDT)
		BEGIN
			Print N'MaTP: ' + @MaTP + N', MaDT: ' + @MaDT + N' không tồn tại!'
			ROLLBACK TRAN
		END
		SELECT * FROM THUCPHAM WHERE MaTP = @MaTP AND MaDT = @MaDT
	END TRY
	BEGIN CATCH
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
	END CATCH
COMMIT TRAN
GO

CREATE PROC TimKiemKhachHang
	@MaKH varchar(10)
AS
BEGIN TRAN
	BEGIN TRY
		IF NOT EXISTS (SELECT * FROM KHACHHANG WHERE MaKH = @MaKH)
		BEGIN
			Print @MaKH + N' không tồn tại!'
			ROLLBACK TRAN
		END
		SELECT * FROM KHACHHANG WHERE MaKH = @MaKH
	END TRY
	BEGIN CATCH
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
	END CATCH
COMMIT TRAN
GO

CREATE PROC TimKiemTaiXe
	@MaTX varchar(10)
AS
BEGIN TRAN
	BEGIN TRY
		IF NOT EXISTS (SELECT * FROM TAIXE WHERE MaTX = @MaTX)
		BEGIN
			Print @MaTX + N' không tồn tại!'
			ROLLBACK TRAN
		END
		SELECT * FROM TAIXE WHERE MaTX = @MaTX
	END TRY
	BEGIN CATCH
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
	END CATCH
COMMIT TRAN
GO

CREATE PROC TimKiemDonDatHang
	@MaDH varchar(10)
AS
BEGIN TRAN
	BEGIN TRY
		IF NOT EXISTS (SELECT * FROM DONDATHANG WHERE MaDH = @MaDH)
		BEGIN
			Print @MaDH + N' không tồn tại!'
			ROLLBACK TRAN
		END
		SELECT * FROM DONDATHANG WHERE MaDH = @MaDH
	END TRY
	BEGIN CATCH
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
	END CATCH
COMMIT TRAN
GO

CREATE PROC TimKiemChiTietDonDatHang
	@MaDH varchar(10),
	@MaTP varchar(10),
	@MaDT varchar(10)
AS
BEGIN TRAN
	BEGIN TRY
		IF NOT EXISTS (SELECT * FROM CHITIETDONDATHANG WHERE MaDH = @MaDH AND MaTP = @MaTP AND MaDT = @MaDT)
		BEGIN
			Print N'MaTP: ' + @MaTP + N', MaDT: ' + @MaDT + N' không tồn tại!'
			ROLLBACK TRAN
		END
		SELECT * FROM CHITIETDONDATHANG WHERE MaDH = @MaDH AND MaTP = @MaTP AND MaDT = @MaDT
	END TRY
	BEGIN CATCH
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
	END CATCH
COMMIT TRAN
GO

/* Tạo dữ liệu cho CSDL
Người làm: 20120592 - Lê Minh Tiến*/

create proc ThemUser
	@Username varchar(20),
	@Pass varchar(30),
	@RoleName varchar(9)
as
	begin tran
		begin try
			if @Username='' or @Pass=''
			begin 
				print N'Thông tin trống'
				rollback tran
				return 1
			end
			if exists(select* from USERS where Username = @Username)
			begin
				print N'Username đã tồn tại'
				rollback tran
				return 1
			end
			if @RoleName != 'DoiTac' and @RoleName != 'KhachHang' and @RoleName != 'TaiXe' and @RoleName != 'NhanVien' and @RoleName != 'QuanTri'
			begin
				print N'Role name không hợp lệ!'
				rollback tran
				return 1
			end
			insert into USERS values(@Username,@Pass,@RoleName)
		end try
		begin catch
			print N'Lỗi hệ thống!'
			ROLLBACK TRAN
			RETURN 1
		END CATCH
COMMIT TRAN
RETURN 0
GO

exec ThemUser 'vvmd','123456', 'QuanTri'
exec ThemUser 'lmt','123456', 'NhanVien'
exec ThemUser 'nhd','123456', 'DoiTac'
exec ThemUser 'mqv','123456', 'KhachHang'
--Thêm proc
--1. Thêm nhân viên
--Mô tả:
--input: thông tin 1 nhân viên
--output: 0- thành công 1- thêm không thành công
--Kiểm tra thông tin nhập không được rỗng
--Kiểm tra mã nhân viên thêm vào đã tồn tại
create proc sp_Them_Nhan_Vien
	@Ma varchar(10),
	@HoTen nvarchar(30)
as
	begin tran
		begin try
			if @Ma='' or @HoTen=''
			begin 
				print N'Thông tin trống'
				rollback tran
				return 1
			end
			if exists(select* from NHANVIEN where MaNV=@Ma)
			begin
				print N'Mã nhân viên đã tồn tại'
				rollback tran
				return 1
			end
			insert into NhanVien values(@Ma,@HoTen)
		end try
		begin catch
			print N'Lỗi hệ thống!'
			ROLLBACK TRAN
			RETURN 1
		END CATCH
COMMIT TRAN
RETURN 0
GO
Exec sp_Them_Nhan_Vien '0001',N'Lê Long'

Exec sp_Them_Nhan_Vien '0001',N'Lê Long'

Exec sp_Them_Nhan_Vien '',N''

Exec sp_Them_Nhan_Vien '0002',N'Hoàng Xuân Huấn'

Exec sp_Them_Nhan_Vien '0004',N'Nguyễn Bách Khá'

Exec sp_Them_Nhan_Vien '0005',N'Phạm Lê Phú'

--2. Thêm 1 đôí tác
--Mô tả:
--input: các thông tin của đối tác (khi thêm vào số lượng chi nhánh =0)
--output: 0- thêm thành công, 1- thêm thất bại
--kiểm tra các thông tin có trống
--Kiểm tra mã đối tác có bị trùng
--Kiểm tra số lượng chi nhánh có hợp lệ (phải bằng 0)
create proc sp_Them_Doi_Tac
	@MaDT varchar(10),
	@Email varchar(30),
	@NgDaiDien nvarchar(30),
	@SLChiNhanh smallint,
	@TenQuan nvarchar(30),
	@LoaiTP nvarchar(30)
as 
	begin tran
		begin try
			if @MaDT='' or @Email='' or @NgDaiDien=''  or @TenQuan='' or @LoaiTP=''
			begin 
				print N'Thông tin trống'
				rollback tran
				return 1
			end
			if exists(select* from DoiTac where MaDT=@MaDT)
			begin
				print N'Mã đối tác đã tồn tại'
				rollback tran
				return 1
			end
			if @SLChiNhanh<>0
			begin
				print N'Số lượng chi nhánh không hợp lệ'
				rollback tran
				return 1
			end
			insert into DoiTac values(@MaDT,@Email,@NgDaiDien,@SLChiNhanh,@TenQuan,@LoaiTP)
		end try
		begin catch
			print N'Lỗi hệ thống!'
			ROLLBACK TRAN
			RETURN 1
		END CATCH
COMMIT TRAN
RETURN 0
GO

Exec sp_Them_Doi_Tac '0001','0001@gmail.com',N'Lee Chong Way',0,N'Cafe Hai Len',N'Cà phê'
Exec sp_Them_Doi_Tac '0002','0002@gmail.com',N'Lê Nguyên Vũ',0,N'Trà sữa KoKo',N'Trà sữa'
Exec sp_Them_Doi_Tac '0003','0003@gmail.com',N'Trần Uy',0,N'Bánh mì Ngon',N'Bánh mì'
Exec sp_Them_Doi_Tac '0004','0004@gmail.com',N'Ngô Văn Quyền',0,N'Cơm Ngô Quyền',N'Cơm'
Exec sp_Them_Doi_Tac '0005','0005@gmail.com',N'Nguyên Văn Kí',0,N'Cơm Nguyên Kí',N'Cơm'


--3. Thêm hợp đồng
--Mô tả:
--input: thông tin của hợp đồng
--output: 0- thành công, 1- thất bại
--kiểm tra MaHD đã tồn tại
--kiểm tra MaDT có tồn tại
--kiểm tra MaNV có tồn tại
--SLChiNhanh ban đầu bằng 0

create proc sp_Them_Hop_Dong
	@MaHD varchar(10),
	@SLChiNhanh smallint,
	@SoTaiKhoan varchar(20),
	@NganHang nvarchar(30),
	@CNNganHang nvarchar(30),
	@MaSoThue varchar(13),
	@NgayKy date,
	@ThoiHan nvarchar(10),
	@NgayHetHan date,
	@MaDT varchar(10),
	@MaNV varchar(10)
as
	begin tran
		begin try
			if exists(select* from HopDong where MaHD=@MaHD)
			begin
				print N'Mã hợp đồng đã tồn tại'
				rollback tran
				return 1
			end
			if not exists(select* from DoiTac where @MaDT=MaDT)
			begin
				print N'Mã đối tác không tồn tại'
				rollback tran
				return 1
			end
			if not exists(select* from NhanVien where MaNV=@MaNV)
			begin
				print N'Mã nhân viên không tồn tại'
				rollback tran
				return 1
			end
			if @SLChiNhanh<>0
			begin 
				print N'Số lượng chi nhánh không hợp lệ'
				rollback tran
				return 1
			end
			declare @ngDaiDien nvarchar(30)
			select @ngDaiDien=ngDaiDien 
			from DoiTac
			where MaDT=@MaDT
			insert into HopDong values
			(@MaHD,@NgDaiDien,@SLChiNhanh,@SoTaiKhoan,@NganHang,@CNNganHang,@MaSoThue,@NgayKy,@ThoiHan,@NgayHetHan,@MaDT,@MaNV)
		end try
		begin catch
			print N'Lỗi hệ thống!'
			ROLLBACK TRAN
			RETURN 1
		END CATCH
COMMIT TRAN
RETURN 0
GO
Exec sp_Them_Hop_Dong '01',0,'789','Agribank',N'CN.Thủ Đức','KD01','2020-1-10',N'3 năm','2023-1-10','0001','0001'
Exec sp_Them_Hop_Dong '02',0,'789','BIDV',N'CN.Thủ Đức','KD01','2020-1-10',N'4 năm','2024-1-10','0004','0003'
Exec sp_Them_Hop_Dong '03',0,'987','Vietcombank',N'CN.Thủ Đức','KD01','2021-1-10',N'5 năm','2026-1-10','0003','0004'
Exec sp_Them_Hop_Dong '03',0,'987','Vietcombank',N'CN.Thủ Đức','KD01','2021-1-10',N'5 năm','2026-1-10','0003','0009'
Exec sp_Them_Hop_Dong '05',0,'987','Vietcombank',N'CN.Thủ Đức','KD01','2021-1-10',N'5 năm','2026-1-10','0009','0001'

--4.Thêm chi nhánh
--Mô tả
--input: các thông tin của chi nhánh
--output: 0- thêm thành công, 1- thêm thất bại
--kiểm tra thông tin có bị trống
--kiểm tra mã đối tác có tồn tại
--kiểm tra (STT,MaDT) có tồn tại
--Tình trạng chỉ là "Bình thường"/"Tạm nghỉ"
--Thêm chi nhánh
create proc sp_Them_Chi_Nhanh
	@STT int,
	@MaDT varchar(10),
	@TP nvarchar(30),
	@Quan nvarchar(30),
	@DiaChiCuThe nvarchar(50),
	@SDT char(10),
	@TinhTrang nvarchar(30),
	@NgayLap date
as 
	begin tran
		begin try
			if @MaDT='' or @TP='' or @Quan='' or @DiaChiCuThe='' 
			or @SDT='' or @TinhTrang='' or @NgayLap=''
			begin
				print N'Thông tin trống'
				rollback tran
				return 1
			end
			if not exists(select* from DoiTac where MaDT=@MaDT)
			begin
				print N'Mã đối tác không tồn tại'
				rollback tran
				return 1
			end
			if exists(select* from ChiNhanh where @MaDT=MaDT and @STT=STT)
			begin 
				print N'Chi nhánh này đã tồn tại'
				rollback tran
				return 1
			end
			if @TinhTrang<>N'Bình thường' and @TinhTrang<>N'Tạm nghỉ'
			begin
				print N'Tình trạng không hợp lệ'
				rollback tran
				return 1
			end
			insert into ChiNhanh values
			(@STT,@MaDT,@TP,@Quan,@DiaChiCuThe,@SDT,@TinhTrang,@NgayLap)
			update DoiTac
			set SLChiNhanh=SLChiNhanh+1
			where @MaDT=MaDT
			update HOPDONG
			set SLChiNhanh=SLChiNhanh+1
			where @MaDT=MaDT
		end try
		begin catch
			print N'Lỗi hệ thống!'
			ROLLBACK TRAN
			RETURN 1
		END CATCH
COMMIT TRAN
RETURN 0
GO
Exec sp_Them_Chi_Nhanh 1,'0001',N'TP.HCM',N'Q.Thủ Đức',N'123,CMT8','0111111111',N'Bình thường','2020-1-1'
Exec sp_Them_Chi_Nhanh 2,'0001',N'TP.HCM',N'Q.Thủ Đức',N'900,CMT8','0111111112',N'Bình thường','2020-2-1'
Exec sp_Them_Chi_Nhanh 1,'0002',N'TP.HCM',N'Q.Bình Thạnh',N'500,Hai Bà Trưng','0111111113',N'Bình thường','2019-1-1'
Exec sp_Them_Chi_Nhanh 3,'0001',N'TP.HCM',N'Q.Thủ Đức',N'123,CMT8','0111111114',N'Tạm nghỉ','2021-1-1'
Exec sp_Them_Chi_Nhanh 1,'0003',N'TP.HCM',N'Q.1',N'112,Nguyễn Đình Chiểu','0686868686',N'Bình thường','2017-1-1'
Exec sp_Them_Chi_Nhanh 1,'0004',N'TP.HCM',N'Q.2',N'111,Hai Bà Trưng','0753951123',N'Bình thường','2022-10-1'
Exec sp_Them_Chi_Nhanh 1,'0005',N'TP.HCM',N'Q.3',N'111,Hai Bà Trưng','0963258741',N'Bình thường','2022-9-1'
Exec sp_Them_Chi_Nhanh 2,'0005',N'TP.HCM',N'Q.3',N'111,Hai Bà Trưng','0741258963',N'Tạm nghỉ','2022-8-1'
select* from DoiTac
select* from HopDong
select* from ChiNhanh
Exec sp_Them_Chi_Nhanh 3,'0005',N'TP.HCM',N'Q.3',N'111,Hai Bà Trưng','0741258963',N'Đang xây','2022-8-1'
				
--5. Thêm thực phẩm
--Mô tả:
--input: thông tin của thực phẩm
--output: 0- thành công, 1-thất bại
--kiểm tra đối tác có tồn tại
--kiểm tra mã món ăn có bị trùng
--kiểm tra tên thực phẩm có bị trùng
--tình trạng món chỉ là "có bán","Hết hàng hôm nay","Tạm ngưng"

create proc sp_Them_Thuc_Pham
	@MaTP varchar(10),
	@MaDT varchar(10),
	@TenMon nvarchar(30),
	@MieuTa nvarchar(50),
	@Gia decimal(10,1),
	@TinhTrang nvarchar(30),
	@TuyChon nvarchar(50)
as
	begin tran
		begin try
			if not exists(select* from DoiTac where MaDT=@MaDT)
			begin
				print N'Mã đối tác không tồn tại'
				rollback tran
				return 1
			end
			if exists(select* from ThucPham where MaDT=@MaDT and MaTP=@MaTP)
			begin
				print N'Thực phẩm này đã tồn tại'
				rollback tran
				return 1
			end
			if exists(select* from ThucPham where @TenMon=TenMon and MaDT=@MaDT)
			begin
				print N'Tên thực phẩm này đã tồn tại'
				rollback tran
				return 1
			end
			if @TinhTrang<>N'Có bán' and @TinhTrang<>N'Hết hàng hôm nay' and @TinhTrang<>N'Tạm ngưng'
			begin
				print N'Tình trạng không hợp lệ'
				rollback tran
				return 1
			end
			insert into ThucPham values 
			(@MaTP,@MaDT,@TenMon,@MieuTa,@Gia,@TinhTrang,@TuyChon)
		end try
		begin catch
			print N'Lỗi hệ thống!'
			ROLLBACK TRAN
			RETURN 1
		END CATCH
COMMIT TRAN
RETURN 0
GO
Exec sp_Them_Thuc_Pham '01','0001',N'Ca phê phân chồn',N'Đậm vị cà phê',200000,N'Có bán',N'Đường/Nhiệt độ'
Exec sp_Them_Thuc_Pham '02','0001',N'Cà phê đá xay',N'Đậm vị cà phê',20000,N'Có bán',N'Đường/Nhiệt độ'
Exec sp_Them_Thuc_Pham '03','0001',N'Cà phê bọt biển',N'Hương vị mới',50000,N'Có bán',N'Đường/Nhiệt độ'
Exec sp_Them_Thuc_Pham '01','0004',N'Gà chiêm nước mắm',N'Hương vị mới',30000,N'Có bán',NULL
Exec sp_Them_Thuc_Pham '02','0004',N'Sườn non',N'Hương vị mới',30000,N'Có bán',NULL
Exec sp_Them_Thuc_Pham '03','0004',N'Sườn nướng',N'Hương vị mới',30000,N'Có bán',NULL

Exec sp_Them_Thuc_Pham '04','0004',N'Thịt xiên que',N'Hương vị mới',30000,N'Có bán',NULL
Exec sp_Them_Thuc_Pham '01','0005',N'Gà chiêm nước mắm',N'Hương vị mới',25000,N'Có bán',NULL

--6. Thêm khách hàng
--Mô tả:
--input: thông tin khách hàng
--output:0- thành công, 1- thất bại
--kiểm tra thông tin có trống
--kiểm tra mã khách hàng đã tồn tại
--thêm khách hàng

create proc sp_Them_Khach_Hang
	@MaKH varchar(10),
	@HoTen nvarchar(30),
	@DiaChi nvarchar(100),
	@SDT char(10),
	@Email varchar(30)
as
	begin tran
		begin try 
			if @MaKH='' or @HoTen='' or @DiaChi='' or @SDT='' or @Email=''
			begin 
				print N'Thông tin trống'
				rollback tran
				return 1
			end
			if exists(select* from KhachHang where MaKH=@MaKH)
			begin 
				print N'Mã khách hàng đã tồn tại'
				rollback tran
				return 1
			end
			insert into KhachHang values(@MaKH,@HoTen,@DiaChi,@SDT,@Email)
		end try
		begin catch
			print N'Lỗi hệ thống!'
			ROLLBACK TRAN
			RETURN 1
		END CATCH
COMMIT TRAN
RETURN 0
GO
declare @check int
Exec @check=sp_Them_Khach_Hang '0001',N'Lê Minh Chiến',N'122, Phạm Văn Đồng, Q.Bình Thạnh,TP.HCM','0123456789','leminhchien122@gmail.com'
print 'OUTPUT: ' + cast(@check as char(1))
Exec sp_Them_Khach_Hang '0002',N'Mai Quyết Chiến',N'333, Phạm Văn Đồng, Q.Bình Thạnh,TP.HCM','0987654321','maiquyetchien333@gmail.com'
Exec sp_Them_Khach_Hang '0003',N'Lê Trí Dũng',N'266,Võ Thị Sáu,Q.1,TP.HCM','0123456788','letridung266@gmail.com'
Exec sp_Them_Khach_Hang '0004',N'Nguyễn Hữu Trung',N'500, Phạm Văn Đồng, Q.Bình Thạnh,TP.HCM','0123456888','nguyenhuutrung500@gmail.com'
Exec sp_Them_Khach_Hang '0005',N'Nguyễn Khoa Nhiên',N'272,Nguyễn Văn Cừ,Q.5,TP.HCM','0123456777','nguyenkhoanhien272@gmail.com'
Exec sp_Them_Khach_Hang '0006',N'Hoàng Phi Hồng',N'23,Hai Bà Trưng,Q.1,TP.HCM','0123456987','hoangphihong23@gmail.com'
Exec sp_Them_Khach_Hang '0007',N'Ngô Bá Khớ',N'555,CMT8,Q.4,TP.HCM','0123456666','ngobakho555@gmail.com'
select* from KhachHang

--7. Thêm tài xế
--Mô tả:
--input:các thông tin của tài xế
--output:0- thêm thành công, 1- thêm thất bại
--kiểm tra thông tin có trống
--kiểm tra mã tài xế đã tồn tại
--kiểm tra CMND có trùng
--thêm tài xế
create proc sp_Them_Tai_Xe
	@MaTX varchar(10),
	@CMND varchar(12),
	@HoTen nvarchar(30),
	@SDT char(10),
	@DiaChi nvarchar(100),
	@BienSoXe varchar(10),
	@KhuVucHoatDong nvarchar(30),
	@Email varchar(30),
	@SoTaiKhoan varchar(20),
	@NganHang nvarchar(30),
	@CNNganHang nvarchar(30)
as
	begin tran
		begin try
			if @MaTX='' or @CMND='' or @HoTen=''
			or @SDT='' or @DiaChi='' or @BienSoXe=''
			or @KhuVucHoatDong='' or @Email='' or @SoTaiKhoan=''
			or @NganHang='' or @CNNganHang=''
			begin 
				print N'Thông tin trống'
				rollback tran
				return 1
			end
			if exists(select* from TaiXe where MaTX=@MaTX)
			begin 
				print N'Mã tài xế đã tôn tại'
				rollback tran
				return 1
			end
			if exists(select* from TaiXe where CMND=@CMND) 
			begin 
				print N'Mã cmnd này đã tồn tại'
				rollback tran
				return 1
			end
			insert into TaiXe values(@MaTX,@CMND,@HoTen,@SDT,@DiaChi,@BienSoXe,@KhuVucHoatDong,@Email,@SoTaiKhoan,@NganHang,@CNNganHang)
		end try
		begin catch
			print N'Lỗi hệ thống!'
			ROLLBACK TRAN
			RETURN 1
		END CATCH
COMMIT TRAN
RETURN 0
GO
select* from TaiXe
Exec sp_Them_Tai_Xe '0001','1234',N'Nguyễn Văn Nam','0278945666',N'Q.Thủ Đức,TP.HCM','72-C2-0123',N'Q.1,TP.HCM','nvnam@gmail.com','123','Agribank',N'CN.Thủ Đức'
Exec sp_Them_Tai_Xe '0002','1235',N'Nguyễn Văn An','0278945665',N'Q.Thủ Đức,TP.HCM','72-C2-0124',N'Q.Thủ Đức,TP.HCM','nvan@gmail.com','124','Agribank',N'CN.Thủ Đức'
Exec sp_Them_Tai_Xe '0003','1236',N'Nguyễn Duy Tân','0278945664',N'Q.Bình Thạnh,TP.HCM','62-C2-1088',N'Q.5,TP.HCM','ndtan@gmail.com','456','BIDV',N'CN.Bình Thạnh'
Exec sp_Them_Tai_Xe '0004','1237',N'Vũ Văn Vang','0278945666',N'Q.Gò Vấp,TP.HCM','60-C1-0128',N'Q.4,TP.HCM','vvvang@gmail.com','129','Agribank',N'CN.Gò Vấp'
Exec sp_Them_Tai_Xe '0004','1238',N'Vũ Văn Vang','0278945666',N'Q.Gò Vấp,TP.HCM','60-C1-0128',N'Q.4,TP.HCM','vvvang@gmail.com','129','Agribank',N'CN.Gò Vấp'
Exec sp_Them_Tai_Xe '0005','1237',N'Vũ Văn Vang','0278945666',N'Q.Gò Vấp,TP.HCM','60-C1-0128',N'Q.4,TP.HCM','vvvang@gmail.com','129','Agribank',N'CN.Gò Vấp'
Exec sp_Them_Tai_Xe '0004','1237',N'Vũ Văn Vang','',N'Q.Gò Vấp,TP.HCM','60-C1-0128',N'Q.4,TP.HCM','vvvang@gmail.com','129','Agribank',N'CN.Gò Vấp'
--8. Thêm đơn đặt hàng
--Mô tả:
--kiểm tra mã đơn hàng đã tồn tại
--kiểm tra mã tài xế có tồn tại
--kiểm tra mã khách hàng có tồn tại
--nếu chưa có tài xế nhận thì đơn hàng ở trạng thái chờ
--giá đơn hàng khởi tạo với giá trị bằng 0
create proc sp_Them_DDH
	@MaDH varchar(10),
	@GioDat varchar(6),
	@NgayDat date,
	@GiaTriDH decimal(10,1),
	@TinhTrang nvarchar(30),
	@MaKH varchar(10),
	@MaTX varchar(10)
as 
	begin tran
		begin try
			if exists(select* from DONDATHANG where @MaDH=MaDH)
			begin
				print N'Mã đơn đặt hàng đã tồn tại'
				rollback tran
				return 1
			end
			if not exists(select* from KHACHHANG where @MaKH=MaKH)
			begin
				print N'Khách hàng không tồn tại'
				rollback tran
				return 1
			end
			if @MaTX=''
			begin 
				set @TinhTrang=N'Chờ'
			end
			else if not exists(select* from TAIXE where @MaTX=MaTX)
			begin
				print N'Tài xế không tồn tại'
				rollback tran
				return 1
			end
			insert into DONDATHANG values
			(@MaDH,@GioDat,@NgayDat,@GiaTriDH,@TinhTrang,@MaKH,@MaTX)
		end try
		begin catch
			print N'Lỗi hệ thống!'
			ROLLBACK TRAN
			RETURN 1
		END CATCH
COMMIT TRAN
RETURN 0
GO
Exec sp_Them_DDH '01','12:20','2022-10-22',0,N'Chờ','0001','0001'
Exec sp_Them_DDH '02','13:20','2022-10-22',0,N'Đang giao','0001','0002'
Exec sp_Them_DDH '03','13:20','2022-10-22',0,N'Đang giao','0004','0002'
Exec sp_Them_DDH '04','13:20','2022-10-22',0,N'Đang giao','0003','0003'
Exec sp_Them_DDH '05','13:20','2022-10-22',0,N'Chờ giao','0001','0002'

--9. Thêm chi tiết đơn đặt hàng
--Mô tả:
--kiểm tra đơn đặt hàng có tồn tại
--kiểm tra mã thực phẩm có tồn tại
--kiểm tra mã đối tác có tồn tại
--kiểm tra tình trạng thưcj phẩm có đang bán
--kiểm tra đối tác có đang hoạt động
--thêm chi tiết đơn hàng
--cập nhật lại tổng giá trong đhang
create proc sp_Them_CT_DDH
	@MaDH varchar(10),
	@MaTP varchar(10),
	@MaDT varchar(10),
	@SoLuong int,
	@DanhGia nvarchar(100)
as
	begin tran
		begin try
			if not exists(select* from DONDATHANG where @MaDH=MaDH)
			begin
				print N'Mã đơn đặt hàng không tồn tại'
				rollback tran
				return 1
			end
			if not exists(select* from ThucPham where MaDT=@MaDT and MaTP=@MaTP)
			begin
				print N'Thực phẩm này không tồn tại'
				rollback tran
				return 1
			end
			declare @TinhTrang nvarchar(30)
			select @TinhTrang=TinhTrang from ThucPham where MaDT=@MaDT and MaTP=@MaTP
			if @TinhTrang<>N'Có bán'
			begin
				print N'Thực phẩm này không còn được bán'
				rollback tran
				return 1
			end
			if not exists(select* from ChiNhanh where MaDT=@MaDT and TinhTrang=N'Bình thường')
			begin
				print N'Đối tác đóng cửa'
				rollback tran
				return 1
			end
			insert into CHITIETDONDATHANG values
			(@MaDH,@MaTP,@MaDT,@SoLuong,@DanhGia)
			declare @gia decimal(10,1)
			select @gia=Gia from ThucPham where MaDT=@MaDT and MaTP=@MaTP
			set @gia=@gia*@SoLuong
			update DONDATHANG
			set GiaTriDH=GiaTriDH+@gia
			where @MaDH=MaDH
		end try
		begin catch
			print N'Lỗi hệ thống!'
			ROLLBACK TRAN
			RETURN 1
		END CATCH
COMMIT TRAN
RETURN 0
GO
select* from DONDATHANG
select* from CHITIETDONDATHANG
Exec sp_Them_CT_DDH '01','01','0004',2,'Like'
Exec sp_Them_CT_DDH '01','02','0004',1,'Like'
Exec sp_Them_CT_DDH '01','03','0004',2,'Like'
Exec sp_Them_CT_DDH '02','01','0001',1,'Like'
Exec sp_Them_CT_DDH '02','02','0001',2,'Like'
Exec sp_Them_CT_DDH '03','01','0004',4,'Like'
go
/* Xóa dữ liệu
Người làm: 20120624 - Mai Quyết Vang*/
-- usp_xoaNhanVien
create proc XoaUser
	@user char(15)
as
BEGIN TRAN
	BEGIN TRY
		IF (@user = '')
		BEGIN
			Print N'Username không được bỏ trống'
			ROLLBACK TRAN
		END
		IF not exists (SELECT * FROM USERS WHERE Username = @user)
		BEGIN
			Print N'Username không tồn tại!'
			ROLLBACK TRAN
		END
		exec sp_droplogin @user

	END TRY
	BEGIN CATCH
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
	END CATCH
COMMIT TRAN
GO


create -- alter
proc usp_xoaNhanVien
		@MaNV varchar(10)
as
begin transaction
	begin try
		-- Kiểm tra thông tin nhập không đượcc rỗng.
		if (@MaNV = '')
			begin
				print N'Thông tin nhập không được rỗng'
				rollback tran
				return 1
			end

		-- Kiểm tra thông tin nhập hợp lệ. 
		if exists(select * from HOPDONG where MaNV = @MaNV)
			begin
				print N'Nhân viên tồn tại hợp đồng'
				rollback tran
				return 1
			end
		-- Kiểm tra tồn tại
		if not exists(select * from NHANVIEN where MaNV = @MaNV)
			begin
				print N'Không thể xoá, nhân viên không tồn tại'
				rollback tran
				return 1
			end
		delete from NHANVIEN where MaNV = @MaNV
	end try

	begin catch
		print N'Lỗi hệ thống!'
		rollback tran
		return 1
	end catch
commit tran
return 0
go


-- usp_xoaChiNhanh
create -- alter
proc usp_xoaChiNhanh
		@STT int,
		@MaDT varchar(10)
as
begin transaction
	begin try
		-- Kiểm tra thông tin nhập không đượcc rỗng.
		if (@MaDT = '' or
			@STT = '')
			begin
				print N'Thông tin nhập không được rỗng'
				rollback tran
				return 1
			end
		-- Kiểm tra tồn tại
		if not exists(select * from CHINHANH where STT = @STT and MaDT = @MaDT)
			begin
				print N'Không thể xoá, chi nhánh không tồn tại'
				rollback tran
				return 1
			end
		delete from CHINHANH where MaDT = @MaDT and STT = @STT
	end try

	begin catch
		print N'Lỗi hệ thống!'
		rollback tran
		return 1
	end catch
commit tran
return 0
go


-- usp_xoaChiTietDonHang
create -- alter
proc usp_xoaChiTietDonHang
		@MaDH varchar(10),
		@MaTP varchar(10),
		@MaDT varchar(10)
as
begin transaction
	begin try
		-- Kiểm tra thông tin nhập không đượcc rỗng.
		if (@MaDH = '' or
			@MaTP = ''or
			@MaDT = '')
			begin
				print N'Thông tin nhập không được rỗng'
				rollback tran
				return 1
			end
		-- Kiểm tra tồn tại
		if not exists(select * from CHITIETDONDATHANG where @MaDH = @MaDH and MaTP = @MaTP and MaDT = @MaDT)
			begin
				print N'Không thể xoá, chi tiết đơn hàng không tồn tại'
				rollback tran
				return 1
			end
		delete from CHITIETDONDATHANG where @MaDH = MaDH and @MaTP = MaTP and
			@MaDT = MaDT
	end try

	begin catch
		print N'Lỗi hệ thống!'
		rollback tran
		return 1
	end catch
commit tran
return 0
go

-- usp_xoaDonDatHang
create -- alter
proc usp_xoaDonDatHang
		@MaDH varchar(10)
as
begin transaction
	begin try
		-- Kiểm tra thông tin nhập không đượcc rỗng.
		if (@MaDH = '')
			begin
				print N'Thông tin nhập không được rỗng'
				rollback tran
				return 1
			end
		if exists(select * from CHITIETDONDATHANG where MaDH = @MaDH)
			begin
				print N'Không thể xoá, chi tiết hơn hàng vẫn tồn tại'
				rollback tran
				return 1
			end
		-- Kiểm tra tồn tại
		if not exists(select * from DONDATHANG where @MaDH = @MaDH)
			begin
				print N'Không thể xoá, đơn hàng không tồn tại'
				rollback tran
				return 1
			end
		delete from DONDATHANG where @MaDH = MaDH
	end try

	begin catch
		print N'Lỗi hệ thống!'
		rollback tran
		return 1
	end catch
commit tran
return 0
go


-- usp_xoaKhachHang
create -- alter
proc usp_xoaKhachHang
		@MaKH varchar(10)
as
begin transaction
	begin try
		-- Kiểm tra thông tin nhập không đượcc rỗng.
		if (@MaKH = '')
			begin
				print N'Thông tin nhập không được rỗng'
				rollback tran
				return 1
			end
		if exists(select * from DONDATHANG where MaKH = @MaKH)
			begin
				print N'Không thể xoá, khách hàng đã có đơn hàng'
				rollback tran
				return 1
			end
		-- Kiểm tra tồn tại
		if not exists(select * from KHACHHANG where @MaKH = MaKH)
			begin
				print N'Không thể xoá, khách hàng không tồn tại'
				rollback tran
				return 1
			end
		delete from KHACHHANG where @MaKH = MaKH
	end try

	begin catch
		print N'Lỗi hệ thống!'
		rollback tran
		return 1
	end catch
commit tran
return 0
go

exec usp_xoaKhachHang '001'
go


-- usp_xoaTaiXe
create -- alter
proc usp_xoaTaiXe
		@CMND varchar(10)
as
begin transaction
	begin try
		-- Kiểm tra thông tin nhập không đượcc rỗng.
		if (@CMND = '')
			begin
				print N'Thông tin nhập không được rỗng'
				rollback tran
				return 1
			end
		if exists(select * from DONDATHANG where MaTX = @CMND)
			begin
				print N'Không thể xoá, khách hàng đã có đơn hàng'
				rollback tran
				return 1
			end
		-- Kiểm tra tồn tại
		if not exists(select * from TAIXE where CMND = @CMND)
			begin
				print N'Không thể xoá, tài xế không tồn tại'
				rollback tran
				return 1
			end
		delete from TAIXE where @CMND = CMND
	end try

	begin catch
		print N'Lỗi hệ thống!'
		rollback tran
		return 1
	end catch
commit tran
return 0
go
exec usp_xoaTaiXe '001'
go


-- usp_xoaThucPham
create -- alter
proc usp_xoaThucPham
		@MaTP varchar(10),
		@MaDT varchar(10)
as
begin transaction
	begin try
		-- Kiểm tra thông tin nhập không đượcc rỗng.
		if (@MaTP = '' or
			@MaDT = '')
			begin
				print N'Thông tin nhập không được rỗng'
				rollback tran
				return 1
			end
		if exists(select * from CHITIETDONDATHANG where MaTP = @MaTP and
			MaDT = @MaDT)
			begin
				print N'Không thể xoá, món ăn đã từng được lên đơn'
				rollback tran
				return 1
			end
		-- Kiểm tra tồn tại
		if not exists(select * from THUCPHAM where MaTP = @MaTP and
			MaDT = @MaDT)
			begin
				print N'Không thể xoá, món ăn không tồn tại'
				rollback tran
				return 1
			end
		delete from THUCPHAM where MaTP = @MaTP and
			MaDT = @MaDT
	end try

	begin catch
		print N'Lỗi hệ thống!'
		rollback tran
		return 1
	end catch
commit tran
return 0
go
exec usp_xoaThucPham '001','0'
go


-- usp_xoaHopDong
create -- alter
proc usp_xoaHopDong
		@MaHD varchar(10)
as
begin transaction
	begin try
		-- Kiểm tra thông tin nhập không đượcc rỗng.
		if (@MaHD = '')
			begin
				print N'Thông tin nhập không được rỗng'
				rollback tran
				return 1
			end
		-- Kiểm tra tồn tại
		if not exists(select * from HOPDONG where MaHD = @MaHD)
			begin
				print N'Không thể xoá, hợp đồng không tồn tại'
				rollback tran
				return 1
			end
		delete from HOPDONG where MaHD = @MaHD
	end try

	begin catch
		print N'Lỗi hệ thống!'
		rollback tran
		return 1
	end catch
commit tran
return 0
go
exec usp_xoaHopDong '001'
go

-- usp_xoaDoiTac
create -- alter
proc usp_xoaDoiTac
		@MaDT varchar(10)
as
begin transaction
	begin try
		-- Kiểm tra thông tin nhập không đượcc rỗng.
		if (@MaDT = '')
			begin
				print N'Thông tin nhập không được rỗng'
				rollback tran
				return 1
			end
		-- Kiểm tra tồn tại
		if not exists(select * from DOITAC where MaDT = @MaDT)
			begin
				print N'Không thể xoá, hợp đồng không tồn tại'
				rollback tran
				return 1
			end
		delete from DOITAC where MaDT = @MaDT
	end try

	begin catch
		print N'Lỗi hệ thống!'
		rollback tran
		return 1
	end catch
commit tran
return 0
go
exec usp_xoaDoiTac '001'
go



-- create proc usp_xoaNhanVien
-- create proc usp_xoaDoiTac
-- create proc usp_xoaHopDong
-- create proc usp_xoaChiNhanh
-- create proc usp_xoaThucPham
-- create proc usp_xoaKhachHang
-- create proc usp_xoaTaiXe
-- create proc usp_xoaDonDatHang
-- create proc usp_xoaChiTietDonHang



/*				Phân quyền
Người làm: 20120049 - Nguyễn Hải Đăng */
use GIAONHANHANG
go

--Tạo role
create role DoiTac
create role KhachHang
create role TaiXe
create role NhanVien
create role QuanTri
go

exec sp_addlogin 'DT01', '123456'
exec sp_addlogin 'KH01', '123456'
exec sp_addlogin 'TX01', '123456'
exec sp_addlogin 'NV01', '123456'
exec sp_addlogin 'QT01', '123456'
go

create user DT01 for login DT01
create user KH01 for login KH01
create user TX01 for login TX01
create user NV01 for login NV01
create user QT01 for login QT01

exec sp_addrolemember 'DoiTac', 'DT01'
exec sp_addrolemember 'KhachHang', 'KH01'
exec sp_addrolemember 'TaiXe', 'TX01'
exec sp_addrolemember 'NhanVien', 'NV01'
exec sp_addrolemember 'QuanTri', 'QT01'

go

/*Phân hệ đối tác*/
--Đăng ký thông tin: Đối tác có thể insert dữ liệu bảng DOITAC
GRANT insert
ON DOITAC
TO DoiTac
go

--Lập hợp đồng: Đối tác có thể insert dữ liệu bảng HOPDONG
GRANT insert
ON HOPDONG
TO DoiTac
go

--Quản lý cửa hàng: Đối tác có thể insert, update, delete
GRANT insert
ON CHINHANH
TO DoiTac
go

GRANT update
ON CHINHANH(Quan, TinhTrang)
TO DoiTac
go

GRANT delete
ON CHINHANH
TO DoiTac
go

--Quản lý thực đơn: Đối tác có thể insert, update, delete
GRANT insert, delete, update
ON THUCPHAM
TO DoiTac
go


--Quản lý đơn đặt hàng
--Quản lý số liệu
GRANT select
ON DONDATHANG
TO DoiTac
go

GRANT update
ON DONDATHANG(TinhTrang)
TO DoiTac
go

/*Phân hệ khách hàng*/
--Đăng ký thành viên: cho phép insert, update bảng KHACHHANG
GRANT insert, update
ON KHACHHANG
TO KhachHang
go

--Đặt hàng: 
--cho phép select bảng DOITAC, THUCPHAM
--Cho phép insert, delete bảng DONDATHANG, CHITIETDONDATHANG
GRANT select
ON DOITAC
TO KhachHang
go

GRANT select
ON THUCPHAM
TO KhachHang
go

GRANT insert, delete
ON DONDATHANG
TO KhachHang
go

GRANT insert, delete
ON CHITIETDONDATHANG
TO KhachHang
go

--Theo dõi đơn hàng: cho phép select bảng Đơn đặt hàng
GRANT select
ON DONDATHANG
TO KhachHang
go

/*Phân hệ tài xế*/
--Đăng ký thành viên: cho phép insert, update bảng TAIXE
GRANT insert, update
ON TAIXE
TO TaiXe
go

--Tiếp nhận và xử lý đơn đặt hàng: chỉ được update vì tài xế chỉ chọn đơn hàng phục vụ và cập nhật tình trạng
GRANT update
ON DONDATHANG
TO TaiXe
go

--Theo dõi thu nhập: cho phép select bảng DONDATHANG
GRANT select
ON DONDATHANG
TO TaiXe
go

/*Phân hệ nhân viên*/
--Quản lý đối tác: cho phép select bảng HOPDONG
GRANT select
on HOPDONG
TO NhanVien

--Xác nhận hợp đồng: cho phép select bảng HOPDONG (ở trên), cho phép update bảng HOPDONG
GRANT update
on HOPDONG
TO NhanVien
go

/*Phân hệ quản trị*/
create proc insertAccount (
	@user char(15),
	@pass char(15),
	@role char(15)
)
as
BEGIN TRAN
	BEGIN TRY
		IF (@user = '' or @pass = '' or @role = '')
		BEGIN
			Print N'Username, pass và role không được bỏ trống'
			ROLLBACK TRAN
		END

		exec sp_addlogin @user, @pass

	END TRY
	BEGIN CATCH
		print N'Lỗi phát sinh, có thể là username đã tồn tại hoặc lỗi hệ thống!'
		ROLLBACK TRAN
	END CATCH
COMMIT TRAN
GO

create proc deleteAccount (
	@user char(15)
)
as
BEGIN TRAN
	BEGIN TRY
		IF (@user = '')
		BEGIN
			Print N'Username không được bỏ trống'
			ROLLBACK TRAN
		END

		exec sp_droplogin @user

	END TRY
	BEGIN CATCH
		print N'Lỗi phát sinh, có thể là username không tồn tại hoặc lỗi hệ thống!'
		ROLLBACK TRAN
	END CATCH
COMMIT TRAN
GO

create proc updateAccount (
	@user char(15),
	@oldPass char(15),
	@newPass char(15)
)
as
BEGIN TRAN
	BEGIN TRY
		IF (@user = '' or @oldPass = '' or @newPass = '')
		BEGIN
			Print N'Username, Old password và New password không được bỏ trống'
			ROLLBACK TRAN
		END
		exec sp_password @oldPass, @newPass, @user
	END TRY
	BEGIN CATCH
		print N'Lỗi phát sinh, có thể là username không tồn tại hoặc lỗi hệ thống!'
		ROLLBACK TRAN
	END CATCH
COMMIT TRAN
GO

create proc enableLogin (
	@user char(15)
)
as
BEGIN TRAN
	BEGIN TRY
		IF (@user = '')
		BEGIN
			Print N'Username không được bỏ trống'
			ROLLBACK TRAN
		END

		DECLARE @sql nvarchar(500)
		SET @sql = 'ALTER LOGIN ' + @user + ' ENABLE'
		EXEC sp_executesql @sql
		print N'Username ' + @user + N' đã được kích hoạt'

	END TRY
	BEGIN CATCH
		print N'Lỗi phát sinh, có thể là username không tồn tại hoặc lỗi hệ thống!'
		ROLLBACK TRAN
	END CATCH
COMMIT TRAN
GO


create proc disableLogin (
	@user char(15)
)
as
BEGIN TRAN
	BEGIN TRY
		IF (@user = '')
		BEGIN
			Print N'Username không được bỏ trống'
			ROLLBACK TRAN
		END

		DECLARE @sql nvarchar(500)
		SET @sql = 'ALTER LOGIN ' + @user + ' DISABLE'
		EXEC sp_executesql @sql
		print N'Username ' + @user + N' đã được vô hiệu hóa'

	END TRY
	BEGIN CATCH
		print N'Lỗi phát sinh, có thể là username không tồn tại hoặc lỗi hệ thống!'
		ROLLBACK TRAN
	END CATCH
COMMIT TRAN
GO

create proc grantPermission (
	@roleOrUser char(15),
	@table char(30),
	@permiss char(15)
)
as
BEGIN TRAN
	BEGIN TRY
		IF (@roleOrUser = '' or @table = '' or @permiss = '')
		BEGIN
			Print N'Role or user, table và permission không được bỏ trống'
			ROLLBACK TRAN
		END
		DECLARE @sql nvarchar(500)
		SET @sql = 'GRANT ' + @permiss + ' on ' + @table + ' to ' + @roleOrUser
		EXEC sp_executesql @sql
	END TRY
	BEGIN CATCH
		print N'Lỗi phát sinh!'
		ROLLBACK TRAN
	END CATCH
COMMIT TRAN
GO
--Cập nhật thông tin tài khoản: cấp quyền update bảng DOITAC, KHACHHANG, NHANHVIEN, TAIXE
GRANT update on DOITAC to QuanTri
GRANT update on TAIXE to QuanTri
GRANT update on KHACHHANG to QuanTri
GRANT update on NHANVIEN to QuanTri

--Insert, delete, update tài khoản: cấp quyền thực hiện các store procedure: insertAccount, deleteAccount, updateAccount
GRANT EXECUTE ON insertAccount TO QuanTri AS dbo
GRANT EXECUTE ON deleteAccount TO QuanTri AS dbo
GRANT EXECUTE ON updateAccount To QuanTri AS dbo

--Khóa, kích hoạt tài khoản: cấp quyền thực hiện store procedure: enableLogin, disableLogin
GRANT EXECUTE ON enableLogin To QuanTri AS dbo
GRANT EXECUTE ON disableLogin To QuanTri AS dbo

--Cấp quyền thao tác: cấp quyền thực hiện store procedure: grantPermission
GRANT EXECUTE ON grantPermission To QuanTri AS dbo

GRANT update
on HOPDONG
TO NhanVien
go

exec insertAccount 'NV02', '123456', 'NhanVien'

