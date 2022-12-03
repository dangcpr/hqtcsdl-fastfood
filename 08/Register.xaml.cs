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
        public string PhanHeUser = "";
        public Register()
        {
            
            InitializeComponent();
        }
        private void ChonPhanHe(object sender, SelectionChangedEventArgs e)
        {
            PhanHeUser = ((ComboBoxItem)PhanHe.SelectedItem).Content.ToString();
        }

        private void ReturnLogin(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            register.Close();
            login.ShowDialog();
        }

        private void NextStepRegister(object sender, RoutedEventArgs e)
        {
            Username = username_account.Text;
            Pass = password_account.Password;
            RoleName = PhanHeUser;
            SqlConnection db = new SqlConnection("server=DANG; database=GIAONHANHANG; integrated security = true");
            try
            {
                db.Open();

                SqlCommand cmd = db.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;

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
                            
                    }
                    else
                    {
                        MessageBox.Show("Tạo tài khoản thành công!");
                            
                        if (RoleName == "DoiTac")
                        {
                            register.Close();

                            Register_DoiTac register_DoiTac = new Register_DoiTac(Username);
                            register_DoiTac.ShowDialog();
                        }
                        if (RoleName == "KhachHang")
                        {
                            register.Close();
                            Register_KhachHang register_KhachHang = new Register_KhachHang(Username);
                            register_KhachHang.ShowDialog();
                        }
                    }
                }
                else if (result == 1)
                {

                        
                    MessageBox.Show("Thông tin trống!");


                }
                else if (result == 2)
                {

                        
                    MessageBox.Show("Username đã tồn tại!");


                }
                else if (result == 3)
                {

                        
                    MessageBox.Show("Role name không hợp lệ!");
                        

                }
                else
                {

                    MessageBox.Show("Lỗi hệ thống!");

                }
                    

                
            }
            catch
            {
                MessageBox.Show("Lỗi hệ thống!");
            }




             
                
            
        }
        
        

    }
}
