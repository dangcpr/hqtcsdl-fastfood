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
using System.Windows.Navigation;
using System.Windows.Shapes;
using _08;

namespace _08
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool Isloaded = false;
        public MainWindow()
        {
            InitializeComponent();
            Isloaded = true;
            main.Hide();
            Login login = new Login();
            login.ShowDialog();
            if (login.IsLogin)
            {
                main.Show();
                
            }
            else
            {
                main.Close();
            }
        }

    }
    
    
}
