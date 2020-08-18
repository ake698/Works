using System;
using System.IO;

namespace WeatherAnalysis
{
    public class Rainfall
    {
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
            //            if (args.Length > 0)
            //            {
            //                string path = args[0];
            //                using (TextReader reader = new StreamReader(args[0]))
            //                {
            //                    PrintRainfallSummary(path, reader);
            //                }
            //            }
            //            else
            //            {
            //                string path = "C:\\Path\\To\\File\\three_days_data.csv";
            //                string threeDaysData = @"Product code,Bureau of Meteorology station number,Year,Month,Day,Rainfall amount (millimetres),Period over which rainfall was measured (days),Quality
            //IDCJAC0009,040913,2012,01,01,2.4,1,N
            //IDCJAC0009,040913,2012,01,02,,,
            //IDCJAC0009,040913,2012,01,03,0.0,1,N";

            //                using (TextReader reader = new StringReader(threeDaysData))
            //                {
            //                    PrintRainfallSummary(path, reader);
            //                }
            //            }
            TextReader reader = new StreamReader(@"C:\Users\sq\Desktop\11\Rainfall\Data\IDCJAC0009_040913_2017_Data.csv");
            PrintRainfallSummary(@"C:\Users\sq\Desktop\11\Rainfall\Data\IDCJAC0009_040913_2017_Data.csv", reader);
        }

        /// <summary>
        ///     Parse the rainfall data and compute a summary according to 
        ///     specification. Results are written to standard output stream.
        /// <para>
        ///     Refer to question text and sample data files for specifics.
        /// </para>
        /// </summary>
        /// <param name="filePath">
        ///     A string containing the path to the data file. This is for
        ///     display purposes only. Do not attempt to open this file.
        /// </param>
        /// <param name="reader">
        ///     A TextReader which has been attached to the data file and is 
        ///     ready to parse content.
        /// </param>
        /// <returns>
        ///		This method does not return a value.
        /// </returns>
        //)

        // INSERT METHOD HERE. 
        public static void PrintRainfallSummary(string filePath, TextReader reader)
        {
            var fparrs = filePath.Split(@"/");

            Console.WriteLine($"Reading data from {fparrs[fparrs.Length - 1]}...");
            Console.WriteLine();
            string line;
            int count = 0, record = 0;

            string year = "2020";
            decimal total = 0, average=0 ,max=0 , miss = 0;
            while ((line = reader.ReadLine()) != null)
            {
                count++;
                if (count < 2)
                {
                    continue;
                }
                var arrs = line.Split(",");
                year = arrs[2];

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
                //Console.WriteLine(line);
                //var flag = decimal.TryParse(rainStr, out rainAmount);
                //if (flag)
                //{
                //    max = max > rainAmount?max:rainAmount;
                //    total += rainAmount;
                //    record++;

                //}
                //else
                //{
                //    Console.WriteLine(rainStr);
                //    Console.WriteLine(line);
                //    miss++;
                //}
            }
            //Console.WriteLine(miss);
            //Console.WriteLine(record);
            //Console.WriteLine(count);
            //Console.WriteLine(miss/record);
            average = Math.Round(total / record, 2);
            miss = Math.Round((miss / (count-1))*100, 2);
            Console.WriteLine($"Year: {year}");
            Console.WriteLine($"Total Rainfall: {total.ToString("#0.00")} mm");
            Console.WriteLine($"Average Rainfall: {average.ToString("#0.00")} mm / day");
            Console.WriteLine($"Maximum Daily Rainfall: {max.ToString("#0.00")} mm");
            Console.WriteLine($"Missing Data: {miss.ToString("#0.00")}%");
        }
    }
}
