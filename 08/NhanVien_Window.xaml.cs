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

namespace _08
{
    /// <summary>
    /// Interaction logic for NhanVien_Window.xaml
    /// </summary>
    public partial class NhanVien_Window : Window
    {
        string Username = "";
        public NhanVien_Window(string Username)
        {
            InitializeComponent();
            this.Username = Username;
        }
    }
}
