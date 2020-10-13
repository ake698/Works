using Display;
using System;
using System.IO;

namespace Life
{
    public class InitializeUniverse
    {
        private readonly string inputFile;
        private readonly int[,] universe;
        private readonly double randomFactor;

        public InitializeUniverse(int rows, int columns, string inputFile, double randomFactor)
        {
            universe = new int[rows, columns];
            this.inputFile = inputFile;
            this.randomFactor = randomFactor;
        }

        public int[,] Initialize()
        {
            int[,] universe;

            if (inputFile == null)
            {
                universe = InitializeFromRandom();
            }
            else
            {
                try
                {
                    universe = InitializeFromFile();
                }
                catch
                {
                    Logging.Warning($"Error initializing universe using \'{inputFile}\'. Reverting to randomised universe...");
                    universe = InitializeFromRandom();
                }
            }

            return universe;
        }

        private int[,] InitializeFromRandom()
        {
            Random random = new Random();
            for (int i = 0; i < universe.GetLength(0); i++)
            {
                for (int j = 0; j < universe.GetLength(1); j++)
                {
                    universe[i, j] = random.NextDouble() < randomFactor ? 1 : 0;
                }
            }

            return universe;
        }

        private int[,] InitializeFromFile()
        {
            using (StreamReader reader = new StreamReader(inputFile))
            {
                string line = reader.ReadLine();
                double version = double.Parse(line.Split("=")[^1]);
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    if (version == 1)
                    {
                        ParseFileVersion1(line);
                    }
                    else
                    {
                        ParseFileVersion2(line);
                    }
                }
            }
            return universe;
        }

        private void ParseFileVersion1(string line)
        {
            string[] elements = line.Split(" ");

            int row = int.Parse(elements[0]);
            int column = int.Parse(elements[1]);
            universe[row, column] = (int)CellState.Full;
        }

        private void ParseFileVersion2(string line)
        {
            string[] elements = line.Replace(",", "").Replace(" :",":").Split(" ");
            string type = elements[1];
            if (type.Equals("cell:", StringComparison.OrdinalIgnoreCase))
            {
                if (elements[0].Contains("o"))
                {
                    int row = int.Parse(elements[2]);
                    int column = int.Parse(elements[3]);
                    universe[row, column] = (int)CellState.Full;
                }
            }
            else
            {
                ParseFileVersion2WithStructure(elements);
            }
        }

        private void ParseFileVersion2WithStructure(string[] elements)
        {

            if (elements[1].Equals("rectangle:", StringComparison.OrdinalIgnoreCase))
            {
                ParseFileVersion2WithRectangle(elements);
            }
            else
            {
                ParseFileVersion2WithEllipse(elements);
            }
        }

        private void ParseFileVersion2WithRectangle(string[] elements)
        {
            int bottom_row = int.Parse(elements[2]);
            int bottom_column = int.Parse(elements[3]);
            int top_row = int.Parse(elements[4]);
            int top_column = int.Parse(elements[5]);
            // rectangle
            for (int i = bottom_row; i <= top_row; i++)
            {
                for (int p = bottom_column; p <= top_column; p++)
                {
                    if (elements[0].Contains("o"))
                        universe[i, p] = (int)CellState.Full;
                    else
                        universe[i, p] = (int)CellState.Blank;
                }
            }
        }

        private void ParseFileVersion2WithEllipse(string[] elements)
        {
            // ellipse
            int bottom_row = int.Parse(elements[2]);
            int bottom_column = int.Parse(elements[3]);
            int top_row = int.Parse(elements[4]);
            int top_column = int.Parse(elements[5]);
            double centerRow = (double)(bottom_row + top_row + 1) / 2;
            double centerColumn = (double)(bottom_column + top_column + 1) / 2;
            double lenthRow = Math.Abs(top_row - bottom_row ) + 1;
            double lengthColumn = Math.Abs(top_column - bottom_column) + 1;
            for (int i = bottom_row; i <= top_row; i++)
            {
                for (int p = bottom_column; p <= top_column; p++)
                {
                    var x = 4 * Math.Pow(i + 0.5 - centerRow, 2) * lengthColumn * lengthColumn;
                    var y = 4 * Math.Pow(p + 0.5 - centerColumn, 2) * lenthRow * lenthRow;
                    var value = 4 * Math.Pow(i +0.5 - centerRow, 2)  * lengthColumn * lengthColumn
                        + 4 * Math.Pow(p + 0.5 - centerColumn, 2) * lenthRow * lenthRow;
                    if (value <= lenthRow * lenthRow * lengthColumn * lengthColumn)
                    {
                        if (elements[0].Contains("o"))
                            universe[i, p] = (int)CellState.Full;
                        else
                            universe[i, p] = (int)CellState.Blank;
                    }
                }
            }
        }
    }
}
