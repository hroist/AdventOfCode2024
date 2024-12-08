using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Days
{
    internal class Day7: IDay
    {
        public void Run()
        {
            string filePath = Path.Combine("Inputs", "Day7.txt");

            var lines = File.ReadAllLines(filePath);

            var calibrationEquations = new List<(long, long[])>();

            foreach (var line in lines)
            {
                var parts = line.Split(':', 2);
                long testValue = long.Parse(parts[0].Trim());
                long[] operators = parts[1].Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
                calibrationEquations.Add((testValue, operators));
            }

            List<long> validResults = new List<long>();

            foreach (var equation in calibrationEquations)
            {

                var numberOfCombinations = 0;
                var operatorsLength = equation.Item2.Length;

                numberOfCombinations = getNumberOfCombinations(operatorsLength);

                var binaryMatrix = CreateBinaryMatrix(numberOfCombinations, operatorsLength);

                foreach (var binary in binaryMatrix)
                {

                    long result = Calculate(equation.Item2, BinaryToOperationsString(binary));
                    if(result == equation.Item1)
                    {
                        validResults.Add(result);
                        break;
                    }

                }           

            }

            long sumOfResults = validResults.Sum(result => result);
            Console.WriteLine(sumOfResults);
        }

        static string BinaryToOperationsString(string binary)
        {
            string operations = string.Empty;
            foreach (char c in binary)
            {
                if (c == '0')
                    operations += '+';
                else if (c == '1')
                    operations += '*';
            }
            return operations;
        }

        static long Calculate(long[] numbers, string operations)
        {
            long result = numbers[0];

            for( int i = 0; i < operations.Length; i++)
            {
                char operation = operations[i];

                switch (operation)
                {
                    case '+':
                        result += numbers[i + 1]; 
                        break;

                    case '*':
                        result *= numbers[i + 1];
                        break;

                    default:
                        Console.WriteLine($"Unsupported operation: {operation}");
                        break;
                }

            }
            return result;
        }
        

        private static int getNumberOfCombinations(int operatorsLength)
        {
            int numberOfCombinations = (int)Math.Pow(2, operatorsLength - 1);

            return numberOfCombinations;
        }

        private static List<string> CreateBinaryMatrix(int numberOfCombinations, int operatorsLength)
        {
            var binaryMatrix = new List<string>();
            for (int i = 0; i < numberOfCombinations; i++)
            {
                string binary = Convert.ToString(i, 2).PadLeft(operatorsLength - 1, '0');
                binaryMatrix.Add(binary);
            }
            return binaryMatrix;
        }
    }
}
