using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCF_Service;

namespace WCF_Middleware {
    public class User {

        private int id;
        private MSG msg;
        private string username;
        private string pswd;
        private Authentifier auth;


        public int Id { get => id; set => id = value; }
        public MSG Msg { get => msg; set => msg = value; }
        public string Username { get => username; set => username = value; }
        public string Pswd { get => pswd; set => pswd = value; }
        public Authentifier Auth { get => auth; set => auth = value; }

        public User(Authentifier auth) {
            
        }

        public MSG login(MSG message) {
            return message;
        }

        public MSG logout() {
            return new MSG();
        }
    }
}
