using BUS_MyShop;
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

namespace GUI_MyShop
{
    /// <summary>
    /// Interaction logic for BackupRestore.xaml
    /// </summary>
    public partial class BackupRestore : UserControl
    {
        public BackupRestore()
        {
            InitializeComponent();
        }

        private string getDataFile()
        {
            //Open file dialog to select file
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".xlsx";
            dlg.Filter = "Excel Files (*.xlsx)|*.xlsx";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                return dlg.FileName;
            }
            else
            {
                return "";
            }
        }
        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string filename = getDataFile();
                if (filename != "")
                {
                    BUS_ImportData.Instance.ImportDataFromExcelFile(filename);

                }
            }
            catch (Exception ex)
            {
                MessageWindow.Show(ex.Message);
            }
        }

        private void btnBackup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BUS_Backup_Restore.Instance.Backup();
            }
            catch (Exception ex)
            {
                MessageWindow.Show(ex.Message);
            }
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BUS_Backup_Restore.Instance.Restore();
            }
            catch (Exception ex)
            {
                MessageWindow.Show(ex.Message);
            }
        }
    }
}
