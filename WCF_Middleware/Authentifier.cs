using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCF_Service;
using System.Data.Sql;
using System.Data.SqlClient;
using System.ServiceModel.Security;
using System.Security.Cryptography;

namespace WCF_Middleware {
    public class Authentifier {

        public DataAccess DataAccess { get ; set ; }
  
        public Authentifier(DataAccess dA) {
            DataAccess = dA;
        }

        /// <summary>
        /// Get the user token associated with the given username and pasdsword, encoding 
        /// the password in md5 to compare it to the one in database
        /// </summary>
        /// <param name="username">Username sent by the client</param>
        /// <param name="pswd">Password sent by the client</param>
        /// <returns>The user token corresponding if it is found</returns>
        public string authenticate(string username, string pswd) {
            using (MD5 md5Hash = MD5.Create()) {
                return DataAccess.authenticate(username, GetMd5Hash(md5Hash, pswd));
            }
        }

        /// <summary>
        /// Encode the given string in MD5
        /// </summary>
        /// <param name="md5Hash">a MD5 object</param>
        /// <param name="input">The strong to encode</param>
        /// <returns>The given string encoded</returns>
        private static string GetMd5Hash(MD5 md5Hash, string input) {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++) {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}
