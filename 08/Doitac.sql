
/*
Các giao tác cần chú ý (Có tranh chấp):
KHACHHANG hủy đơn trong khi DOITAC chuyển tình trạng đơn hàng sang “Đã tiếp nhận” (không thểhủy), khi đó xảy ra tranh chấp Lost Update.

Đối tác đang cập nhật giá cho 1 thực phẩm, thì khách hàng tìm kiếm thông tin thực phẩm đó, nhưng do giá đối tác nhập không hợp lệ(vô tình nhập sốâm) dẫn đến Dirty read

khách hàng đang tìm kiếm thực phẩm với một tình trạng cụthểthì đối tác cập nhật tình trạng  thực phẩm dẫn đến Unrepeatable Read.

khách hàng đang kiểm tra đơn đặt hàng thì đối tác cập nhật thông tin trạng thái giao hàng Unrepeatable Read

*/
CREATE DATABASE GIAONHANHANG1
GO
USE GIAONHANHANG1
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
	Username varchar(20),
	Primary key(MaDT),
	Foreign key(Username) references USERS(Username)
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
	MaNV varchar(10),
	TrangThai nvarchar(30)
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
	MaHopDong varchar(10),
	Primary key(STT,MaDT),
	Foreign key(MaDT) references DOITAC(MaDT),
	Foreign key(MaHopDong) references HOPDONG(MaHD)
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

go

---------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------
-------------------------------------------------------- KHÔNG CẦN --------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------
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
				ROLLBACK TRAN
				select 1 as code
			end
			if exists(select* from USERS where Username = @Username)
			begin
				print N'Username đã tồn tại'
				ROLLBACK TRAN
				select 2 as code
			end
			if @RoleName != 'DoiTac' and @RoleName != 'KhachHang' and @RoleName != 'TaiXe' and @RoleName != 'NhanVien' and @RoleName != 'QuanTri'
			begin
				print N'Role name không hợp lệ!'
				ROLLBACK TRAN
				select 3 as code
			end
			insert into USERS values(@Username,@Pass,@RoleName)
		end try
		begin catch
			print N'Lỗi hệ thống!'
			ROLLBACK TRAN
			select 4 as code
		END CATCH
COMMIT TRAN
select 0 as code
GO

exec ThemUser 'vvmd','123456', 'QuanTri'
exec ThemUser 'lmt','123456', 'NhanVien'
exec ThemUser 'nhd','123456', 'DoiTac'
exec ThemUser 'mqv','123456', 'KhachHang'

--Thêm đối tác (để test)
insert into DOITAC Values ('1','1@gmail.com',N'Lee Chong Way',0,N'Cafe Hai Len',N'Cà phê','nhd')
insert into DOITAC Values ( '2','2@gmail.com',N'Lê Nguyên Vũ',0,N'Trà sữa KoKo',N'Trà sữa','lmt')
insert into DOITAC Values ( '3','3@gmail.com',N'Trần Uy',0,N'Bánh mì Ngon',N'Bánh mì',null)
insert into DOITAC Values ( '4','4@gmail.com',N'Ngô Văn Quyền',0,N'Cơm Ngô Quyền',N'Cơm',null)
insert into DOITAC Values ( '5','5@gmail.com',N'Nguyên Văn Kí',0,N'Cơm Nguyên Kí',N'Cơm',null)


--Thêm chi nhánh (Tiến sửa)
--ĐÃ SỬA
go
--thêm khách hàng
begin try 
	drop proc sp_Them_Khach_Hang
