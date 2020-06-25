using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF_Service;

namespace WCF_Middleware
{
    public class SVR : iSVR
    {
        
        public MSG m_service(MSG message)
        {
       
            Console.WriteLine("Message " + message.appVersion);
            return new MSG() { tokenApp = "Comm reussie" };
        }
    }
}
