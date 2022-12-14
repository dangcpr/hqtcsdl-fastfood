USE [GIAONHANHANG]
GO

/****** Object:  Table [dbo].[CHINHANH]    Script Date: 11/12/2022 01:03:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CHINHANH](
	[STT] [int] NOT NULL,
	[MaDT] [varchar](10) NOT NULL,
	[TP] [nvarchar](30) NULL,
	[Quan] [nvarchar](30) NULL,
	[DiaChiCuThe] [nvarchar](50) NULL,
	[SDT] [char](10) NULL,
	[TinhTrang] [nvarchar](30) NULL,
	[NgayLap] [datetime] NULL,
	[MaHopDong] [varchar](30) NULL,
 CONSTRAINT [PK__CHINHANH__886CEEF5322FFC7F] PRIMARY KEY CLUSTERED 
(
	[STT] ASC,
	[MaDT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CHITIETDONDATHANG]    Script Date: 11/12/2022 01:03:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CHITIETDONDATHANG](
	[MaDH] [varchar](10) NOT NULL,
	[MaTP] [varchar](10) NOT NULL,
	[MaDT] [varchar](10) NOT NULL,
	[SoLuong] [int] NULL,
	[DanhGia] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaDH] ASC,
	[MaTP] ASC,
	[MaDT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DOITAC]    Script Date: 11/12/2022 01:03:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DOITAC](
	[MaDT] [varchar](10) NOT NULL,
	[Email] [varchar](30) NULL,
	[NgDaiDien] [nvarchar](30) NULL,
	[SLChiNhanh] [smallint] NULL,
	[TenQuan] [nvarchar](30) NULL,
	[LoaiTP] [nvarchar](30) NULL,
	[Username] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaDT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DONDATHANG]    Script Date: 11/12/2022 01:03:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DONDATHANG](
	[MaDH] [varchar](10) NOT NULL,
	[GioDat] [varchar](6) NULL,
	[NgayDat] [date] NULL,
	[GiaTriDH] [decimal](10, 1) NULL,
	[TinhTrang] [nvarchar](30) NULL,
	[MaKH] [varchar](10) NULL,
	[MaTX] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaDH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HOPDONG]    Script Date: 11/12/2022 01:03:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HOPDONG](
	[MaHD] [varchar](10) NOT NULL,
	[NgDaiDien] [nvarchar](30) NULL,
	[SLChiNhanh] [smallint] NULL,
	[SoTaiKhoan] [varchar](20) NULL,
	[NganHang] [nvarchar](30) NULL,
	[CNNganHang] [nvarchar](30) NULL,
	[MaSoThue] [varchar](13) NULL,
	[NgayKy] [date] NULL,
	[ThoiHan] [nvarchar](10) NULL,
	[NgayHetHan] [date] NULL,
	[MaDT] [varchar](10) NULL,
	[MaNV] [varchar](10) NULL,
	[TrangThai] [nvarchar](10) NULL,
 CONSTRAINT [PK__HOPDONG__2725A6E029CEB031] PRIMARY KEY CLUSTERED 
(
	[MaHD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KHACHHANG]    Script Date: 11/12/2022 01:03:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KHACHHANG](
	[MaKH] [varchar](10) NOT NULL,
	[HoTen] [nvarchar](30) NULL,
	[DiaChi] [nvarchar](100) NULL,
	[SDT] [char](10) NULL,
	[Email] [varchar](30) NULL,
	[Username] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaKH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NHANVIEN]    Script Date: 11/12/2022 01:03:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NHANVIEN](
	[MaNV] [varchar](10) NOT NULL,
	[HoTen] [nvarchar](30) NULL,
	[Username] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaNV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QUANTRI]    Script Date: 11/12/2022 01:03:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QUANTRI](
	[MaQT] [varchar](10) NOT NULL,
	[HoTen] [nvarchar](30) NULL,
	[Username] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaQT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TAIXE]    Script Date: 11/12/2022 01:03:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TAIXE](
	[MaTX] [varchar](10) NOT NULL,
	[CMND] [varchar](12) NULL,
	[HoTen] [nvarchar](30) NULL,
	[SDT] [char](10) NULL,
	[DiaChi] [nvarchar](100) NULL,
	[BienSoXe] [varchar](10) NULL,
	[KhuVucHoatDong] [nvarchar](30) NULL,
	[Email] [varchar](30) NULL,
	[SoTaiKhoan] [varchar](20) NULL,
	[NganHang] [nvarchar](30) NULL,
	[CNNganHang] [nvarchar](30) NULL,
	[Username] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaTX] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[THUCPHAM]    Script Date: 11/12/2022 01:03:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[THUCPHAM](
	[MaTP] [varchar](10) NOT NULL,
	[MaDT] [varchar](10) NOT NULL,
	[TenMon] [nvarchar](30) NULL,
	[MieuTa] [nvarchar](50) NULL,
	[Gia] [decimal](10, 1) NULL,
	[TinhTrang] [nvarchar](30) NULL,
	[TuyChon] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaTP] ASC,
	[MaDT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[USERS]    Script Date: 11/12/2022 01:03:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USERS](
	[Username] [varchar](20) NOT NULL,
	[Pass] [varchar](30) NULL,
	[RoleName] [varchar](9) NULL,
	[TrangThai] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CHINHANH]  WITH CHECK ADD  CONSTRAINT [FK__CHINHANH__MaDT__33D4B598] FOREIGN KEY([MaDT])
REFERENCES [dbo].[DOITAC] ([MaDT])
GO
ALTER TABLE [dbo].[CHINHANH] CHECK CONSTRAINT [FK__CHINHANH__MaDT__33D4B598]
GO
ALTER TABLE [dbo].[CHITIETDONDATHANG]  WITH CHECK ADD FOREIGN KEY([MaDH])
REFERENCES [dbo].[DONDATHANG] ([MaDH])
GO
ALTER TABLE [dbo].[CHITIETDONDATHANG]  WITH CHECK ADD FOREIGN KEY([MaTP], [MaDT])
REFERENCES [dbo].[THUCPHAM] ([MaTP], [MaDT])
GO
ALTER TABLE [dbo].[DOITAC]  WITH CHECK ADD  CONSTRAINT [FK_DOITAC_USERS] FOREIGN KEY([Username])
REFERENCES [dbo].[USERS] ([Username])
GO
ALTER TABLE [dbo].[DOITAC] CHECK CONSTRAINT [FK_DOITAC_USERS]
GO
ALTER TABLE [dbo].[DONDATHANG]  WITH CHECK ADD FOREIGN KEY([MaKH])
REFERENCES [dbo].[KHACHHANG] ([MaKH])
GO
ALTER TABLE [dbo].[DONDATHANG]  WITH CHECK ADD FOREIGN KEY([MaTX])
REFERENCES [dbo].[TAIXE] ([MaTX])
GO
ALTER TABLE [dbo].[HOPDONG]  WITH CHECK ADD FOREIGN KEY([MaDT])
REFERENCES [dbo].[DOITAC] ([MaDT])
GO
ALTER TABLE [dbo].[HOPDONG]  WITH CHECK ADD FOREIGN KEY([MaNV])
REFERENCES [dbo].[NHANVIEN] ([MaNV])
GO
ALTER TABLE [dbo].[KHACHHANG]  WITH CHECK ADD  CONSTRAINT [FK_KHACHHANG_USERS] FOREIGN KEY([Username])
REFERENCES [dbo].[USERS] ([Username])
GO
ALTER TABLE [dbo].[KHACHHANG] CHECK CONSTRAINT [FK_KHACHHANG_USERS]
GO
ALTER TABLE [dbo].[NHANVIEN]  WITH CHECK ADD  CONSTRAINT [FK_NHANVIEN_USERS] FOREIGN KEY([Username])
REFERENCES [dbo].[USERS] ([Username])
GO
ALTER TABLE [dbo].[NHANVIEN] CHECK CONSTRAINT [FK_NHANVIEN_USERS]
GO
ALTER TABLE [dbo].[QUANTRI]  WITH CHECK ADD FOREIGN KEY([Username])
REFERENCES [dbo].[USERS] ([Username])
GO
ALTER TABLE [dbo].[TAIXE]  WITH CHECK ADD  CONSTRAINT [FK_TAIXE_USERS] FOREIGN KEY([Username])
REFERENCES [dbo].[USERS] ([Username])
GO
ALTER TABLE [dbo].[TAIXE] CHECK CONSTRAINT [FK_TAIXE_USERS]
GO
ALTER TABLE [dbo].[THUCPHAM]  WITH CHECK ADD FOREIGN KEY([MaDT])
REFERENCES [dbo].[DOITAC] ([MaDT])
GO

--Data
INSERT [dbo].[USERS] ([Username], [Pass], [RoleName], [TrangThai]) VALUES (N'ab', N'abc', N'KhachHang', N'Hoạt động')
INSERT [dbo].[USERS] ([Username], [Pass], [RoleName], [TrangThai]) VALUES (N'abc', N'abc', N'KhachHang', N'Hoạt động')
INSERT [dbo].[USERS] ([Username], [Pass], [RoleName], [TrangThai]) VALUES (N'bcd', N'bcd', N'DoiTac', N'Hoạt động')
INSERT [dbo].[USERS] ([Username], [Pass], [RoleName], [TrangThai]) VALUES (N'dang', N'dang', N'TaiXe', N'Hoạt động')
INSERT [dbo].[USERS] ([Username], [Pass], [RoleName], [TrangThai]) VALUES (N'dang2', N'dang2', N'TaiXe', N'Hoạt động')
INSERT [dbo].[USERS] ([Username], [Pass], [RoleName], [TrangThai]) VALUES (N'dang3', N'dang3', N'TaiXe', N'Hoạt động')
INSERT [dbo].[USERS] ([Username], [Pass], [RoleName], [TrangThai]) VALUES (N'dt1', N'1234', N'KhachHang', N'Hoạt động')
INSERT [dbo].[USERS] ([Username], [Pass], [RoleName], [TrangThai]) VALUES (N'dt2', N'123456', N'DoiTac', N'Hoạt động')
INSERT [dbo].[USERS] ([Username], [Pass], [RoleName], [TrangThai]) VALUES (N'dt5', N'dt5', N'DoiTac', N'Hoạt động')
INSERT [dbo].[USERS] ([Username], [Pass], [RoleName], [TrangThai]) VALUES (N'kh3', N'kh3', N'KhachHang', N'Hoạt động')
INSERT [dbo].[USERS] ([Username], [Pass], [RoleName], [TrangThai]) VALUES (N'lmt', N'123456', N'NhanVien', N'Hoạt động')
INSERT [dbo].[USERS] ([Username], [Pass], [RoleName], [TrangThai]) VALUES (N'mqv', N'123456', N'KhachHang', N'Hoạt động')
INSERT [dbo].[USERS] ([Username], [Pass], [RoleName], [TrangThai]) VALUES (N'nhd', N'123456', N'DoiTac', N'Hoạt động')
INSERT [dbo].[USERS] ([Username], [Pass], [RoleName], [TrangThai]) VALUES (N'qt1', N'qt1', N'QuanTri', N'Hoạt động')
INSERT [dbo].[USERS] ([Username], [Pass], [RoleName], [TrangThai]) VALUES (N'qt3', N'qt3', N'QuanTri', N'Hoạt động')
INSERT [dbo].[USERS] ([Username], [Pass], [RoleName], [TrangThai]) VALUES (N'vvmd1', N'1234', N'QuanTri', N'Hoạt động')
GO
INSERT [dbo].[QUANTRI] ([MaQT], [HoTen], [Username]) VALUES (N'1', N'Võ Văn Minh Đoàn', N'vvmd1')
INSERT [dbo].[QUANTRI] ([MaQT], [HoTen], [Username]) VALUES (N'2', N'qt3', N'qt3')
GO
INSERT [dbo].[KHACHHANG] ([MaKH], [HoTen], [DiaChi], [SDT], [Email], [Username]) VALUES (N'1', N'Nguyễn Văn A', NULL, NULL, NULL, NULL)
INSERT [dbo].[KHACHHANG] ([MaKH], [HoTen], [DiaChi], [SDT], [Email], [Username]) VALUES (N'2', N'Nguyễn Hải Đăng', N'Thủ Đức', N'0369994914', N'dang2@gmail.com', N'abc')
INSERT [dbo].[KHACHHANG] ([MaKH], [HoTen], [DiaChi], [SDT], [Email], [Username]) VALUES (N'3', N'ab', N'Quận 1 - TPHCM', N'ab        ', N'ab', N'ab')
INSERT [dbo].[KHACHHANG] ([MaKH], [HoTen], [DiaChi], [SDT], [Email], [Username]) VALUES (N'4', N'kh3', N'Quận 1', N'012345    ', N'kh3', N'kh3')
GO
INSERT [dbo].[DOITAC] ([MaDT], [Email], [NgDaiDien], [SLChiNhanh], [TenQuan], [LoaiTP], [Username]) VALUES (N'1', N'dt2@gmail.com', N'NVA', 2, N'Gà nướng', N'Gà', N'nhd')
INSERT [dbo].[DOITAC] ([MaDT], [Email], [NgDaiDien], [SLChiNhanh], [TenQuan], [LoaiTP], [Username]) VALUES (N'2', N'abc@gmail.com', N'Nguyễn Văn B', 13, N'Barbecue Gia Định', N'TPHCM', NULL)
INSERT [dbo].[DOITAC] ([MaDT], [Email], [NgDaiDien], [SLChiNhanh], [TenQuan], [LoaiTP], [Username]) VALUES (N'3', N'vv', N'vv', 14, N'abc', N'abc', NULL)
INSERT [dbo].[DOITAC] ([MaDT], [Email], [NgDaiDien], [SLChiNhanh], [TenQuan], [LoaiTP], [Username]) VALUES (N'4', N'bcd@gmail.com', N'Nguyễn Văn A', 8, N'Gia Đình', N'Cơm bình dân', N'bcd')
INSERT [dbo].[DOITAC] ([MaDT], [Email], [NgDaiDien], [SLChiNhanh], [TenQuan], [LoaiTP], [Username]) VALUES (N'5', N'dt2@gmail.com', N'NVA', 2, N'Gà nướng', N'Gà', N'dt2')
INSERT [dbo].[DOITAC] ([MaDT], [Email], [NgDaiDien], [SLChiNhanh], [TenQuan], [LoaiTP], [Username]) VALUES (N'6', N'dt5@gmail.com', N'NVA', 1, N'DT5', N'Vịt quay', N'dt5')
GO
INSERT [dbo].[TAIXE] ([MaTX], [CMND], [HoTen], [SDT], [DiaChi], [BienSoXe], [KhuVucHoatDong], [Email], [SoTaiKhoan], [NganHang], [CNNganHang], [Username]) VALUES (N'1', N'0294003884', N'Nguyễn Văn B', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[TAIXE] ([MaTX], [CMND], [HoTen], [SDT], [DiaChi], [BienSoXe], [KhuVucHoatDong], [Email], [SoTaiKhoan], [NganHang], [CNNganHang], [Username]) VALUES (N'2', N'123456789', N'Đăng', N'0123456789', N'Quận 2', N'12345', N'Quận 2', N'dang@gmail.com', N'1234567', N'BIDV', N'ABC', N'dang')
INSERT [dbo].[TAIXE] ([MaTX], [CMND], [HoTen], [SDT], [DiaChi], [BienSoXe], [KhuVucHoatDong], [Email], [SoTaiKhoan], [NganHang], [CNNganHang], [Username]) VALUES (N'3', N'123456789', N'NVA', N'0123456789', N'ABC', N'123456', N'Quận 2', N'abc@gmail.com', N'12345678912', N'Agribank', N'Bình Thạnh', N'dang2')
INSERT [dbo].[TAIXE] ([MaTX], [CMND], [HoTen], [SDT], [DiaChi], [BienSoXe], [KhuVucHoatDong], [Email], [SoTaiKhoan], [NganHang], [CNNganHang], [Username]) VALUES (N'4', N'123456789', N'Nguyễn Văn B', N'0123456789', N'Củ Chi - TPHCM', N'71BA-00345', N'Củ Chi - TPHCM', N'dang3@gmail.com', N'123456789', N'BIDV', N'Phố Núi', N'dang3')
GO
INSERT [dbo].[NHANVIEN] ([MaNV], [HoTen], [Username]) VALUES (N'1', N'Nguyễn Văn B', N'lmt')
GO
INSERT [dbo].[HOPDONG] ([MaHD], [NgDaiDien], [SLChiNhanh], [SoTaiKhoan], [NganHang], [CNNganHang], [MaSoThue], [NgayKy], [ThoiHan], [NgayHetHan], [MaDT], [MaNV], [TrangThai]) VALUES (N'1', N'Nguyễn Văn A', 3, N'123456', N'Agribank', N'Bình Thạnh', N'23456', CAST(N'2022-12-10' AS Date), N'2 tháng', CAST(N'2023-01-22' AS Date), N'1', N'1', N'Đã duyệt')
INSERT [dbo].[HOPDONG] ([MaHD], [NgDaiDien], [SLChiNhanh], [SoTaiKhoan], [NganHang], [CNNganHang], [MaSoThue], [NgayKy], [ThoiHan], [NgayHetHan], [MaDT], [MaNV], [TrangThai]) VALUES (N'2', N'Nguyễn Văn A', 1, N'1234', N'BIDV', N'Bình Thạnh', N'1111', CAST(N'2022-12-11' AS Date), N'3 năm', CAST(N'2025-12-11' AS Date), N'1', N'1', N'Đã duyệt')
INSERT [dbo].[HOPDONG] ([MaHD], [NgDaiDien], [SLChiNhanh], [SoTaiKhoan], [NganHang], [CNNganHang], [MaSoThue], [NgayKy], [ThoiHan], [NgayHetHan], [MaDT], [MaNV], [TrangThai]) VALUES (N'3', N'Nguyễn Văn A', 1, N'123', N'BIDV', N'BT', N'123', CAST(N'2022-12-11' AS Date), N'2 năm', CAST(N'2024-12-24' AS Date), N'1', N'1', N'Đã duyệt')
INSERT [dbo].[HOPDONG] ([MaHD], [NgDaiDien], [SLChiNhanh], [SoTaiKhoan], [NganHang], [CNNganHang], [MaSoThue], [NgayKy], [ThoiHan], [NgayHetHan], [MaDT], [MaNV], [TrangThai]) VALUES (N'4', N'Nguyễn Văn A', 1, N'123456789', N'BIDV', N'BT', N'123', CAST(N'2022-08-11' AS Date), N'2 năm', CAST(N'2024-12-22' AS Date), N'1', NULL, N'Chờ duyệt')
INSERT [dbo].[HOPDONG] ([MaHD], [NgDaiDien], [SLChiNhanh], [SoTaiKhoan], [NganHang], [CNNganHang], [MaSoThue], [NgayKy], [ThoiHan], [NgayHetHan], [MaDT], [MaNV], [TrangThai]) VALUES (N'5', N'NVA', 1, N'127980', N'BIDV', N'Miền Đông', N'9702', CAST(N'2022-08-20' AS Date), N'1 tháng', CAST(N'2022-09-20' AS Date), N'6', NULL, N'Chờ duyệt')
GO
INSERT [dbo].[CHINHANH] ([STT], [MaDT], [TP], [Quan], [DiaChiCuThe], [SDT], [TinhTrang], [NgayLap], [MaHopDong]) VALUES (1, N'1', N'TPHCM', N'Quận 1', N'22 Xô Viết Nghệ Tĩnh', N'03123455  ', N'Bình thường', CAST(N'2022-08-08T00:00:00.000' AS DateTime), N'2')
INSERT [dbo].[CHINHANH] ([STT], [MaDT], [TP], [Quan], [DiaChiCuThe], [SDT], [TinhTrang], [NgayLap], [MaHopDong]) VALUES (1, N'5', N'TPHCM', N'Quận 1', N'22 Nguyễn Thiện Thuật', N'012345678 ', N'Bình thường', NULL, N'2022-01-08')
INSERT [dbo].[CHINHANH] ([STT], [MaDT], [TP], [Quan], [DiaChiCuThe], [SDT], [TinhTrang], [NgayLap], [MaHopDong]) VALUES (1, N'6', N'TPHCM', N'12', N'Nguyễn Siêu', N'0387      ', N'Bình thường', CAST(N'2022-08-19T00:00:00.000' AS DateTime), N'5')
INSERT [dbo].[CHINHANH] ([STT], [MaDT], [TP], [Quan], [DiaChiCuThe], [SDT], [TinhTrang], [NgayLap], [MaHopDong]) VALUES (2, N'1', N'HCM', N'12', N'Nguyễn Thị Định', N'012345    ', N'Bình thường', CAST(N'2022-08-01T00:00:00.000' AS DateTime), N'3')
INSERT [dbo].[CHINHANH] ([STT], [MaDT], [TP], [Quan], [DiaChiCuThe], [SDT], [TinhTrang], [NgayLap], [MaHopDong]) VALUES (3, N'1', N'TPHCM', N'3', N'Ung Văn Khiêm', N'0123456   ', N'Bình thường', CAST(N'2022-08-03T00:00:00.000' AS DateTime), N'4')
GO
INSERT [dbo].[THUCPHAM] ([MaTP], [MaDT], [TenMon], [MieuTa], [Gia], [TinhTrang], [TuyChon]) VALUES (N'1', N'1', N'abc', N'abc', CAST(30000.0 AS Decimal(10, 1)), N'Còn hàng', N'Ít cơm')
INSERT [dbo].[THUCPHAM] ([MaTP], [MaDT], [TenMon], [MieuTa], [Gia], [TinhTrang], [TuyChon]) VALUES (N'1', N'6', N'Vịt nướng Vân Đình', N'Đặc sản Vĩnh Phúc', CAST(250000.0 AS Decimal(10, 1)), N'Có bán', N'Bình thường')
INSERT [dbo].[THUCPHAM] ([MaTP], [MaDT], [TenMon], [MieuTa], [Gia], [TinhTrang], [TuyChon]) VALUES (N'2', N'2', N'ghu', N'ghu', CAST(50000.0 AS Decimal(10, 1)), N'Còn hàng', N'Nhiều cơm')
INSERT [dbo].[THUCPHAM] ([MaTP], [MaDT], [TenMon], [MieuTa], [Gia], [TinhTrang], [TuyChon]) VALUES (N'3', N'1', N'bcd', N'bcd', CAST(40000.0 AS Decimal(10, 1)), N'Hết hàng', NULL)
INSERT [dbo].[THUCPHAM] ([MaTP], [MaDT], [TenMon], [MieuTa], [Gia], [TinhTrang], [TuyChon]) VALUES (N'4', N'1', N'Cà chua', N'Ngon ngất ngây', CAST(25000.0 AS Decimal(10, 1)), N'Có bán', N'')
GO
INSERT [dbo].[DONDATHANG] ([MaDH], [GioDat], [NgayDat], [GiaTriDH], [TinhTrang], [MaKH], [MaTX]) VALUES (N'1', N'13:30', CAST(N'2022-09-25' AS Date), CAST(350000.0 AS Decimal(10, 1)), N'Đã đến nơi', N'1', N'2')
INSERT [dbo].[DONDATHANG] ([MaDH], [GioDat], [NgayDat], [GiaTriDH], [TinhTrang], [MaKH], [MaTX]) VALUES (N'10', N'17:12', CAST(N'2022-12-11' AS Date), CAST(220000.0 AS Decimal(10, 1)), N'Chờ xử lý', N'2', NULL)
INSERT [dbo].[DONDATHANG] ([MaDH], [GioDat], [NgayDat], [GiaTriDH], [TinhTrang], [MaKH], [MaTX]) VALUES (N'11', N'17:12', CAST(N'2022-12-11' AS Date), CAST(120000.0 AS Decimal(10, 1)), N'Chờ xử lý', N'3', NULL)
INSERT [dbo].[DONDATHANG] ([MaDH], [GioDat], [NgayDat], [GiaTriDH], [TinhTrang], [MaKH], [MaTX]) VALUES (N'2', N'22:35', CAST(N'2022-01-12' AS Date), CAST(250000.0 AS Decimal(10, 1)), N'Tài xế đang giao', N'3', N'3')
INSERT [dbo].[DONDATHANG] ([MaDH], [GioDat], [NgayDat], [GiaTriDH], [TinhTrang], [MaKH], [MaTX]) VALUES (N'3', N'14:25', CAST(N'2022-08-24' AS Date), CAST(150000.0 AS Decimal(10, 1)), N'Đã hủy đơn', N'3', NULL)
INSERT [dbo].[DONDATHANG] ([MaDH], [GioDat], [NgayDat], [GiaTriDH], [TinhTrang], [MaKH], [MaTX]) VALUES (N'4', N'16:12', CAST(N'2022-12-10' AS Date), NULL, N'Đã hủy đơn', N'3', NULL)
INSERT [dbo].[DONDATHANG] ([MaDH], [GioDat], [NgayDat], [GiaTriDH], [TinhTrang], [MaKH], [MaTX]) VALUES (N'5', N'16:12', CAST(N'2022-12-10' AS Date), CAST(240000.0 AS Decimal(10, 1)), N'Tài xế đang giao', N'3', N'2')
INSERT [dbo].[DONDATHANG] ([MaDH], [GioDat], [NgayDat], [GiaTriDH], [TinhTrang], [MaKH], [MaTX]) VALUES (N'6', N'21:12', CAST(N'2022-12-10' AS Date), CAST(160000.0 AS Decimal(10, 1)), N'Chờ giao', N'2', N'1')
INSERT [dbo].[DONDATHANG] ([MaDH], [GioDat], [NgayDat], [GiaTriDH], [TinhTrang], [MaKH], [MaTX]) VALUES (N'7', N'14:17', CAST(N'2022-12-11' AS Date), CAST(70000.0 AS Decimal(10, 1)), N'Đã hủy đơn', N'3', N'1')
INSERT [dbo].[DONDATHANG] ([MaDH], [GioDat], [NgayDat], [GiaTriDH], [TinhTrang], [MaKH], [MaTX]) VALUES (N'8', N'14:12', CAST(N'2022-12-11' AS Date), CAST(70000.0 AS Decimal(10, 1)), N'Đã đến nơi', N'2', N'2')
INSERT [dbo].[DONDATHANG] ([MaDH], [GioDat], [NgayDat], [GiaTriDH], [TinhTrang], [MaKH], [MaTX]) VALUES (N'9', N'14:12', CAST(N'2022-12-11' AS Date), CAST(130000.0 AS Decimal(10, 1)), N'Tài xế đang giao', N'2', N'2')
GO
INSERT [dbo].[CHITIETDONDATHANG] ([MaDH], [MaTP], [MaDT], [SoLuong], [DanhGia]) VALUES (N'10', N'2', N'2', 3, NULL)
INSERT [dbo].[CHITIETDONDATHANG] ([MaDH], [MaTP], [MaDT], [SoLuong], [DanhGia]) VALUES (N'10', N'4', N'1', 2, NULL)
INSERT [dbo].[CHITIETDONDATHANG] ([MaDH], [MaTP], [MaDT], [SoLuong], [DanhGia]) VALUES (N'11', N'1', N'1', 2, NULL)
INSERT [dbo].[CHITIETDONDATHANG] ([MaDH], [MaTP], [MaDT], [SoLuong], [DanhGia]) VALUES (N'11', N'3', N'1', 1, NULL)
INSERT [dbo].[CHITIETDONDATHANG] ([MaDH], [MaTP], [MaDT], [SoLuong], [DanhGia]) VALUES (N'2', N'1', N'1', 3, N'Tốt')
INSERT [dbo].[CHITIETDONDATHANG] ([MaDH], [MaTP], [MaDT], [SoLuong], [DanhGia]) VALUES (N'2', N'3', N'1', 2, N'Rất tốt')
INSERT [dbo].[CHITIETDONDATHANG] ([MaDH], [MaTP], [MaDT], [SoLuong], [DanhGia]) VALUES (N'4', N'1', N'1', 2, NULL)
INSERT [dbo].[CHITIETDONDATHANG] ([MaDH], [MaTP], [MaDT], [SoLuong], [DanhGia]) VALUES (N'4', N'3', N'1', 2, NULL)
INSERT [dbo].[CHITIETDONDATHANG] ([MaDH], [MaTP], [MaDT], [SoLuong], [DanhGia]) VALUES (N'5', N'1', N'1', 2, NULL)
INSERT [dbo].[CHITIETDONDATHANG] ([MaDH], [MaTP], [MaDT], [SoLuong], [DanhGia]) VALUES (N'5', N'3', N'1', 4, NULL)
INSERT [dbo].[CHITIETDONDATHANG] ([MaDH], [MaTP], [MaDT], [SoLuong], [DanhGia]) VALUES (N'6', N'1', N'1', 2, NULL)
INSERT [dbo].[CHITIETDONDATHANG] ([MaDH], [MaTP], [MaDT], [SoLuong], [DanhGia]) VALUES (N'8', N'2', N'2', 1, NULL)
INSERT [dbo].[CHITIETDONDATHANG] ([MaDH], [MaTP], [MaDT], [SoLuong], [DanhGia]) VALUES (N'9', N'1', N'1', 1, NULL)
INSERT [dbo].[CHITIETDONDATHANG] ([MaDH], [MaTP], [MaDT], [SoLuong], [DanhGia]) VALUES (N'9', N'3', N'1', 2, NULL)
GO

