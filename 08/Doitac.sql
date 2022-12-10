-- Tóm tắm hàm

/*
Đây là những proc mà phân hệ đối tác dùng


SoLuongDonTheoNam
SoLuongDonTheoThang
SoLuongDonTheoNgay
SuaThucPham
XoaThucPham
ThemHopDong
ThemThucPham
DsChiNhanh
DsChiNhanhNull
XuHuongBan
SuaChiNhanh
XoaChiNhanh
ThemChiNhanh


select * from USERS
insert into USERS values ()

*/


-- Database

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
	MaHopDong varchar(30)
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
-- Đối tác (done)
go
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

create -- alter
proc XoaChiNhanh
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
			update DoiTac
			set SLChiNhanh=SLChiNhanh-1
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

create --  alter
proc SuaChiNhanh
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
			or @SDT='' or @NgayLap='' or @stt =''
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


 

-- update DONDATHANG set TinhTrang = N'Chờ'
go

create function DemSoLuongBan(@MaTP varchar(10), @MaDT varchar(10))
returns int
as
begin
	declare @out int
	set @out = (select (SUM(SoLuong+0)) from CHITIETDONDATHANG ct where ct.MaTP = @MaTP and ct.MaDT = @MaDT)
	if @out is null
		set @out = 0
	return @out
end	
go
create -- alter 
proc XuHuongBan
	@madt varchar(10)
as
	begin try		
		select tp.MaDT, tp.MaTP, tp.TenMon,
			(select COUNT(MaDH) from CHITIETDONDATHANG ct where ct.MaTP = tp.MaTP and ct.MaDT = tp.MaDT and DanhGia = 'Like') Like_,
			(dbo.DemSoLuongBan(tp.MaTP,tp.MaDT)) Ban
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
exec DsChiNhanhNull '1'
exec DsChiNhanh '1'
	
select * from DONDATHANG
go
--Thêm thực phẩm 
create -- alter
proc ThemThucPham
	@MaDT varchar(10),
	@TenMon nvarchar(30),
	@MieuTa nvarchar(50),
	@Gia decimal(10,1),
	@TinhTrang nvarchar(30),
	@TuyChon nvarchar(50)
as
	begin tran
		begin try
			if @MaDT='' or @TenMon='' --or @MieuTa='' 
			or @TinhTrang=''-- or @TuyChon=''
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
			declare @MaTP int
			set @MaTP=0
			if exists (select * from ThucPham where MaDT=@MaDT)
			begin 
				set @MaTP=(select max(MaTP) from ThucPham where MaDT=@MaDT) 
			end
			set @MaTP=@MaTP+1
			insert into ThucPham values 
			(CONVERT(varchar(10), @MaTP),@MaDT,@TenMon,@MieuTa,@Gia,@TinhTrang,@TuyChon)
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

create --alter
proc ThemHopDong
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

			select (@MaHD*-1)
			insert into HopDong values (CONVERT(varchar(10), @MaHD),@NgDaiDien,@SLChiNhanh,@SoTaiKhoan,@NganHang,@CNNganHang,@MaSoThue,@NgayKy,@ThoiHan,@NgayHetHan,@MaDT,NULL,N'Chờ duyệt')
			
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

select * from ThucPham
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
create -- alter
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
			if @MaDT='' or @TenMon='' 
			or @TinhTrang=''   or @MaTP=''
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


Exec ThemThucPham '1',N'Gà xối mỡa',N' ','25000.0',N'Có bán',N' '


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

-- Khách hàng (chưa)

-- Tài xế (done)
create proc ThemTaiXe
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
	@CNNganHang nvarchar(30),
	@Username varchar(20)
as 
	begin tran ThemTaiXe
		begin try
			if @MaTX='' or @CMND='' or @HoTen=''
			or @SDT='' or @DiaChi='' or @BienSoXe=''
			or @KhuVucHoatDong='' or @Email='' or @SoTaiKhoan=''
			or @NganHang='' or @CNNganHang='' OR @Username =''
			begin 
				print N'Thông tin trống'
				select 1
				rollback tran ThemTaiXe
			end
			if exists(SELECT * from TAIXE where MaTX = @MaTX)
			begin
				print N'Mã tài xế đã tồn tại'
				select 2
				rollback tran ThemTaiXe
			end
			insert into TAIXE values(@MaTX, @CMND,@HoTen,@SDT,@DiaChi,@BienSoXe,@KhuVucHoatDong,@Email,@SoTaiKhoan,@NganHang,@CNNganHang,@Username)
		end try
		begin catch
			print N'Lỗi hệ thống!'
			ROLLBACK TRAN ThemTaiXe
		END CATCH
