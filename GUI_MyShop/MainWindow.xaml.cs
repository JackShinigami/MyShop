using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BUS_MyShop;
namespace GUI_MyShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            BUS_User.Instance!.SaveUser("admin", "admin");

            if(BUS_User.Instance!.CheckLogin("admin", "admin"))
            {
                MessageBox.Show("Login successfully");
            }
            else
            {
                MessageBox.Show("Login failed");
            }
        }
    }
}
