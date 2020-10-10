using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Display;

namespace Life
{
    class Program
    {
        static void Main(string[] args)
        {
            Options options = ArgumentProcessor.Process(args);
            //int[,] universe = InitializeUniverse(options);
            bool isSteadyState = false;
            string periodicity = "N/A";
            int[,] universe = new InitializeUniverse(options.Rows, options.Columns, options.InputFile, options.RandomFactor).Initialize();
            Grid grid = new Grid(options.Rows, options.Columns);
            var records = new List<int[,]>();

            Logging.Message("Press spacebar to begin the game...");
            WaitSpacebar();

            grid.InitializeWindow();

            Stopwatch stopwatch = new Stopwatch();

            int iteration = 0;
            while (iteration <= options.Generations)
            {
                stopwatch.Restart();

                if (iteration != 0)
                {
                    universe = EvolveUniverse(universe, options);
                }

                UpdateGrid(grid, universe);
                // check steady state
                isSteadyState = IsSteadyState(records, universe, options.Memory, out periodicity);

                grid.SetFootnote($"Generation: {iteration++}");
                grid.Render();

                if (options.StepMode)
                {
                    WaitSpacebar();
                }
                else
                {
                    while (stopwatch.ElapsedMilliseconds < 1000 / options.UpdateRate) ;
                }
                if (isSteadyState) break;
            }

            grid.IsComplete = true;
            grid.Render();
            WaitSpacebar();

            grid.RevertWindow();

            if (isSteadyState) Logging.Message($"Steady-state detected... periodicity = {periodicity}");
            else Logging.Message($"Steady-state not detected...");
            Logging.Message("Press spacebar to exit program...");
            WaitSpacebar();
        }

        private static int[,] EvolveUniverse(int[,] universe, Options options)
        {
            const int ALIVE = 1;
            const int DEAD = 0;

            int rows = universe.GetLength(0);
            int columns = universe.GetLength(1);

            int[,] buffer = new int[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    int neighbours = CountNeighbours(universe, i, j, options.Periodic, options.Neighbourhood, options.Order, options.Center);

                    if (universe[i, j] == ALIVE && (Array.IndexOf(options.Survival, neighbours) >= 0))
                    {
                        buffer[i, j] = ALIVE;
                    }
                    else if (universe[i, j] != ALIVE && (Array.IndexOf(options.Birth, neighbours) >= 0))
                    {
                        buffer[i, j] = ALIVE;
                    }
                    else
                    {
                        if (universe[i, j] == ALIVE && options.Ghost)
                        {
                            buffer[i, j] = (int)CellState.Dark;
                        }
                        else if (universe[i, j] > DEAD && universe[i, j] != (int)CellState.Light && options.Ghost)
                        {
                            buffer[i, j] = universe[i, j] + 1;
                        }
                        else
                        {
                            buffer[i, j] = DEAD;
                        }
                    }
                }
            }

            return buffer.Clone() as int[,];
        }

        private static int CountNeighbours(int[,] universe, int i, int j, bool periodic, Neighbourhood neighbourhood, int order, bool center)
        {
            int rows = universe.GetLength(0);
            int columns = universe.GetLength(1);

            int neighbours = 0;
            int index = 0;

            if (!periodic)
            {
                for (int r = i - order; r <= i + order; r++)
                {
                    for (int c = j - order; c <= j + order; c++)
                    {
                        if ((r != i || c != j) && r >= 0 && r < rows && c >= 0 && c < columns)
                        {
                            if (Math.Abs(j - c) > index && Neighbourhood.VONNEUMANN == neighbourhood) continue;
                            if(universe[r, c] == 1)
                                neighbours += universe[r, c];
                        }
                    }
                    _ = (index >= order) ? index++ : index--;
                }
            }
            else
            {
                for (int r = i - order; r <= i + order; r++)
                {
                    for (int c = j - order; c <= j + order; c++)
                    {
                        if (r != i || c != j)
                        {
                            if (Math.Abs(j - c) > index && Neighbourhood.VONNEUMANN == neighbourhood) continue;
                            neighbours += universe[Modulus(r, rows), Modulus(c, columns)];
                        }
                    }
                    _ = (index >= order) ? index++ : index--;
                }
            }
            if (center)
            {
                neighbours += universe[i, j];
            }

            return neighbours;
        }

