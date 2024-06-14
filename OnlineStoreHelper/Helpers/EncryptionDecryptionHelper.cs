using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace OnlineStoreHelper.Helpers
{
    public class EncryptionDecryptionHelper
    {
        public static string Encrypt(string plainText)
        {
            byte[] plainBytes = Encoding.UTF32.GetBytes(plainText);
            byte[] encryptedBytes = MachineKey.Protect(plainBytes);
            return Convert.ToBase64String(encryptedBytes);
        }

        public static string Decrypt(string encryptedText)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            byte[] plaintTextBytes = MachineKey.Unprotect(encryptedBytes);
            return Encoding.UTF32.GetString(plaintTextBytes);
        }
    }
}
