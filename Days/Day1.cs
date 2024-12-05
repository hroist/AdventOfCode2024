using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Days
{
    public class Day1: IDay 
    {
        public void Run()
        {
            string filePath = Path.Combine("Inputs", "Day1.txt");
            var lines = File.ReadAllLines(filePath);

            var data = new List<(int, int)>();
            foreach (var line in lines)
            {
                var parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 2 && int.TryParse(parts[0], out int col1) && int.TryParse(parts[1], out int col2))
                {
                    data.Add((col1, col2));
                }
            }

            var column1 = data.Select(pair => pair.Item1).OrderBy(x => x).ToList();
            var column2 = data.Select(pair => pair.Item2).OrderBy(x => x).ToList();

            var sortedData = new List<(int, int)>();
            for (int i = 0; i < column1.Count; i++)
            {
                sortedData.Add((column1[i], column2[i]));
            }

            int sumOfAbsoluteDifferences = sortedData.Sum(pair => Math.Abs(pair.Item1 - pair.Item2));
            Console.WriteLine(sumOfAbsoluteDifferences);

            int similarityScore = 0;
            foreach (int value in column1)
            {
                int countAppearanceInColumn2 = column2.Count(x => x == value);
                similarityScore += value * countAppearanceInColumn2;
            }
            Console.WriteLine(similarityScore);
        }
    }
}

