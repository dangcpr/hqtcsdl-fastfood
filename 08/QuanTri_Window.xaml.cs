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
    /// Interaction logic for QuanTri_Window.xaml
    /// </summary>
    public partial class QuanTri_Window : Window
    {
        private string Username = "";
        private string MaQT = "";
        private string HoTenQT = "";
        public QuanTri_Window(string Username)
        {
            InitializeComponent();
            this.Username = Username;
            UsernameBox.Text = Username;
            SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
            try
            {
                db.Open();
                SqlCommand cmd = db.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "TimKiemQuanTri";
                cmd.Parameters.AddWithValue("@Username", Username);
                SqlDataReader sdr = cmd.ExecuteReader();
                sdr.Read();
                MaQT = sdr["MaQT"].ToString();
                HoTenQT = sdr["HoTen"].ToString();
                MaQTBox.Text = MaQT;
                HoTenQTBox.Text = HoTenQT;
            }
            catch
            {
                MessageBox.Show("Lỗi hệ thống!");
            }
        }


        private void ResetVisibility()
        {
            ThongTinCaNhan.Visibility = Visibility.Collapsed;
            QuanLyNgDung.Visibility = Visibility.Collapsed;
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
                cmd.CommandText = "TimKiemQuanTri";
                cmd.Parameters.AddWithValue("@Username", Username);
                SqlDataReader sdr = cmd.ExecuteReader();
                sdr.Read();
                MaQT = sdr["MaQT"].ToString();
                HoTenQT = sdr["HoTen"].ToString();
                MaQTBox.Text = MaQT;
                HoTenQTBox.Text = HoTenQT;
            }
            catch
            {
                MessageBox.Show("Lỗi hệ thống!");
            }
        }

        private void ShowQLND(object sender, RoutedEventArgs e)
        {
            ResetVisibility();
            QuanLyNgDung.Visibility = Visibility.Visible;
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
            HoTenQTBox.IsEnabled = true;
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
                cmd.CommandText = "CapNhatQuanTri";
                cmd.Parameters.AddWithValue("@MaQT", MaQT);
                cmd.Parameters.AddWithValue("@HoTen", HoTenQTBox.Text);
                cmd.Parameters.AddWithValue("@Username", UsernameBox.Text);
                int result = Convert.ToInt32(cmd.ExecuteScalar());
                if (result == 0)
                {
                    MessageBox.Show("Cập nhật thành công!");
                    trans.Commit();
                    HoTenQT = HoTenQTBox.Text;
                    Username = UsernameBox.Text;
                    SaveBtn.Visibility = Visibility.Collapsed;
                    EditBtn.Visibility = Visibility.Visible;
                    UsernameBox.IsEnabled = false;
                    HoTenQTBox.IsEnabled = false;
                    ChangePassBtn.IsEnabled = true;
                }
                else if (result == 1)
                {
                    MessageBox.Show("Quản trị viên không tồn tại!");
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



        private void HienThiDSNgDung(object sender, RoutedEventArgs e)
        {
            dgNgDung.Visibility = Visibility.Visible;
            SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
            try
            {
                db.Open();
                SqlDataAdapter GetDSDoiTac = new SqlDataAdapter("Select * from USERS", db);
                DataTable DSDoiTac = new DataTable("USERS");
                GetDSDoiTac.Fill(DSDoiTac);
                dgNgDung.ItemsSource = DSDoiTac.DefaultView;
            }
            catch
            {
            }
        }

        public class ND //Người dùng
        {
            public string Username { get; set; }
            public string Pass { get; set; }
            public string RoleName { get; set; }
            public string TrangThai { get; set; }
            
        }
        ND user = new ND();

        private void SelectDGNgDung(object sender, SelectionChangedEventArgs e)
        {

            DataRowView dataRow = (DataRowView)dgNgDung.SelectedItem;
            user.Username = dataRow.Row.ItemArray[0].ToString();
            user.Pass = dataRow.Row.ItemArray[1].ToString();
            user.RoleName = dataRow.Row.ItemArray[2].ToString();
            user.TrangThai = dataRow.Row.ItemArray[3].ToString();
            UserBox.Text = user.Username;
            PassBox.Text = user.Pass;
            RoleNameBox.Text = user.RoleName;
            TrangThaiBox.Text = user.TrangThai;
        }



    }
}
