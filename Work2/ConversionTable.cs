using System;
using System.Collections.Generic;
using System.Text;


    public class ConversionTable
    {
        public static void main()
        {
            while (true)
            {
                Console.Write("Enter the number of rows (q to quit): ");
                var input = Console.ReadLine();
                if (input == "q")
                {
                    break;
                }
                decimal mph = 15;
                decimal kph = new decimal(24.14);
                int count = int.Parse(input);
                Console.WriteLine("MPH\tKPH");
                for (int i = 0; i < count; i++)
                {
                    Console.WriteLine("{0}\t{1}",mph,kph);
                    mph += 10;
                    kph = Math.Round(mph * (1 / new decimal(0.62137)), 2);
                }
            }
            
        }
    }

