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
            Console.ReadLine();

        }
    }
}
