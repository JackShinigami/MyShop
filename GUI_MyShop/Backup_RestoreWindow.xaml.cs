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
using BUS_MyShop;

namespace GUI_MyShop
{
    /// <summary>
    /// Interaction logic for Backup_RestoreWindow.xaml
    /// </summary>
    public partial class Backup_RestoreWindow : Window
    {
        
        public Backup_RestoreWindow()
        {
            InitializeComponent();
        }

        private void btnBackup_Click(object sender, RoutedEventArgs e)
        {
            BUS_MyShop.BUS_Backup_Restore.Instance.Backup();
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            BUS_MyShop.BUS_Backup_Restore.Instance.Restore();
        }
    }
}
