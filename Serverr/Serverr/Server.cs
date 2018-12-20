using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Configuration;

namespace Server
{
    public class Server
    {
        static void Main(string[] args)
        {
            Uri address = new Uri("http://localhost:4000/IService1");

            BasicHttpBinding basicHttpBinding = new BasicHttpBinding();

            Type contract = typeof(IService1);
            ServiceHost host = new ServiceHost(typeof(Service1));
            

            host.AddServiceEndpoint(contract, basicHttpBinding, address);
            host.Open();

            
            Console.ReadKey();
            Console.ReadKey();
            host.Close();

        }

        private static string GetConnectionString()
        {
            throw new NotImplementedException();
        }
    }
}