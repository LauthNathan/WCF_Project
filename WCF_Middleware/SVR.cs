using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WCF_Service;

namespace WCF_Middleware {
    public class SVR : iSVR {
        public static string tokkenApp = "#A`ut8kNX7t.%L%#Ierr3sBYi}`S=bXRK5.iWo[Reu>^|Km9fW+K!C%{Q}O&xU,";
        public static MSG response = new MSG();

        [System.Security.Permissions.PrincipalPermission(
           System.Security.Permissions.SecurityAction.Demand,
           Role = @"BUILTIN\Utilisateurs")]
        public MSG m_service(MSG message) {
            Console.WriteLine("nath");
            response = message;
            Console.WriteLine(message.tokenApp);
            if (message.tokenApp != tokkenApp) {
                response.statut_Op = false;
                response.info = "Wrong app token";
                return response;
            } else if (!checkToken(message.tokenUser)) {
                response.statut_Op = false;
                response.info = "Wrong client token";
                return response;
            }
            Console.WriteLine("là");
            switch (message.operationName) {
                // DECRYPT
                case "Decrypt":
                    Thread workerThread = new Thread(() => DecryptService.DecryptAction(message));
                    workerThread.Start();

                    response.statut_Op = true;
                    response.info = "Deencryption started";
                    return response;

                // AUTHENTICATION
                case "Auth":
                    Console.WriteLine("ici");
                    DataAccess dA = new DataAccess();
                    Authentifier auth = new Authentifier(dA);
                    User user = new User(auth);
                    MSG mess = user.login(message);
                    Console.WriteLine(mess.statut_Op);
                    return mess;
                   

                // DEFAULT
                default:
                    response.statut_Op = false;
                    response.info = "Operation " + response.operationName + "does not exist.";
                    return response;
            }
        }

        public bool checkToken(string token) {
            if (token != "") return true;
            return false;
        }

        public MSG Auth(MSG msg) {

            Console.WriteLine("Message" + msg.appVersion);

            return new MSG() { tokenApp = "Comm reussie" };
        }
    }
}
