using System;
using System.ServiceModel;

namespace BinaryServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(BinaryService)))
            {
                host.Open();

                Console.WriteLine("WCF service started...");
                Console.ReadLine();
            }
        }
    }
}