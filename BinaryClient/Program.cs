using System;
using System.ServiceModel;
using BinaryInterfaces;

namespace BinaryClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ChannelFactory<IBinaryService> channel =
                new ChannelFactory<IBinaryService>("BinaryServiceEndpoint");

            IBinaryService proxy = channel.CreateChannel();

            Console.Write("Enter decimal number: ");

            int number = int.Parse(Console.ReadLine());

            string result = proxy.DecimalToBinary(number);

            Console.WriteLine($"Binary number: {result}");

            Console.ReadLine();
        }
    }
}