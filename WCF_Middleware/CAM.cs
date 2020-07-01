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

       
        public DataAccess DataAccess { get ; set; }
        public string Path { get; set; }
        public StreamWriter Sw { get; set; }
        

        public CAM(DataAccess dA) {
            DataAccess = dA;
            Path = @"C:\Users\Vimaire\source\repos\WCF_Project\WCF_Server\log.txt";
            
        }

        public bool checkToken(MSG message) {

           
                Sw = File.AppendText(Path);
           



            if (DataAccess.checkToken(message)) {

                DateTime td = DateTime.Now;
                Sw.WriteLine(td.ToString() + " : L\'utilisateur " + DataAccess.getUsername(message) + " a effectué l\'opération " + message.operationName + " avec succès");
                Sw.Close();
                return true;

            } else {
                
                DateTime td = DateTime.Now;
                Sw.WriteLine(td.ToString() + " : Un accès à l'opération " + message.operationName + " a été tenté sans autorisation");
                Sw.Close();
                return false;
            }
        }

        public bool checkAppToken(MSG message) {

            
                Sw = File.AppendText(Path);
            

            if (DataAccess.checkAppToken(message)) {

                DateTime td = DateTime.Now;
                Sw.WriteLine(td.ToString() + " : L\'application possédant l\'app token " + message.tokenApp + " s'est connecté");
                Sw.Close();
                return true;

            } else {

                DateTime td = DateTime.Now;
                Sw.WriteLine(td.ToString() + " : L\'application possédant l\'app token " + message.tokenApp + " a essayé de se connecter sans succès");
                Sw.Close();
                return false;
            }
        }
    }
}
