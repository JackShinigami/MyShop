using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_MyShop
{
    public class DAL_User
    {
        private static DAL_User? instance;
        private DAL_User()
        {

        }
        public static DAL_User? Instance
        {
            get
            {
                if (instance == null)
                    instance = new DAL_User();
                return instance;
            }
        }
        
        public void SaveUser(string username, string password, string entropy)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["Username"].Value = username;
            config.AppSettings.Settings["Password"].Value = password;
            config.AppSettings.Settings["Entropy"].Value = entropy;
            config.Save(ConfigurationSaveMode.Minimal);

            ConfigurationManager.RefreshSection("appSettings");
        }

        public void SetRemember(bool rememChecked)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (rememChecked)
                config.AppSettings.Settings["Remember"].Value = "true";
            else
                config.AppSettings.Settings["Remember"].Value = "false";

            config.Save(ConfigurationSaveMode.Minimal);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public string? GetRemember()
        {
            return ConfigurationManager.AppSettings["Remember"];
        }

        public string? GetUsername()
        {
            return ConfigurationManager.AppSettings["Username"];
        }

        public string? GetPassword()
        {
            return ConfigurationManager.AppSettings["Password"];
        }

        public string? GetEntropy()
        {
            return ConfigurationManager.AppSettings["Entropy"];
        }
    }
}
