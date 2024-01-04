using DocumentFormat.OpenXml.InkML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace DAL_MyShop
{
    public class DAL_Backup_Restore
    {
        private static DAL_Backup_Restore? instance;
        BookshopContext context = new BookshopContext();
        private DAL_Backup_Restore()
        {

        }
        public static DAL_Backup_Restore? Instance
        {
            get
            {
                if (instance == null)
                    instance = new DAL_Backup_Restore();
                return instance;
            }
        }

        public bool Backup(string path)
        {
            try
            {
                string backupFileName = path;
                string databaseName = context.Database.GetDbConnection().Database;
                string sqlCommand = $"BACKUP DATABASE [{databaseName}] TO DISK='{backupFileName}' WITH FORMAT, MEDIANAME = 'SQLServerBackups', NAME = 'Full Backup of {databaseName}';";
                context.Database.ExecuteSqlRaw(sqlCommand);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        public bool Restore(string path)
        {
            try
            {
                string backupFileName = path;
                string databaseName = context.Database.GetDbConnection().Database;
                string sqlCommand = $"USE master RESTORE DATABASE [{databaseName}] FROM DISK='{backupFileName}' WITH REPLACE;";
                context.Database.ExecuteSqlRaw(sqlCommand);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }
    }
}
