﻿using System;
using System.Collections.Generic;

namespace Days
{
    class Program
    {
        static void Main(string[] args)
        {
            var days = new Dictionary<int, IDay>
            {
                { 1, new Days.Day1() },
                { 2, new Days.Day2() },
                { 3, new Days.Day3() }
                // Add more days as needed
            };

            Console.WriteLine("Enter the day number to run:");
            if (int.TryParse(Console.ReadLine(), out int dayNumber) && days.ContainsKey(dayNumber))
            {
                days[dayNumber].Run();
            }
            else
            {
                Console.WriteLine("Invalid day number.");
            }
        }
    }
}