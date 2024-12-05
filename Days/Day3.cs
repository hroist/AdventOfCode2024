using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Days
{
    internal class Day3: IDay
    {
        public void Run()
        {
            string filePath = Path.Combine("Inputs", "Day3.txt");

            string InputString = File.ReadAllText(filePath);

            string patternMul = @"mul\(\d+,\d+\)";
            string patternInt = @"\d+";

            Regex regexMul = new Regex(patternMul);
            Regex regexInt = new Regex(patternInt);

            MatchCollection matches = regexMul.Matches(InputString);

            var Products = new List<int>();

            foreach (Match match in matches)
            {
                var ints = FindIntsInString(match.Value);
                var product = ints.Item1 * ints.Item2;
                Products.Add(product);
                Console.WriteLine(match.Value);
            }

            (int, int) FindIntsInString(string text)
            {
                MatchCollection matches = regexInt.Matches(text);

                int num1 = int.Parse(matches[0].Value);
                int num2 = int.Parse(matches[1].Value);
                return (num1, num2);
            }

            int SumOfProducts = Products.Sum();

            Console.WriteLine(SumOfProducts);
        }
    }
}
