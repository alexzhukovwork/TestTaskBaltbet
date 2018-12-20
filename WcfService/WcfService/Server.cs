using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace WcfService
{
    public class Server
    {
        static void Main(string[] args)
        {
            Uri address = new Uri("http://localhost:4000/IService1");

            BasicHttpBinding basicHttpBinding = new BasicHttpBinding();

            Type contract = typeof(IService1);
            ServiceHost host = new ServiceHost(typeof(Service));

            host.AddServiceEndpoint(contract, basicHttpBinding, address);
        //    host.Open();
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;" +
                "|DataDirectory|\\BK.mdf;" +
                "Integrated Security=True";
            SqlConnection con = new SqlConnection(connectionString);

            con.Open();

            SqlCommand cmd = new SqlCommand("insert into Player(id,name) values(@id,@name)", con);

            cmd.Parameters.AddWithValue("@id", 5);

            cmd.Parameters.AddWithValue("@name", "LOL");
           
            


            int result = cmd.ExecuteNonQuery();
            String Message;
            if (result == 1)

            {
                Console.WriteLine("success");
            }

            else

            {
                Console.WriteLine("Bad");
            }
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