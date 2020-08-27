using System;
using System.Collections.Generic;
using System.IO;

namespace WeatherAnalysis
{
    public class MonthlyRainfall
    {
        /// <summary>
        /// Enumeration used to translate month numbers in 1..12 to month names. 
        /// </summary>
        enum Month
        {
            January = 1,
            February,
            March,
            April,
            May,
            June,
            July,
            August,
            September,
            October,
            November,
            December
        }

        /// <summary>
        ///     Test driver for PrintRainfallSummary.
        ///     Creates a TextReader attached to a text stream which can be 
        ///     used to read tabulated rainfall data for a single year.
        /// </summary>
        /// <param name="args">
        ///     A string array containing command line arguments. If an argument 
        ///     is supplied, it is a valid path to a file containing rainfall 
        ///     data. Otherwise, the program will use hard-coded data defined
        ///     in-line in the code.
        /// </param>
        public static void main(string[] args)
        {
            if (args.Length >= 2)
            {
                string inPath = args[0];
                string outPath = args[1];
                using (TextReader reader = new StreamReader(inPath))
                {
                    using (TextWriter writer = new StreamWriter(outPath))
                    {
                        MonthlyRainfallSummary(reader, writer);
                    }
                }
            }
            else
            {
                Console.Error.WriteLine($"Expected two arguments: inPath and outPath.");
            }
        }

        /// <summary>
        ///     Parse the rainfall data and generate a CSV-formatted report
        ///     containing a monthly summary of rainfall statistics.
        /// <para>
        ///     Refer to question text and sample data files for specifics.
        /// </para>
        /// </summary>
        /// <param name="reader">
        ///     A TextReader which has been attached to an input data stream and 
        ///     is ready to parse content.
        /// </param>
        /// <param name="writer">
        ///     A TextWriter which has been attached to an output stream which 
        ///     is ready to receive data.
        /// </param>
        /// <returns>
        ///		This method does not return a value.
        /// </returns>

        // INSERT METHOD HERE. 
        public static void MonthlyRainfallSummary(TextReader reader, TextWriter writer)
        {
            writer.WriteLine("Month,TotalRainfall(mm),AverageRainfall(mm/day),MaximimDailyRainfall(mm),MissingData(%)");

            string line;
            int month = 1;
            int count = 0, monthCount = 0, record = 0;
            decimal total = 0, average = 0, max = 0, miss = 0;

            string[][] monthRain = new string[12][];

            while ((line = reader.ReadLine()) != null)
            {
                
                count++;
                if (count < 2)
                {
                    continue;
                }
                monthCount++;
                var arrs = line.Split(",");

                int curMonth = int.Parse(arrs[3]);
                if(curMonth != month)
                {
                    average = Math.Round(total / record, 2);
                    miss = Math.Round((miss / (monthCount)) * 100, 2);
                    monthRain[month - 1] = new string[]
                    {
                        Enum.GetName(typeof(Month), month), total.ToString("#0.00"),
                        average.ToString("#0.00"), max.ToString("#0.00"), miss.ToString("#0.00")
                    };
                    record = 0;
                    total = average = max = miss = 0;
                    month = curMonth;
                }
                string rainStr = arrs[5];
                decimal rainAmount = 0;

                if (!string.IsNullOrEmpty(rainStr))
                {
                    rainAmount = decimal.Parse(rainStr);
                    max = max > rainAmount ? max : rainAmount;
                    total += rainAmount;
                    record++;
                }
                else
                {
                    miss++;

                }
            };

            for (int i = 0; i < monthRain.Length; i++)
            {
                writer.WriteLine(string.Join(",", monthRain[i]));
            }
        }
    }
}
