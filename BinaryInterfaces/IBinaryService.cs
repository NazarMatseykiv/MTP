using System.ServiceModel;

namespace BinaryInterfaces
{
    [ServiceContract]
    public interface IBinaryService
    {
        [OperationContract]
        string DecimalToBinary(int number);
    }
}