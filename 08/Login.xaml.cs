using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data;

namespace _08
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public string UserName = "";
        string RoleName = "";
        public bool IsLogin = false;
        public Login()
        {
            IsLogin = false;
            InitializeComponent();

        }

        private void SubmitLogin(object sender, RoutedEventArgs e)
        {
            
            SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
            try
            {
                db.Open();
                SqlCommand cmd = db.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "LoginUser";
                cmd.Parameters.AddWithValue("@Username", username_account.Text);
                cmd.Parameters.AddWithValue("@Pass", password_account.Password);
                UserName = username_account.Text;
                int result = Convert.ToInt32(cmd.ExecuteScalar());
                if (result == 0)
                {
                    MessageBox.Show("Đăng nhập thành công!");
                    IsLogin = true;
                    login.Hide();
                    try
                    {
                        SqlCommand cmd2 = db.CreateCommand();
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.CommandText = "TimKiemUser";
                        cmd2.Parameters.AddWithValue("@Username", username_account.Text);
                        SqlDataReader sdr = cmd2.ExecuteReader();
                        sdr.Read();
                        RoleName = sdr["RoleName"].ToString();
                        if (RoleName == "NhanVien")
                        {
                            NhanVien_Window nv = new NhanVien_Window(UserName);
                            nv.ShowDialog();
                            login.Close();
                        }
                        if (RoleName == "KhachHang")
                        {
                            KhachHang_Window kh = new KhachHang_Window(UserName);
                            kh.ShowDialog();
                            login.Close();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Lỗi hệ thống!");
                    }
                }
                else
                {
                    MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!");
                }
            }
            catch
            {
                MessageBox.Show("Lỗi hệ thống!");
            }
            
        }
        private void ClickRegister(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            login.Close();
            register.ShowDialog();
        }

    }
}
