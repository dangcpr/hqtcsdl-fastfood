﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _08.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class GIAONHANHANGEntities : DbContext
    {
        public GIAONHANHANGEntities()
            : base("name=GIAONHANHANGEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CHINHANH> CHINHANHs { get; set; }
        public virtual DbSet<CHITIETDONDATHANG> CHITIETDONDATHANGs { get; set; }
        public virtual DbSet<DOITAC> DOITACs { get; set; }
        public virtual DbSet<DONDATHANG> DONDATHANGs { get; set; }
        public virtual DbSet<HOPDONG> HOPDONGs { get; set; }
        public virtual DbSet<KHACHHANG> KHACHHANGs { get; set; }
        public virtual DbSet<NHANVIEN> NHANVIENs { get; set; }
        public virtual DbSet<TAIXE> TAIXEs { get; set; }
        public virtual DbSet<THUCPHAM> THUCPHAMs { get; set; }
        public virtual DbSet<USER> USERS { get; set; }
    
        public virtual int sp_Them_Chi_Nhanh(Nullable<int> sTT, string maDT, string tP, string quan, string diaChiCuThe, string sDT, string tinhTrang, Nullable<System.DateTime> ngayLap)
        {
            var sTTParameter = sTT.HasValue ?
                new ObjectParameter("STT", sTT) :
                new ObjectParameter("STT", typeof(int));
    
            var maDTParameter = maDT != null ?
                new ObjectParameter("MaDT", maDT) :
                new ObjectParameter("MaDT", typeof(string));
    
            var tPParameter = tP != null ?
                new ObjectParameter("TP", tP) :
                new ObjectParameter("TP", typeof(string));
    
            var quanParameter = quan != null ?
                new ObjectParameter("Quan", quan) :
                new ObjectParameter("Quan", typeof(string));
    
            var diaChiCuTheParameter = diaChiCuThe != null ?
                new ObjectParameter("DiaChiCuThe", diaChiCuThe) :
                new ObjectParameter("DiaChiCuThe", typeof(string));
    
            var sDTParameter = sDT != null ?
                new ObjectParameter("SDT", sDT) :
                new ObjectParameter("SDT", typeof(string));
    
            var tinhTrangParameter = tinhTrang != null ?
                new ObjectParameter("TinhTrang", tinhTrang) :
                new ObjectParameter("TinhTrang", typeof(string));
    
            var ngayLapParameter = ngayLap.HasValue ?
                new ObjectParameter("NgayLap", ngayLap) :
                new ObjectParameter("NgayLap", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_Them_Chi_Nhanh", sTTParameter, maDTParameter, tPParameter, quanParameter, diaChiCuTheParameter, sDTParameter, tinhTrangParameter, ngayLapParameter);
        }
    
        public virtual int sp_Them_CT_DDH(string maDH, string maTP, string maDT, Nullable<int> soLuong, string danhGia)
        {
            var maDHParameter = maDH != null ?
                new ObjectParameter("MaDH", maDH) :
                new ObjectParameter("MaDH", typeof(string));
    
            var maTPParameter = maTP != null ?
                new ObjectParameter("MaTP", maTP) :
                new ObjectParameter("MaTP", typeof(string));
    
            var maDTParameter = maDT != null ?
                new ObjectParameter("MaDT", maDT) :
                new ObjectParameter("MaDT", typeof(string));
    
            var soLuongParameter = soLuong.HasValue ?
                new ObjectParameter("SoLuong", soLuong) :
                new ObjectParameter("SoLuong", typeof(int));
    
            var danhGiaParameter = danhGia != null ?
                new ObjectParameter("DanhGia", danhGia) :
                new ObjectParameter("DanhGia", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_Them_CT_DDH", maDHParameter, maTPParameter, maDTParameter, soLuongParameter, danhGiaParameter);
        }
    
        public virtual int sp_Them_DDH(string maDH, string gioDat, Nullable<System.DateTime> ngayDat, Nullable<decimal> giaTriDH, string tinhTrang, string maKH, string maTX)
        {
            var maDHParameter = maDH != null ?
                new ObjectParameter("MaDH", maDH) :
                new ObjectParameter("MaDH", typeof(string));
    
            var gioDatParameter = gioDat != null ?
                new ObjectParameter("GioDat", gioDat) :
                new ObjectParameter("GioDat", typeof(string));
    
            var ngayDatParameter = ngayDat.HasValue ?
                new ObjectParameter("NgayDat", ngayDat) :
                new ObjectParameter("NgayDat", typeof(System.DateTime));
    
            var giaTriDHParameter = giaTriDH.HasValue ?
                new ObjectParameter("GiaTriDH", giaTriDH) :
                new ObjectParameter("GiaTriDH", typeof(decimal));
    
            var tinhTrangParameter = tinhTrang != null ?
                new ObjectParameter("TinhTrang", tinhTrang) :
                new ObjectParameter("TinhTrang", typeof(string));
    
            var maKHParameter = maKH != null ?
                new ObjectParameter("MaKH", maKH) :
                new ObjectParameter("MaKH", typeof(string));
    
            var maTXParameter = maTX != null ?
                new ObjectParameter("MaTX", maTX) :
                new ObjectParameter("MaTX", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_Them_DDH", maDHParameter, gioDatParameter, ngayDatParameter, giaTriDHParameter, tinhTrangParameter, maKHParameter, maTXParameter);
        }
    
        public virtual int sp_Them_Doi_Tac(string maDT, string email, string ngDaiDien, Nullable<short> sLChiNhanh, string tenQuan, string loaiTP)
        {
            var maDTParameter = maDT != null ?
                new ObjectParameter("MaDT", maDT) :
                new ObjectParameter("MaDT", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("Email", email) :
                new ObjectParameter("Email", typeof(string));
    
            var ngDaiDienParameter = ngDaiDien != null ?
                new ObjectParameter("NgDaiDien", ngDaiDien) :
                new ObjectParameter("NgDaiDien", typeof(string));
    
            var sLChiNhanhParameter = sLChiNhanh.HasValue ?
                new ObjectParameter("SLChiNhanh", sLChiNhanh) :
                new ObjectParameter("SLChiNhanh", typeof(short));
    
            var tenQuanParameter = tenQuan != null ?
                new ObjectParameter("TenQuan", tenQuan) :
                new ObjectParameter("TenQuan", typeof(string));
    
            var loaiTPParameter = loaiTP != null ?
                new ObjectParameter("LoaiTP", loaiTP) :
                new ObjectParameter("LoaiTP", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_Them_Doi_Tac", maDTParameter, emailParameter, ngDaiDienParameter, sLChiNhanhParameter, tenQuanParameter, loaiTPParameter);
        }
    
        public virtual int sp_Them_Hop_Dong(string maHD, Nullable<short> sLChiNhanh, string soTaiKhoan, string nganHang, string cNNganHang, string maSoThue, Nullable<System.DateTime> ngayKy, string thoiHan, Nullable<System.DateTime> ngayHetHan, string maDT, string maNV)
        {
            var maHDParameter = maHD != null ?
                new ObjectParameter("MaHD", maHD) :
                new ObjectParameter("MaHD", typeof(string));
    
            var sLChiNhanhParameter = sLChiNhanh.HasValue ?
                new ObjectParameter("SLChiNhanh", sLChiNhanh) :
                new ObjectParameter("SLChiNhanh", typeof(short));
    
            var soTaiKhoanParameter = soTaiKhoan != null ?
                new ObjectParameter("SoTaiKhoan", soTaiKhoan) :
                new ObjectParameter("SoTaiKhoan", typeof(string));
    
            var nganHangParameter = nganHang != null ?
                new ObjectParameter("NganHang", nganHang) :
                new ObjectParameter("NganHang", typeof(string));
    
            var cNNganHangParameter = cNNganHang != null ?
                new ObjectParameter("CNNganHang", cNNganHang) :
                new ObjectParameter("CNNganHang", typeof(string));
    
            var maSoThueParameter = maSoThue != null ?
                new ObjectParameter("MaSoThue", maSoThue) :
                new ObjectParameter("MaSoThue", typeof(string));
    
            var ngayKyParameter = ngayKy.HasValue ?
                new ObjectParameter("NgayKy", ngayKy) :
                new ObjectParameter("NgayKy", typeof(System.DateTime));
    
            var thoiHanParameter = thoiHan != null ?
                new ObjectParameter("ThoiHan", thoiHan) :
                new ObjectParameter("ThoiHan", typeof(string));
    
            var ngayHetHanParameter = ngayHetHan.HasValue ?
                new ObjectParameter("NgayHetHan", ngayHetHan) :
                new ObjectParameter("NgayHetHan", typeof(System.DateTime));
    
            var maDTParameter = maDT != null ?
                new ObjectParameter("MaDT", maDT) :
                new ObjectParameter("MaDT", typeof(string));
    
            var maNVParameter = maNV != null ?
                new ObjectParameter("MaNV", maNV) :
                new ObjectParameter("MaNV", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_Them_Hop_Dong", maHDParameter, sLChiNhanhParameter, soTaiKhoanParameter, nganHangParameter, cNNganHangParameter, maSoThueParameter, ngayKyParameter, thoiHanParameter, ngayHetHanParameter, maDTParameter, maNVParameter);
        }
    
        public virtual int sp_Them_Khach_Hang(string maKH, string hoTen, string diaChi, string sDT, string email)
        {
            var maKHParameter = maKH != null ?
                new ObjectParameter("MaKH", maKH) :
                new ObjectParameter("MaKH", typeof(string));
    
            var hoTenParameter = hoTen != null ?
                new ObjectParameter("HoTen", hoTen) :
                new ObjectParameter("HoTen", typeof(string));
    
            var diaChiParameter = diaChi != null ?
                new ObjectParameter("DiaChi", diaChi) :
                new ObjectParameter("DiaChi", typeof(string));
    
            var sDTParameter = sDT != null ?
                new ObjectParameter("SDT", sDT) :
                new ObjectParameter("SDT", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("Email", email) :
                new ObjectParameter("Email", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_Them_Khach_Hang", maKHParameter, hoTenParameter, diaChiParameter, sDTParameter, emailParameter);
        }
    
        public virtual int sp_Them_Nhan_Vien(string ma, string hoTen)
        {
            var maParameter = ma != null ?
                new ObjectParameter("Ma", ma) :
                new ObjectParameter("Ma", typeof(string));
    
            var hoTenParameter = hoTen != null ?
                new ObjectParameter("HoTen", hoTen) :
                new ObjectParameter("HoTen", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_Them_Nhan_Vien", maParameter, hoTenParameter);
        }
    
        public virtual int sp_Them_Tai_Xe(string maTX, string cMND, string hoTen, string sDT, string diaChi, string bienSoXe, string khuVucHoatDong, string email, string soTaiKhoan, string nganHang, string cNNganHang)
        {
            var maTXParameter = maTX != null ?
                new ObjectParameter("MaTX", maTX) :
                new ObjectParameter("MaTX", typeof(string));
    
            var cMNDParameter = cMND != null ?
                new ObjectParameter("CMND", cMND) :
                new ObjectParameter("CMND", typeof(string));
    
            var hoTenParameter = hoTen != null ?
                new ObjectParameter("HoTen", hoTen) :
                new ObjectParameter("HoTen", typeof(string));
    
            var sDTParameter = sDT != null ?
                new ObjectParameter("SDT", sDT) :
                new ObjectParameter("SDT", typeof(string));
    
            var diaChiParameter = diaChi != null ?
                new ObjectParameter("DiaChi", diaChi) :
                new ObjectParameter("DiaChi", typeof(string));
    
            var bienSoXeParameter = bienSoXe != null ?
                new ObjectParameter("BienSoXe", bienSoXe) :
                new ObjectParameter("BienSoXe", typeof(string));
    
            var khuVucHoatDongParameter = khuVucHoatDong != null ?
                new ObjectParameter("KhuVucHoatDong", khuVucHoatDong) :
                new ObjectParameter("KhuVucHoatDong", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("Email", email) :
                new ObjectParameter("Email", typeof(string));
    
            var soTaiKhoanParameter = soTaiKhoan != null ?
                new ObjectParameter("SoTaiKhoan", soTaiKhoan) :
                new ObjectParameter("SoTaiKhoan", typeof(string));
    
            var nganHangParameter = nganHang != null ?
                new ObjectParameter("NganHang", nganHang) :
                new ObjectParameter("NganHang", typeof(string));
    
            var cNNganHangParameter = cNNganHang != null ?
                new ObjectParameter("CNNganHang", cNNganHang) :
                new ObjectParameter("CNNganHang", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_Them_Tai_Xe", maTXParameter, cMNDParameter, hoTenParameter, sDTParameter, diaChiParameter, bienSoXeParameter, khuVucHoatDongParameter, emailParameter, soTaiKhoanParameter, nganHangParameter, cNNganHangParameter);
        }
    
        public virtual int sp_Them_Thuc_Pham(string maTP, string maDT, string tenMon, string mieuTa, Nullable<decimal> gia, string tinhTrang, string tuyChon)
        {
            var maTPParameter = maTP != null ?
                new ObjectParameter("MaTP", maTP) :
                new ObjectParameter("MaTP", typeof(string));
    
            var maDTParameter = maDT != null ?
                new ObjectParameter("MaDT", maDT) :
                new ObjectParameter("MaDT", typeof(string));
    
            var tenMonParameter = tenMon != null ?
                new ObjectParameter("TenMon", tenMon) :
                new ObjectParameter("TenMon", typeof(string));
    
            var mieuTaParameter = mieuTa != null ?
                new ObjectParameter("MieuTa", mieuTa) :
                new ObjectParameter("MieuTa", typeof(string));
    
            var giaParameter = gia.HasValue ?
                new ObjectParameter("Gia", gia) :
                new ObjectParameter("Gia", typeof(decimal));
    
            var tinhTrangParameter = tinhTrang != null ?
                new ObjectParameter("TinhTrang", tinhTrang) :
                new ObjectParameter("TinhTrang", typeof(string));
    
            var tuyChonParameter = tuyChon != null ?
                new ObjectParameter("TuyChon", tuyChon) :
                new ObjectParameter("TuyChon", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_Them_Thuc_Pham", maTPParameter, maDTParameter, tenMonParameter, mieuTaParameter, giaParameter, tinhTrangParameter, tuyChonParameter);
        }
    
        public virtual int ThemUser(string username, string pass, string roleName)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("Username", username) :
                new ObjectParameter("Username", typeof(string));
    
            var passParameter = pass != null ?
                new ObjectParameter("Pass", pass) :
                new ObjectParameter("Pass", typeof(string));
    
            var roleNameParameter = roleName != null ?
                new ObjectParameter("RoleName", roleName) :
                new ObjectParameter("RoleName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ThemUser", usernameParameter, passParameter, roleNameParameter);
        }
    
        public virtual ObjectResult<TimKiemChiNhanh_Result> TimKiemChiNhanh(Nullable<int> sTT, string maDT)
        {
            var sTTParameter = sTT.HasValue ?
                new ObjectParameter("STT", sTT) :
                new ObjectParameter("STT", typeof(int));
    
            var maDTParameter = maDT != null ?
                new ObjectParameter("MaDT", maDT) :
                new ObjectParameter("MaDT", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TimKiemChiNhanh_Result>("TimKiemChiNhanh", sTTParameter, maDTParameter);
        }
    
        public virtual ObjectResult<TimKiemChiTietDonDatHang_Result> TimKiemChiTietDonDatHang(string maDH, string maTP, string maDT)
        {
            var maDHParameter = maDH != null ?
                new ObjectParameter("MaDH", maDH) :
                new ObjectParameter("MaDH", typeof(string));
    
            var maTPParameter = maTP != null ?
                new ObjectParameter("MaTP", maTP) :
                new ObjectParameter("MaTP", typeof(string));
    
            var maDTParameter = maDT != null ?
                new ObjectParameter("MaDT", maDT) :
                new ObjectParameter("MaDT", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TimKiemChiTietDonDatHang_Result>("TimKiemChiTietDonDatHang", maDHParameter, maTPParameter, maDTParameter);
        }
    
        public virtual ObjectResult<TimKiemDoiTac_Result> TimKiemDoiTac(string maDT)
        {
            var maDTParameter = maDT != null ?
                new ObjectParameter("MaDT", maDT) :
                new ObjectParameter("MaDT", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TimKiemDoiTac_Result>("TimKiemDoiTac", maDTParameter);
        }
    
        public virtual ObjectResult<TimKiemDonDatHang_Result> TimKiemDonDatHang(string maDH)
        {
            var maDHParameter = maDH != null ?
                new ObjectParameter("MaDH", maDH) :
                new ObjectParameter("MaDH", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TimKiemDonDatHang_Result>("TimKiemDonDatHang", maDHParameter);
        }
    
        public virtual ObjectResult<TimKiemHopDong_Result> TimKiemHopDong(string maHD)
        {
            var maHDParameter = maHD != null ?
                new ObjectParameter("MaHD", maHD) :
                new ObjectParameter("MaHD", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TimKiemHopDong_Result>("TimKiemHopDong", maHDParameter);
        }
    
        public virtual ObjectResult<TimKiemKhachHang_Result> TimKiemKhachHang(string maKH)
        {
            var maKHParameter = maKH != null ?
                new ObjectParameter("MaKH", maKH) :
                new ObjectParameter("MaKH", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TimKiemKhachHang_Result>("TimKiemKhachHang", maKHParameter);
        }
    
        public virtual ObjectResult<TimKiemNhanVien_Result> TimKiemNhanVien(string maNV)
        {
            var maNVParameter = maNV != null ?
                new ObjectParameter("MaNV", maNV) :
                new ObjectParameter("MaNV", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TimKiemNhanVien_Result>("TimKiemNhanVien", maNVParameter);
        }
    
        public virtual ObjectResult<TimKiemTaiXe_Result> TimKiemTaiXe(string maTX)
        {
            var maTXParameter = maTX != null ?
                new ObjectParameter("MaTX", maTX) :
                new ObjectParameter("MaTX", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TimKiemTaiXe_Result>("TimKiemTaiXe", maTXParameter);
        }
    
        public virtual ObjectResult<TimKiemThucPham_Result> TimKiemThucPham(string maTP, string maDT)
        {
            var maTPParameter = maTP != null ?
                new ObjectParameter("MaTP", maTP) :
                new ObjectParameter("MaTP", typeof(string));
    
            var maDTParameter = maDT != null ?
                new ObjectParameter("MaDT", maDT) :
                new ObjectParameter("MaDT", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TimKiemThucPham_Result>("TimKiemThucPham", maTPParameter, maDTParameter);
        }
    
        public virtual ObjectResult<TimKiemUser_Result> TimKiemUser(string username)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("Username", username) :
                new ObjectParameter("Username", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TimKiemUser_Result>("TimKiemUser", usernameParameter);
        }
    
        public virtual int usp_xoaChiNhanh(Nullable<int> sTT, string maDT)
        {
            var sTTParameter = sTT.HasValue ?
                new ObjectParameter("STT", sTT) :
                new ObjectParameter("STT", typeof(int));
    
            var maDTParameter = maDT != null ?
                new ObjectParameter("MaDT", maDT) :
                new ObjectParameter("MaDT", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_xoaChiNhanh", sTTParameter, maDTParameter);
        }
    
        public virtual int usp_xoaChiTietDonHang(string maDH, string maTP, string maDT)
        {
            var maDHParameter = maDH != null ?
                new ObjectParameter("MaDH", maDH) :
                new ObjectParameter("MaDH", typeof(string));
    
            var maTPParameter = maTP != null ?
                new ObjectParameter("MaTP", maTP) :
                new ObjectParameter("MaTP", typeof(string));
    
            var maDTParameter = maDT != null ?
                new ObjectParameter("MaDT", maDT) :
                new ObjectParameter("MaDT", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_xoaChiTietDonHang", maDHParameter, maTPParameter, maDTParameter);
        }
    
        public virtual int usp_xoaDoiTac(string maDT)
        {
            var maDTParameter = maDT != null ?
                new ObjectParameter("MaDT", maDT) :
                new ObjectParameter("MaDT", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_xoaDoiTac", maDTParameter);
        }
    
        public virtual int usp_xoaDonDatHang(string maDH)
        {
            var maDHParameter = maDH != null ?
                new ObjectParameter("MaDH", maDH) :
                new ObjectParameter("MaDH", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_xoaDonDatHang", maDHParameter);
        }
    
        public virtual int usp_xoaHopDong(string maHD)
        {
            var maHDParameter = maHD != null ?
                new ObjectParameter("MaHD", maHD) :
                new ObjectParameter("MaHD", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_xoaHopDong", maHDParameter);
        }
    
        public virtual int usp_xoaKhachHang(string maKH)
        {
            var maKHParameter = maKH != null ?
                new ObjectParameter("MaKH", maKH) :
                new ObjectParameter("MaKH", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_xoaKhachHang", maKHParameter);
        }
    
        public virtual int usp_xoaNhanVien(string maNV)
        {
            var maNVParameter = maNV != null ?
                new ObjectParameter("MaNV", maNV) :
                new ObjectParameter("MaNV", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_xoaNhanVien", maNVParameter);
        }
    
        public virtual int usp_xoaTaiXe(string cMND)
        {
            var cMNDParameter = cMND != null ?
                new ObjectParameter("CMND", cMND) :
                new ObjectParameter("CMND", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_xoaTaiXe", cMNDParameter);
        }
    
        public virtual int usp_xoaThucPham(string maTP, string maDT)
        {
            var maTPParameter = maTP != null ?
                new ObjectParameter("MaTP", maTP) :
                new ObjectParameter("MaTP", typeof(string));
    
            var maDTParameter = maDT != null ?
                new ObjectParameter("MaDT", maDT) :
                new ObjectParameter("MaDT", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_xoaThucPham", maTPParameter, maDTParameter);
        }
    
        public virtual int XoaUser(string user)
        {
            var userParameter = user != null ?
                new ObjectParameter("user", user) :
                new ObjectParameter("user", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("XoaUser", userParameter);
        }
    }
}