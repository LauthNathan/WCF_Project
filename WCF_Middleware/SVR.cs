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

            if (message.operationName != "Stop" && !Cam.checkAppToken(message)) {
                message.statut_Op = false;
                message.info = "Wrong app token";
                dA.Cnn.Close();

                return message;
            }

            switch (message.operationName) {
                // DECRYPT
                case "Decrypt":
                    if (Cam.checkToken(message)) {
                        var x = Task.Factory.StartNew(() => {
                            DecryptService.DecryptAction(message);
                        });
                        Task.WaitAll(x);
                        if (Utils.FOUND_SECRET) {
                            message.statut_Op = true;
                            message.info = "Found secret";
                            message.data = new object[] { Utils.SECRET_CONTENT, Utils.SECRET_FILENAME, Utils.SECRET_KEY, Utils.SECRET_CONFIDENCE };
                            MailSender.sendMail(message);
                        } else {
                            message.statut_Op = false;
                            message.info = "No secret found";
                        }
                        dA.Cnn.Close();

                        return message;
                    } else {
                        message.statut_Op = false;
                        message.info = "Wrong user token";
                        dA.Cnn.Close();


                        return message;
                    }

                // AUTHENTICATION
                case "Auth":
                    Authentifier auth = new Authentifier(dA);
                    User = new User(auth);
                    message = User.login(message);
                    dA.Cnn.Close();

                    return message;

                case "Stop":
                    Console.WriteLine("million");
                    Console.WriteLine(message.tokenUser);
                    
                        Utils.SECRET_CONTENT = message.data[0].ToString();
                        Utils.SECRET_FILENAME = message.data[1].ToString();
                        Utils.SECRET_KEY = message.data[2].ToString();
                        Utils.SECRET_CONFIDENCE = message.data[3].ToString();
                        Utils.FOUND_SECRET = true;
                        Console.WriteLine(Utils.FOUND_SECRET);
                        dA.Cnn.Close();
                       
                        return message;
                    

                // DEFAULT
                default:
                    message.statut_Op = false;
                    message.info = "Operation " + message.operationName + " does not exist.";
                    return message;
            }
        }
    }
}
