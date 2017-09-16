using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace CalculatorClient
{
    internal class Program
    {
        private const int CorrectArgumnetsCount = 3;
        private const int OperationParameterIndex = 0;
        private const int NonArgumentsParametersCount = 1;

        private static readonly Dictionary<string, Func<double[], CalculatorReference.CalculatorClient, double>> CalculatorOperations =
            new Dictionary<string, Func<double[], CalculatorReference.CalculatorClient, double>>
        {
            { "add", (arguments, client) => client.Add(arguments[0], arguments[1]) },
            { "div",  (arguments, client) => client.Divide(arguments[0], arguments[1]) },
            { "mul",  (arguments, client) => client.Multiply(arguments[0], arguments[1]) },
            { "sub",  (arguments, client) => client.Substract(arguments[0], arguments[1]) },
            { "sqrt",  (arguments, client) => client.Sqrt(arguments[0]) }
        };

        public static void Main(string[] args)
        {
            int argumentsCount = args.Length;
            if ((argumentsCount == CorrectArgumnetsCount) || ((argumentsCount == CorrectArgumnetsCount - 1) && (args[OperationParameterIndex].Equals("sqrt"))))
            {
                string operation = args[OperationParameterIndex];
                if (CalculatorOperations.Keys.Contains(operation))
                {
                    double[] operationArguments;
                    if (!TryParseArguments(args, out operationArguments))
                    {
                        Console.WriteLine("Invalid arguments");
                    }
                    else
                    {
                        using (var calculatorClient = new CalculatorReference.CalculatorClient())
                        {
                            PerformCalculation(operation, operationArguments, calculatorClient);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid operation");
                }
            }
            else
            {
                Console.WriteLine("Invalid arguments count");
            }
        }

        private static bool TryParseArguments(string[] args, out double[] arguments)
        {
            bool result = true;
            arguments = new double[args.Length - NonArgumentsParametersCount];
            for (int i = OperationParameterIndex + 1; (i < args.Length) && result; i++)
            {
                if (!double.TryParse(args[i], out arguments[i - NonArgumentsParametersCount]))
                {
                    result = false;
                }
            }
            return result;
        }

        private static void PerformCalculation(string operation, double[] args, CalculatorReference.CalculatorClient calculatorClient)
        {
            try
            {
                double operationResult = CalculatorOperations[operation](args, calculatorClient);
                Console.WriteLine(operationResult);
            }
            catch (FaultException e)
            {
                Console.WriteLine(e.Reason);
            }
            catch (CommunicationException)
            {
                Console.WriteLine("Connection error");
            }
        }
    }
}
