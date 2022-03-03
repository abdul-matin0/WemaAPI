using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WemaAPI.RegistrationService.Helpers
{
    public static class EncryptPassword
    {
        public static string EncryptHash(string value)
        {

            byte[] data = System.Text.Encoding.ASCII.GetBytes(value);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            String hashValue = System.Text.Encoding.ASCII.GetString(data);

            return hashValue;
        }
    }
}
