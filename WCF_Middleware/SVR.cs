using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF_Service;

namespace WCF_Middleware {
    public class SVR : iSVR {
        
        public MSG m_service(MSG message) {
            Console.WriteLine(message.appVersion);
            switch (message.operationName) {
                case "Decrypt":
                    return DecryptService.DecryptAction(message);
                case "Auth":
                    return Auth(message);
                default:
                    Console.WriteLine(message.appVersion);
                    return new MSG();
            }
        }

        public MSG Auth(MSG msg) {
            
            Console.WriteLine("Message" + msg.appVersion);

            return new MSG() { tokenApp = "Comm reussie" };
        }
    }
}
