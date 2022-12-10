using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _08
{
    /// <summary>
    /// Interaction logic for NhanVien_Window.xaml
    /// </summary>
    public partial class NhanVien_Window : Window
    {
        private string Username = "";
        private string MaNV = "";
        private string HoTenNV = "";

        public NhanVien_Window(string Username)
        {
            InitializeComponent();
            this.DataContext = this;
            this.Username = Username;
            UsernameBox.Text = Username;
            SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
            try
            {
                db.Open();
                SqlCommand cmd = db.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "TimKiemNhanVien";
                cmd.Parameters.AddWithValue("@Username", Username);
                SqlDataReader sdr = cmd.ExecuteReader();
                sdr.Read();
                MaNV = sdr["MaNV"].ToString();
                HoTenNV = sdr["HoTen"].ToString();
                MaNVBox.Text = MaNV;
                HoTenNVBox.Text = HoTenNV;
            }
            catch
            {
                MessageBox.Show("Lỗi hệ thống!");
            }
        }

        private void ResetVisibility()
        {
            ThongTinCaNhan.Visibility = Visibility.Collapsed;
            QuanLyDoiTac.Visibility = Visibility.Collapsed;
            QuanLyHopDong.Visibility = Visibility.Collapsed;
        }

        private void TTCNDefaultVisibility()
        {
            SavePassBtn.Visibility = Visibility.Collapsed;
            ChangePassBtn.Visibility = Visibility.Visible;
            PassBox1.Visibility = Visibility.Collapsed;
            PassBox2.Visibility = Visibility.Collapsed;
            InfoBox1.Visibility = Visibility.Visible;
            InfoBox2.Visibility = Visibility.Visible;
            InfoBox3.Visibility = Visibility.Visible;
            EditBtn.IsEnabled = true;
        }

        private void ShowTTCN(object sender, RoutedEventArgs e)
        {
            ResetVisibility();
            ThongTinCaNhan.Visibility = Visibility.Visible;
            TTCNDefaultVisibility();
            SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
            try
            {
                db.Open();
                SqlCommand cmd = db.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "TimKiemNhanVien";
                cmd.Parameters.AddWithValue("@Username", Username);
                SqlDataReader sdr = cmd.ExecuteReader();
                sdr.Read();
                MaNV = sdr["MaNV"].ToString();
                HoTenNV = sdr["HoTen"].ToString();
                MaNVBox.Text = MaNV;
                HoTenNVBox.Text = HoTenNV;
            }
            catch
            {
                MessageBox.Show("Lỗi hệ thống!");
            }
        }

        private void ShowQLDT(object sender, RoutedEventArgs e)
        {
            ResetVisibility();
            QuanLyDoiTac.Visibility = Visibility.Visible;
        }

        private void ShowQLHD(object sender, RoutedEventArgs e)
        {
            ResetVisibility();
            QuanLyHopDong.Visibility = Visibility.Visible;
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            InfoBox1.Visibility = Visibility.Visible;
            InfoBox2.Visibility = Visibility.Visible;
            InfoBox3.Visibility = Visibility.Visible;
            PassBox1.Visibility = Visibility.Collapsed;
            PassBox2.Visibility = Visibility.Collapsed;
            EditBtn.Visibility = Visibility.Collapsed;
            SaveBtn.Visibility = Visibility.Visible;
            UsernameBox.IsEnabled = true;
            HoTenNVBox.IsEnabled = true;
            ChangePassBtn.IsEnabled = false;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
            try
            {
                db.Open();
                SqlTransaction trans = db.BeginTransaction();
                SqlCommand cmd = db.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trans;
                cmd.CommandText = "CapNhatNhanVien";
                cmd.Parameters.AddWithValue("@MaNV", MaNV);
                cmd.Parameters.AddWithValue("@HoTen", HoTenNVBox.Text);
                cmd.Parameters.AddWithValue("@Username", UsernameBox.Text);
                int result = Convert.ToInt32(cmd.ExecuteScalar());
                if (result == 0)
                {
                    MessageBox.Show("Cập nhật thành công!");
                    trans.Commit();
                    HoTenNV = HoTenNVBox.Text;
                    Username = UsernameBox.Text;
                    SaveBtn.Visibility = Visibility.Collapsed;
                    EditBtn.Visibility = Visibility.Visible;
                    UsernameBox.IsEnabled = false;
                    HoTenNVBox.IsEnabled = false;
                    ChangePassBtn.IsEnabled = true;
                }
                else if (result == 1)
                {
                    MessageBox.Show("Nhân viên không tồn tại!");
                    trans.Rollback();
                }
                else if (result == 2)
                {
                    MessageBox.Show("Username mới đã tồn tại!");
                    trans.Rollback();
                }
                else
                {
                    MessageBox.Show("Lỗi hệ thống!");
                    trans.Rollback();
                }
            }
            catch
            {
                MessageBox.Show("Lỗi hệ thống!");
            }

        }

        private void DoiMatKhau(object sender, RoutedEventArgs e)
        {
            InfoBox1.Visibility = Visibility.Collapsed;
            InfoBox2.Visibility = Visibility.Collapsed;
            InfoBox3.Visibility = Visibility.Collapsed;
            PassBox1.Visibility = Visibility.Visible;
            PassBox2.Visibility = Visibility.Visible;
            ChangePassBtn.Visibility = Visibility.Collapsed;
            SavePassBtn.Visibility = Visibility.Visible;
            EditBtn.IsEnabled = false;
        }

        private void SavePass(object sender, RoutedEventArgs e)
        {
            SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
            try
            {
                db.Open();
                SqlTransaction trans = db.BeginTransaction();
                SqlCommand cmd = db.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trans;
                cmd.CommandText = "DoiMatKhau";
                cmd.Parameters.AddWithValue("@Username", Username);
                cmd.Parameters.AddWithValue("@OldPass", OldPassBox.Password);
                cmd.Parameters.AddWithValue("@NewPass", NewPassBox.Password);

                int result = Convert.ToInt32(cmd.ExecuteScalar());
                if (result == 0)
                {
                    MessageBox.Show("Đổi mật khẩu thành công!");
                    trans.Commit();
                    SavePassBtn.Visibility = Visibility.Collapsed;
                    ChangePassBtn.Visibility = Visibility.Visible;
                    PassBox1.Visibility = Visibility.Collapsed;
                    PassBox2.Visibility = Visibility.Collapsed;
                    InfoBox1.Visibility = Visibility.Visible;
                    InfoBox2.Visibility = Visibility.Visible;
                    InfoBox3.Visibility = Visibility.Visible;
                    EditBtn.IsEnabled = true;
                }
                else if (result == 1)
                {
                    MessageBox.Show("Username không tồn tại!");
                    trans.Rollback();
                }
                else if (result == 2)
                {
                    MessageBox.Show("Sai mật khẩu!");
                    trans.Rollback();
                }
                else
                {
                    MessageBox.Show("Lỗi hệ thống!");
                    trans.Rollback();
                }
            }
            catch
            {
                MessageBox.Show("Lỗi hệ thống!");
            }

        }

        private void HienThiDSDoiTac(object sender, RoutedEventArgs e)
        {
            dgDoiTac.Visibility = Visibility.Visible;
            SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
            try
            {
                db.Open();
                SqlDataAdapter GetDSDoiTac = new SqlDataAdapter("Select * from DOITAC", db);
                DataTable DSDoiTac = new DataTable("DOITAC");
                GetDSDoiTac.Fill(DSDoiTac);
                dgDoiTac.ItemsSource = DSDoiTac.DefaultView;
            }
            catch
            {
            }
        }

        private void HienThiDSHopDong(object sender, RoutedEventArgs e)
        {

            dgHopDong.Visibility = Visibility.Visible;

            SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
            try
            {
                db.Open();
                SqlDataAdapter GetDSHopDong = new SqlDataAdapter("Select * from HOPDONG", db);
                DataTable DSHopDong = new DataTable("HOPDONG");
                GetDSHopDong.Fill(DSHopDong);
                dgHopDong.ItemsSource = DSHopDong.DefaultView;

            }
            catch
            {
            }
        }

        public class DT //Đối tác
        {
            public string MaDT { get; set; }
            public string Email { get; set; }
            public string NgDaiDien { get; set; }
            public string SLChiNhanh { get; set; }
            public string TenQuan { get; set; }
            public string LoaiTP { get; set; }
            public string Username { get; set; }
        }
        DT dt = new DT();

        public class HD //Hợp đồng
        {
            public string MaHD { get; set; }
            public string NgDaiDien { get; set; }
            public string SLChiNhanh { get; set; }
            public string SoTaiKhoan { get; set; }
            public string NganHang { get; set; }
            public string CNNganHang { get; set; }
            public string MaSoThue { get; set; }
            public string NgayKy { get; set; }
            public string ThoiHan { get; set; }
            public string NgayHetHan { get; set; }
            public string MaDT { get; set; }
            public string MaNV { get; set; }
            public string TrangThai { get; set; }
        }
        HD hd = new HD();

        private void SelectDGDoiTac(object sender, SelectionChangedEventArgs e)
        {
            //https://stackoverflow.com/questions/19225568/wpf-datagrid-get-selected-cell-value

            if (dgDoiTac.SelectedItem != null)
            {
                DataRowView dataRow = (DataRowView)dgDoiTac.SelectedItem;
                dt.MaDT = dataRow.Row.ItemArray[0].ToString();
                dt.Email = dataRow.Row.ItemArray[1].ToString();
                dt.NgDaiDien = dataRow.Row.ItemArray[2].ToString();
                dt.SLChiNhanh = dataRow.Row.ItemArray[3].ToString();
                dt.TenQuan = dataRow.Row.ItemArray[4].ToString();
                dt.LoaiTP = dataRow.Row.ItemArray[5].ToString();
                dt.Username = dataRow.Row.ItemArray[6].ToString();
                MaDTBox.Text = dt.MaDT;
                EmailBox.Text = dt.Email;
                NgDaiDienBox.Text = dt.NgDaiDien;
                SLChiNhanhBox.Text = dt.SLChiNhanh;
                TenQuanBox.Text = dt.TenQuan;
                LoaiTPBox.Text = dt.LoaiTP;
                UsernameDTBox.Text = dt.Username;
            }
            else
            {
                MaDTBox.Text = "";
                EmailBox.Text = "";
                NgDaiDienBox.Text = "";
                SLChiNhanhBox.Text = "";
                TenQuanBox.Text = "";
                LoaiTPBox.Text = "";
                UsernameDTBox.Text = "";
            }
            
        }


        private void FindDT(object sender, RoutedEventArgs e)
        {
            SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
            try
            {
                db.Open();
                SqlCommand cmd = db.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "TimKiemDoiTac";
                cmd.Parameters.AddWithValue("@MaDT", FindDTBox.Text);
                SqlDataReader sdr = cmd.ExecuteReader();
                sdr.Read();
                dt.MaDT = sdr["MaDT"].ToString();
                dt.Email = sdr["Email"].ToString();
                dt.NgDaiDien = sdr["NgDaiDien"].ToString();
                dt.SLChiNhanh = sdr["SLChiNhanh"].ToString();
                dt.TenQuan = sdr["TenQuan"].ToString();
                dt.LoaiTP = sdr["LoaiTP"].ToString();
                dt.Username = sdr["Username"].ToString();

                MaDTBox.Text = dt.MaDT;
                EmailBox.Text = dt.Email;
                NgDaiDienBox.Text = dt.NgDaiDien;
                SLChiNhanhBox.Text = dt.SLChiNhanh;
                TenQuanBox.Text = dt.TenQuan;
                LoaiTPBox.Text = dt.LoaiTP;
                UsernameDTBox.Text = dt.Username;

            }
            catch
            {
                MessageBox.Show("Không tìm thấy!");
            }


        }

        private void SelectDGHopDong(object sender, SelectionChangedEventArgs e)
        {
            if (dgHopDong.SelectedItem != null)
            {
                DataRowView dataRow = (DataRowView)dgHopDong.SelectedItem;
                hd.MaHD = dataRow.Row.ItemArray[0].ToString();
                hd.NgDaiDien = dataRow.Row.ItemArray[1].ToString();
                hd.SLChiNhanh = dataRow.Row.ItemArray[2].ToString();
                hd.SoTaiKhoan = dataRow.Row.ItemArray[3].ToString();
                hd.NganHang = dataRow.Row.ItemArray[4].ToString();
                hd.CNNganHang = dataRow.Row.ItemArray[5].ToString();
                hd.MaSoThue = dataRow.Row.ItemArray[6].ToString();
                hd.NgayKy = dataRow.Row.ItemArray[7].ToString();
                hd.ThoiHan = dataRow.Row.ItemArray[8].ToString();
                hd.NgayHetHan = dataRow.Row.ItemArray[9].ToString();
                hd.MaDT = dataRow.Row.ItemArray[10].ToString();
                hd.MaNV = dataRow.Row.ItemArray[11].ToString();
                hd.TrangThai = dataRow.Row.ItemArray[12].ToString();
                MaHDBox.Text = hd.MaHD;
                NgDaiDien_HDBox.Text = hd.NgDaiDien;
                SLChiNhanh_HDBox.Text = hd.SLChiNhanh;
                SoTaiKhoanBox.Text = hd.SoTaiKhoan;
                NganHangBox.Text = hd.NganHang;
                CNNganHangBox.Text = hd.CNNganHang;
                MaSoThueBox.Text = hd.MaSoThue;
                NgayKyBox.Text = hd.NgayKy;
                ThoiHanBox.Text = hd.ThoiHan;
                NgayHetHanBox.Text = hd.NgayHetHan;
                MaDT_HDBox.Text = hd.MaDT;
                MaNV_HDBox.Text = hd.MaNV;
                TrangThaiBox.Text = hd.TrangThai;
            }
            else
            {
                MaHDBox.Text = "";
                NgDaiDien_HDBox.Text = "";
                SLChiNhanh_HDBox.Text = "";
                SoTaiKhoanBox.Text = "";
                NganHangBox.Text = "";
                CNNganHangBox.Text = "";
                MaSoThueBox.Text = "";
                NgayKyBox.Text = "";
                ThoiHanBox.Text = "";
                NgayHetHanBox.Text = "";
                MaDT_HDBox.Text = "";
                MaNV_HDBox.Text = "";
                TrangThaiBox.Text = "";
            }
            if (MaHDBox.Text != "")
            {
                DuyetThoiHanBox.IsEnabled = true;
                DuyetNgayHetHanBox.IsEnabled = true;
                DuyetHD.IsEnabled = true;
            }
            else
            {
                DuyetThoiHanBox.IsEnabled = false;
                DuyetNgayHetHanBox.IsEnabled = false;
                DuyetHD.IsEnabled = false;
            }

        }

        private void FindHD(object sender, RoutedEventArgs e)
        {
            SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
            try
            {
                db.Open();
                SqlCommand cmd = db.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "TimKiemHopDong";
                cmd.Parameters.AddWithValue("@MaHD", FindHDBox.Text);
                SqlDataReader sdr = cmd.ExecuteReader();
                sdr.Read();
                hd.MaHD = sdr["MaHD"].ToString();
                hd.NgDaiDien = sdr["NgDaiDien"].ToString();
                hd.SLChiNhanh = sdr["SLChiNhanh"].ToString();
                hd.SoTaiKhoan = sdr["SoTaiKhoan"].ToString();
                hd.NganHang = sdr["NganHang"].ToString();
                hd.CNNganHang = sdr["CNNganHang"].ToString();
                hd.MaSoThue = sdr["MaSoThue"].ToString();
                hd.NgayKy = sdr["NgayKy"].ToString();
                hd.ThoiHan = sdr["ThoiHan"].ToString();
                hd.NgayHetHan = sdr["NgayHetHan"].ToString();
                hd.MaDT = sdr["MaDT"].ToString();
                hd.MaNV = sdr["MaNV"].ToString();
                hd.TrangThai = sdr["TrangThai"].ToString();
                MaHDBox.Text = hd.MaHD;
                NgDaiDien_HDBox.Text = hd.NgDaiDien;
                SLChiNhanh_HDBox.Text = hd.SLChiNhanh;
                SoTaiKhoanBox.Text = hd.SoTaiKhoan;
                NganHangBox.Text = hd.NganHang;
                CNNganHangBox.Text = hd.CNNganHang;
                MaSoThueBox.Text = hd.MaSoThue;
                NgayKyBox.Text = hd.NgayKy;
                ThoiHanBox.Text = hd.ThoiHan;
                NgayHetHanBox.Text = hd.NgayHetHan;
                MaDT_HDBox.Text = hd.MaDT;
                MaNV_HDBox.Text = hd.MaNV;
                TrangThaiBox.Text = hd.TrangThai;


            }
            catch
            {
                MessageBox.Show("Không tìm thấy!");
            }
            if (MaHDBox.Text != "")
            {
                DuyetThoiHanBox.IsEnabled = true;
                DuyetNgayHetHanBox.IsEnabled = true;
                DuyetHD.IsEnabled = true;
            }
            else
            {
                DuyetThoiHanBox.IsEnabled = false;
                DuyetNgayHetHanBox.IsEnabled = false;
                DuyetHD.IsEnabled = false;
            }

        }

        private void DuyetHopDong(object sender, RoutedEventArgs e)
        {
            SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
            try
            {
                db.Open();
                SqlTransaction trans = db.BeginTransaction();
                SqlCommand cmd = db.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trans;
                cmd.CommandText = "DuyetHopDong";
                cmd.Parameters.AddWithValue("@MaHD", hd.MaHD);
                cmd.Parameters.AddWithValue("@ThoiHan", DuyetThoiHanBox.Text);
                cmd.Parameters.AddWithValue("@NgayHetHan", DuyetNgayHetHanBox.Text);
                int result = Convert.ToInt32(cmd.ExecuteScalar());
                if (result == 0)
                {
                    MessageBox.Show("Duyệt hợp đồng thành công!");
                    trans.Commit();
                    HienThiDSHopDong(null, null);
                }
                else if (result == 1)
                {
                    MessageBox.Show("Mã hợp đồng không tồn tại!");
                    trans.Rollback();
                }
                else if (result == 2)
                {
                    MessageBox.Show("Hợp đồng ở trạng thái đã được duyệt!");
                    trans.Rollback();
                }
                else
                {
                    MessageBox.Show("Duyệt hợp đồng thất bại!");
                    trans.Rollback();
                }
            }
            catch
            {
                MessageBox.Show("Lỗi hệ thống!");
            }
        }


    }
}
