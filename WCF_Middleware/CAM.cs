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

        public CAM() {
            DataAccess = new DataAccess();
        }

        public bool checkToken(MSG message) {
            Rq_sql = "SELECT tokenUser FROM users WHERE username = '" + message.data[0].ToString() + "' AND tokenUser = '" + message.tokenUser + "';";
            cmd = new SqlCommand(Rq_sql, DataAccess.Cnn);
            DataReader = cmd.ExecuteReader();

            if (DataReader.Read()) {

                DataReader.Close();
                cmd.Dispose();
                DataAccess.Cnn.Close();
                return true;

            } else {
                Console.WriteLine("Wrong user token");
                DataReader.Close();
                cmd.Dispose();
                DataAccess.Cnn.Close();
                return false;
            }
        }
    }
}
