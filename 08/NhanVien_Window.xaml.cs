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
                else if (result==1)
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
                MessageBox.Show("Lỗi hệ thống!");
            }
        }

        private void HienThiDSHopDong(object sender, RoutedEventArgs e)
        {
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
                MessageBox.Show("Lỗi hệ thống!");
            }
        }

        
    }
}
