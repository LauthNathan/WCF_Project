using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using WCF_Service;
using WCF_Middleware;

namespace WCF_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 2: Create a ServiceHost instance.
            ServiceHost selfHost = new ServiceHost(typeof(SVR));


            selfHost.Open();
            Console.WriteLine("Server launched");
            //FileRef.FileWebServiceClient fileService = new FileRef.FileWebServiceClient();
            //Console.WriteLine(fileService.printLong());
            Authentifier auth = new Authentifier();
            MSG msg = auth.authenticate("io", "098f6bcd4621d373cade4e832627b4f6");
            Console.WriteLine("Le tokenUser est : " + msg.tokenUser);
            Console.ReadLine();

        }
    }
}
