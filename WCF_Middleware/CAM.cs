using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WCF_Service;

namespace WCF_Middleware {
    public class CAM {

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

        public CAM(DataAccess dA) {
            DataAccess = dA;
        }

        public bool checkToken(MSG message, string usr) {
            Console.WriteLine(usr);
            Rq_sql = "SELECT tokenUser FROM users WHERE username = '" + usr + "' AND tokenUser = '" + message.tokenUser + "';";
            cmd = new SqlCommand(Rq_sql, DataAccess.Cnn);
            DataReader = cmd.ExecuteReader();

            if (DataReader.Read()) {

                DataReader.Close();
                cmd.Dispose();
                return true;

            } else {
                Console.WriteLine("Wrong user token");
                DataReader.Close();
                cmd.Dispose();
                return false;
            }
        }

        public bool checkAppToken(MSG message) {
            Rq_sql = "SELECT tokenApp FROM tokenApp WHERE tokenApp = '" + message.tokenApp + "';";
            cmd = new SqlCommand(Rq_sql, DataAccess.Cnn);
            DataReader = cmd.ExecuteReader();

            if (DataReader.Read()) {

                DataReader.Close();
                cmd.Dispose();
                return true;

            } else {
                Console.WriteLine("Wrong app token");
                DataReader.Close();
                cmd.Dispose();
                return false;
            }
        }
    }
}
