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

