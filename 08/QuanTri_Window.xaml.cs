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
            QuanLyPhanHe.Visibility = Visibility.Collapsed;
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

        private void ShowQLPH(object sender, RoutedEventArgs e)
        {
            ResetVisibility();
            QuanLyPhanHe.Visibility = Visibility.Visible;
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
            if (dgNgDung.SelectedItem != null)
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
            else
            {
                UserBox.Text = "";
                PassBox.Text = "";
                RoleNameBox.Text = "";
                TrangThaiBox.Text = "";
            }
            if (UserBox.Text != "")
            {
                Khoa_MoKhoaTK.IsEnabled = true;
            }
            else
            {
                Khoa_MoKhoaTK.IsEnabled = false;
            }
        }


        private void Block_Unblock(object sender, RoutedEventArgs e)
        {
            SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
            try
            {
                db.Open();
                SqlTransaction trans = db.BeginTransaction();
                SqlCommand cmd = db.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trans;
                cmd.CommandText = "Block_Unblock";
                cmd.Parameters.AddWithValue("@Username", user.Username);

                int result = Convert.ToInt32(cmd.ExecuteScalar());
                if (result == 2)
                {
                    MessageBox.Show("Kích hoạt tài khoản thành công!");
                    trans.Commit();
                    HienThiDSNgDung(null, null);
                }
                if (result == 3)
                {
                    MessageBox.Show("Khóa tài khoản thành công!");
                    trans.Commit();
                    HienThiDSNgDung(null, null);
                }
                else if (result == 1)
                {
                    MessageBox.Show("Username không tồn tại!");
                    trans.Rollback();
                }
            }
            catch
            {
                MessageBox.Show("Lỗi hệ thống!");
            }
        }

        private void FindUser(object sender, RoutedEventArgs e)
        {
            SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
            try
            {
                db.Open();
                SqlCommand cmd = db.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "TimKiemUser";
                cmd.Parameters.AddWithValue("@Username", FindUserBox.Text);
                SqlDataReader sdr = cmd.ExecuteReader();
                sdr.Read();
                user.Username = sdr["Username"].ToString();
                user.Pass = sdr["Pass"].ToString();
                user.RoleName = sdr["RoleName"].ToString();
                user.TrangThai = sdr["TrangThai"].ToString();
                UserBox.Text = user.Username;
                PassBox.Text = user.Pass;
                RoleNameBox.Text = user.RoleName;
                TrangThaiBox.Text = user.TrangThai;
            }
            catch
            {
                MessageBox.Show("Không tìm thấy!");
            }
            if (UserBox.Text != "")
            {
                Khoa_MoKhoaTK.IsEnabled = true;
            }
            else
            {
                Khoa_MoKhoaTK.IsEnabled = false;
            }
        }

        class PhanHe
        {
            public string Username { get; set; }
            public string Pass { get; set; }
            public string RoleName { get; set; }

            public string PhanHeUser { get; set; }

            public int MaNV { get; set; }
            public int MaQT { get; set; }
            public int MaDT { get; set; }
            public int MaKH { get; set; }
            public int MaTX { get; set; }
        }
        PhanHe ph = new PhanHe();

        private void ResetQLPHVisibility()
        {
            ThemTTPH.Visibility = Visibility.Collapsed;
            SuaTTPH.Visibility = Visibility.Collapsed;
            XoaTTPH.Visibility = Visibility.Collapsed;
            XemTTPH.Visibility = Visibility.Collapsed;
        }



        private void ChonPhanHe(object sender, SelectionChangedEventArgs e)
        {

            ph.RoleName = ((ComboBoxItem)SelectPhanHe.SelectedItem).Content.ToString();




        }

        private void ShowThemTT(object sender, RoutedEventArgs e)
        {
            ResetQLPHVisibility();
            ThemTTPH.Visibility = Visibility.Visible;
        }


        private void SubmitRegistration(object sender, RoutedEventArgs e)
        {
            ph.Username = username_account.Text;
            ph.Pass = password_account.Password;

            SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
            try
            {
                db.Open();
                SqlTransaction trans = db.BeginTransaction();
                SqlCommand cmd = db.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trans;
                cmd.CommandText = "ThemUser";

                cmd.Parameters.AddWithValue("@Username", ph.Username);
                cmd.Parameters.AddWithValue("@Pass", ph.Pass);
                cmd.Parameters.AddWithValue("@RoleName", ph.RoleName);


                int result = Convert.ToInt32(cmd.ExecuteScalar());

                if (result == 0)
                {
                    if (ph.Pass != confirm_password.Password)
                    {

                        MessageBox.Show("Sai mật khẩu xác nhận!");
                        trans.Rollback();
                    }
                    else
                    {
                        MessageBox.Show("Tạo tài khoản thành công!");
                        trans.Commit();
                        if (ph.RoleName == "NhanVien")
                        {
                            int Ma = 1;
                            try
                            {
                                SqlCommand createMa = db.CreateCommand();
                                createMa.CommandType = CommandType.Text;
                                createMa.CommandText = "SELECT MAX(MaNV) FROM NHANVIEN";
                                Ma = Convert.ToInt32(createMa.ExecuteScalar()) + 1;
                            }
                            catch
                            {
                                Ma = 1;
                            }
                            try
                            {
                                SqlCommand com = db.CreateCommand();
                                com.CommandType = CommandType.Text;
                                com.CommandText = "INSERT INTO NHANVIEN VALUES (@MaNV,null,@Username)";
                                com.Parameters.AddWithValue("@MaNV", Ma);
                                com.Parameters.AddWithValue("@Username", username_account.Text);
                                com.ExecuteNonQuery();
                            }
                            catch
                            {
                                MessageBox.Show("Lỗi hệ thống!");
                            }
                        }

                        if (ph.RoleName == "QuanTri")
                        {
                            int Ma = 1;
                            try
                            {
                                SqlCommand createMa = db.CreateCommand();
                                createMa.CommandType = CommandType.Text;
                                createMa.CommandText = "SELECT MAX(MaQT) FROM QUANTRI";
                                Ma = Convert.ToInt32(createMa.ExecuteScalar()) + 1;
                            }
                            catch
                            {
                                Ma = 1;
                            }
                            try
                            {
                                SqlCommand com = db.CreateCommand();
                                com.CommandType = CommandType.Text;
                                com.CommandText = "INSERT INTO QUANTRI VALUES (@MaQT,null,@Username)";
                                com.Parameters.AddWithValue("@MaQT", Ma);
                                com.Parameters.AddWithValue("@Username", username_account.Text);
                                com.ExecuteNonQuery();
                            }
                            catch
                            {
                                MessageBox.Show("Lỗi hệ thống!");
                            }
                        }

                        if (ph.RoleName == "DoiTac")
                        {
                            int Ma = 1;
                            try
                            {
                                SqlCommand createMa = db.CreateCommand();
                                createMa.CommandType = CommandType.Text;
                                createMa.CommandText = "SELECT MAX(MaDT) FROM DOITAC";
                                Ma = Convert.ToInt32(createMa.ExecuteScalar()) + 1;
                            }
                            catch
                            {
                                Ma = 1;
                            }
                            try
                            {
                                SqlCommand com = db.CreateCommand();
                                com.CommandType = CommandType.Text;
                                com.CommandText = "INSERT INTO DOITAC VALUES (@MaDT,null,null,null,null,null,@Username)";
                                com.Parameters.AddWithValue("@MaDT", Ma);
                                com.Parameters.AddWithValue("@Username", username_account.Text);
                                com.ExecuteNonQuery();
                            }
                            catch
                            {
                                MessageBox.Show("Lỗi hệ thống!");
                            }
                        }


                        if (ph.RoleName == "KhachHang")
                        {
                            int Ma = 1;
                            try
                            {
                                SqlCommand createMa = db.CreateCommand();
                                createMa.CommandType = CommandType.Text;
                                createMa.CommandText = "SELECT MAX(MaKH) FROM KHACHHANG";
                                Ma = Convert.ToInt32(createMa.ExecuteScalar()) + 1;
                            }
                            catch
                            {
                                Ma = 1;
                            }
                            try
                            {
                                SqlCommand com = db.CreateCommand();
                                com.CommandType = CommandType.Text;
                                com.CommandText = "INSERT INTO KHACHHANG VALUES (@MaKH,null,null,null,null,@Username)";
                                com.Parameters.AddWithValue("@MaKH", Ma);
                                com.Parameters.AddWithValue("@Username", username_account.Text);
                                com.ExecuteNonQuery();
                            }
                            catch
                            {
                                MessageBox.Show("Lỗi hệ thống!");
                            }
                        }

                        if (ph.RoleName == "TaiXe")
                        {
                            int Ma = 1;
                            try
                            {
                                SqlCommand createMa = db.CreateCommand();
                                createMa.CommandType = CommandType.Text;
                                createMa.CommandText = "SELECT MAX(MaTX) FROM TAIXE";
                                Ma = Convert.ToInt32(createMa.ExecuteScalar()) + 1;
                            }
                            catch
                            {
                                Ma = 1;
                            }
                            try
                            {
                                SqlCommand com = db.CreateCommand();
                                com.CommandType = CommandType.Text;
                                com.CommandText = "INSERT INTO TAIXE VALUES (@MaTX,null,null,null,null,null,null,null,null,null,null,@Username)";
                                com.Parameters.AddWithValue("@MaTX", Ma);
                                com.Parameters.AddWithValue("@Username", username_account.Text);
                                com.ExecuteNonQuery();
                            }
                            catch
                            {
                                MessageBox.Show("Lỗi hệ thống!");
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

                    MessageBox.Show("Tạo tài khoản thất bại!");
                    trans.Rollback();
                }




            }
            catch
            {
                MessageBox.Show("Tạo tài khoản thất bại!");

            }
        }

        private void ShowSuaTT(object sender, RoutedEventArgs e)
        {
            ResetQLPHVisibility();
            SuaTTPH.Visibility = Visibility.Visible;
        }

        ND user_Sua = new ND();

        private void FindUser_Sua(object sender, RoutedEventArgs e)
        {
            SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
            try
            {
                db.Open();
                SqlCommand cmd = db.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM USERS WHERE Username = @Username";
                cmd.Parameters.AddWithValue("@Username", username_SuaTT.Text);
                SqlDataReader sdr = cmd.ExecuteReader();
                sdr.Read();
                user_Sua.Username = sdr["Username"].ToString();
                user_Sua.Pass = sdr["Pass"].ToString();
                user_Sua.RoleName = sdr["RoleName"].ToString();
                user_Sua.TrangThai = sdr["TrangThai"].ToString();
                if (user_Sua.Username != "")
                {
                    MessageBox.Show("Đã tìm thấy user!");
                    UpdateBtn.IsEnabled = true;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy!");
                    UpdateBtn.IsEnabled = false;
                }
            }
            catch
            {
                MessageBox.Show("Không tìm thấy!");
            }

        }



        PhanHe ph_Sua = new PhanHe();



        private void ChonPhanHeMoi(object sender, SelectionChangedEventArgs e)
        {

            ph_Sua.RoleName = ((ComboBoxItem)SelectPhanHeMoi.SelectedItem).Content.ToString();

            if (ph_Sua.RoleName == "DoiTac")
            {
                CapNhat_DoiTac.Visibility = Visibility.Visible;


                if (user_Sua.RoleName == "DoiTac")
                {
                    SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
                    try
                    {
                        db.Open();
                        SqlCommand getDT = db.CreateCommand();
                        getDT.CommandType = CommandType.Text;
                        getDT.CommandText = "SELECT MADT FROM DOITAC WHERE Username=@Username";
                        getDT.Parameters.AddWithValue("@Username", user_Sua.Username);
                        ph_Sua.MaDT = Convert.ToInt32(getDT.ExecuteScalar());


                    }
                    catch
                    {
                        MessageBox.Show("Lỗi hệ thống!");
                    }
                }

            }
            if (ph_Sua.RoleName == "NhanVien" || ph_Sua.RoleName == "QuanTri" || ph_Sua.RoleName == "KhachHang" || ph_Sua.RoleName == "TaiXe")
            {
                CapNhat_DoiTac.Visibility = Visibility.Collapsed;
            }
        }



        private void CapNhatTT(object sender, RoutedEventArgs e)
        {
            SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
            try
            {
                db.Open();

                SqlTransaction trans = db.BeginTransaction();
                SqlCommand cmd = db.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trans;
                cmd.CommandText = "CapNhatUser";
                cmd.Parameters.AddWithValue("@Username", username_SuaTT.Text);
                cmd.Parameters.AddWithValue("@Pass", password_Moi.Password);
                cmd.Parameters.AddWithValue("@RoleName", ph_Sua.RoleName);

                int result = Convert.ToInt32(cmd.ExecuteScalar());
                if (result == 0)
                {
                    MessageBox.Show("Cập nhật thông tin tài khoản(Pass,RoleName) thành công!");
                    trans.Commit();

                    if (ph_Sua.RoleName == "DoiTac")
                    {
                        try
                        {
                            SqlTransaction trans2 = db.BeginTransaction();
                            SqlCommand cmd2 = db.CreateCommand();
                            cmd2.CommandType = CommandType.StoredProcedure;
                            cmd2.Transaction = trans2;
                            cmd2.CommandText = "capNhatDoiTac";
                            cmd2.Parameters.AddWithValue("@MaDT", ph_Sua.MaDT);
                            cmd2.Parameters.AddWithValue("@Email", Email_Sua.Text);
                            cmd2.Parameters.AddWithValue("@NgDaiDien", NgDaiDien_Sua.Text);
                            cmd2.Parameters.AddWithValue("@SLChiNhanh", SLChiNhanh_Sua.Text);
                            cmd2.Parameters.AddWithValue("@TenQuan", TenQuan_Sua.Text);
                            cmd2.Parameters.AddWithValue("@LoaiTP", LoaiTP_Sua.Text);
                            int result2 = Convert.ToInt32(cmd2.ExecuteScalar());
                            if (result2 == 0)
                            {
                                MessageBox.Show("Cập nhật thông tin đối tác thành công!");
                                trans2.Commit();
                                CapNhat_DoiTac.Visibility = Visibility.Collapsed;
                            }
                            else if (result2 == 1)
                            {
                                MessageBox.Show("Thông tin trống!");
                                trans2.Rollback();
                            }
                            else if (result2 == 2)
                            {
                                MessageBox.Show("Mã đối tác chưa tồn tại!");
                                trans2.Rollback();
                            }
                            else
                            {
                                MessageBox.Show("Cập nhật thất bại!");
                                trans2.Rollback();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }




                }
                else if (result == 1)
                {
                    MessageBox.Show("Username không tồn tại!");
                    trans.Rollback();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại!");
                    trans.Rollback();
                }
            }
            catch
            {
                MessageBox.Show("Cập nhật thất bại!");
            }
        }

        private void ShowXoaTT(object sender, RoutedEventArgs e)
        {
            ResetQLPHVisibility();
            XoaTTPH.Visibility = Visibility.Visible;
        }
        ND user_Xoa = new ND();
        PhanHe ph_Xoa = new PhanHe();
        private void FindUser_Xoa(object sender, RoutedEventArgs e)
        {
            SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
            try
            {
                db.Open();
                SqlCommand cmd = db.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM USERS WHERE Username = @Username";
                cmd.Parameters.AddWithValue("@Username", username_XoaTT.Text);
                SqlDataReader sdr = cmd.ExecuteReader();
                sdr.Read();
                user_Xoa.Username = sdr["Username"].ToString();
                user_Xoa.Pass = sdr["Pass"].ToString();
                user_Xoa.RoleName = sdr["RoleName"].ToString();
                user_Xoa.TrangThai = sdr["TrangThai"].ToString();
                if (user_Xoa.Username != "")
                {
                    MessageBox.Show("Đã tìm thấy user!");
                    sdr.Close();
                    if (user_Xoa.RoleName == "DoiTac")
                    {
                        Ma.Content = "Mã đối tác";
                        SqlCommand cmd2 = db.CreateCommand();
                        cmd2.CommandType = CommandType.Text;
                        cmd2.CommandText = "SELECT MaDT FROM DOITAC WHERE Username = @Username";
                        cmd2.Parameters.AddWithValue("@Username", user_Xoa.Username);
                        ph_Xoa.MaDT = Convert.ToInt32(cmd2.ExecuteScalar());
                        Ma_Xoa.Text = ph_Xoa.MaDT.ToString();
                        XoaBtn.IsEnabled = true;
                    }
                    if (user_Xoa.RoleName == "NhanVien")
                    {
                        Ma.Content = "Mã nhân viên";
                        SqlCommand cmd2 = db.CreateCommand();
                        cmd2.CommandType = CommandType.Text;
                        cmd2.CommandText = "SELECT MaNV FROM NHANVIEN WHERE Username = @Username";
                        cmd2.Parameters.AddWithValue("@Username", user_Xoa.Username);
                        ph_Xoa.MaNV = Convert.ToInt32(cmd2.ExecuteScalar());
                        Ma_Xoa.Text = ph_Xoa.MaNV.ToString();
                        XoaBtn.IsEnabled = true;
                    }
                    if (user_Xoa.RoleName == "KhachHang")
                    {
                        Ma.Content = "Mã khách hàng";
                        SqlCommand cmd2 = db.CreateCommand();
                        cmd2.CommandType = CommandType.Text;
                        cmd2.CommandText = "SELECT MaKH FROM KHACHHANG WHERE Username = @Username";
                        cmd2.Parameters.AddWithValue("@Username", user_Xoa.Username);
                        ph_Xoa.MaKH = Convert.ToInt32(cmd2.ExecuteScalar());
                        Ma_Xoa.Text = ph_Xoa.MaKH.ToString();
                        XoaBtn.IsEnabled = true;
                    }
                    if (user_Xoa.RoleName == "QuanTri")
                    {
                        Ma.Content = "Mã quản trị";
                        SqlCommand cmd2 = db.CreateCommand();
                        cmd2.CommandType = CommandType.Text;
                        cmd2.CommandText = "SELECT MaQT FROM QUANTRI WHERE Username = @Username";
                        cmd2.Parameters.AddWithValue("@Username", user_Xoa.Username);
                        ph_Xoa.MaQT = Convert.ToInt32(cmd2.ExecuteScalar());
                        Ma_Xoa.Text = ph_Xoa.MaQT.ToString();
                        XoaBtn.IsEnabled = true;
                    }
                    if (user_Xoa.RoleName == "TaiXe")
                    {
                        Ma.Content = "Mã tài xế";
                        SqlCommand cmd2 = db.CreateCommand();
                        cmd2.CommandType = CommandType.Text;
                        cmd2.CommandText = "SELECT MaTX FROM TAIXE WHERE Username = @Username";
                        cmd2.Parameters.AddWithValue("@Username", user_Xoa.Username);
                        ph_Xoa.MaTX = Convert.ToInt32(cmd2.ExecuteScalar());
                        Ma_Xoa.Text = ph_Xoa.MaTX.ToString();
                        XoaBtn.IsEnabled = true;
                    }

                }
                else
                {
                    MessageBox.Show("Không tìm thấy!");
                    XoaBtn.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không tìm thấy!");
                XoaBtn.IsEnabled = false;
            }

        }

        private void XoaTT(object sender, RoutedEventArgs e)
        {
            SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
            try
            {
                db.Open();

                if (user_Xoa.RoleName == "DoiTac")
                {
                    SqlTransaction trans = db.BeginTransaction();
                    SqlCommand cmd = db.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Transaction = trans;
                    cmd.CommandText = "xoaDoiTac";
                    cmd.Parameters.AddWithValue("@MaDT", ph_Xoa.MaDT);
                    int result = Convert.ToInt32(cmd.ExecuteScalar());
                    if (result == 0)
                    {

                        MessageBox.Show("Xóa thông tin đối tác thành công!");
                        trans.Commit();
                        SqlCommand cmd2 = db.CreateCommand();
                        cmd2.CommandType = CommandType.Text;
                        cmd2.CommandText = "DELETE FROM USERS WHERE Username = @Username";
                        cmd2.Parameters.AddWithValue("@Username", user_Xoa.Username);
                        cmd2.ExecuteScalar();
                    }
                    else if (result == 1)
                    {
                        MessageBox.Show("Thông tin trống!");
                        trans.Rollback();
                    }
                    else if (result == 2)
                    {
                        MessageBox.Show("Không thể xóa, đối tác không tồn tại!");
                        trans.Rollback();
                    }
                }
                if (user_Xoa.RoleName == "NhanVien")
                {
                    SqlCommand cmd = db.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "DELETE FROM NHANVIEN WHERE MaNV = @MaNV";
                    cmd.Parameters.AddWithValue("@MaNV", ph_Xoa.MaNV);
                    cmd.ExecuteNonQuery();
                    SqlCommand cmd2 = db.CreateCommand();
                    cmd2.CommandType = CommandType.Text;
                    cmd2.CommandText = "DELETE FROM USERS WHERE Username = @Username";
                    cmd2.Parameters.AddWithValue("@Username", user_Xoa.Username);
                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("Xóa thông tin nhân viên thành công!");
                }
                if (user_Xoa.RoleName == "KhachHang")
                {
                    SqlCommand cmd = db.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "DELETE FROM KHACHHANG WHERE MaKH = @MaKH";
                    cmd.Parameters.AddWithValue("@MaKH", ph_Xoa.MaKH);
                    cmd.ExecuteNonQuery();
                    SqlCommand cmd2 = db.CreateCommand();
                    cmd2.CommandType = CommandType.Text;
                    cmd2.CommandText = "DELETE FROM USERS WHERE Username = @Username";
                    cmd2.Parameters.AddWithValue("@Username", user_Xoa.Username);
                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("Xóa thông tin khách hàng thành công!");
                }
                if (user_Xoa.RoleName == "QuanTri")
                {
                    SqlCommand cmd = db.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "DELETE FROM QUANTRI WHERE MaQT = @MaQT";
                    cmd.Parameters.AddWithValue("@MaQT", ph_Xoa.MaQT);
                    cmd.ExecuteNonQuery();
                    SqlCommand cmd2 = db.CreateCommand();
                    cmd2.CommandType = CommandType.Text;
                    cmd2.CommandText = "DELETE FROM USERS WHERE Username = @Username";
                    cmd2.Parameters.AddWithValue("@Username", user_Xoa.Username);
                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("Xóa thông tin quản trị thành công!");
                }
                if (user_Xoa.RoleName == "TaiXe")
                {
                    SqlCommand cmd = db.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "DELETE FROM TAIXE WHERE MaTX = @MaTX";
                    cmd.Parameters.AddWithValue("@MaTX", ph_Xoa.MaTX);
                    cmd.ExecuteNonQuery();
                    SqlCommand cmd2 = db.CreateCommand();
                    cmd2.CommandType = CommandType.Text;
                    cmd2.CommandText = "DELETE FROM USERS WHERE Username = @Username";
                    cmd2.Parameters.AddWithValue("@Username", user_Xoa.Username);
                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("Xóa thông tin tài xế thành công!");
                }




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Xóa thất bại!");
            }
        }

        private void ShowXemTT(object sender, RoutedEventArgs e)
        {
            ResetQLPHVisibility();
            XemTTPH.Visibility = Visibility.Visible;
        }

        private void ResetXemVisibility()
        {
            dgDT.Visibility = Visibility.Collapsed;
            dgKH.Visibility = Visibility.Collapsed;
            dgNV.Visibility = Visibility.Collapsed;
            dgQT.Visibility = Visibility.Collapsed;
            dgTX.Visibility = Visibility.Collapsed;
        }

        private void XemDT(object sender, RoutedEventArgs e)
        {
            SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
            try
            {
                ResetXemVisibility();
                db.Open();

                SqlCommand cmd = db.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM DOITAC WHERE MaDT = @MaDT";
                cmd.Parameters.AddWithValue("@MaDT", MaDT_XemTT.Text);
                SqlDataAdapter GetTT = new SqlDataAdapter(cmd);
                DataTable TT = new DataTable();
                GetTT.Fill(TT);
                dgDT.ItemsSource = TT.DefaultView;
                dgDT.Visibility = Visibility.Visible;
                if (TT.Rows.Count.ToString() == "0")
                {
                    ResetXemVisibility();
                    MessageBox.Show("Không tìm thấy!");
                }
                else
                {
                    dgDT.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void XemKH(object sender, RoutedEventArgs e)
        {
            SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
            try
            {
                ResetXemVisibility();
                db.Open();

                SqlCommand cmd = db.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "timKiemKhachHang";
                cmd.Parameters.AddWithValue("@MaKH", MaKH_XemTT.Text);

                SqlDataAdapter GetTT = new SqlDataAdapter(cmd);
                DataTable TT = new DataTable();
                GetTT.Fill(TT);
                dgKH.ItemsSource = TT.DefaultView;
                if (TT.Columns.Count.ToString() == "0" || TT.Columns.Count.ToString() == "1")
                {

                    MessageBox.Show("Không tìm thấy!");
                }
                else
                {
                    dgKH.Visibility = Visibility.Visible;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void XemNV(object sender, RoutedEventArgs e)
        {
            SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
            try
            {
                ResetXemVisibility();
                db.Open();

                SqlCommand cmd = db.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM NHANVIEN WHERE MaNV = @MaNV";
                cmd.Parameters.AddWithValue("@MaNV", MaNV_XemTT.Text);
                SqlDataAdapter GetTT = new SqlDataAdapter(cmd);
                DataTable TT = new DataTable();
                GetTT.Fill(TT);
                dgNV.ItemsSource = TT.DefaultView;
                dgNV.Visibility = Visibility.Visible;
                if (TT.Rows.Count.ToString() == "0")
                {
                    ResetXemVisibility();
                    MessageBox.Show("Không tìm thấy!");
                }
                else
                {
                    dgNV.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void XemQT(object sender, RoutedEventArgs e)
        {
            SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
            try
            {
                ResetXemVisibility();
                db.Open();

                SqlCommand cmd = db.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM QUANTRI WHERE MaQT = @MaQT";
                cmd.Parameters.AddWithValue("@MaQT", MaQT_XemTT.Text);
                SqlDataAdapter GetTT = new SqlDataAdapter(cmd);
                DataTable TT = new DataTable();
                GetTT.Fill(TT);
                dgQT.ItemsSource = TT.DefaultView;
                dgQT.Visibility = Visibility.Visible;
                if (TT.Rows.Count.ToString() == "0")
                {
                    ResetXemVisibility();
                    MessageBox.Show("Không tìm thấy!");
                }
                else
                {
                    dgQT.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void XemTX(object sender, RoutedEventArgs e)
        {
            SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
            try
            {
                ResetXemVisibility();
                db.Open();

                SqlCommand cmd = db.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM TAIXE WHERE MaTX = @MaTX";
                cmd.Parameters.AddWithValue("@MaTX", MaNV_XemTT.Text);
                SqlDataAdapter GetTT = new SqlDataAdapter(cmd);
                DataTable TT = new DataTable();
                GetTT.Fill(TT);
                dgTX.ItemsSource = TT.DefaultView;
                dgTX.Visibility = Visibility.Visible;
                if (TT.Rows.Count.ToString() == "0")
                {
                    ResetXemVisibility();
                    MessageBox.Show("Không tìm thấy!");
                }
                else
                {
                    dgTX.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}