end try
begin catch
end catch
go
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
Exec @check=sp_Them_Khach_Hang '1',N'Lê Minh Chiến',N'122, Phạm Văn Đồng, Q.Bình Thạnh,TP.HCM','0123456789','leminhchien122@gmail.com'
print 'OUTPUT: ' + cast(@check as char(1))
Exec sp_Them_Khach_Hang '2',N'Mai Quyết Chiến',N'333, Phạm Văn Đồng, Q.Bình Thạnh,TP.HCM','0987654321','maiquyetchien333@gmail.com'
Exec sp_Them_Khach_Hang '3',N'Lê Trí Dũng',N'266,Võ Thị Sáu,Q.1,TP.HCM','0123456788','letridung266@gmail.com'
Exec sp_Them_Khach_Hang '4',N'Nguyễn Hữu Trung',N'500, Phạm Văn Đồng, Q.Bình Thạnh,TP.HCM','0123456888','nguyenhuutrung500@gmail.com'
Exec sp_Them_Khach_Hang '5',N'Nguyễn Khoa Nhiên',N'272,Nguyễn Văn Cừ,Q.5,TP.HCM','0123456777','nguyenkhoanhien272@gmail.com'
Exec sp_Them_Khach_Hang '6',N'Hoàng Phi Hồng',N'23,Hai Bà Trưng,Q.1,TP.HCM','0123456987','hoangphihong23@gmail.com'
Exec sp_Them_Khach_Hang '7',N'Ngô Bá Khớ',N'555,CMT8,Q.4,TP.HCM','0123456666','ngobakho555@gmail.com'
select* from KhachHang
go
--7. Thêm tài xế
--Mô tả:
--input:các thông tin của tài xế
--output:0- thêm thành công, 1- thêm thất bại
--kiểm tra thông tin có trống
--kiểm tra mã tài xế đã tồn tại
--kiểm tra CMND có trùng
--thêm tài xế
go
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
Exec sp_Them_Tai_Xe '1','1234',N'Nguyễn Văn Nam','0278945666',N'Q.Thủ Đức,TP.HCM','72-C2-0123',N'Q.1,TP.HCM','nvnam@gmail.com','123','Agribank',N'CN.Thủ Đức'
Exec sp_Them_Tai_Xe '2','1235',N'Nguyễn Văn An','0278945665',N'Q.Thủ Đức,TP.HCM','72-C2-0124',N'Q.Thủ Đức,TP.HCM','nvan@gmail.com','124','Agribank',N'CN.Thủ Đức'
Exec sp_Them_Tai_Xe '3','1236',N'Nguyễn Duy Tân','0278945664',N'Q.Bình Thạnh,TP.HCM','62-C2-1088',N'Q.5,TP.HCM','ndtan@gmail.com','456','BIDV',N'CN.Bình Thạnh'
Exec sp_Them_Tai_Xe '4','1237',N'Vũ Văn Vang','0278945666',N'Q.Gò Vấp,TP.HCM','60-C1-0128',N'Q.4,TP.HCM','vvvang@gmail.com','129','Agribank',N'CN.Gò Vấp'
Exec sp_Them_Tai_Xe '4','1238',N'Vũ Văn Vang','0278945666',N'Q.Gò Vấp,TP.HCM','60-C1-0128',N'Q.4,TP.HCM','vvvang@gmail.com','129','Agribank',N'CN.Gò Vấp'
Exec sp_Them_Tai_Xe '5','1237',N'Vũ Văn Vang','0278945666',N'Q.Gò Vấp,TP.HCM','60-C1-0128',N'Q.4,TP.HCM','vvvang@gmail.com','129','Agribank',N'CN.Gò Vấp'
Exec sp_Them_Tai_Xe '4','1237',N'Vũ Văn Vang','',N'Q.Gò Vấp,TP.HCM','60-C1-0128',N'Q.4,TP.HCM','vvvang@gmail.com','129','Agribank',N'CN.Gò Vấp'
select* from TaiXe
go
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
Exec sp_Them_DDH '1','12:20','2022-10-22',0,N'Chờ','1','1'
Exec sp_Them_DDH '2','13:20','2022-10-22',0,N'Đang giao','1','2'
Exec sp_Them_DDH '3','13:20','2022-10-22',0,N'Đang giao','4','2'
Exec sp_Them_DDH '4','13:20','2022-10-22',0,N'Đang giao','3','3'
Exec sp_Them_DDH '5','13:20','2022-10-22',0,N'Chờ giao','1','2'
go
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
Exec sp_Them_CT_DDH '1','1','4',2,'Like'
Exec sp_Them_CT_DDH '1','2','4',1,'Like'
Exec sp_Them_CT_DDH '1','3','4',2,'Like'
Exec sp_Them_CT_DDH '2','1','1',1,'Like'
Exec sp_Them_CT_DDH '2','2','1',2,'Like'
Exec sp_Them_CT_DDH '3','1','4',4,'Like'
select * from THUCPHAM
select* from DONDATHANG
go

