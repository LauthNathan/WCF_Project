using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCF_Service;
using System.Data.Sql;
using System.Data.SqlClient;
using System.ServiceModel.Security;

namespace WCF_Middleware {
    public class Authentifier {

        public DataAccess DataAccess { get ; set ; }
  
        public Authentifier(DataAccess dA) {
            DataAccess = dA;
        }

        public string authenticate(string username, string pswd) {
            return DataAccess.authenticate(username, pswd);
        }
    }
}
