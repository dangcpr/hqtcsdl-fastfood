
using System.Data;
using System.Data.SqlClient;
using System.Windows;

<<<<<<< Updated upstream
namespace VangVaTien_DoiTac.DBClass
{
    class DBConnect
    {
        static string chuoiKetNoi = "Data Source=.;Initial Catalog = GIAONHANHANG1; Integrated Security = True";
=======
namespace _08.DBClass
{
    class DBConnect
    {
        static string chuoiKetNoi = "Data Source=.;Initial Catalog = GIAONHANHANG; Integrated Security = True";
>>>>>>> Stashed changes
        public SqlConnection traCon = new SqlConnection(chuoiKetNoi);
        public DataTable sql_select(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection con = new SqlConnection(chuoiKetNoi);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();//Mở kết nối
                }
                SqlCommand com = new SqlCommand(query, con);
                SqlDataReader dataRead = com.ExecuteReader();
                dt.Load(dataRead);//Đưa dữ liệu vào bảng
                con.Close();//Đóng kết nỗi
                return dt;
            }
            catch
            {
                MessageBox.Show("Lỗi hệ thống!", "Thông báo");
                return dt;
            }
           
            
        }
        public int sql_insert_update_delete(string query)
        {
            SqlConnection con = new SqlConnection(chuoiKetNoi);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();//Mở kết nối
            }
            try
            {
                SqlCommand com = new SqlCommand(query, con);
                int sql_status = com.ExecuteNonQuery();
                return sql_status;
            }
            catch
            {
                MessageBox.Show("Lỗi hệ thống giao tác!", "Thông báo");
            }
            con.Close();//Đóng kết nỗi
            return -1;

        }
        public string layMotGiaTri(string query) 
        {
            try
            {
                SqlConnection con = new SqlConnection(chuoiKetNoi);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();//Mở kết nối
                }
                SqlCommand com = new SqlCommand();
                com.CommandType = CommandType.Text;
                com.CommandText = query;
                com.Connection = con;
                string giatri = (string)com.ExecuteScalar();
                con.Close();
                return giatri;
            }
            catch
            {
                MessageBox.Show("Lỗi hệ thống!", "Thông báo");
                return "";
            }
        }
    }
}