---------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------- CÓ CẦN ----------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------
create proc ThemChiNhanh
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
				select 1
				return
			end
			if not exists(select* from DOITAC where MaDT=@MaDT)
			begin
				print N'Mã đối tác không tồn tại'
				rollback tran
				select 2 
				return
			end
			if exists(select* from CHINHANH where SDT=@SDT)
			begin
				print N'Số điện thoại đã tồn tại'
				rollback tran 
				select 3
				return
			end
			if @TinhTrang<>N'Bình thường' and @TinhTrang<>N'Tạm nghỉ'
			begin
				print N'Tình trạng không hợp lệ'
				rollback tran
				select 4
				return
			end
			declare @stt int
			set @stt=0
			if exists (select * from ChiNhanh where MaDT=@MaDT)
			begin 
				set @stt=(select max(STT) from ChiNhanh where MaDT=@MaDT) 
			end
			set @stt=@stt+1
			insert into ChiNhanh values
			(@stt,@MaDT,@TP,@Quan,@DiaChiCuThe,@SDT,@TinhTrang,@NgayLap,NULL)
			update DoiTac
			set SLChiNhanh=SLChiNhanh+1
			where @MaDT=MaDT
		end try
		begin catch
			print N'Lỗi hệ thống!'
			ROLLBACK TRAN
			select 10
			return 
		END CATCH
COMMIT TRAN
select 0 
return
GO

create proc XoaChiNhanh
	@MaDT varchar(10),
	@STT int
as 
	begin tran
		begin try
			if @MaDT='' or @STT =''
			begin
				print N'Thông tin trống'
				rollback tran
				select 1
				return
			end
			if not exists(select* from DOITAC where MaDT=@MaDT)
			begin
				print N'Mã đối tác không tồn tại'
				rollback tran
				select 2 
				return
			end
			if not exists(select STT from CHINHANH where MaDT=@MaDT and @STT = STT)
			begin
				print N'Số thứ tự không tồn tại'
				rollback tran
				select 5
				return
			end
			delete CHINHANH 
				where MaDT = @MaDT and STT = @STT


		end try
		begin catch
			print N'Lỗi hệ thống!'
			ROLLBACK TRAN
			select 10
			return 
		END CATCH
COMMIT TRAN
select 0 
return
GO

create proc SuaChiNhanh
	@MaDT varchar(10),
	@TP nvarchar(30),
	@Quan nvarchar(30),
	@DiaChiCuThe nvarchar(50),
	@SDT char(10),
	@TinhTrang nvarchar(30),
	@NgayLap date,
	@stt varchar(10)
as 
	begin tran
		begin try
			if @MaDT='' or @TP='' or @Quan='' or @DiaChiCuThe='' 
			or @SDT='' or @TinhTrang='' or @NgayLap='' or @stt =''
			begin
				print N'Thông tin trống'
				rollback tran
				select 1
				return
			end
			if not exists(select* from DOITAC where MaDT=@MaDT)
			begin
				print N'Mã đối tác không tồn tại'
				rollback tran
				select 2 
				return
			end
			if exists(select *from CHINHANH where SDT=@SDT and (MaDT!= @MaDT or STT != @stt)
			)
			begin
				print N'Số điện thoại đã tồn tại'
				rollback tran 
				select 3
				return
			end
			if @TinhTrang<>N'Bình thường' and @TinhTrang<>N'Tạm nghỉ'
			begin
				print N'Tình trạng không hợp lệ'
				rollback tran
				select 4
				return
			end
			update ChiNhanh set 
				MaDT=@MaDT,
				TP=@TP,
				Quan=@Quan,
				DiaChiCuThe=@DiaChiCuThe,
				SDT=@SDT,
				TinhTrang=@TinhTrang,
				NgayLap=@NgayLap
				where
				STT=@stt and MaDT =@MaDT
		end try
		begin catch
			print N'Lỗi hệ thống!'
			ROLLBACK TRAN
			select 10
			return 
		END CATCH
