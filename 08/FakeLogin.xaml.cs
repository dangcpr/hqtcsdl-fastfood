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
    /// Interaction logic for FakeLogin.xaml
    /// </summary>
    public partial class FakeLogin : Window
    {
        public FakeLogin()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            DoiTacWindow dt = new DoiTacWindow(tb_username.Text);
            this.Hide();
            dt.Show();
            this.Close();
        }
    }
}
