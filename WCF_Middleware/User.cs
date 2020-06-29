using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCF_Service;
using System.Security.Cryptography;

namespace WCF_Middleware {
    public class User {
        private MSG msg;
        private string username;
        private Authentifier auth;

        public MSG Msg { get => msg; set => msg = value; }
        public string Username { get => username; set => username = value; }
        public Authentifier Auth { get => auth; set => auth = value; }

        public User(Authentifier auth) {
            Auth = auth;
        }

        /// <summary>
        /// Logs in the user
        /// </summary>
        public MSG login(MSG message) {
            using (MD5 md5Hash = MD5.Create()) {
                string tokenUser = Auth.authenticate(message.data[0].ToString(), GetMd5Hash(md5Hash, message.data[1].ToString()));

                if (tokenUser != null) {
                    message.tokenUser = tokenUser;
                    message.statut_Op = true;
                    Username = message.data[0].ToString();
                    return message;
                }
                message.statut_Op = false;
                message.info = "Wrong credentials";
                return message;
            }
        }

        public MSG logout() {
            return new MSG();
        }
        static string GetMd5Hash(MD5 md5Hash, string input) {
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
