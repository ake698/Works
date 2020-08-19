using Display;
using System;
using System.Collections.Generic;
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
            Console.WriteLine(args);
            lifeParams = new LifeParams(args);
            grid = GenerateCell(lifeParams);
            cells = grid.GetCells();
        }

        public void Start()
        {
            Console.WriteLine("Press any key to start...");
            //Console.ReadKey();
            grid.InitializeWindow();

            // Set the footnote (appears in the bottom left of the screen).
            grid.SetFootnote("Smiley");

            // Set complete marker as true
            grid.IsComplete = true;

            // Render updates to the console window (grid should now display COMPLETE)...
            grid.Render();

            // Wait for user to press a key... 
            Console.ReadKey();

            // Revert grid window size and buffer to normal
            grid.RevertWindow();
        }

        #region Generate

        private void NextGenerate()
        {
            int live = 0;
            for (int i = 0; i < lifeParams.Rows; i++)
            {
                for (int p = 0; p < lifeParams.Colums; p++)
                {
                    var a = cells[i][p];
                    a.GetState();
                }
            }
        }


        private bool LiveOrDead(int x, int y, CellState state)
        {
            if(state == CellState.Full)
            {

            }
            else
            {

            }
        }

        private int CountAlive(int x, int y)
        {
            int live = 0;
            Cell tmp;
            int tx = x, ty = y;
            for (int i = -1; i <= 1; i++)
            {
                for (int p = -1; p <= 1; p++)
                {
                    if (i == p && i == 0) continue;
                    tmp = cells[x + i][y + i];

                    if(x == lifeParams.Rows || y == lifeParams.Colums)
                    {
                        if (lifeParams.Periodic)
                        {

                        }
                        else
                        {

                        }
                    }
                    
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
                    }
                }
            }
            else
            {
                // seed file
                string filePath = args.FilePath;
                var fparrs = filePath.Split(@"/");
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
            live = new int[30][];
            TextReader reader = new StreamReader(@"C:\Users\admin\Desktop\ddddd\CAB201_2020S2_ProjectPartA_nXXXXXXXX\Seeds\figure-eight_14x14.seed");
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
