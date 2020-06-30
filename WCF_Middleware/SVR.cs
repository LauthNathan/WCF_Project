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
        public CAM Cam { get; set; }
        public DataAccess dA { get; set; }
        public User User { get; set; }

        public MSG m_service(MSG message) {

            dA = new DataAccess();
            Cam = new CAM(dA);

            if (!Cam.checkAppToken(message)) {
                message.statut_Op = false;
                message.info = "Wrong app token";
                return message;
            }

            switch (message.operationName) {
                // DECRYPT
                case "Decrypt":
                    if (Cam.checkToken(message)) {
                        Thread workerThread = new Thread(() => DecryptService.DecryptAction(message));
                        workerThread.Start();

                        message.statut_Op = true;
                        message.info = "Deencryption started";
                        

                        return message;
                    } else {
                        message.statut_Op = false;
                        message.info = "Wrong user token";
                        

                        return message;
                    }

                // AUTHENTICATION
                case "Auth":
                    Authentifier auth = new Authentifier(dA);
                    User = new User(auth);
                    message = User.login(message); 

                    return message;

                case "Stop":
                    Console.WriteLine("Stop");
                    Console.WriteLine(message.statut_Op);
                    Console.WriteLine(message.info);
                    Console.WriteLine(message.data[0].ToString());

                    if (Cam.checkToken(message)) {
                        Utils.FOUND_SECRET = true;
                        Console.WriteLine(Utils.FOUND_SECRET);
                        return message;
                    } else {
                        message.statut_Op = false;
                        message.info = "Wrong user token";

                        return message;
                    }

                // DEFAULT
                default:
                    message.statut_Op = false;
                    message.info = "Operation " + message.operationName + " does not exist.";
                    return message;
            }
        }
    }
}
