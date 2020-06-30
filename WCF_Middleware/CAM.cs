using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WCF_Service;
using System.IO;
using System.Threading;

namespace WCF_Middleware {
    public class CAM {

        private DataAccess dataAccess;
        private MSG msg;
        private SqlCommand cmd;
        private string rq_sql;
        

        public DataAccess DataAccess { get => dataAccess; set => dataAccess = value; }
        public MSG Msg { get => msg; set => msg = value; }
 

        public string path { get; set; }
        public StreamWriter sw { get; set; }
        

        public CAM(DataAccess dA) {
            DataAccess = dA;
            path = @"C:\Users\Vimaire\source\repos\WCF_Project\WCF_Server\log.txt";
            
        }

        public bool checkToken(MSG message) {

            if(File.AppendText(path) == null) {
                sw = File.AppendText(path);
            }
            else {
                Thread.Sleep(1000);
                sw = File.AppendText(path);
            }



            if (DataAccess.checkToken(message)) {

                DateTime td = DateTime.Now;
                sw.WriteLine(td.ToString() + " : L\'utilisateur " + DataAccess.getUsername(message) + " a effectué l\'opération " + message.operationName + " avec succès");
                sw.Close();
                return true;

            } else {
                
                DateTime td = DateTime.Now;
                sw.WriteLine(td.ToString() + " : Un accès à l'opération " + message.operationName + " a été tenté sans autorisation");
                sw.Close();
                return false;
            }
        }

        public bool checkAppToken(MSG message) {

            if (File.AppendText(path) == null) {
                sw = File.AppendText(path);
            } else {
                Thread.Sleep(1000);
                sw = File.AppendText(path);
            }

            if (DataAccess.checkAppToken(message)) {

                DateTime td = DateTime.Now;
                sw.WriteLine(td.ToString() + " : L\'application possédant l\'app token " + message.tokenApp + " s'est connecté");
                sw.Close();
                return true;

            } else {

                DateTime td = DateTime.Now;
                sw.WriteLine(td.ToString() + " : L\'application possédant l\'app token " + message.tokenApp + " a essayé de se connecter sans succès");
                sw.Close();
                return false;
            }
        }
    }
}
