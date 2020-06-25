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
        [System.Security.Permissions.PrincipalPermission(
            System.Security.Permissions.SecurityAction.Demand,
            Role = @"BUILTIN\Utilisateurs")]
        public MSG m_service(MSG message)
        {
            System.ServiceModel.ServiceSecurityContext csx = System.ServiceModel.OperationContext.Current.ServiceSecurityContext;
            Console.WriteLine("Message" + message.appVersion);
          
            return new MSG() { tokenApp = "Comm reussie" };
        }
    }
}
