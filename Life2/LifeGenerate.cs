using Display;
using Life2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace Life
{
    public class LifeGenerate
    {
        private LifeParams lifeParams;
        private Grid grid;
        private Cell[][] cells;
        private int version = 1;
        private List<Cell[][]> ghost;
        private List<Cell[][]> memory;

        public LifeGenerate(string[] args)
        {
            // Initialization parameters
            lifeParams = new LifeParams(args);
            // Initialization grid
            grid = GenerateCell(lifeParams);
            cells = grid.GetCells();
            PrintParams();
        }

        /// <summary>
        /// Display the runtime settings
        /// </summary>
        private void PrintParams()
        {
            Console.WriteLine($"[{DateTime.Now.ToString("HH:MM:ss:fff")}] The program will use the following settings:");
            Console.WriteLine();
            Console.WriteLine($"\t\t Input File: {lifeParams.FilePath ?? "N/A"}");
            Console.WriteLine($"\t\tOutput File: {lifeParams.OutPutFilePath ?? "N/A"}");
            Console.WriteLine($"\t\tGenerations: {lifeParams.Generations}");
            Console.WriteLine($"\t\t     Memory: {lifeParams.Memory}");
            Console.WriteLine($"\t       Refresh Rate: {lifeParams.Rate} updates/s");
            Console.WriteLine($"\t\t     Roules: {lifeParams.Rules}");
            Console.WriteLine($"\t      Neighbourhood: {lifeParams.Neighbourhood} ({lifeParams.Order})");
            var periodicStr = lifeParams.Periodic == true ? "Yes" : "No";
            Console.WriteLine($"\t\t   Periodic: {periodicStr}");
            Console.WriteLine($"\t\t        Row: {lifeParams.Rows}");
            Console.WriteLine($"\t\t    Columns: {lifeParams.Colums}");
            Console.WriteLine($"\t      Random Factor: {(lifeParams.Random * 100).ToString("#0.00")} %");
            var stepMode = lifeParams.Step == true ? "Yes" : "No";
            Console.WriteLine($"\t\t  Step Mode: {stepMode}");
            var ghostMode = lifeParams.Ghost == true ? "Yes" : "No";
            Console.WriteLine($"\t\t Ghost Mode: {ghostMode}");
            Console.WriteLine();
            Console.WriteLine($"[{DateTime.Now:HH:MM:ss:fff}] Press spacebar to start simulation...");
            Console.ReadKey();
        }

        public void Start()
        {
            grid.InitializeWindow();
            grid.SetFootnote($"Iteration 0");
            grid.Render();
            Console.ReadKey();

            Stopwatch watch = new Stopwatch();

            for (int i = 1; i <= lifeParams.Generations; i++)
            {
                watch.Restart();
                NextGenerate();
                grid.SetFootnote($"Iteration:{i}");
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

            // go back
            grid.RevertWindow();
            Console.WriteLine($"[{DateTime.Now.ToString("HH:MM:ss:fff")}] Press spacebar to close program...");
            Console.ReadKey();
        }


        #region Generate
        /// <summary>
        /// Show life
        /// </summary>
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
                    if (result)
                    {
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

        /// <summary>
        /// To determine whether its next generation is alive or not
        /// </summary>
        /// <param name="x">row</param>
        /// <param name="y">column</param>
        /// <param name="state">cell state</param>
        /// <returns></returns>
        private bool LiveOrDead(int x, int y, CellState state)
        {
            if (x == 22 && y == 22)
            {
                var a = "dd";
            }
            int count = CountAlive(x, y);
            if (state == CellState.Full)
            {
                for (int i = 0; i < lifeParams.Survival.Length; i++)
                {
                    if (count == lifeParams.Survival[i]) return true;
                }
            }
            else
            {
                for (int i = 0; i < lifeParams.Birth.Length; i++)
                {
                    if (count == lifeParams.Birth[i]) return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Count the living cells around it
        /// </summary>
        /// <param name="x">row</param>
        /// <param name="y">column</param>
        /// <returns></returns>
        private int CountAliveOld(int x, int y)
        {
            int live = 0;
            Cell tmp;
            // Moor
            for (int i = -1 * lifeParams.Order; i <= 1 * lifeParams.Order; i++)
            {
                for (int p = -1 * lifeParams.Order; p <= 1 * lifeParams.Order; p++)
                {
                    int tx = x, ty = y;
                    // self
                    if (i == p && i == 0 && !lifeParams.Center) continue;
                    if (lifeParams.Periodic)
                    {
                        if (tx + i < 0)
                        {
                            tx = cells.Length - Math.Abs(tx + i);
                        }
                        else if (tx + i > cells.Length - 1)
                        {
                            tx = (tx + i) % cells.Length;
                        }
                        else
                        {
                            tx += i;
                        }
                        if (ty + p < 0)
                        {
                            ty = cells[0].Length - Math.Abs(ty + p);
                        }
                        else if (ty + p > cells[0].Length - 1)
                        {
                            ty = (ty + p) % cells[0].Length;
                        }
                        else
                        {
                            ty += p;
                        }
                    }
                    else
                    {
                        if (tx + i < 0 || tx + i > cells.Length - 1 || ty + p < 0 || ty + p > cells[0].Length - 1) continue;
                        tx = tx + i;
                        ty = ty + p;
                    }

                    tmp = cells[tx][ty];
                    if (tmp.GetState() == CellState.Full) live++;
                }
            }
            return live;
        }

        private int CountAlive(int x, int y)
        {
            int live = 0;
            int order = 0;
            Cell tmp;
            for (int i = -1 * lifeParams.Order; i <= 1 * lifeParams.Order; i++)
            {
                for (int p = -1 * lifeParams.Order; p <= 1 * lifeParams.Order; p++)
                {
                    if (i == p && i == 0 && !lifeParams.Center) continue;
                    int tx = x, ty = y;
                    if (lifeParams.Periodic)
                    {
                        int dis = 0;
                        if (tx + i < 0)
                        {
                            tx = cells.Length - Math.Abs(tx + i);
                        }
                        else if (tx + i > cells.Length - 1)
                        {
                            tx = (tx + i) % cells.Length;
                        }
                        else
                        {
                            tx += i;
                        }
                        if (ty + p < 0)
                        {
                            ty = cells[0].Length - Math.Abs(ty + p);
                            //dis = cells[0].Length - p + y;
                        }
                        else if (ty + p > cells[0].Length - 1)
                        {
                            ty = (ty + p) % cells[0].Length;
                            //dis = cells[0].Length - y + p;
                        }
                        else
                        {
                            ty += p;
                            //dis = p;
                        }
                        if (p > order && lifeParams.Neighbourhood == Neighbourhood.VONNEUMANN) continue;
                    }
                    else
                    {
                        if (tx + i < 0 || tx + i > cells.Length - 1 || ty + p < 0 || ty + p > cells[0].Length - 1) continue;
                        if (Math.Abs(p) > order && lifeParams.Neighbourhood == Neighbourhood.VONNEUMANN) continue;
                        tx = tx + i;
                        ty = ty + p;
                        if (i == p && i== 0)
                        {
                            var c = "dd";
                        }
                    }
                    tmp = cells[tx][ty];
                    if (tmp.GetState() == CellState.Full) live++;
                }

                _ = order >= lifeParams.Order ? order-- : order++;
            }
            return live;
        }

        #endregion


        #region DataSeed
        /// <summary>
        /// Generate cells according settings
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private Grid GenerateCell(LifeParams args)
        {
            Grid grid;
            if (string.IsNullOrEmpty(args.FilePath))
            {
                // No seed files automatically generate seed data
                grid = new Grid(args.Rows, args.Colums);
                int max = (int)(args.Random * 100);
                for (int i = 0; i < args.Rows; i++)
                {
                    for (int p = 0; p < args.Colums; p++)
                    {
                        Random random = new Random();
                        int next = random.Next(1, 101);
                        if (next <= max)
                        {
                            grid.UpdateCell(i, p, CellState.Full);
                        }
                        else
                        {
                            grid.UpdateCell(i, p, CellState.Blank);
                        }
                    }
                }
            }
            else
            {
                // Parse seed file information
                string filePath = args.FilePath;

                var fparrs = filePath.Split(@"\");
                string fileName = fparrs[fparrs.Length - 1];
                string pattern = @"(\d+)x(\d+)";
                var matches = Regex.Matches(fileName, pattern);
                if (matches.Count > 0)
                {
                    var rc = matches[0].Value.Split("x");
                    args.Colums = int.Parse(rc[1]);
                    args.Rows = int.Parse(rc[0]);
                }
                grid = new Grid(args.Rows, args.Colums);
                GenerateCell(grid, filePath);
            }
            return grid;
        }

        /// <summary>
        /// Generate cells from seed file
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="filePath"></param>
        private void GenerateCell(Grid grid, string filePath)
        {
            TextReader reader = new StreamReader(filePath);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.StartsWith("#")) 
                {
                    var versionInfos = line.Split('=');
                    int.TryParse(versionInfos[1], out version);
                    continue;
                };
                if(version == 1)
                {
                    var arrs = line.Split(" ");
                    grid.UpdateCell(int.Parse(arrs[0]), int.Parse(arrs[1]), CellState.Full);
                }
                else
                {
                    var arrs = line.Replace(",","").Split(" ");
                    
                    string type = arrs[1];
                    if (type.Equals("cell", StringComparison.OrdinalIgnoreCase))
                    {
                        if (arrs[0].Contains("o"))
                            grid.UpdateCell(int.Parse(arrs[2]), int.Parse(arrs[3]), CellState.Full);
                    }
                    else 
                    {
                         GenerateCellByStructure(arrs, grid);
                    }
                    
                }
                
            }
        }

        private void GenerateCellByStructure(string[] arrs, Grid grid)
        {
            int bottom_row = int.Parse(arrs[3]);
            int bottom_column = int.Parse(arrs[4]);
            int top_row = int.Parse(arrs[5]);
            int top_column = int.Parse(arrs[6]);
            if (arrs[1].Equals("rectangle", StringComparison.OrdinalIgnoreCase))
            {
                //rectangle
                for (int i = bottom_row; i <= top_row; i++)
                {
                    for (int p = bottom_column; p <= top_column; p++)
                    {
                        if (arrs[0].Contains("o"))
                            grid.UpdateCell(i, p, CellState.Full);
                        else
                            grid.UpdateCell(i, p, CellState.Blank);
                    }
                }
            }
            else
            {
                // ellipse
                int centerRow = (bottom_row + top_row) / 2;
                int centerColumn = (bottom_column + top_column) / 2;
                int lenthRow = (top_row - bottom_row + 1);
                int lengthColumn = (top_column - bottom_column + 1);
                for (int i = bottom_row; i <= top_row; i++)
                {
                    for (int p = bottom_column; p <= top_column; p++)
                    {
                        var value = 4 * (i - 0.5 - centerRow) * (i - 0.5 - centerRow) * lengthColumn * lengthColumn
                            + 4 * (p - 0.5 - centerColumn) * (p - 0.5 - centerColumn) * lenthRow * lenthRow;
                        if(value <= lenthRow * lenthRow * lengthColumn * lengthColumn)
                        {
                            if (arrs[0].Contains("o"))
                                grid.UpdateCell(i, p, CellState.Full);
                            else
                                grid.UpdateCell(i, p, CellState.Blank);
                        }
                    }
                }
            }
        }
        #endregion
    }
}
