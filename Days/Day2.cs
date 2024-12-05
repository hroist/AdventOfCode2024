using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Days
{
    internal class Day2: IDay
    {
        public void Run() 
        {
            string filePath = Path.Combine("Inputs", "Day2.txt");

            var lines = File.ReadAllLines(filePath);

            var reports = new List<int[]>();
            foreach (var line in lines)
            {
                var reportOfStrings = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                int[] reportOfInts = new int[reportOfStrings.Length];

                for (int i = 0; i < reportOfStrings.Length; i++)
                {
                    int levelInt = int.Parse(reportOfStrings[i]);
                    reportOfInts[i] = levelInt;
                }
                reports.Add(reportOfInts);
            }

            static bool IsDescending(int[] report)
            {
                bool dampenerApplied = false;

                for (int i = 0; i < report.Length - 1; i++)
                {
                    if (report[i] <= report[i + 1] || report[i] - report[i + 1] > 3)
                    {
                        if (!dampenerApplied)
                        {
                            int[] dampenedReport1 = DampenReport(report, i);
                            int[] dampenedReport2 = DampenReport(report, i + 1);

                            if (ValidateDescending(dampenedReport1) || ValidateDescending(dampenedReport2))
                            {
                                Console.WriteLine($"Dampened descending report: {string.Join(", ", dampenedReport1)}");
                                Console.WriteLine($"Dampened descending report: {string.Join(", ", dampenedReport2)}");
                                return true;
                            }
                            dampenerApplied = true;
                        }
                        return false;
                    }
                }
                return true;
            }

            static bool IsAscending(int[] report)
            {
                bool dampenerApplied = false;

                for (int i = 0; i < report.Length - 1; i++)
                {
                    if (report[i] >= report[i + 1] || report[i + 1] - report[i] > 3)
                    {
                        if (!dampenerApplied)
                        {
                            int[] dampenedReport1 = DampenReport(report, i);
                            int[] dampenedReport2 = DampenReport(report, i + 1);

                            if (ValidateAscending(dampenedReport1) || ValidateAscending(dampenedReport2))
                            {
                                Console.WriteLine($"Dampened ascending report: {string.Join(", ", dampenedReport1)}");
                                Console.WriteLine($"Dampened ascending report: {string.Join(", ", dampenedReport2)}");

                                return true;
                            }
                            dampenerApplied = true;
                        }
                        return false;
                    }
                }
                return true;
            }

            static int[] DampenReport(int[] report, int indexToRemove)
            {
                return report.Where((value, index) => index != indexToRemove).ToArray();
            }

            static bool ValidateDescending(int[] report)
            {
                for (int i = 0; i < report.Length - 1; i++)
                {
                    if (report[i] <= report[i + 1] || report[i] - report[i + 1] > 3)
                    {
                        return false;
                    }
                }
                return true;
            }

            static bool ValidateAscending(int[] report)
            {
                for (int i = 0; i < report.Length - 1; i++)
                {
                    if (report[i] >= report[i + 1] || report[i + 1] - report[i] > 3)
                    {
                        return false;
                    }
                }
                return true;
            }



            int countSafeReports = 0;

            foreach (var report in reports)
            {
                if (IsDescending(report) || IsAscending(report))
                    countSafeReports++;
            }

            Console.WriteLine(countSafeReports);
        }

    }
    
}
