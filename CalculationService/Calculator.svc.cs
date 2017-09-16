using System;
using System.ServiceModel;

namespace CalculationService
{
    public class Calculator : ICalculator
    {
        public double Add(double a, double b)
        {
            return PerformCalculation(() => a + b);
        }

        public double Substract(double a, double b)
        {
            return PerformCalculation(() => a - b);
        }

        public double Multiply(double a, double b)
        {
            return PerformCalculation(() => a * b);
        }

        public double Divide(double a, double b)
        {
            return PerformCalculation(() => a / b);
        }

        public double Sqrt(double a)
        {
            return PerformCalculation(() => Math.Sqrt(a));
        }

        private static double PerformCalculation(Func<double> calculation)
        {
            double result = calculation();
            if (double.IsNaN(result))
            {
                throw new FaultException("NaN");
            }
            if (double.IsInfinity(result))
            {
                throw new FaultException("Infinity");
            }
            return result;
        }
    }
}