        // "Borrowed" from: https://stackoverflow.com/questions/1082917/mod-of-negative-number-is-melting-my-brain
        private static int Modulus(int x, int m)
        {
            return (x % m + m) % m;
        }

        private static void UpdateGrid(Grid grid, int[,] universe)
        {
            for (int i = 0; i < universe.GetLength(0); i++)
            {
                for (int j = 0; j < universe.GetLength(1); j++)
                {
                    grid.UpdateCell(i, j, (CellState)universe[i, j]);
                }
            }
        }

        private static void WaitSpacebar()
        {
            while (Console.ReadKey(true).Key != ConsoleKey.Spacebar) ;
        }

        private static bool IsSteadyState(List<int[,]> records, int[,] universe, int memory, out string periodicity)
        {
            periodicity = "N/A";
            if (IsAllDead(universe))
            {
                return true;
            }
            int generates = ContainUniverse(records, universe);
            periodicity = (generates > 1) ? generates.ToString() : periodicity;
            if (generates > 0)
            {
                return true;
            }
            AddMemory(records, universe, memory);
            return false;
        }

        private static void AddMemory(List<int[,]> records, int[,] universe, int memory)
        {
            if (records.Count >= memory)
            {
                records.RemoveAt(0);
            }
            records.Add(universe);
        }

