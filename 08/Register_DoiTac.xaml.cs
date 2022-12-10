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
    /// Interaction logic for Register_DoiTac.xaml
    /// </summary>
    public partial class Register_DoiTac : Window
    {
        string Username = "";
        public static int MaDT = 1;
        public Register_DoiTac(string Username)
        {
            InitializeComponent();
            this.Username = Username;
        }
        private void SubmitRegistration(object sender, RoutedEventArgs e)
        {
            SqlConnection db = new SqlConnection("server=.; database=GIAONHANHANG; integrated security = true");
            try
            {
                db.Open();
                //Tìm mã đối tác mới bằng cách lấy mã đối tác lớn nhất hiện tại + 1
                SqlCommand createMaDT = db.CreateCommand();
                createMaDT.CommandType = CommandType.Text;
                createMaDT.CommandText = "SELECT MAX(MADT) FROM DOITAC";
                MaDT = Convert.ToInt32(createMaDT.ExecuteScalar()) + 1;

                SqlCommand cmd = db.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ThemDoiTac";
                cmd.Parameters.AddWithValue("@MaDT", MaDT);
                cmd.Parameters.AddWithValue("@Email", Email.Text);
                cmd.Parameters.AddWithValue("@NgDaiDien", NgDaiDien.Text);
                cmd.Parameters.AddWithValue("@SLChiNhanh", SLChiNhanh.Text);
                cmd.Parameters.AddWithValue("@TenQuan", TenQuan.Text);
                cmd.Parameters.AddWithValue("@LoaiTP", LoaiTP.Text);
                cmd.Parameters.AddWithValue("@Username", Username);
                int result = Convert.ToInt32(cmd.ExecuteScalar());
                if (result == 0)
                {
                    MessageBox.Show("Đăng ký thông tin thành công!");
                    register_DoiTac.Close();
                }
                else if (result == 1)
                {
                    
                    MessageBox.Show("Thông tin trống!");

                }
                else if (result == 2)
                {
                    MessageBox.Show("Mã đối tác đã tồn tại!");
                    
                }
                else if (result == 3)
                {
                    MessageBox.Show("Số lượng chi nhánh không hợp lệ!");

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