COMMIT TRAN
select 0 
return
GO

/*Exec SuaChiNhanh '1',N'TP.HCM',N'Q.Thủ Đức',N'900,CMT8','4',N'Bình thường','2/1/2020','14'
GO
select * from CHINHANH
--Thêm chi nhánh(để test)
-- delete from Chinhanh

Exec ThemChiNhanh '1',N'TP.HCM',N'Q.Thủ Đức',N'123,CMT8','0111111111',N'Bình thường','2020-1-1'
Exec ThemChiNhanh  '1',N'TP.HCM',N'Q.Thủ Đức',N'900,CMT8','0111111112',N'Bình thường','2020-2-1'
Exec ThemChiNhanh '2',N'TP.HCM',N'Q.Bình Thạnh',N'500,Hai Bà Trưng','0111111113',N'Bình thường','2019-1-1'
Exec ThemChiNhanh '1',N'TP.HCM',N'Q.Thủ Đức',N'123,CMT8','0111111114',N'Tạm nghỉ','2021-1-1'
Exec ThemChiNhanh '3',N'TP.HCM',N'Q.1',N'112,Nguyễn Đình Chiểu','0686868686',N'Bình thường','2017-1-1'
Exec ThemChiNhanh '4',N'TP.HCM',N'Q.2',N'111,Hai Bà Trưng','0753951123',N'Bình thường','2022-10-1'
*/



--6. Thêm khách hàng
--Mô tả:
--input: thông tin khách hàng
--output:0- thành công, 1- thất bại
--kiểm tra thông tin có trống
--kiểm tra mã khách hàng đã tồn tại




--5. Thêm thực phẩm
--Mô tả:
--input: thông tin của thực phẩm
--output: 0- thành công, 1-thất bại
--kiểm tra đối tác có tồn tại
--kiểm tra mã món ăn có bị trùng
--kiểm tra tên thực phẩm có bị trùng
--tình trạng món chỉ là "có bán","Hết hàng hôm nay","Tạm ngưng"


 
 /*
 
 update DONDATHANG set TinhTrang = N'Chờ'
 select * from DONDATHANG
 
 update CHITIETDONDATHANG set MaDT = '1'
 select * from CHITIETDONDATHANG

 select *, (select k.HoTen from KHACHHANG k where k.MaKH = dh_cho.MaKH) TenKH,
 (select t.HoTen from TAIXE t where t.MaTX = dh_cho.MaTX) TenTX 
 from(select * from DONDATHANG where TinhTrang = N'Chờ') dh_cho  
 where MaDH in (select distinct MaDH from CHITIETDONDATHANG where MaDT = '1' )

 */
create proc XuHuongBan
	@madt varchar(10)
as
	begin try		
		select tp.MaDT, tp.MaTP, tp.TenMon,
			(select COUNT(MaDH) from CHITIETDONDATHANG ct where ct.MaTP = tp.MaTP and ct.MaDT = tp.MaDT and DanhGia = 'Like') Like_,
			(select SUM(SoLuong) from CHITIETDONDATHANG ct where ct.MaTP = tp.MaTP and ct.MaDT = tp.MaDT) Ban
		from THUCPHAM tp
		where MaDT =@madt

	end try
		
	begin catch
	end catch
go

exec XuHuongBan '1'
go

select distinct MaDT from CHITIETDONDATHANG where MaDH = '1'
go


create proc DsChiNhanhNull 
	@madt varchar(10)
as
	begin try
		select * from CHINHANH where MaDT = @madt and MaHopDong Is null
	end try
	begin catch
	end catch
go

create proc DsChiNhanh  
	@mahd varchar(10)
as
	begin try
		select * from CHINHANH where MaHopDong = @mahd
	end try
	begin catch
	end catch
