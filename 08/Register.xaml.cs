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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public string Username = "";
        public string Pass = "";
        public string RoleName = "";
        public static int MaNV = 1;
        public static int MaQT = 1;
        public static int MaDT = 1;
        public Register()
        {
            
            InitializeComponent();
        }
        private void ResetVisibility()
        {
            Register_NhanVien.Visibility = Visibility.Collapsed;
            Register_DoiTac.Visibility = Visibility.Collapsed;
            Register_QuanTri.Visibility = Visibility.Collapsed;
        }
        private void ChonPhanHe(object sender, SelectionChangedEventArgs e)
        {
            
            RoleName = ((ComboBoxItem)PhanHe.SelectedItem).Content.ToString();
            
            if (RoleName == "NhanVien")
            {
                ResetVisibility();
                Register_NhanVien.Visibility = Visibility.Visible;
                SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
                try
                {
                    db.Open();
                    SqlCommand createMaNV = db.CreateCommand();
                    createMaNV.CommandType = CommandType.Text;
                    createMaNV.CommandText = "SELECT MAX(MANV) FROM NHANVIEN";
                    MaNV = Convert.ToInt32(createMaNV.ExecuteScalar()) + 1;
                }
                catch
                {
                    MaNV = 1;
                }
                MNV.Text = MaNV.ToString();
            }

            if (RoleName == "QuanTri")
            {
                ResetVisibility();
                Register_QuanTri.Visibility = Visibility.Visible;
                SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
                try
                {
                    db.Open();
                    SqlCommand createMaQT = db.CreateCommand();
                    createMaQT.CommandType = CommandType.Text;
                    createMaQT.CommandText = "SELECT MAX(MAQT) FROM QUANTRI";
                    MaQT = Convert.ToInt32(createMaQT.ExecuteScalar()) + 1;
                }
                catch
                {
                    MaQT = 1;
                }
                MQT.Text = MaQT.ToString();
            }

            if (RoleName == "DoiTac")
            {
                ResetVisibility();
                Register_DoiTac.Visibility = Visibility.Visible;
                SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
                try
                {
                    db.Open();
                    SqlCommand createMaDT = db.CreateCommand();
                    createMaDT.CommandType = CommandType.Text;
                    createMaDT.CommandText = "SELECT MAX(MADT) FROM DOITAC";
                    MaDT = Convert.ToInt32(createMaDT.ExecuteScalar()) + 1;
                }
                catch
                {
                    MaDT = 1;
                }

            }
            
        }

        private void ReturnLogin(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            register.Close();
            login.ShowDialog();
        }

        private void SubmitRegistration(object sender, RoutedEventArgs e)
        {
            Username = username_account.Text;
            Pass = password_account.Password;
            
            SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
            try
            {
                db.Open();
                SqlTransaction trans = db.BeginTransaction();
                SqlCommand cmd = db.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trans;
                cmd.CommandText = "ThemUser";
                
                cmd.Parameters.AddWithValue("@Username", Username);
                cmd.Parameters.AddWithValue("@Pass", Pass);
                cmd.Parameters.AddWithValue("@RoleName", RoleName);


                int result = Convert.ToInt32(cmd.ExecuteScalar());

                if (result == 0)
                {
                    if (Pass != confirm_password.Password)
                    {

                        MessageBox.Show("Sai mật khẩu xác nhận!");
                        trans.Rollback();
                    }
                    else
                    {
                        MessageBox.Show("Tạo tài khoản thành công!");
                        trans.Commit();
                        

                        //NhanVien
                        if (RoleName == "NhanVien")
                        {
                            SqlTransaction trans_NV = db.BeginTransaction();
                            SqlCommand cmd_NV = db.CreateCommand();
                            cmd_NV.Transaction = trans_NV;
                            cmd_NV.CommandType = CommandType.StoredProcedure;
                            cmd_NV.CommandText = "ThemNhanVien";
                            cmd_NV.Parameters.AddWithValue("@MaNV", MaNV.ToString());
                            cmd_NV.Parameters.AddWithValue("@HoTen", HoTenNV.Text);
                            cmd_NV.Parameters.AddWithValue("@Username", Username);
                            int result_NV = Convert.ToInt32(cmd_NV.ExecuteScalar());
                            if (result_NV == 0)
                            {

                                MessageBox.Show("Đăng ký thông tin thành công!");
                                
                                trans_NV.Commit();
                                
                                register.Hide();
                                NhanVien_Window nv = new NhanVien_Window(Username);
                                nv.Show();
                                register.Close();
                            }
                            else if (result_NV == 1)
                            {

                                MessageBox.Show("Thông tin trống!");
                                trans_NV.Rollback();
                            }
                            else if (result_NV == 2)
                            {
                                MessageBox.Show("Mã nhân viên đã tồn tại!");
                                trans_NV.Rollback();
                            }
                            else
                            {
                                MessageBox.Show("Đăng ký thông tin thất bại!");
                                trans_NV.Rollback();
                            }
                            
                        }


                        //QuanTri
                        if (RoleName == "QuanTri")
                        {
                            SqlTransaction trans_QT = db.BeginTransaction();
                            SqlCommand cmd_QT = db.CreateCommand();
                            cmd_QT.Transaction = trans_QT;
                            cmd_QT.CommandType = CommandType.StoredProcedure;
                            cmd_QT.CommandText = "ThemQuanTri";
                            cmd_QT.Parameters.AddWithValue("@MaQT", MaQT.ToString());
                            cmd_QT.Parameters.AddWithValue("@HoTen", HoTenQTV.Text);
                            cmd_QT.Parameters.AddWithValue("@Username", Username);
                            int result_QT = Convert.ToInt32(cmd_QT.ExecuteScalar());
                            if (result_QT == 0)
                            {

                                MessageBox.Show("Đăng ký thông tin thành công!");

                                trans_QT.Commit();

                                register.Hide();
                                
                                register.Close();
                            }
                            else if (result_QT == 1)
                            {

                                MessageBox.Show("Thông tin trống!");
                                trans_QT.Rollback();
                            }
                            else if (result_QT == 2)
                            {
                                MessageBox.Show("Mã quản trị viên đã tồn tại!");
                                trans_QT.Rollback();
                            }
                            else
                            {
                                MessageBox.Show("Đăng ký thông tin thất bại!");
                                trans_QT.Rollback();
                            }

                        }

                        //DoiTac
                        if (RoleName == "DoiTac")
                        {
                            SqlTransaction trans_DT = db.BeginTransaction();
                            SqlCommand cmd_DT = db.CreateCommand();
                            cmd_DT.Transaction = trans_DT;
                            cmd_DT.CommandType = CommandType.StoredProcedure;
                            cmd_DT.CommandText = "ThemDoiTac";
                            cmd_DT.Parameters.AddWithValue("@MaDT", MaDT);
                            cmd_DT.Parameters.AddWithValue("@Email", Email.Text);
                            cmd_DT.Parameters.AddWithValue("@NgDaiDien", NgDaiDien.Text);
                            cmd_DT.Parameters.AddWithValue("@SLChiNhanh", SLChiNhanh.Text);
                            cmd_DT.Parameters.AddWithValue("@TenQuan", TenQuan.Text);
                            cmd_DT.Parameters.AddWithValue("@LoaiTP", LoaiTP.Text);
                            cmd_DT.Parameters.AddWithValue("@Username", Username);
                            int result_DT = Convert.ToInt32(cmd_DT.ExecuteScalar());
                            if (result_DT == 0)
                            {
                                MessageBox.Show("Đăng ký thông tin thành công!");
                                trans_DT.Commit();

                                register.Hide();

                                register.Close();
                            }
                            else if (result_DT == 1)
                            {

                                MessageBox.Show("Thông tin trống!");
                                trans_DT.Rollback();
                            }
                            else if (result_DT == 2)
                            {
                                MessageBox.Show("Mã đối tác đã tồn tại!");
                                trans_DT.Rollback();
                            }
                            else if (result_DT == 3)
                            {
                                MessageBox.Show("Số lượng chi nhánh không hợp lệ!");
                                trans_DT.Rollback();
                            }
                            else
                            {
                                MessageBox.Show("Đăng ký thông tin thất bại!");
                                trans_DT.Rollback();
                            }

                        }



                    }
                }
                else if (result == 1)
                {


                    MessageBox.Show("Thông tin trống!");
                    trans.Rollback();

                }
                else if (result == 2)
                {


                    MessageBox.Show("Username đã tồn tại!");
                    trans.Rollback();

                }
                else if (result == 3)
                {


                    MessageBox.Show("Role name không hợp lệ!");
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
        
        

    }
}
