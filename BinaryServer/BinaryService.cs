using System;
using BinaryInterfaces;

namespace BinaryServer
{
    public class BinaryService : IBinaryService
    {
        public string DecimalToBinary(int number)
        {
            return Convert.ToString(number, 2);
        }
    }
}