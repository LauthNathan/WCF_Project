using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
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
            /*FileService.FileWebServiceClient fs = new FileService.FileWebServiceClient();
            FileService.msg test = new FileService.msg();
            List<string> data = new List<string>();
            data.Add("zizi");

            test.appVersion = "app version";
            test.info = "test";
            test.tokenApp = "tokenapp";
            test.tokenUser = "token";
            test.statutOp = true;
            test.operationName = "decrypt";
            test.operationVersion = "vcersion";
            test.data = data.ToArray();

            fs.fileCheck(test);*/

            Console.ReadLine();

        }
    }
}
