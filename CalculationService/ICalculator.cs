using System.ServiceModel;

namespace CalculationService
{
    [ServiceContract(Namespace = "by.bsuir.mpp", Name = "Calculator")]
    public interface ICalculator
    {
        [OperationContract]
        double Add(double a, double b);

        [OperationContract]
        double Substract(double a, double b);

        [OperationContract]
        double Multiply(double a, double b);

        [OperationContract]
        double Divide(double a, double b);

        [OperationContract]
        double Sqrt(double a);
    }
}
