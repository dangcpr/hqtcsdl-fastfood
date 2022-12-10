
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using _08.DBClass;


namespace _08
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class DoiTacWindow : Window
    {

        public string MaDoiTac;
        DBConnect db = new DBConnect();
        

        public DoiTacWindow(string username)
        {
            InitializeComponent();
            MaDoiTac = db.layMotGiaTri("select MaDT from DOITAC where Username = '"+username+"'");
            if (MaDoiTac == null)
            {
                MessageBox.Show("Username không tồn tại tài khoản Đối Tác");
                this.Close();
            }    
        }
        DoiTac doitac = new DoiTac();

        private void Btn_dangxuat_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        ///================================================================================================================
        ///================================================================================================================
        ///================================================ BẢNG THÔNG TIN ================================================
        ///================================================================================================================
        ///================================================================================================================
        private void Tt_load_Datagrid()
        {
            try
            {
                doitac.MaDT = "1";
                DataTable dt = doitac.LayThongTinDoiTac();
                DataRow r = dt.Rows[0];
                TT_tb_email.Text = r["Email"].ToString();
                TT_tb_nguoidaidien.Text = r["NgDaiDien"].ToString();
                TT_tb_slchinhanh.Text = r["SLChiNhanh"].ToString();
                TT_tb_tenquan.Text = r["TenQuan"].ToString();
                TT_tb_loaithucpham.Text = r["LoaiTP"].ToString();
                //Tt_lb_thongtin_errorout.Content = "Thông tin user : " + doitac.username;
                //Tt_lb_thongtin_errorout.Background = Brushes.LightGreen;
                Tt_lb_thongtin_errorout.Content = "";
                Tt_lb_thongtin_errorout.Background = Brushes.Transparent;
            }
            catch
            {
                Tt_lb_thongtin_errorout.Content = "Không tìm được thông tin. Lỗi!!!";
                Tt_lb_thongtin_errorout.Background = Brushes.IndianRed;
            }
        }
        private void Tt_capnhatthongtin_click(object sender, RoutedEventArgs e)
        {
            //DialogResult t= MessageBox.Show("Bạn có muốn cập nhật thông tin?","Thong bao", MessageBoxButton.YesNo, );
            int sl = Int32.Parse(TT_tb_slchinhanh.Text);
            doitac.capNhatThongTin(TT_tb_email.Text, TT_tb_nguoidaidien.Text, sl, TT_tb_slchinhanh.Text, TT_tb_loaithucpham.Text);
            //doitac.capNhatThongTin();
            //MessageBox.Show("Bạn đã cập nhật thông tin thành công","Thông báo");
        }
        private void Tt_ThongTin_loaded(object sender, RoutedEventArgs e)
        {
            Tt_load_Datagrid();
        }
        private void Tt_capnhatthongtincanhan_click(object sender, RoutedEventArgs e)
        {
            //DialogResult t= MessageBox.Show("Bạn có muốn cập nhật thông tin?","Thong bao", MessageBoxButton.YesNo, );
            int sl = Int32.Parse(TT_tb_slchinhanh.Text);
            doitac.capNhatThongTin(TT_tb_email.Text, TT_tb_nguoidaidien.Text, sl, TT_tb_tenquan.Text, TT_tb_loaithucpham.Text);
            //doitac.capNhatThongTin();
            Tt_lb_thongtin_errorout.Content = "Cập nhật thông tin thành công";
            Tt_lb_thongtin_errorout.Background = Brushes.LightGreen;
        }
        private void Tt_capnhatmatkhau_click(object sender, RoutedEventArgs e)
        {
            if (TT_tb_matkhaucu.Text != doitac.layMatKhau())
            {
                Tt_lb_doimatkhau_errorout.Content = "Mật khẩu cũ không đúng!";
                Tt_lb_doimatkhau_errorout.Background = Brushes.IndianRed;
                //MessageBox.Show("Mật khẩu không đúng!", "Thông báo");
            }
            else
            {
                if (TT_tb_matkhaumoi.Password != TT_tb_matkhaumoi_2.Password)
                {
                    Tt_lb_doimatkhau_errorout.Content = "Mật khẩu nhập lại không đúng!";
                    Tt_lb_doimatkhau_errorout.Background = Brushes.IndianRed;
                    //MessageBox.Show("Mật khẩu nhập lại không đúng!", "Thông báo");
                }
                else
                {

                    doitac.doiMatKhau(TT_tb_matkhaumoi.Password);
                    Tt_lb_doimatkhau_errorout.Content = "Đổi mật khẩu thành công";
                    Tt_lb_doimatkhau_errorout.Background = Brushes.LightGreen;
                    //MessageBox.Show("Cập nhật thành công!", "Thông báo");
                }
            }

        }
        private void tt_bt_doimatkhau_loaded(object sender, RoutedEventArgs e)
        {
            TT_tb_taikhoan.Text = doitac.username;

        }
        
        ///================================================================================================================
        ///================================================================================================================
        ///================================================ BẢNG CHI NHANH ================================================
        ///================================================================================================================
        ///================================================================================================================

        //Hàm load datagrid của tab Chi nhánh
        private void HienMaLoi_Label(int kq, string cauLenh, string tenTab)
        {
            if (kq == 0)
            {
                Cn_lb_errorout.Content = "Đã " + cauLenh + " " + tenTab.ToLower() + " thành công";
                Cn_load_Datagrid();
                Cn_lb_errorout.Background = Brushes.LightGreen;
            }
            else
            {
                switch (kq)
                {
                    case 1:
                        Cn_lb_errorout.Content = "Có trường trống";
                        break;
                    case 2:
                        Cn_lb_errorout.Content = "Mã đối tác không tồn tại";
                        break;
                    case 3:
                        Cn_lb_errorout.Content = "Số điện thoại trùng lắp";
                        break;
                    case 4:
                        Cn_lb_errorout.Content = "Tình trạng không hợp lệ";
                        break;
                    case 5:
                        Cn_lb_errorout.Content = "Mã chi nhánh không tồn tại";
                        break;
                    case 10:
                        Cn_lb_errorout.Content = "Lỗi Server SQL";
                        break;
                    case 11:
                        Cn_lb_errorout.Content = "Chọn dòng cần " + cauLenh;
                        break;

                    default:
                        break;
                }
                Cn_lb_errorout.Background = Brushes.IndianRed;
            }
        }
        private void Cn_load_Datagrid()
        {
            try
            {
                DataTable dt = db.sql_select("select * from CHINHANH where MaDT ='" + MaDoiTac + "'");
                cn_datagrid.ItemsSource = dt.DefaultView;
            }
            catch
            {   }
        }
        private void Cn_btn_them_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int maLoi = doitac.QueryChiNhanh("Them", "", MaDoiTac.ToString(),
                    Cn_tb_thanhpho.Text.ToString(), Cn_tb_quan.Text.ToString(), Cn_tb_diachi.Text.ToString(),
                    Cn_tb_sdt.Text.ToString(), Cn_tb_tinhtrang.Text.ToString(), Cn_tb_ngaylap.Text.ToString());
                HienMaLoi_Label(maLoi,"thêm", "chi nhánh");
                if (maLoi == 0)
                {
                    Cn_tb_sdt.Text = "";
                    Cn_tb_tinhtrang.Text = "";
                    Cn_tb_ngaylap.Text = "";
                    Cn_tb_thanhpho.Text = "";
                    Cn_tb_quan.Text = "";
                    Cn_tb_diachi.Text = "";
                    Tt_load_Datagrid();
                }
            }
            catch
            {   }
        }
        private void Cn_btn_xoa_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView rowview = (DataRowView)cn_datagrid.SelectedItem;
                if (rowview != null)
                {
                    int maLoi = doitac.QueryChiNhanh("Xoa", rowview["STT"].ToString(), MaDoiTac.ToString(),
                            "", "", "",
                            "", "", "");
                    HienMaLoi_Label(maLoi, "xoá", "chi nhánh");
                    Tt_load_Datagrid();
                }
                else
                {
                    HienMaLoi_Label(11, "xoá", "chi nhánh");
                }
            }
            catch
            {
            }
        }
        private void Cn_btn_sua_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView rowview = (DataRowView)cn_datagrid.SelectedItem;
                if (rowview != null)
                {
                    int maLoi = doitac.QueryChiNhanh("Sua",  rowview["STT"].ToString(), MaDoiTac.ToString(),
                        Cn_tb_thanhpho.Text.ToString(), Cn_tb_quan.Text.ToString(), Cn_tb_diachi.Text.ToString(),
                        Cn_tb_sdt.Text.ToString(), Cn_tb_tinhtrang.Text.ToString(), Cn_tb_ngaylap.Text.ToString());
                    HienMaLoi_Label(maLoi, "sửa", "chi nhánh");
                }
                else
                {
                    HienMaLoi_Label(11, "sửa", "chi nhánh");
                } 
                    
            }
            catch
            {
            }
        }


        //Khi datagrid Chi Nhánh load lần đầu
        private void Cn_datagrid_Loaded(object sender, RoutedEventArgs e)
        {
            Cn_load_Datagrid();
        }
        //Khi nhấn vào 1 dòng của bảng ChiNhanh
        private void Cn_datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cn_datagrid.SelectedIndex.ToString() != null)
                {
                    DataRowView rowview = (DataRowView)cn_datagrid.SelectedItem;
                    if (rowview != null)
                    {
                        Cn_tb_sdt.Text = rowview["SDT"].ToString();
                        Cn_tb_tinhtrang.Text = rowview["TinhTrang"].ToString();
                        Cn_tb_ngaylap.Text = rowview["NgayLap"].ToString();
                        Cn_tb_thanhpho.Text = rowview["TP"].ToString();
                        Cn_tb_quan.Text = rowview["Quan"].ToString();
                        Cn_tb_diachi.Text = rowview["DiaChiCuThe"].ToString();
                    }
                }
            }
            catch
            {

            }
        }

        ///================================================================================================================
        ///================================================================================================================
        ///================================================ BẢNG THỰC PHẨM ================================================
        ///================================================================================================================
        ///================================================================================================================
        private void Tp_HienMaLoi_Label(int kq, string cauLenh)
        {
            if (kq == 0)
            {
                Tp_lb_errorout.Content = "Đã " + cauLenh + " thực phẩm thành công";
                Tp_load_Datagrid();
                Tp_lb_errorout.Background = Brushes.LightGreen;
            }
            else
            {
                switch (kq)
                {
                    case 1:
                        Tp_lb_errorout.Content = "Có trường trống";
                        break;
                    case 2:
                        Tp_lb_errorout.Content = "Mã đối tác không tồn tại";
                        break;
                    case 3:
                        Tp_lb_errorout.Content = "Tên thực phẩm này đã tồn tại";
                        break;
                    case 4:
                        Tp_lb_errorout.Content = "Tình trạng không hợp lệ";
                        break;
                    case 5:
                        Tp_lb_errorout.Content = "Không thể xoá, món ăn đã từng được lên đơn";
                        break;
                    case 6:
                        Tp_lb_errorout.Content = "Không thể xoá, món ăn không tồn tại";
                        break;
                    case 7:
                        Tp_lb_errorout.Content = "Giá không hợp lệ";
                        break;
                    case 10:
                        Tp_lb_errorout.Content = "Lỗi Server SQL";
                        break;
                    case 11:
                        Tp_lb_errorout.Content = "Chọn dòng cần " + cauLenh;
                        break;

                    default:
                        break;
                }
                Tp_lb_errorout.Background = Brushes.IndianRed;
            }
        }
        private void Tp_load_Datagrid()
        {
            DataTable dt = db.sql_select("select * from THUCPHAM where MaDT ='" + MaDoiTac + "'");
            Tp_datagrid.ItemsSource = dt.DefaultView;
        }

        private void Tp_btn_them_Click_new(object sender, RoutedEventArgs e)
        {
            try
            {
                int maLoi = doitac.QueryThucPham("Them","",  MaDoiTac.ToString(),
                    Tp_tb_tenmon.Text.ToString(),Tp_tb_mieuta.Text.ToString(), Tp_tb_gia.Text.ToString(),Tp_tb_tinhtrang.Text.ToString(),
                    Tp_tb_tuychon.Text.ToString());
                Tp_HienMaLoi_Label(maLoi, "thêm");
                if (maLoi == 0)
                {
                    Tp_tb_matp.Text = "";
                    Tp_tb_mieuta.Text = "";
                    Tp_tb_tenmon.Text = "";
                    Tp_tb_tinhtrang.Text = "";
                    Tp_tb_gia.Text = "";
                    Tp_tb_tuychon.Text = "";
                }
            }
            catch
            { }
        }
        private void Tp_btn_sua_Click_new(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView rowview = (DataRowView)Tp_datagrid.SelectedItem;
                if (rowview != null)
                {
                    int maLoi = doitac.QueryThucPham("Sua", Tp_tb_matp.Text, MaDoiTac.ToString(),
                        Tp_tb_tenmon.Text.ToString(), Tp_tb_mieuta.Text.ToString(), Tp_tb_gia.Text.ToString(), Tp_tb_tinhtrang.Text.ToString(),
                        Tp_tb_tuychon.Text.ToString());
                    Tp_HienMaLoi_Label(maLoi, "sửa");
                }    
                else
                {
                    Tp_HienMaLoi_Label(11, "sửa");
                }    
            }
            catch
            {
            }
        }
        private void Tp_btn_xoa_Click_new(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView rowview = (DataRowView)Tp_datagrid.SelectedItem;

                if (rowview != null)
                {
                    int maLoi = doitac.QueryThucPham("Xoa", rowview["MaTP"].ToString(), MaDoiTac.ToString(),
                            "", "", "",
                            "", "");
                    Tp_HienMaLoi_Label(maLoi, "xoá");
                }
                else
                {
                    Tp_HienMaLoi_Label(11, "xoá");
                }
            }
            catch
            {
            }
        }

        private void Tp_datagrid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable dt = db.sql_select("select * from THUCPHAM where MaDT ='" + MaDoiTac + "'");
                Tp_datagrid.ItemsSource = dt.DefaultView;
            }
            catch
            {

            }
        }
        private void TP_datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (Tp_datagrid.SelectedIndex.ToString() != null)
                {
                    DataRowView rowview = (DataRowView)Tp_datagrid.SelectedItem;
                    if (rowview != null)
                    {
                        Tp_tb_matp.Text = rowview["MaTP"].ToString();
                        Tp_tb_tenmon.Text = rowview["TenMon"].ToString();
                        Tp_tb_mieuta.Text = rowview["MieuTa"].ToString();
                        Tp_tb_gia.Text = rowview["Gia"].ToString();
                        Tp_tb_tinhtrang.Text = rowview["TinhTrang"].ToString();
                        Tp_tb_tuychon.Text = rowview["TuyChon"].ToString();
                    }
                }
            }
            catch
            {
            }
        }
        

        ///================================================================================================================
        ///================================================================================================================
        ///================================================ BẢNG ĐƠN HÀNG =================================================
        ///================================================================================================================
        ///================================================================================================================

        private void Dh_load_Datagrid()
        {
            try
            {
                DataTable dt = db.sql_select("select *, (select k.HoTen from KHACHHANG k where k.MaKH = dh_cho.MaKH) TenKH,(select t.HoTen from TAIXE t where t.MaTX = dh_cho.MaTX) TenTX from(select * from DONDATHANG where TinhTrang = N'Chờ') dh_cho  where MaDH in (select distinct MaDH from CHITIETDONDATHANG where MaDT = '" + MaDoiTac + "' )");
                Dh_datagrid.ItemsSource = dt.DefaultView;
            }
            catch
            {

            }
        }
        private void Dh_load_datagrid_Chitietdon(string MaDonHang)
        {
            try
            {
                DataTable dt = db.sql_select("select* ,(select tp.TenMon from THUCPHAM tp where tp.MaTP = ct.MaTP and tp.MaDT= '" + MaDoiTac + "') TenMon,(select tp.Gia from THUCPHAM tp where tp.MaTP = ct.MaTP and tp.MaDT= '" + MaDoiTac + "') Gia from CHITIETDONDATHANG ct where ct.MaDH = '" + MaDonHang + "'");
                Dh_datagrid_Chitietdon.ItemsSource = dt.DefaultView;
            }
            catch
            {

            }
        }

        private void Dh_btn_nhandon_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView rowview = (DataRowView)Dh_datagrid.SelectedItem;
                if (rowview != null)
                {
                    string query = " begin try " +
                    "update DONDATHANG set TinhTrang = N'Chờ giao' where MaDH='" + Dh_tb_madon.Text + "' " +
                    "select 0 end try " +
                    "begin catch select 1 end catch";
                    int kq = (int)db.sql_select(query).Rows[0][0];
                    if (kq ==0)
                    {
                        Dh_lb_errorout.Content = "Nhận đơn thành công";
                        Dh_lb_errorout.Background = Brushes.LightGreen;
                        ///Refresh bảng
                        Dh_load_Datagrid();

                        ///Refresh textbox
                        Dh_tb_madon.Text = "";
                        Dh_tb_mataixe.Text = "";
                        Dh_tb_khachhang.Text = "";
                        return;
                    }
                    else
                    {
                        Dh_lb_errorout.Content = "Lỗi khi kết nốt SQL";
                        Dh_lb_errorout.Background = Brushes.IndianRed; ;
                    }
                }
                else
                {
                    Dh_lb_errorout.Content = "Chọn đơn hàng để nhận";
                    Dh_lb_errorout.Background = Brushes.IndianRed; ;
                }

            }
            catch
            { }
        }
        private void Dh_btn_huydon_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView rowview = (DataRowView)Dh_datagrid.SelectedItem;
                if (rowview != null)
                {
                    string query = " begin try " +
                    "update DONDATHANG set TinhTrang = N'Huỷ' where MaDH='" + Dh_tb_madon.Text + "' " +
                    "select 0 end try " +
                    "begin catch select 1 end catch";
                    int kq = (int)db.sql_select(query).Rows[0][0];
                    if (kq == 0)
                    {
                        Dh_lb_errorout.Content = "Huỷ đơn thành công";
                        Dh_lb_errorout.Background = Brushes.LightGreen;
                        ///Refresh bảng
                        Dh_load_Datagrid();

                        ///Refresh textbox
                        Dh_tb_madon.Text = "";
                        Dh_tb_mataixe.Text = "";
                        Dh_tb_khachhang.Text = "";
                    }
                }
                else
                {
                    Dh_lb_errorout.Content = "Chọn đơn hàng để huỷ";
                    Dh_lb_errorout.Background = Brushes.IndianRed; ;
                }
            }
            catch
            { }
        }
        private void Dh_datagrid_Loaded(object sender, RoutedEventArgs e)
        {
            Dh_load_Datagrid();
        }
        
        private void Dh_datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (Dh_datagrid.SelectedIndex.ToString() != null)
                {
                    DataRowView rowview = (DataRowView)Dh_datagrid.SelectedItem;
                    if (rowview != null)
                    {
                        Dh_tb_madon.Text = rowview["MaDH"].ToString();
                        Dh_tb_mataixe.Text = rowview["MaTX"].ToString();
                        Dh_tb_khachhang.Text = rowview["MaKH"].ToString();
                        Dh_load_datagrid_Chitietdon(rowview["MaDH"].ToString());
                    }
                }
            }
            catch
            {

            }
        }


        ///================================================================================================================
        ///================================================================================================================
        ///================================================ BẢNG HỢP ĐỒNG =================================================
        ///================================================================================================================
        ///================================================================================================================
        private void Hd_HienMaLoi_Label(int kq, string cauLenh)
        {
            if (kq < 0)
            {
                Hd_lb_errorout.Content = "Đã " + cauLenh + " hợp đồng thành công";
                Hd_load_datagrid();
                Hd_lb_errorout.Background = Brushes.LightGreen;
            }
            else
            {
                switch (kq)
                {
                    case 1:
                        Hd_lb_errorout.Content = "Có trường trống";
                        break;
                    case 2:
                        Hd_lb_errorout.Content = "Mã đối tác không tồn tại";
                        break;
                    case 3:
                        Hd_lb_errorout.Content = "Ngày hết hạn phải sau ngày ký";
                        break;

                    default:
                        break;
                }
                Hd_lb_errorout.Background = Brushes.IndianRed;
            }
        }
        private void Hd_load_datagrid()
        {
            try
            {
                DataTable dt = db.sql_select("select *,(select NgDaiDien from DOITAC where MaDT = '"+ MaDoiTac+"')NguoiDaiDien  from HopDong where MaDT ='" + MaDoiTac + "'");
                Hd_datagrid.ItemsSource = dt.DefaultView;
            }
            catch
            {}
        }
        //Hàm sẽ load danh sách chi nhánh null vào datagrid_chinhanh
        private void Hd_load_datagrid_chinhanh_null()
        {
            try
            {
                DataTable dt = db.sql_select("exec DsChiNhanhNull '" + MaDoiTac + "'");
                Hd_datagrid_chinhanh.ItemsSource = dt.DefaultView;
            }
            catch
            { }

        }
        //Hàm sẽ load danh sách chi nhánh của DÒNG ĐANG ĐƯỢC CHỌN vào datagrid_chinhanh
        private void Hd_load_datagrid_chinhanh(string MaHopDong)
        {
            try
            {
                DataTable dt = db.sql_select("exec DsChiNhanh '" + MaHopDong + "'");
                Hd_datagrid_chinhanh.ItemsSource = dt.DefaultView;
            }
            catch
            { }

        }

        // Khi nhấn nút thêm hợp đồng
        private void Hd_btn_them_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int maLoi = doitac.QueryHopDong(Hd_LayChiNhanhDaChon().Count.ToString(), Hd_tb_sotaikhoan.Text, Hd_tb_nganhang.Text,
                    Hd_tb_chinhanhnganhang.Text, Hd_tb_masothue.Text, Hd_tb_ngayki.Text,
                    Hd_tb_thoihan.Text, Hd_tb_ngayhethan.Text, MaDoiTac);


                Hd_HienMaLoi_Label(maLoi, "thêm");

                if (maLoi < 0)
                {
                    var DsChiNhanh = Hd_LayChiNhanhDaChon();
                    foreach (string stt in DsChiNhanh)
                    {
                        Hd_capNhatTinhTrangChiNhanh(stt, (maLoi * -1).ToString());
                    }
                    Hd_load_datagrid();
                    Hd_load_datagrid_chinhanh_null();

                }

            }
            catch
            { }

        }
        //Khi nhấn nút reload trong tab Hợp đồng
        private void Hd_btn_reload_Click(object sender, RoutedEventArgs e)
        {
            Hd_datagrid_chinhanh.IsReadOnly = false;
            Hd_load_datagrid_chinhanh_null();
        }

        /// Khi chọn 1 dòng trong bảng Hd_chinhanh thì tự update số lượng chi nhánh đã chọn        
        private void Hd_datagrid_chinhanh_GotFocus(object sender, RoutedEventArgs e)
        {
            List<string> x = Hd_LayChiNhanhDaChon();
            Hd_tb_slchinhanh.Text = x.Count.ToString();
        }
        private void Hd_datagrid_chinhanh_Loaded(object sender, RoutedEventArgs e)
        {
            Hd_load_datagrid_chinhanh_null();
        }
        // tự load datagrid chính khi mở tab Hợp Đồng
        private void Hd_datagrid_Loaded(object sender, RoutedEventArgs e)
        {
            Hd_load_datagrid();
        }
        
        private void Hd_datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (Hd_datagrid.SelectedIndex.ToString() != null)
                {
                    DataRowView rowview = (DataRowView)Hd_datagrid.SelectedItem;
                    if (rowview != null)
                    {
                        Hd_datagrid_chinhanh.IsReadOnly = true;

                        Hd_load_datagrid_chinhanh(rowview["MaHD"].ToString());
                        Hd_tb_slchinhanh.Text = rowview["SLChiNhanh"].ToString();
                        Hd_tb_sotaikhoan.Text = rowview["SoTaiKhoan"].ToString();
                        Hd_tb_nganhang.Text = rowview["NganHang"].ToString();
                        Hd_tb_chinhanhnganhang.Text = rowview["CNNganHang"].ToString();
                        Hd_tb_masothue.Text = rowview["MaSoThue"].ToString();
                        Hd_tb_tinhtrang.Text = rowview["TrangThai"].ToString();
                        Hd_tb_ngayki.Text = rowview["NgayKy"].ToString();
                        Hd_tb_thoihan.Text = rowview["ThoiHan"].ToString();
                        Hd_tb_ngayhethan.Text = rowview["NgayHetHan"].ToString();
                        Hd_tb_nguoidaidien.Text = rowview["NguoiDaiDien"].ToString();
                    }
                }
            }
            catch
            {}
        }

        /// Lấy STT của các chi nhánh đã chọn trong datagrid Tab Hợp Đồng 
        List<string> Hd_LayChiNhanhDaChon()
        {
            /// Lấy list index dòng đã chọn
            var SelectedList = new List<string>();
            for (int i = 0; i < Hd_datagrid_chinhanh.Items.Count; i++)
            {
                var item = Hd_datagrid_chinhanh.Items[i];
                var mycheckbox = Hd_datagrid_chinhanh.Columns[2].GetCellContent(item) as CheckBox;
                if ((bool)mycheckbox.IsChecked)
                {
                    TextBlock x = Hd_datagrid_chinhanh.Columns[0].GetCellContent(Hd_datagrid_chinhanh.Items[i]) as TextBlock;
                    SelectedList.Add(x.Text);
                }
            }
            return SelectedList;
        }
        private void Hd_capNhatTinhTrangChiNhanh(string sttChiNhanh,string MaHopDong)
        {
            try
            {
                DataTable dt = db.sql_select("update CHINHANH set MaHopDong ='"+MaHopDong+"' where MaDT ='" + MaDoiTac + "' and STT = '" + sttChiNhanh + "'");
            }
            catch
            { }
        }


        ///================================================================================================================
        ///================================================================================================================
        ///================================================ BẢNG SỐ LIỆU ==================================================
        ///================================================================================================================
        ///================================================================================================================
        private int SL_HienMaLoi_Label(string kq, string kieu)
        {
            switch (kq)
            {
                case "-1":
                    Sl_lb_errorout.Content = "Có trường trống";
                    Sl_lb_errorout.Background = Brushes.IndianRed;
                    return 1;
                    break;
                case "-2":
                    Sl_lb_errorout.Content = "Mã đối tác không có thực phẩm ";
                    Sl_lb_errorout.Background = Brushes.IndianRed;
                    return 1;
                    break;
                case "-10":
                    Sl_lb_errorout.Content = "Lỗi Server SQL";
                    Sl_lb_errorout.Background = Brushes.IndianRed;
                    return 1;
                    break;

                default:
                    Sl_lb_errorout.Content = "Danh sách đơn hàng theo " + kieu;
                    Sl_lb_errorout.Background = Brushes.LightGreen;
                    return 0;
                    break;
            }
        }
        private void SL_load_rating_Datagrid()
        {
            try
            {
                DataTable dt = db.sql_select("Exec XuHuongBan '" + MaDoiTac + "'");
                Sl_rating_datagrid.ItemsSource = dt.DefaultView;
            }
            catch
            {}
        }

        private void Sl_rating_datagrid_Loaded(object sender, RoutedEventArgs e)
        {
            SL_load_rating_Datagrid();
        }
        private void Sl_datagrid_Loaded(object sender, RoutedEventArgs e)
        {


        }
        private void Sl_datagridLoaded(string kieu)
        {
            DataTable dt = doitac.laySoLuongDon(kieu, MaDoiTac);
            string hienthi = dt.Rows[0][0].ToString();
            int kq = SL_HienMaLoi_Label(hienthi, kieu);
            if (kq == 0)
                Sl_datagrid.ItemsSource = dt.DefaultView;
        }

        private void Sl_rb_ngay_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Sl_datagridLoaded("Ngay");
            }
            catch
            {

            }

        }

        private void Sl_rb_thang_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Sl_datagridLoaded("Thang");
            }
            catch
            {

            }
        }

        private void Sl_rb_nam_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Sl_datagridLoaded("Nam");
            }
            catch
            {

            }
        }

    }
}
