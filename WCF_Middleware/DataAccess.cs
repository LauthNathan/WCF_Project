using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCF_Service;
using System.Data.SqlClient;

namespace WCF_Middleware {
    public class DataAccess {

        private MSG msg;
        private string connectionString = null;
        private SqlConnection cnn;

        public MSG Msg { get => msg; set => msg = value; }
        
        public SqlConnection Cnn { get => cnn; set => cnn = value; }

        public DataAccess() {
            connectionString = "Data Source=DESKTOP-500VV2V\\WCF_SQL_SERVER;Initial Catalog=wcf_bdd;Integrated Security=True;";
            Cnn = new SqlConnection(connectionString);
            try {
                Cnn.Open();
               Console.WriteLine("Connection Open ! ");
                
            } catch (Exception ex) {
                Console.WriteLine("Can not open connection ! ");
            }
        }

        public void closeConnection() {
            Cnn.Close();
        }
    }

}
