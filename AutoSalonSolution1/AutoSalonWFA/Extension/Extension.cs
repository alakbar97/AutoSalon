using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace AutoSalonWFA.Extension
{
    public class Extension
    {
        public static string HashPassword(string password)
        {
            byte[] byteArray = ASCIIEncoding.ASCII.GetBytes(password);

            byte[] hashedArray = new SHA256Managed().ComputeHash(byteArray);

            string hashedPassword = ASCIIEncoding.ASCII.GetString(hashedArray);

            return hashedPassword;
        }

        public static bool CheckPassword(string password, string hashedPassword)
        {
            return HashPassword(password) == hashedPassword;
        }
    }
}
