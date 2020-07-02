using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCF_Service;

namespace WCF_Middleware {
    public class User {
    
        public string Username { get ; set ; }
        public Authentifier Auth { get; set ; }

        public User(Authentifier auth) {
            Auth = auth;
        }

        /// <summary>
        /// Log in the user using teh data send in the MSG object
        /// </summary>
        /// <param name="message">MSG object ciming from the client</param>
        /// <returns>The message, with the user token</returns>
        public MSG login(MSG message) {
            
                string tokenUser = Auth.authenticate(message.data[0].ToString(), message.data[1].ToString());

                if (tokenUser != null) {
                    message.tokenUser = tokenUser;
                    message.statut_Op = true;
                    Username = message.data[0].ToString();
                    Console.WriteLine("User logged in");
                    return message;
                }
                message.statut_Op = false;
                message.info = "Wrong credentials";
                return message;
            
        }

        

    }

}