go
/*
exec DsChiNhanhNull '1'
exec DsChiNhanh '1'
	
select * from DONDATHANG
go
*/
--Thêm thực phẩm 
create proc ThemThucPham
	@MaDT varchar(10),
	@TenMon nvarchar(30),
	@MieuTa nvarchar(50),
	@Gia decimal(10,1),
	@TinhTrang nvarchar(30),
	@TuyChon nvarchar(50)
as
	begin tran
		begin try
			if @MaDT='' or @TenMon='' or @MieuTa='' 
			or @TinhTrang='' or @TinhTrang='' or @TuyChon=''
			begin
				print N'Thông tin trống'
				rollback tran
				select 1
				return
			end
			if not exists(select* from DoiTac where MaDT=@MaDT)
			begin
				print N'Mã đối tác không tồn tại'
				rollback tran
				select 2
				return
			end
			if exists(select* from ThucPham where @TenMon=TenMon and MaDT=@MaDT)
			begin
				print N'Tên thực phẩm này đã tồn tại'
				rollback tran
				select 3
				return
			end
			if @TinhTrang<>N'Có bán' and @TinhTrang<>N'Hết hàng hôm nay' and @TinhTrang<>N'Tạm ngưng'
			begin
				print N'Tình trạng không hợp lệ'
				rollback tran
				select 4
				return
			end
			declare @MaTP varchar(10)
			set @MaTP='0'
			if exists (select * from ThucPham where MaDT=@MaDT)
			begin 
				set @MaTP=(select max(MaTP) from ThucPham where MaDT=@MaDT) 
			end
			set @MaTP=@MaTP+1
			insert into ThucPham values 
			(@MaTP,@MaDT,@TenMon,@MieuTa,@Gia,@TinhTrang,@TuyChon)
		end try
		begin catch
			print N'Lỗi hệ thống!'
			rollback tran
			select 10
			return
		END CATCH
COMMIT TRAN
select 0 
return
GO

create proc ThemHopDong
	@SLChiNhanh smallint,
	@SoTaiKhoan varchar(20),
	@NganHang nvarchar(30),
	@CNNganHang nvarchar(30),
	@MaSoThue varchar(13),
	@NgayKy date,
	@ThoiHan nvarchar(10),
	@NgayHetHan date,
	@MaDT varchar(10)
as
	begin tran
		begin try
			if @SLChiNhanh<=0  or  @SoTaiKhoan='' or @CNNganHang='' or @NganHang='' or
			@MaSoThue='' or @NgayKy='' or @ThoiHan='' or @NgayHetHan='' or @MaDT=''
			begin
				print N'Thông tin trống'
				rollback tran
				select 1
				return
			end
			if not exists(select* from DoiTac where @MaDT=MaDT)
			begin
				print N'Mã đối tác không tồn tại'
				select 2
				return
			end
			if @NgayKy>@NgayHetHan
			begin
				print N'Ngày hết hạn phải sau ngày ký'
				rollback tran
				select 3
				return
			end
			declare @ngDaiDien nvarchar(30)
			select @ngDaiDien=ngDaiDien 
			from DoiTac
			where MaDT=@MaDT


			declare @MaHD int
			set @MaHD=0
			if (select count(*) from HopDong)>0
			begin
				set @MaHD=(select max(CAST(MaHD as int)) from HopDong)
			end
			set @MaHD=@MaHD+1

			insert into HopDong values (CONVERT(varchar(10), @MaHD),@NgDaiDien,@SLChiNhanh,@SoTaiKhoan,@NganHang,@CNNganHang,@MaSoThue,@NgayKy,@ThoiHan,@NgayHetHan,@MaDT,NULL,N'Chờ duyệt')
			select (@MaHD*-1)
			
		end try
		begin catch
			print N'Lỗi hệ thống!'
			ROLLBACK TRAN
			select 10
			return
		END CATCH
COMMIT TRAN
GO

--Exec ThemHopDong 2,'stk','nh','cn','123123','2022-1-1','12','2022-1-2','1'
--Exec ThemHopDong 2,'12','21','23','32','12-8-2022','12331','11-8-2023','1'


