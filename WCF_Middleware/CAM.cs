using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WCF_Service;
using System.IO;

namespace WCF_Middleware {
    public class CAM {

        private DataAccess dataAccess;
        private MSG msg;
        private SqlCommand cmd;
        private string rq_sql;
        

        public DataAccess DataAccess { get => dataAccess; set => dataAccess = value; }
        public MSG Msg { get => msg; set => msg = value; }
        public SqlCommand Cmd { get => cmd; set => cmd = value; }
        public string Rq_sql { get => rq_sql; set => rq_sql = value; }
        public SqlDataReader DataReader { get; set ; }

        public string path { get; set; }
        public StreamWriter sw { get; set; }
        

        public CAM(DataAccess dA) {
            DataAccess = dA;
            path = @"C:\Users\Vimaire\source\repos\WCF_Project\WCF_Server\log.txt";
            
        }

        public bool checkToken(MSG message) {
            
            Rq_sql = "SELECT username, tokenUser FROM users WHERE tokenUser = '" + message.tokenUser + "';";
            sw = File.AppendText(path);
            cmd = new SqlCommand(Rq_sql, DataAccess.Cnn);
            DataReader = cmd.ExecuteReader();

            if (DataReader.Read()) {

                DateTime td = DateTime.Now;
                sw.WriteLine(td.ToString() + " : L\'utilisateur " + DataReader.GetString(0) + " a effectué l\'opération " + message.operationName + " avec succès");
                sw.Close();
                DataReader.Close();
                cmd.Dispose();
                return true;

            } else {
                
                DateTime td = DateTime.Now;
                sw.WriteLine(td.ToString() + " : Un accès à l'opération " + message.operationName + " a été tenté sans autorisation");
                sw.Close();
                DataReader.Close();
                cmd.Dispose();
                return false;
            }
        }

        public bool checkAppToken(MSG message) {
            Rq_sql = "SELECT tokenApp FROM tokenApp WHERE tokenApp = '" + message.tokenApp + "';";
            cmd = new SqlCommand(Rq_sql, DataAccess.Cnn);
            sw = File.AppendText(path);
            DataReader = cmd.ExecuteReader();

            if (DataReader.Read()) {

                DateTime td = DateTime.Now;
                sw.WriteLine(td.ToString() + " : L\'application possédant l\'app token " + message.tokenApp + " s'est connecté");
                DataReader.Close();
                cmd.Dispose();
                sw.Close();
                return true;

            } else {

                DateTime td = DateTime.Now;
                sw.WriteLine(td.ToString() + " : L\'application possédant l\'app token " + message.tokenApp + " a essayé de se connecter sans succès");
                DataReader.Close();
                cmd.Dispose();
                sw.Close();
                return false;
            }
        }
    }
}