COMMIT TRAN ThemTaiXe
select 0
GO

-- Quản trị viên (chưa)

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
			Select 1 as code
			ROLLBACK TRAN
		END
		
	END TRY
	BEGIN CATCH
		print N'Lỗi hệ thống!'
		Select 2 as code
		ROLLBACK TRAN
	END CATCH
COMMIT TRAN
Select 0 as code
GO

/*

insert into USERS values ('vanniee','1','','')
insert into USERS values ('tx','1','','')
insert into USERS values ('kh','1','','')
select * from USERS

insert into DOITAC values ('1','','','','','','vanniee')
select * from DOITAC

insert into TAIXE values ('1','',N'Nguyễn Tài Xế','','','','','','','','','tx')
select * from TAIXE

insert into KHACHHANG values ('1',N'Phạm Khách Hàng','','','','kh')
select * from KHACHHANG
select * from THUCPHAM

insert into DONDATHANG values ('1','12:00','2022-12-13',45000,'','1','1')
insert into DONDATHANG values ('2','12:00','2022-12-13',30000,'','1','1')
insert into DONDATHANG values ('3','12:00','2022-12-13',15000,'','1','1')
insert into DONDATHANG values ('4','12:00','2022-12-13',45000,'','1','1')
insert into DONDATHANG values ('5','12:00','2022-12-13',60000,'','1','1')
insert into DONDATHANG values ('6','12:00','2022-12-13',25000,'','1','1')
insert into DONDATHANG values ('7','12:00','2022-12-13',35000,'','1','1')
insert into DONDATHANG values ('8','12:00','2022-12-13',55000,'','1','1')
insert into DONDATHANG values ('9','12:00','2021-5-13',55000,'','1','1')
insert into DONDATHANG values ('10','12:00','2021-2-13',55000,'','1','1')
insert into DONDATHANG values ('11','12:00','2021-4-13',55000,'','1','1')
insert into DONDATHANG values ('12','12:00','2021-2-13',55000,'','1','1')
insert into DONDATHANG values ('13','12:00','2021-1-13',55000,'','1','1')
insert into DONDATHANG values ('14','12:00','2022-5-13',55000,'','1','1')
insert into DONDATHANG values ('15','12:00','2022-4-13',55000,'','1','1')
insert into DONDATHANG values ('16','12:00','2022-7-13',55000,'','1','1')
insert into DONDATHANG values ('17','12:00','2021-4-13',55000,'','1','1')
insert into DONDATHANG values ('18','12:00','2022-8-13',55000,'','1','1')
insert into DONDATHANG values ('19','12:00','2021-1-13',55000,'','1','1')
insert into DONDATHANG values ('20','12:00','2021-5-13',55000,'','1','1')
insert into DONDATHANG values ('21','12:00','2020-5-13',55000,'','1','1')
update DONDATHANG set TinhTrang = N'Chờ'
select * from DONDATHANG

insert into CHITIETDONDATHANG values ('1','2','1',3,'Like')
insert into CHITIETDONDATHANG values ('2','1','1',1,'Like')
insert into CHITIETDONDATHANG values ('3','4','1',1,'Like')
insert into CHITIETDONDATHANG values ('4','3','1',1,'Like')
insert into CHITIETDONDATHANG values ('4','4','1',1,'Like')
insert into CHITIETDONDATHANG values ('5','3','1',1,'Like')
insert into CHITIETDONDATHANG values ('5','5','1',1,'Like')
insert into CHITIETDONDATHANG values ('5','6','1',1,'Like')
insert into CHITIETDONDATHANG values ('5','7','1',1,'Like')
insert into CHITIETDONDATHANG values ('9','1','1',1,'Like')
insert into CHITIETDONDATHANG values ('10','2','1',1,'Like')
insert into CHITIETDONDATHANG values ('11','6','1',1,'Like')
insert into CHITIETDONDATHANG values ('12','3','1',1,'Like')
insert into CHITIETDONDATHANG values ('13','5','1',1,'Like')
insert into CHITIETDONDATHANG values ('14','1','1',1,'Like')
insert into CHITIETDONDATHANG values ('15','2','1',1,'Like')
insert into CHITIETDONDATHANG values ('16','3','1',1,'Like')
insert into CHITIETDONDATHANG values ('17','4','1',1,'Like')
insert into CHITIETDONDATHANG values ('18','2','1',1,'Like')
insert into CHITIETDONDATHANG values ('19','5','1',1,'Like')
insert into CHITIETDONDATHANG values ('20','2','1',1,'Like')
insert into CHITIETDONDATHANG values ('21','1','1',1,'Like')
select * from CHITIETDONDATHANG
select * from THUCPHAM




*/