--delete from HOPDONG
--select * from HOPDONG
--update CHINHANH set MaHopDong=null
--select * from CHINHANH
--update DONDATHANG set TinhTrang = N'Chờ'
/*
Exec ThemThucPham '1',N'Ca phê phân chồn',N'Đậm vị cà phê',200000,N'Có bán',N'Đường/Nhiệt độ'
Exec ThemThucPham '2',N'Cà phê đá xay',N'Đậm vị cà phê',20000,N'Có bán',N'Đường/Nhiệt độ'
Exec ThemThucPham '3',N'Cà phê bọt biển',N'Hương vị mới',50000,N'Có bán',N'Đường/Nhiệt độ'
Exec ThemThucPham '1',N'Gà chiêm nước mắm',N'Hương vị mới',30000,N'Có bán',N'Đường/Nhiệt độ'
Exec ThemThucPham '2',N'Sườn non',N'Hương vị mới',30000,N'Có bán',N'Đường/Nhiệt độ'
Exec ThemThucPham '3',N'Sườn nướng',N'Hương vị mới',30000,N'Có bán',N'Đường/Nhiệt độ'
*/
go
/*

delete ThuCPham
select * from ThucPham


*/

go

--Xóa thực phẩm
create -- create
proc XoaThucPham
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
				select 1
				return
			end
		if exists(select * from CHITIETDONDATHANG where MaTP = @MaTP and
			MaDT = @MaDT)
			begin
				print N'Không thể xoá, món ăn đã từng được lên đơn'
				rollback tran
				select 5
				return 
			end
		-- Kiểm tra tồn tại
		if not exists(select * from THUCPHAM where MaTP = @MaTP and
			MaDT = @MaDT)
			begin
				print N'Không thể xoá, món ăn không tồn tại'
				rollback tran
				select 6
				return
			end
		delete from THUCPHAM where MaTP = @MaTP and
			MaDT = @MaDT
	end try

	begin catch
		print N'Lỗi hệ thống!'
		rollback tran
		select 10
		return 
	end catch
commit tran
select 0
return 
go
/*
select * from ThucPham
exec XoaThucPham '2','3'
*/


--Update Thực phẩm
--Sửa thực phẩm
create --create
proc SuaThucPham
	@MaDT varchar(10),
	@TenMon nvarchar(30),
	@MieuTa nvarchar(50),
	@Gia decimal(10,1),
	@TinhTrang nvarchar(30),
	@TuyChon nvarchar(50),
	@MaTP varchar(10)
as
	begin tran
		begin try
			if @MaDT='' or @TenMon='' or @MieuTa='' 
			or @TinhTrang=''  or @TuyChon='' or @MaTP=''
			begin
				print N'Thông tin trống'
				rollback tran
				select 1
				return
			end
			if not exists(select* from DoiTac where MaDT=@MaDT)
			begin
				print N'Mã đối tác không tồn tại'
				rollback tran
				select 2
				return
			end
			if exists(select* from ThucPham where @TenMon=TenMon and (MaDT!=@MaDT or MaTP!=@MaTP))
			begin
				print N'Tên thực phẩm này đã tồn tại'
				rollback tran
				select 3
				return
			end
			if @TinhTrang<>N'Có bán' and @TinhTrang<>N'Hết hàng hôm nay' and @TinhTrang<>N'Tạm ngưng'
			begin
				print N'Tình trạng không hợp lệ'
				rollback tran
				select 4
				return
			end
			update THUCPHAM
			set TenMon=@TenMon,MieuTa=@MieuTa,Gia=@Gia,TinhTrang=@TinhTrang,TuyChon=@TuyChon
			where MaTP=@MaTp and MaDT=@MaDT
			if @Gia<=0
			begin 
				print N'Giá phải lớn hơn 0'
				rollback tran
				select 7
				return
			end
		end try
		begin catch
			print N'Lỗi hệ thống!'
			rollback tran
			select 10
			return
		END CATCH
COMMIT TRAN
select 0 
return
GO

