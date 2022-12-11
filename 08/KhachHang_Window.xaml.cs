using Google.Apis.AnalyticsReporting.v4.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
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
    public partial class KhachHang_Window : Window
    {
        string Username = "";
        public KhachHang_Window(string Username)
        {
            InitializeComponent();
            this.Username = Username;
            Info();
        }
        private void Info()
        {
            string CmdString = string.Empty;
            SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
            try
            {
                CmdString = "SELECT * FROM KHACHHANG WHERE Username = \'" + Username + "\'";
                db.Open();
                SqlCommand cmd = new SqlCommand(CmdString, db);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable("KHACHHANG");
                sda.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    CMaKH.Text = dr["MaKH"].ToString();
                    CEmail.Text = dr["Email"].ToString();
                    CHoTen.Text = dr["HoTen"].ToString();
                    CSDT.Text = dr["SDT"].ToString();
                    CDiaChi.Text = dr["DiaChi"].ToString();
                }
                db.Close();
            }
            catch
            {
                MessageBox.Show("Lỗi hệ thống!");
            }
        }
        private void ResetVisibility()
        {
            Infomation.Visibility = Visibility.Collapsed;
            DoiTac.Visibility = Visibility.Collapsed;
            MonAn.Visibility = Visibility.Collapsed;
            DonHang.Visibility = Visibility.Collapsed;
        }
        private void GetInfo(object sender, RoutedEventArgs e)
        {
            ResetVisibility();
            Infomation.Visibility = Visibility.Visible;
            Info();
        }
        private void SelectDoiTac(object sender, RoutedEventArgs e)
        {
            ResetVisibility();
            DoiTac.Visibility = Visibility.Visible;
            string CmdString = string.Empty;
            SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
            try
            {
                db.Open();
                SqlTransaction trans_DT = db.BeginTransaction();
                SqlCommand cmd_DT = db.CreateCommand();
                cmd_DT.Transaction = trans_DT;
                cmd_DT.CommandType = CommandType.StoredProcedure;
                cmd_DT.CommandText = "sp_DSDoiTac_fix";


                SqlDataAdapter sda = new SqlDataAdapter(cmd_DT);

                DataTable dt = new DataTable("DOITAC");
                sda.Fill(dt);

                BangDoiTac.ItemsSource = dt.DefaultView;

            }
            catch
            {
                MessageBox.Show("Lỗi hệ thống!");
            }
        }
        int TotalFee = 20000;
        private void GetMonAn(object sender, RoutedEventArgs e)
        {
            ResetVisibility();
            MonAn.Visibility = Visibility.Visible;
            items.Clear();
            Total.Content = "0";
            TotalFee = 20000;
            
            BangMonAnDaDat.Items.Clear();
            //Lấy thông tin đối tác
            try
            {
                SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
                string CmdString = "SELECT * FROM DOITAC";
                db.Open();
                SqlCommand cmd = new SqlCommand(CmdString, db);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable("DOITAC");
                sda.Fill(dt);
                MaDT.Items.Clear();

                foreach (DataRow dr in dt.Rows)
                {
                    MaDT.Items.Add(dr["MaDT"].ToString());
                }
                int currentMaDT = MaDT.SelectedIndex;

                //Lấy thông tin món ăn
                string CmdString2 = "SELECT * FROM THUCPHAM";
                SqlCommand cmd2 = new SqlCommand(CmdString2, db);

                SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);

                DataTable dt2 = new DataTable("THUCPHAM");
                sda2.Fill(dt2);

                BangMonAn.ItemsSource = dt2.DefaultView;

                //Lấy mã đơn hàng
                string CmdMaDHMax = "SELECT MAX(MaDH) FROM DONDATHANG";
                SqlCommand cmd3 = new SqlCommand(CmdMaDHMax, db);

                int MaDH = Convert.ToInt32(cmd3.ExecuteScalar()) + 1;
                CMaDH.Text = (MaDH).ToString();
                db.Close();

                //Add columns bảng chi tiết đơn hàng tạm thời
                BangMonAnDaDat.Columns.Clear();
                BangMonAnDaDat.Columns.Add(new DataGridTextColumn { Header = "MaTP", Binding = new Binding("MaTP") });
                BangMonAnDaDat.Columns.Add(new DataGridTextColumn { Header = "MaDT", Binding = new Binding("MaDT") });
                BangMonAnDaDat.Columns.Add(new DataGridTextColumn { Header = "SoLuong", Binding = new Binding("SoLuong") });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.ToString());
            }
        }
        private void ClickMonAn(object sender, RoutedEventArgs e)
        {
        //    string CmdString = string.Empty;
        //    SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
        //    try
        //    {
        //        string MaDoiTac = MaDT.Text;
        //        CmdString = "SELECT * FROM THUCPHAM WHERE MADT = " + MaDoiTac;
        //        db.Open();
        //        SqlCommand cmd = new SqlCommand(CmdString, db);

        //        SqlDataAdapter sda = new SqlDataAdapter(cmd);

        //        DataTable dt = new DataTable("THUCPHAM");
        //        sda.Fill(dt);

        //        BangMonAn.ItemsSource = dt.DefaultView;
        //    }
        //    catch
        //    {
        //        MessageBox.Show("Lỗi hệ thống hoặc dữ liệu không tồn tại!");
        //    }
        }

        private void ChonDoiTac(object sender, SelectionChangedEventArgs e)
        {
            SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
            db.Open();
            try {

                //string MaDTDaChon = ((ComboBoxItem)MaDT.SelectedItem).Content.ToString();
                int selectedIndex1 = MaDT.SelectedIndex;
                Object selectedItem1 = MaDT.SelectedItem;
                string value1 = selectedItem1.ToString();
                string CmdStringMonAn = "SELECT * FROM THUCPHAM WHERE MaDT = \'" + value1 + "\'";
                SqlCommand cmdMonAn = new SqlCommand(CmdStringMonAn, db);

                SqlDataAdapter sdaMA = new SqlDataAdapter(cmdMonAn);

                DataTable dtMA = new DataTable("THUCPHAM");
                sdaMA.Fill(dtMA);
                MaTP.Items.Clear();

                foreach (DataRow dr in dtMA.Rows)
                {
                    MaTP.Items.Add(dr["MaTP"].ToString());
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + Ex.ToString());
            }
        }

        private void ClickUpdateInfo(object sender, RoutedEventArgs e)
        {
            string CmdString = string.Empty;
            SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
            try
            {
                db.Open();
                CmdString = "UPDATE KHACHHANG SET Email = \'" + CEmail.Text + "\', HoTen = N\'"
                        + CHoTen.Text + "\', SDT = \'" + CSDT.Text + "\', DiaChi = N\'" + CDiaChi.Text + "\'  WHERE Username = \'"
                        + Username + "\'";
                SqlCommand cmd = new SqlCommand(CmdString, db);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Cập nhật thành công!");
                db.Close();
            }
            catch (SqlException odbcEx)
            {
                MessageBox.Show("Lỗi SQL: " + odbcEx.ToString());

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Exception: " + ex.ToString());
            }
        }
        private DataTable GetDonHang()
        {
            SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
            string CmdString = "SELECT * FROM DONDATHANG WHERE MaKH = \'" + CMaKH.Text + "\'";
            db.Open();
            SqlCommand cmd = new SqlCommand(CmdString, db);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable("DONDATHANG");
            sda.Fill(dt);
            BangDonHang.ItemsSource = dt.DefaultView;
            ChonMaDH.Items.Clear();

            foreach (DataRow dr in dt.Rows)
            {
                ChonMaDH.Items.Add(dr["MaDH"].ToString());
            }

            return dt;
        }
        private void TDDH(object sender, RoutedEventArgs e)
        {
            ResetVisibility();
            DonHang.Visibility = Visibility.Visible;
            GetDonHang();
        }

        private void ClickReloadDonHang(object sender, RoutedEventArgs e)
        {
            GetDonHang();
        }

        private void ClickXemChiTiet(object sender, RoutedEventArgs e)
        {
            int selectedIndex1 = ChonMaDH.SelectedIndex;
            Object selectedItem1 = ChonMaDH.SelectedItem;
            string value1 = selectedItem1.ToString();

            SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
            string CmdString = "SELECT CT.MaDH, CT.MaTP, CT.MaDT, TP.TenMon, TP.MieuTa, TP.Gia, TP.TinhTrang, TP.TuyChon, CT.SoLuong, CT.DanhGia" +
                " FROM CHITIETDONDATHANG CT" +
                " JOIN THUCPHAM TP ON CT.MaTP = TP.MaTP and CT.MaDT = TP.MaDT " +
                "WHERE MaDH = \'" + value1 + "\'";
            db.Open();
            SqlCommand cmd = new SqlCommand(CmdString, db);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable("DONDATHANG");

            sda.Fill(dt);
            BangChiTiet.ItemsSource = dt.DefaultView;
            
        }

        private void ChonMaDH_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ClickReloadChiTiet(object sender, RoutedEventArgs e)
        {
            DataTable dt = GetDonHang();
            ChonMaDH.Items.Clear();

            foreach (DataRow dr in dt.Rows)
            {
                ChonMaDH.Items.Add(dr["MaDH"].ToString());
            }
        }

        private void ItemDonHang(object sender, SelectionChangedEventArgs e)
        {

        }

        //List<DataItem> dataItemsMonAn = new List<DataItem>();
        List<DataItem2> items = new List<DataItem2>();
        
        private void ClickInsertMonAn(object sender, RoutedEventArgs e)
        {
            try
            {
                int selectedIndex1 = MaDT.SelectedIndex;
                Object selectedItem1 = MaDT.SelectedItem;
                string MaDoiTac = selectedItem1.ToString();

                int selectedIndex2 = MaTP.SelectedIndex;
                Object selectedItem2 = MaTP.SelectedItem;
                string MaThucPham = selectedItem2.ToString();
                // ... Create a List of objects.
                items.Add(new DataItem2(MaDoiTac, MaThucPham, CSoLuong.Text));

                BangMonAnDaDat.Items.Add(new DataItem2(MaDoiTac, MaThucPham, CSoLuong.Text));

                SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
                string CmdString = "SELECT Gia FROM THUCPHAM WHERE MaTP = \'" + MaThucPham + "\' AND MaDT = \'" + MaDoiTac + "\'";
                db.Open();

                SqlCommand cmd = new SqlCommand(CmdString, db);

                TotalFee += Convert.ToInt32(cmd.ExecuteScalar()) * int.Parse(CSoLuong.Text);
                Total.Content = TotalFee;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + Ex.ToString());
            }
            
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
        //public ObservableCollection<ProductData> DataGridItemsSource { get; set; }

        private void ClickXacNhanDatHang(object sender, RoutedEventArgs e)
        {
            try
            {
                string MaDH = CMaDH.Text;
                SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
                db.Open();

                string CmdString = "INSERT INTO DONDATHANG (MaDH, GioDat, NgayDat, GiaTriDH, TinhTrang, MaKH) VALUES (\'"
                    + MaDH + "\',format(getdate(), 'HH:MM'), getdate()"+ "," + int.Parse(Total.Content.ToString()) + ",N\'" + "Chờ xử lý" + "\','" + CMaKH.Text + "\')";
                SqlCommand cmd = new SqlCommand(CmdString, db);
                cmd.ExecuteNonQuery();

                string MaTP, MaDT;
                int SoLuong;

                foreach (DataItem2 item in items)
                {
                    MaTP = item.MaTP.ToString();
                    MaDT = item.MaDT.ToString();
                    SoLuong = int.Parse(item.SoLuong.ToString());
                    CmdString = "INSERT INTO CHITIETDONDATHANG (MaDH, MaTP, MaDT, SoLuong) VALUES (\'"
                    + MaDH + "\',\'" + MaTP + "\','" + MaDT + "\'," + SoLuong + ')';
                    cmd = new SqlCommand(CmdString, db);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Tạo đơn hàng thành công!");
                BangMonAnDaDat.Items.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.ToString());
            }
        }

        private void ListMonAn(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ClickHuyDon(object sender, RoutedEventArgs e)
        {
            int selectedIndex1 = ChonMaDH.SelectedIndex;
            Object selectedItem1 = ChonMaDH.SelectedItem;
            string value1 = selectedItem1.ToString();

            SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
            db.Open();
            string cmdStringTT = "SELECT TinhTrang FROM DONDATHANG WHERE MaDH = '" + value1 + "'";
            SqlCommand cmdTT = new SqlCommand(cmdStringTT, db);

            if (cmdTT.ExecuteScalar().ToString() == "Chờ xử lý")
            {
                try
                {
                    SqlTransaction trans = db.BeginTransaction();
                    SqlCommand cmd = db.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Transaction = trans;
                    cmd.CommandText = "sp_KhachHangHuyDon_fix";

                    cmd.Parameters.AddWithValue("@MaDonDH", value1);

                    cmd.ExecuteScalar();
                    trans.Commit();
                    MessageBox.Show("Hủy đơn thành công");

                    db.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi hệ thống: " + ex.ToString());
                }
            }
            else
            {
                try
                {
                    MessageBox.Show("Không thể hủy đơn");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi hệ thống: " + ex.ToString());
                }
            }
        }
    }

    internal class DataItem2
    {

        public DataItem2(string value1, string value2, string value3)
        {
            this.MaDT = value1;
            this.MaTP = value2;
            this.SoLuong = value3;
        }

        public string MaDT { get; set; }
        public string MaTP { get; set; }
        public string SoLuong { get; set; }
    }
}
