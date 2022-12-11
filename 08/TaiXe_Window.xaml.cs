using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
    /// Interaction logic for TaiXe_Window.xaml
    /// </summary>
    public partial class TaiXe_Window : Window
    {
        string Username = "";
        public TaiXe_Window(string Username)
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
                CmdString = "SELECT * FROM TAIXE WHERE Username = \'" + Username + "\'";
                db.Open();
                SqlCommand cmd = new SqlCommand(CmdString, db);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable("TAIXE");
                sda.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    TMaTX.Text = dr["MaTX"].ToString();
                    TCMND.Text = dr["CMND"].ToString();
                    THoTen.Text = dr["HoTen"].ToString();
                    TDiaChi.Text = dr["DiaChi"].ToString();
                    TSDT.Text = dr["SDT"].ToString();
                    TBienSoXe.Text = dr["BienSoXe"].ToString();
                    TKhuVuc.Text = dr["KhuVucHoatDong"].ToString();
                    TEmail.Text = dr["Email"].ToString();
                    TSTK.Text = dr["SoTaiKhoan"].ToString();
                    TNganHang.Text = dr["NganHang"].ToString();
                    TSTK.Text = dr["SoTaiKhoan"].ToString();
                    TChiNhanhNH.Text = dr["CNNganHang"].ToString();
                }
                db.Close();
            }
            catch
            {
                MessageBox.Show("Lỗi hệ thống!");
            }
        }

        private void infoDon(DataGrid dg, ComboBox cb)
        {
            string CmdString = string.Empty;
            SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
            try
            {
                db.Open();
                CmdString = "SELECT * FROM DONDATHANG";
                SqlCommand cmd = new SqlCommand(CmdString, db);


                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable("DONDATHANG");
                sda.Fill(dt);

                dg.ItemsSource = dt.DefaultView;

                cb.Items.Clear();

                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["MaTX"].ToString() == "" && dr["TinhTrang"].ToString() != "Đã hủy đơn")
                        cb.Items.Add(dr["MaDH"].ToString());
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
            PhucVu.Visibility = Visibility.Collapsed;
            UpdateDonHang.Visibility = Visibility.Collapsed;
        }
        private void GetInfo(object sender, RoutedEventArgs e)
        {
            ResetVisibility();
            Infomation.Visibility = Visibility.Visible;
            Info();
        }

        private void DSDonDatHang()
        {
            infoDon(BangPhucVu, DonHang);
        }
        private void SelectDonHang(object sender, RoutedEventArgs e)
        {
            ResetVisibility();
            PhucVu.Visibility = Visibility.Visible;
            DSDonDatHang();
        }

        private void ClickPhucVu(object sender, RoutedEventArgs e)
        {
            int selectedIndex = DonHang.SelectedIndex;
            Object selectedItem = DonHang.SelectedItem;
            string value = selectedItem.ToString();
            string CmdString = string.Empty;
            SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
            try
            {
                db.Open();
                CmdString = "UPDATE DONDATHANG SET MaTX = \'" + TMaTX.Text + "\', TinhTrang = N\'Tài xế đang giao\'" + " WHERE MaDH = \'" + value + "\' AND MaTX is NULL";
                SqlCommand cmd = new SqlCommand(CmdString, db);
                cmd.ExecuteNonQuery();
                DSDonDatHang();

            }
            catch
            {
                MessageBox.Show("Lỗi hệ thống!");
            }
        }
    
        private void ButtonUpdateDon(object sender, RoutedEventArgs e)
        {
            ResetVisibility();
            UpdateDonHang.Visibility = Visibility.Visible;
            string CmdString = string.Empty;
            SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
            try
            {
                db.Open();
                CmdString = "SELECT * FROM DONDATHANG WHERE MaTX = \'" + TMaTX.Text + "\'";
                SqlCommand cmd = new SqlCommand(CmdString, db);


                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable("DONDATHANG");
                sda.Fill(dt);

                BangDonHangUpdate.ItemsSource = dt.DefaultView;

                DHPV.Items.Clear();

                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["MaTX"].ToString() == TMaTX.Text)
                        DHPV.Items.Add(dr["MaDH"].ToString());
                }
                db.Close();
            }
            catch
            {
                MessageBox.Show("Lỗi hệ thống!");
            }
        }
        private void ClickTT(object sender, RoutedEventArgs e)
        {
            int selectedIndex1 = DHPV.SelectedIndex;
            Object selectedItem1 = DHPV.SelectedItem;
            string value1 = selectedItem1.ToString();

            var comboBoxItem = TrangThai.Items[TrangThai.SelectedIndex] as ComboBoxItem;
            string value2 = "";

            value2 = comboBoxItem.Content.ToString();

            string CmdString = string.Empty;
            SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
            try
            {
                db.Open();
                CmdString = "UPDATE DONDATHANG SET TinhTrang = N\'" + value2 + "\' WHERE MaDH = \'" + value1 + "\'";
                SqlCommand cmd = new SqlCommand(CmdString, db);
                cmd.ExecuteNonQuery();
                //DSDonDatHang();

                string CmdStringReload = "SELECT * FROM DONDATHANG WHERE MaTX = \'" + TMaTX.Text + "\'";
                SqlCommand cmdReload = new SqlCommand(CmdStringReload, db);
                SqlDataAdapter sda = new SqlDataAdapter(cmdReload);

                DataTable dt = new DataTable("DONDATHANG");
                sda.Fill(dt);

                BangDonHangUpdate.ItemsSource = dt.DefaultView;

                MessageBox.Show("Cập nhật đơn hàng thành công!");

                db.Close();
            }
            catch
            {
                MessageBox.Show("Lỗi hệ thống!");
            }
        }

        private void ChonTT(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ClickUpdateInfo(object sender, RoutedEventArgs e)
        {
            string CmdString = string.Empty;
            SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
            try
            {
                db.Open();
                CmdString = "UPDATE TAIXE SET CMND = \'" + TCMND.Text + "\', HoTen = N\'"
                        + THoTen.Text + "\', SDT = \'" + TSDT.Text + "\', DiaChi = N\'" + TDiaChi.Text 
                        + "\', BienSoXe = \'" + TBienSoXe.Text + "\', KhuVucHoatDong = N\'" + TKhuVuc.Text 
                        + "\', Email = \'" + TEmail.Text + "\', SoTaiKhoan = \'" + TSTK.Text 
                        + "\', NganHang = N\'" + TNganHang.Text + "\', CNNganHang = N\'" + TChiNhanhNH.Text + "\'  WHERE Username = \'"
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


        private void ChonDonHang(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ClickReload(object sender, RoutedEventArgs e)
        {
            ResetVisibility();
            PhucVu.Visibility = Visibility.Visible;
            DSDonDatHang();
        }

        private void DonHangPhucVu(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ClickReloadUpdate(object sender, RoutedEventArgs e)
        {
            SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
            try
            {
                string CmdStringReload = "SELECT * FROM DONDATHANG WHERE MaTX = \'" + TMaTX.Text + "\'";
                SqlCommand cmdReload = new SqlCommand(CmdStringReload, db);
                SqlDataAdapter sda = new SqlDataAdapter(cmdReload);

                DataTable dt = new DataTable("DONDATHANG");
                sda.Fill(dt);

                BangDonHangUpdate.ItemsSource = dt.DefaultView;

                db.Close();
            }
            catch
            {
                MessageBox.Show("Lỗi hệ thống!");
            }
        }
    }
}