/*
Exec SuaThucPham '1',N'Ca phê phân chồn',N'Đậm vị cà phê',200000,N'Có bán',N'Đường/Nhiệt độ','1'
Exec SuaThucPham '2',N'Cà phê đá xay',N'Đậm vị cà phê',20000,N'Có bán',N'Đường/Nhiệt độ','5'
Exec SuaThucPham '3',N'Cà phê bọt biển',N'Hương vị mới',50000,N'Có bán',N'Đường/Nhiệt độ','1'
Exec SuaThucPham '1',N'Gà chiêm nước mắm',N'Hương vị mới',30000,N'Có bán',N'Đường/Nhiệt độ','1'
Exec SuaThucPham '2',N'Sườn non',N'Hương vị mới',30000,N'Có bán',N'Đường/Nhiệt độ','1'
Exec SuaThucPham '3',N'Sườn nướng',N'Hương vị mới',30000,N'Có bán',N'Đường/Nhiệt độ','1'
*/


create --alter
proc SoLuongDonTheoNgay
	@MaDT varchar(10)
as
	begin tran
		begin try
			if not exists(select* from DoiTac where @MaDT=MaDT)
			begin
				print N'Mã đối tác không tồn tại'
				rollback tran
				select -1
				return
			end
			if not exists(select* from CHITIETDONDATHANG where MaDT=@MaDT)
			begin
				print N'Đối tác không có đơn hàng nào'
				rollback tran
				select -2
				return
			end
			select dh.NgayDat,count(*) as SLDon
			from (select ddh.NgayDat,ddh.MaDH from DONDATHANG ddh, CHITIETDONDATHANG ct where ddh.MaDH=ct.MaDH and ct.MaDT=@MaDT) dh
			group by dh.NgayDat
			order by dh.NgayDat desc
		end try
		begin catch
			print N'Lỗi hệ thống!'
			ROLLBACK TRAN
			select -10
			return
		END CATCH
COMMIT TRAN
return
GO
/*
Exec SoLuongDonTheoNgay '1'
Exec SoLuongDonTheoNgay '10'
*/
go
create --alter
proc SoLuongDonTheoThang
	@MaDT varchar(10)
as
	begin tran
		begin try
			if not exists(select* from DoiTac where @MaDT=MaDT)
			begin
				print N'Mã đối tác không tồn tại'
				rollback tran
				select -1
				return
			end
			if not exists(select* from CHITIETDONDATHANG where MaDT=@MaDT)
			begin
				print N'Đối tác không có đơn hàng nào'
				rollback tran
				select -2
				return
			end
			select year(dh.NgayDat) as Nam, month(dh.NgayDat) as Thang,count(*) as SLDon
			from (select ddh.NgayDat,ddh.MaDH from DONDATHANG ddh, CHITIETDONDATHANG ct where ddh.MaDH=ct.MaDH and ct.MaDT=@MaDT) dh
			group by year(dh.NgayDat),month(dh.NgayDat)
			order by year(dh.NgayDat) desc, month(dh.NgayDat) desc

		end try
		begin catch
			print N'Lỗi hệ thống!'
			ROLLBACK TRAN
			select -10
			return
		END CATCH
COMMIT TRAN
return
GO
go
create --alter
proc SoLuongDonTheoNam
	@MaDT varchar(10)
as
	begin tran
		begin try
			if not exists(select* from DoiTac where @MaDT=MaDT)
			begin
				print N'Mã đối tác không tồn tại'
				rollback tran
				select -1
				return
			end
			if not exists(select* from CHITIETDONDATHANG where MaDT=@MaDT)
			begin
				print N'Đối tác không có đơn hàng nào'
				rollback tran
				select -2
				return
			end
			select year(dh.NgayDat) as N'Năm',count(*) as SLDon
			from (select ddh.NgayDat,ddh.MaDH from DONDATHANG ddh, CHITIETDONDATHANG ct where ddh.MaDH=ct.MaDH and ct.MaDT=@MaDT) dh
			group by year(dh.NgayDat) 
			order by year(dh.NgayDat) desc

		end try
		begin catch
			print N'Lỗi hệ thống!'
			ROLLBACK TRAN
			select -10
			return
		END CATCH
COMMIT TRAN
return
GO
/*
Exec SoLuongDonTheoThang '1'
go
Exec SoLuongDonTheoNam '1'
go
*/