        private static bool IsAllDead(int[,] universe)
        {
            for (int i = 0; i < universe.GetLength(0); i++)
            {
                for (int p = 0; p < universe.GetLength(1); p++)
                { 
                    if(universe[i ,p] == (int)CellState.Full)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static int ContainUniverse(List<int[,]> records, int[,] universe)
        {
            for (int i = 0; i < records.Count; i++)
            {
                if(EqualUniverse(records[i], universe))
                {
                    return records.Count - i;
                }
            }
            return -1;
        }

        private static bool EqualUniverse(int[,] source, int[,] target)
        {
            for (int i = 0; i < source.GetLength(0); i++)
            {
                for (int p = 0; p < source.GetLength(1); p++)
                {
                    int s = source[i, p];
                    int t = target[i, p];
                    if(source[i, p] != target[i, p])
                    {
                        if(s== (int)CellState.Full || t == (int)CellState.Full)
                        return false;
                    }
                }
            }
            return true;
        }


        #region delete
        //private static int[,] InitializeUniverse(Options options)
        //{
        //    int[,] universe;

        //    if (options.InputFile == null)
        //    {
        //        universe = InitializeFromRandom(options.Rows, options.Columns, options.RandomFactor);
        //    }
        //    else
        //    {
        //        try
        //        {
        //            universe = InitializeFromFile(options.Rows, options.Columns, options.InputFile);
        //            //universe = new InitializeFromFile(options.Rows, options.Columns, options.InputFile).Initialize();
        //        }
        //        catch
        //        {
        //            Logging.Warning($"Error initializing universe using \'{options.InputFile}\'. Reverting to randomised universe...");
        //            universe = InitializeFromRandom(options.Rows, options.Columns, options.RandomFactor);
        //        }
        //    }

        //    return universe;
        //}

        //private static int[,] InitializeFromRandom(int rows, int columns, double randomFactor)
        //{
        //    int[,] universe = new int[rows, columns];

        //    Random random = new Random();
        //    for (int i = 0; i < universe.GetLength(0); i++)
        //    {
        //        for (int j = 0; j < universe.GetLength(1); j++)
        //        {
        //            universe[i, j] = random.NextDouble() < randomFactor ? 1 : 0;
        //        }
        //    }

        //    return universe;
        //}

        //private static int[,] InitializeFromFile(int rows, int columns, string inputFile)
        //{
        //    int[,] universe = new int[rows, columns];
        //    using (StreamReader reader = new StreamReader(inputFile))
        //    {
        //        string line = reader.ReadLine();
        //        double version = double.Parse(line.Split("=")[^1]);
        //        while (!reader.EndOfStream)
        //        {
        //            line = reader.ReadLine();
        //            if(version == 1)
        //            {
        //                ParseFileVersion1(line, universe);
        //            }
        //            else
        //            {
        //                ParseFileVersion2(line, universe);
        //            }
        //        }
        //    }

        //    return universe;
        //}

        //private static void ParseFileVersion1(string line, int[,] universe)
        //{
        //    string[] elements = line.Split(" ");

        //    int row = int.Parse(elements[0]);
        //    int column = int.Parse(elements[1]);
        //    universe[row, column] = (int)CellState.Full;
        //}

        //private static void ParseFileVersion2(string line, int[,] universe)
        //{
        //    string[] elements = line.Replace(",", "").Split(" ");
        //    string type = elements[1];
        //    if(type.Equals("cell:", StringComparison.OrdinalIgnoreCase))
        //    {
        //        if (elements[0].Contains("o"))
        //        {
        //            int row = int.Parse(elements[2]);
        //            int column = int.Parse(elements[3]);
        //            universe[row, column] = (int)CellState.Full;
        //        }
        //    }
        //    else
        //    {
        //        ParseFileVersion2WithStructure(elements, universe);
        //    }
        //}

        //private static void ParseFileVersion2WithStructure(string[] elements, int[,] universe)
        //{

        //    if(elements[1].Equals("rectangle", StringComparison.OrdinalIgnoreCase))
        //    {
        //        ParseFileVersion2WithRectangle(elements, universe);
        //    }
        //    else
        //    {
        //        ParseFileVersion2WithEllipse(elements, universe);
        //    }
        //}

        //private static void ParseFileVersion2WithRectangle(string[] elements, int[,] universe)
        //{
        //    int bottom_row = int.Parse(elements[3]);
        //    int bottom_column = int.Parse(elements[4]);
        //    int top_row = int.Parse(elements[5]);
        //    int top_column = int.Parse(elements[6]);
        //    // rectangle
        //    for (int i = bottom_row; i <= top_row; i++)
        //    {
        //        for (int p = bottom_column; p <= top_column; p++)
        //        {
        //            if (elements[0].Contains("o"))
        //                universe[i, p] = (int)CellState.Full;
        //            else
        //                universe[i, p] = (int)CellState.Blank;
        //        }
        //    }
        //}

        //private static void ParseFileVersion2WithEllipse(string[] elements, int[,] universe)
        //{
        //    // ellipse
        //    int bottom_row = int.Parse(elements[3]);
        //    int bottom_column = int.Parse(elements[4]);
        //    int top_row = int.Parse(elements[5]);
        //    int top_column = int.Parse(elements[6]);
        //    int centerRow = (bottom_row + top_row) / 2;
        //    int centerColumn = (bottom_column + top_column) / 2;
        //    int lenthRow = (top_row - bottom_row + 1);
        //    int lengthColumn = (top_column - bottom_column + 1);
        //    for (int i = bottom_row; i <= top_row; i++)
        //    {
        //        for (int p = bottom_column; p <= top_column; p++)
        //        {
        //            var value = 4 * (i - 0.5 - centerRow) * (i - 0.5 - centerRow) * lengthColumn * lengthColumn
        //                + 4 * (p - 0.5 - centerColumn) * (p - 0.5 - centerColumn) * lenthRow * lenthRow;
        //            if (value <= lenthRow * lenthRow * lengthColumn * lengthColumn)
        //            {
        //                if (elements[0].Contains("o"))
        //                    universe[i, p] = (int)CellState.Full;
        //                else
        //                    universe[i, p] = (int)CellState.Blank;
        //            }
        //        }
        //    }
        //}
        #endregion
    }
}
