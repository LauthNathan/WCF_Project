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
            Console.WriteLine("Utilisateur : (0)\n(1)\n(2) pour le message (3)",
                csx.WindowsIdentity.User,
                csx.WindowsIdentity.Name,
                System.Threading.Thread.CurrentPrincipal.Identity.Name,
                message.appVersion);
            return new MSG() { tokenApp = "Comm reussie" };
        }
    }
}
