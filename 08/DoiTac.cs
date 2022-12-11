using System.Data;

namespace _08.DBClass
{
    public class DoiTac
    {
        public string username;

        private string maDT;
        private string email;
        private string ngDaiDien;
        private string sLChiNhanh;
        private string tenQuan;
        private string loaiThucPham;
        private string user;

        public string MaDT { get => maDT; set => maDT = value; }
        public string Email { get => email; set => email = value; }
        public string NgDaiDien { get => ngDaiDien; set => ngDaiDien = value; }
        public string SLChiNhanh { get => sLChiNhanh; set => sLChiNhanh = value; }
        public string TenQuan { get => tenQuan; set => tenQuan = value; }
        public string LoaiThucPham { get => loaiThucPham; set => loaiThucPham = value; }
        public string User { get => user; set => user = value; }
        //
        DBConnect dbconnect = new DBConnect();


        public DataTable LayThongTinDoiTac()
        {

            string sql = "select * from DOITAC where MaDT='" + MaDT + "'";
            DataTable dt = new DataTable();
            dt = dbconnect.sql_select(sql);
            return dt;
        }
        public void capNhatThongTin(string email, string nguoidaidien, int slChiNhanh, string tenquan, string loaithucpham)
        {
            string sql = "update DOITAC set Email='" + email + "', NgDaiDien= N'" + nguoidaidien + "' , SLChiNhanh='" + slChiNhanh + "' , TenQuan=N'" + tenquan + "' , LoaiTP=N'" + loaithucpham + "' where MaDT='" + MaDT + "'";
            dbconnect.sql_insert_update_delete(sql);
        }

        public void doiMatKhau(string passmoi, string user)
        {
            string query = "update USERS set Pass='" + passmoi + "' where Username='" + user + "'";
            dbconnect.sql_insert_update_delete(query);
        }
        public string layMatKhau(string user)
        {
            string query = "select Pass from USERS where Username='" + user + "'";
            return dbconnect.layMotGiaTri(query);
        }
        public int QueryChiNhanh(string LoaiQuery, string stt, string Ma, string TP, string Quan, string DiaChi, string SDT, string TT, string NgayLap)
        {
            string query = "Exec " + LoaiQuery + "ChiNhanh '" + Ma + "',N'" + TP +
                "',N'" + Quan + "',N'" + DiaChi
                + "','" + SDT + "',N'" + TT + "','" + NgayLap + "'";
            switch (LoaiQuery)
            {
                case "Xoa":
                    query = "Exec " + LoaiQuery + "ChiNhanh '" + Ma + "','" + stt.ToString() + "'";
                    break;
                case "Sua":
                    query = query + ",'" + stt + "'";
                    break;
                default:
                    break;
            }
            return (int)dbconnect.sql_select(query).Rows[0][0];
        }
        public int QueryThucPham(string LoaiQuery, string MaTP, string MaDT, string TenMon, string MieuTa, string gia,
            string TinhTrang, string TuyChon)
        {
            string query = "Exec " + LoaiQuery + "ThucPham '" + MaDT + "',N'" + TenMon +
                "',N'" + MieuTa + "','" + gia + "',N'" + TinhTrang + "',N'" + TuyChon + "'";
            switch (LoaiQuery)
            {
                case "Xoa":
                    query = "Exec " + LoaiQuery + "ThucPham '" + MaTP + "','" + MaDT + "'";
                    break;
                case "Sua":
                    query = query + ",'" + MaTP + "'";
                    break;
                default:
                    break;
            }
            return (int)dbconnect.sql_select(query).Rows[0][0];
        }


        public int QueryHopDong(string SLChiNhanh, string SoTaiKhoan, string NganHang,
            string CNNganHang, string MaSoThue, string NgayKy,
            string ThoiHan, string NgayHetHan, string MaDT)
        {
            string query = "Exec ThemHopDong " + SLChiNhanh + ",'" + SoTaiKhoan + "',N'" + NganHang + "',N'" +
                    CNNganHang + "','" + MaSoThue + "','" + NgayKy + "',N'" +
                    ThoiHan + "','" + NgayHetHan + "','" + MaDT + "'";
            return (int)dbconnect.sql_select(query).Rows[0][0];
        }
        public DataTable laySoLuongDon(string kieu, string MaDT)
        {
            string sql = "Exec SoLuongDonTheo" + kieu + " " + "'" + MaDT + "'";
            return dbconnect.sql_select(sql);
        }
    }
}
