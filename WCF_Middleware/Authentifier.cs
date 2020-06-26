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

        private DataAccess dataAccess;
        private MSG msg;
        private SqlCommand cmd;
        private string rq_sql;
        private SqlDataReader dataReader;

        public DataAccess DataAccess { get => dataAccess; set => dataAccess = value; }
        public MSG Msg { get => msg; set => msg = value; }
        public SqlCommand Cmd { get => cmd; set => cmd = value; }
        public string Rq_sql { get => rq_sql; set => rq_sql = value; }
        public SqlDataReader DataReader { get => dataReader; set => dataReader = value; }

        public Authentifier(DataAccess dA) {
            DataAccess = dA;
        }

        public string authenticate(string username, string pswd) {
            Rq_sql = "SELECT tokenUser FROM users WHERE username = '" + username + "' AND password = '" + pswd + "';";
            cmd = new SqlCommand(Rq_sql, DataAccess.Cnn);
            DataReader = cmd.ExecuteReader();
            string response;
           
           if (DataReader.Read()) {
                Console.WriteLine(DataReader.GetString(0));

                response = DataReader.GetString(0);
                DataReader.Close();
                cmd.Dispose();
                return response;

            } else {
                Console.WriteLine("Wrong credentials");
                DataReader.Close();
                cmd.Dispose();
                return null;
            }
        }
    }
}
