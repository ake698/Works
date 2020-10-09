using Display;
using Life2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Life
{
    public class LifeGenerate
    {
        private LifeParams lifeParams;
        private Grid grid;
        private Cell[][] cells;
        private int version = 1;
        private List<List<int[]>> ghost;
        private List<List<int[]>> memory;
        private List<CellState> cellStates;
        private int periodictity;
        private bool isSteadyState;
        private bool allDieOrConstant;

        private LifeGenerate()
        {
            ghost = new List<List<int[]>>();
            memory = new List<List<int[]>>();
            cellStates = new List<CellState> { CellState.Light, CellState.Medium, CellState.Dark };
            isSteadyState = false;
            allDieOrConstant = false;
            periodictity = -1;
        }

        public LifeGenerate(string[] args):this()
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
            Console.WriteLine($"[{DateTime.Now:HH:MM:ss:fff}] The program will use the following settings:");
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
            Console.WriteLine($"\t      Random Factor: {lifeParams.Random * 100:#0.00} %");
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
                var running = NextGenerate();
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
                if (!running)
                {
                    isSteadyState = true;
                    break;
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

            // output file
            if(lifeParams.OutPutFilePath != null)OutPutToFile();

            // go back
            grid.RevertWindow();
            string periodicityStr = allDieOrConstant == true ? "N/A" : periodictity.ToString();
            if (isSteadyState) Console.WriteLine($"[{DateTime.Now:HH:MM:ss:fff}] Steady-state detected... periodicity = {periodicityStr}");
            else Console.WriteLine($"[{DateTime.Now:HH:MM:ss:fff}] Steady-state not detected...");
            if(lifeParams.OutPutFilePath != null) Utils.ConsoleSuccessMsg($"Final generation written to file: {lifeParams.OutPutFilePath}");
            Console.WriteLine($"[{DateTime.Now:HH:MM:ss:fff}] Press spacebar to close program...");
            Console.ReadKey();
        }

        private void OutPutToFile()
        {
            FileStream file = new FileStream(lifeParams.OutPutFileFullPath, FileMode.Create);
            StreamWriter sw = new StreamWriter(file);
            var lastGenerate = memory[^1];
            sw.WriteLine("#version=2.0");
            lastGenerate.ForEach(x =>
            {
                sw.WriteLine($"(o) cell: {x[0]}, {x[1]}");
            });

             sw.Close();
            file.Close();
        }
        #region Generate
        /// <summary>
        /// Show life
        /// </summary>
        private bool NextGenerate()
        {
            List<int[]> currentGenerateList = new List<int[]>();
            List<int[]> lastGenerateList = new List<int[]>();
            for (int i = 0; i < lifeParams.Rows; i++)
            {
                for (int p = 0; p < lifeParams.Colums; p++)
                {
                    var cell = cells[i][p];
                    var state = cell.GetState();
                    if(state == CellState.Full)
                    {
                        int[] arr = new int[] { i, p };
                        lastGenerateList.Add(arr);
                    }
                    bool result = LiveOrDead(i, p, state);
                    if (result)
                    {
                        int[] arr = new int[] { i, p };
                        currentGenerateList.Add(arr);
                    }
                    //grid.UpdateCell(i, p, reuslt == true ? CellState.Full : CellState.Blank);
                }
            }

            if (ghost.Count >= 3) ghost.RemoveAt(0);
            ghost.Add(lastGenerateList);

            if (memory.Count >= lifeParams.Memory) memory.RemoveAt(0);
            memory.Add(lastGenerateList);

            if (lastGenerateList.EqualList(currentGenerateList))
            {
                allDieOrConstant = true;
                return false;
            }

            // cycle
            if (IsCycle(currentGenerateList)) return false;

            UpdateCell(currentGenerateList);
            return true;
        }

        /// <summary>
        /// Determine whether there is a repetition in the cycle
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        private bool IsCycle(List<int[]> target)
        {
            for (int i = 0; i < memory.Count; i++)
            {
                if (memory[i].EqualList(target))
                {
                    periodictity = memory.Count - i;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Update Cell
        /// </summary>
        /// <param name="nextGenerateList"></param>
        private void UpdateCell(List<int[]> nextGenerateList)
        {
            for (int i = 0; i < lifeParams.Rows; i++)
            {
                for (int p = 0; p < lifeParams.Colums; p++)
                {
                    grid.UpdateCell(i, p, CellState.Blank);
                }
            }

            if (lifeParams.Ghost)
            {
                if (ghost.Count < 3)
                {
                    int cellIndex = 2;
                    for (int i = ghost.Count - 1; i >= 0; i--)
                    {
                        ghost[i].ForEach(x => grid.UpdateCell(x[0], x[1], cellStates[cellIndex]));
                        cellIndex--;
                    }
                }
                else
                {
                    for (int i = 0; i < ghost.Count; i++)
                    {
                        ghost[i].ForEach(x => grid.UpdateCell(x[0], x[1], cellStates[i]));
                    }
                }
                
            }

            nextGenerateList.ForEach(x => grid.UpdateCell(x[0], x[1], CellState.Full));
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
                        if (p > order && lifeParams.Neighbourhood == Neighbourhood.VONNEUMANN) continue;
                    }
                    else
                    {
                        if (tx + i < 0 || tx + i > cells.Length - 1 || ty + p < 0 || ty + p > cells[0].Length - 1) continue;
                        if (Math.Abs(p) > order && lifeParams.Neighbourhood == Neighbourhood.VONNEUMANN) continue;
                        tx += i;
                        ty += p;
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
                string fileName = fparrs[^1];
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
                    if (type.Equals("cell:", StringComparison.OrdinalIgnoreCase))
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

        /// <summary>
        /// Generate cells from seed file(type:rectangle,ellipse and version:2)
        /// </summary>
        /// <param name="arrs"></param>
        /// <param name="grid"></param>
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
