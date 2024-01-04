using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_MyShop;
using Microsoft.Win32;

namespace BUS_MyShop
{
    public class BUS_Backup_Restore
    {
        private static BUS_Backup_Restore? instance;
        private DAL_Backup_Restore dal = DAL_Backup_Restore.Instance;
        private BUS_Backup_Restore()
        {

        }
        public static BUS_Backup_Restore? Instance
        {
            get
            {
                if (instance == null)
                    instance = new BUS_Backup_Restore();
                return instance;
            }
        }

        public string SaveFile()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Backup Files (*.bak)|*.bak";
            dialog.DefaultExt = "bak";
            if (dialog.ShowDialog() == true)
            {
                return dialog.FileName;
            }
            else
            {
                return null;
            }
        }

        public bool Backup()
        {
            string path = SaveFile();
            if (path == null)
                return false;
            if (dal.Backup(path))
            {
                return true;
            }
            else
            {
                File.Delete(path);
                return false;
            }
           
        }

        public string OpenFile()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Backup Files (*.bak)|*.bak";
            dialog.DefaultExt = "bak";
            if (dialog.ShowDialog() == true)
            {
                return dialog.FileName;
            }
            else
            {
                return null;
            }
        }

        public bool Restore()
        {
            string path = OpenFile();
            if (path == null)
                return false;
            return dal.Restore(path);
        }
    }
}
