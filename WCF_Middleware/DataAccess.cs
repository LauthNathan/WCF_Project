using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCF_Service;
using System.Data.SqlClient;

namespace WCF_Middleware {
    public class DataAccess {

        private MSG msg;
        private string rq_sql;
        private string connectionString = null;
        private SqlConnection cnn;

        public MSG Msg { get => msg; set => msg = value; }
        public string Rq_sql { get => rq_sql; set => rq_sql = value; }

        public DataAccess() {
            connectionString = "Data Source=WCF_SQL_SERVER;Initial Catalog=wcf_bdd;Integrated Security=True;";
            cnn = new SqlConnection(connectionString);
            try {
                cnn.Open();
               Console.WriteLine("Connection Open ! ");
                cnn.Close();
            } catch (Exception ex) {
                Console.WriteLine("Can not open connection ! ");
            }
        }
    }

}
