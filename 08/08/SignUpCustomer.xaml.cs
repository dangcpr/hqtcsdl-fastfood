using _08.Model;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class SignUpCustomer : Window
    {
        private bool IsSignUp { get; set; }
        public SignUpCustomer()
        {
            IsSignUp = false;
            InitializeComponent();

        }

        private void SubmitSignUp(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection("server=DANG; database=GIAONHANHANG; integrated security = true");
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "TimKiemUserPass";
                cmd.Parameters.AddWithValue("@Username", username_account.Text);
                cmd.Parameters.AddWithValue("@Password", password_account.Password);
                object kq = cmd.ExecuteScalar();
                int code = Convert.ToInt32(kq);
                if (code == 1)
                {
                    MessageBox.Show("Chào mừng User đăng nhập");
                    conn.Close();
                }
                else if (code == 0)
                {
                    MessageBox.Show("Đăng nhập thất bại");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SubmitLogin(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Ban nhan vao dang ky!");
        }
    }
}
