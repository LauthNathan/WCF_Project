using System;
using System.ServiceModel;
using WCF_Middleware;

namespace WCF_Server {
  class Program {
        static void Main(string[] args) {
            ServiceHost selfHost = new ServiceHost(typeof(SVR));
            selfHost.Open();
           
            Console.WriteLine("Server launched");
            Console.ReadLine();
        }
    }
}
