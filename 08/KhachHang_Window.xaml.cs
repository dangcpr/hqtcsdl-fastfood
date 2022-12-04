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
    public partial class KhachHang_Window : Window
    {
        string Username = "";
        public KhachHang_Window(string Username)
        {
            InitializeComponent();
            this.Username = Username;
        }
        private void ResetVisibility()
        {
            Infomation.Visibility = Visibility.Collapsed;
            DoiTac.Visibility = Visibility.Collapsed;
            MonAn.Visibility = Visibility.Collapsed;
        }
        private void GetInfo(object sender, RoutedEventArgs e)
        {
            ResetVisibility();
            Infomation.Visibility = Visibility.Visible;
        }
        private void SelectDoiTac(object sender, RoutedEventArgs e)
        {
            ResetVisibility();
            DoiTac.Visibility = Visibility.Visible;
            string CmdString = string.Empty;
            SqlConnection db = new SqlConnection("Server=.;Database=GIAONHANHANG;integrated security = true");
            try
            {
                CmdString = "SELECT * FROM DOITAC";
                db.Open();
                SqlCommand cmd = new SqlCommand(CmdString, db);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable("DOITAC");
                sda.Fill(dt);

                BangDoiTac.ItemsSource = dt.DefaultView;
            }
            catch
            {
                MessageBox.Show("Lỗi hệ thống!");
            }
        }
        private void GetMonAn(object sender, RoutedEventArgs e)
        {
            ResetVisibility();
            MonAn.Visibility = Visibility.Visible;
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
                MaDT.Items.Add(dr["MaDT"].ToString() + " - " + dr["TenQuan"].ToString());
            }
            db.Close();
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

        }
    }
}
