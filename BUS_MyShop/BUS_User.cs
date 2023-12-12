using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DAL_MyShop;
namespace BUS_MyShop
{
    public class BUS_User
    {
        private static BUS_User? instance;
        private DAL_User? dalUser = DAL_User.Instance;
        private BUS_User()
        {

        }
        public static BUS_User? Instance
        {
            get
            {
                if (instance == null)
                    instance = new BUS_User();
                return instance;
            }
        }
        
        public void SaveUser(string username, string password)
        {
            var passwordInBytes = Encoding.UTF8.GetBytes(password);
            var entropy = new byte[20];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(entropy);
            }
            var cypherText = ProtectedData.Protect(passwordInBytes, entropy,
                DataProtectionScope.CurrentUser);
            var passwordIn64 = Convert.ToBase64String(cypherText);
            var entropyIn64 = Convert.ToBase64String(entropy);
            dalUser.SaveUser(username, passwordIn64, entropyIn64);
        }

        public bool CheckLogin(string username, string password)
        {
            if(dalUser.GetUsername() == null || dalUser.GetPassword() == null || dalUser.GetEntropy() == null)
                return false;
            if(dalUser.GetUsername() == "" || dalUser.GetPassword() == "" || dalUser.GetEntropy() == "")
                return false;
            if(dalUser.GetUsername() != username)
                return false;

            var passwordIn64 = dalUser.GetPassword();
            var entropyIn64 = dalUser.GetEntropy();
            if (passwordIn64 == null || entropyIn64 == null)
                return false;

            var passwordInBytes = Convert.FromBase64String(passwordIn64);
            var entropy = Convert.FromBase64String(entropyIn64);

            var passwordInBytes2 = ProtectedData.Unprotect(passwordInBytes, entropy,
                               DataProtectionScope.CurrentUser);
            var password2 = Encoding.UTF8.GetString(passwordInBytes2);

            if (username == dalUser.GetUsername() && password == password2)
                return true;
            return false;
        }

        public string? GetUsername()
        {
            if(dalUser.GetUsername() == null || dalUser.GetUsername()=="")
                return null;
            return dalUser.GetUsername();
        }

        public string? GetPassword()
        {
            var passwordIn64 = dalUser.GetPassword();
            var entropyIn64 = dalUser.GetEntropy();
            if (passwordIn64 == null || entropyIn64 == null || passwordIn64=="" ||passwordIn64=="")
                return null;

            var passwordInBytes = Convert.FromBase64String(passwordIn64);
            var entropy = Convert.FromBase64String(entropyIn64);

            var passwordInBytes2 = ProtectedData.Unprotect(passwordInBytes, entropy,
                               DataProtectionScope.CurrentUser);
            var realPassword = Encoding.UTF8.GetString(passwordInBytes2);
            
            return realPassword;
        }
    }
}
