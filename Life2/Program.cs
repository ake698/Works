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

            LoggingSteadyState(isSteadyState, periodicity);
            OutPutFile(options.OutputFile, universe);
            Logging.Message("Press spacebar to exit program...");
            WaitSpacebar();
        }

        private static void LoggingSteadyState(bool isSteadyState, string periodicity)
        {
            if (isSteadyState)
            {
                Logging.Message($"Steady-state detected... periodicity = {periodicity}");
            }
            else 
            { 
                Logging.Message($"Steady-state not detected...");
            }
        }
        private static void OutPutFile(string outputFile, int[,] universe)
        {
            if (!(string.IsNullOrEmpty(outputFile)))
            {
                Logging.Message($"Final generation written to file: {outputFile}");
                CreateOutputFileDir(outputFile);
                FileStream file = new FileStream(outputFile, FileMode.Create);
                StreamWriter sw = new StreamWriter(file);
                sw.WriteLine("#version=2.0");
                for (int i = 0; i < universe.GetLength(0); i++)
                {
                    for (int p = 0; p < universe.GetLength(1); p++)
                    {
                        if(universe[i,p] == (int)CellState.Full)
                        {
                            sw.WriteLine($"(o) cell: {i}, {p}");
                        }
                    }
                }
                        sw.Close();
                file.Close();
            }
        }

        private static void CreateOutputFileDir(string outputFile)
        {
            var fileFullPath = Path.GetFullPath(outputFile);
            var dir = Path.GetDirectoryName(fileFullPath);
            var dirFullPath = Path.GetFullPath(dir);
            Directory.CreateDirectory(dirFullPath);
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
                    _ = (index >= order) ? index--: index++;
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
                            if (universe[Modulus(r, rows), Modulus(c, columns)] == 1)
                                neighbours += universe[Modulus(r, rows), Modulus(c, columns)];
                        }
                    }
                    _ = (index >= order) ? index++ : index--;
                }
            }
            if (center)
            {
                if (universe[i, j] == 1)
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
    }
}
