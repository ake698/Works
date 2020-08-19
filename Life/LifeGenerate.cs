using Display;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Life
{
    public class LifeGenerate
    {
        private LifeParams lifeParams;
        private Grid grid;
        private Cell[][] cells;

        public LifeGenerate(string [] args)
        {
            lifeParams = new LifeParams(args);
            grid = GenerateCell(lifeParams);
            cells = grid.GetCells();
            PrintParms();
        }

        public void PrintParms()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[{DateTime.Now.ToString("HH:MM:ss:fff")}]Success: Command line arguments processed.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"[{DateTime.Now.ToString("HH:MM:ss:fff")}]The program will use the following settings:");
            Console.WriteLine();
            Console.WriteLine($"\t\t Input File: {lifeParams.FilePath??"N/A"}");
            Console.WriteLine($"\t\tGenerations: {lifeParams.Generations}");
            Console.WriteLine($"\t       Refresh Rate: {lifeParams.Rate} updates/s");
            var periodicStr = lifeParams.Periodic == true ? "Yes" : "No";
            Console.WriteLine($"\t\t   Periodic: {periodicStr}");
            Console.WriteLine($"\t\t        Row: {lifeParams.Rows}");
            Console.WriteLine($"\t\t    Columns: {lifeParams.Colums}");
            Console.WriteLine($"\t      Random Factor: {(lifeParams.Random * 100).ToString("#0.00")} %");
            var stepMode = lifeParams.Step == true ? "Yes" : "No";
            Console.WriteLine($"\t\t  Step Mode: {stepMode}");
            Console.WriteLine($"[{DateTime.Now.ToString("HH:MM:ss:fff")}]Press spacebar to start simulation...");
            Console.WriteLine();
            Console.ReadKey();
        }

        public void Start()
        {
            grid.InitializeWindow();

            // Set the footnote (appears in the bottom left of the screen).
            grid.SetFootnote("Smiley");
            grid.Render();
            Console.ReadKey();
            Stopwatch watch = new Stopwatch();
            
            for (int i = 1; i <= lifeParams.Generations; i++)
            {
                watch.Restart();
                NextGenerate();
                grid.SetFootnote($"Iteration:{i}")   ;
                // Render updates to the console window...
                grid.Render();
                if (lifeParams.Step)
                {
                    Console.ReadKey();
                }
                else
                {
                    while (watch.ElapsedMilliseconds < (1000 / lifeParams.Rate)) ;
                    
                }
            }

            End();

        }


        private void End()
        {
            // complete
            grid.IsComplete = true;
            grid.Render();
            Console.ReadKey();

            // normal
            grid.RevertWindow();
            Console.WriteLine($"[{DateTime.Now.ToString("HH:MM:ss:fff")}]Press spacebar to close program...");
            Console.ReadKey();
        }
        #region Generate
        
        private void NextGenerate()
        {
            List<int[]> list = new List<int[]>();
            for (int i = 0; i < lifeParams.Rows; i++)
            {
                for (int p = 0; p < lifeParams.Colums; p++)
                {
                    var a = cells[i][p];
                    var state = a.GetState();
                    bool result = LiveOrDead(i, p, state);
                    if (result) {
                        int[] arr = new int[] { i, p };
                        list.Add(arr);
                    }
                    //grid.UpdateCell(i, p, reuslt == true ? CellState.Full : CellState.Blank);
                }
            }

            for (int i = 0; i < lifeParams.Rows; i++)
            {
                for (int p = 0; p < lifeParams.Colums; p++)
                {
                    grid.UpdateCell(i, p, CellState.Blank);
                }
            }

            list.ForEach(x => grid.UpdateCell(x[0], x[1], CellState.Full));
        }


        private bool LiveOrDead(int x, int y, CellState state)
        {
            int count = CountAlive(x, y);
            if(state == CellState.Full)
            {
                if(count == 2 || count == 3)
                {
                    return true;
                }
            }
            else
            {
                if (count == 3) return true;
            }
            return false;
        }

        private int CountAlive(int x, int y)
        {
            int live = 0;
            Cell tmp;
            
            for (int i = -1; i <= 1; i++)
            {
                for (int p = -1; p <= 1; p++)
                {
                    int tx = x, ty = y;
                    if (i == p && i == 0) continue;
                    if (lifeParams.Periodic)
                    {
                        if (tx + i < 0)
                        {
                            tx = cells.Length - 1;
                        }
                        else if(tx + i > cells.Length - 1)
                        {
                            tx = 0;
                        }
                        else
                        {
                            tx += i;
                        }
                        if(ty + p < 0)
                        {
                            ty = cells[0].Length - 1;
                        }
                        else if(ty + p > cells[0].Length - 1)
                        {
                            ty = 0;
                        }
                        else
                        {
                            ty += p;
                        }
                    }
                    else
                    {
                        if (tx + i < 0 || tx + i > cells.Length - 1  || ty + p < 0 || ty + p > cells[0].Length - 1) continue;
                        tx = tx + i;
                        ty = ty + p;
                    }

                    tmp = cells[tx][ty];
                    if (tmp.GetState() == CellState.Full) live++;
                }
            }

            return live;
        }

        #endregion

        #region DataSeed
        private Grid GenerateCell(LifeParams args)
        {
            Grid grid;
            if (string.IsNullOrEmpty(args.FilePath))
            {
                // random
                grid = new Grid(args.Rows, args.Colums);
                int max = (int)(args.Random * 100);
                for (int i = 0; i < args.Rows; i++)
                {
                    for (int p = 0; p < args.Colums; p++)
                    {
                        Random random = new Random();
                        int next = random.Next(1, 101);
                        if (next <= max) grid.UpdateCell(i, p, CellState.Full);
                        else grid.UpdateCell(i, p, CellState.Blank);
                    }
                }
            }
            else
            {
                // seed file
                string filePath = args.FilePath;
                if (!File.Exists(filePath)) throw new ArgumentOutOfRangeException("ff");
                var fparrs = filePath.Split(@"\");
                string fileName = fparrs[fparrs.Length - 1];
                var arrs = fileName.Split(".")[0].Split("_");
                if (arrs.Length > 1)
                {
                    // has rows columns
                    var rc = arrs[1].Split("x");
                    args.Colums = int.Parse(rc[1]);
                    args.Rows = int.Parse(rc[0]);
                    Console.WriteLine($"{args.Rows} x {args.Colums}");
                    //Console.ReadKey();
                }
                grid = new Grid(args.Rows, args.Colums);
                GenerateCell(grid, filePath);
            }
            return grid;
        }

        private void GenerateCell(Grid grid, string filePath)
        {
            TextReader reader = new StreamReader(filePath);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.StartsWith("#")) continue;
                var arrs = line.Split(" ");
                grid.UpdateCell(int.Parse(arrs[0]), int.Parse(arrs[1]), CellState.Full);
            }
        }
        #endregion
    }
}
