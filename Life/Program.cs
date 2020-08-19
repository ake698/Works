using Display;
using System;
using System.Diagnostics;
using System.IO;

namespace Life
{
    class Program
    {
        static void Main(string[] args)
        {
            LifeGenerate life = new LifeGenerate(args);
            life.Start();
        }
        public static Grid GenerateCell(LifeParams args)
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
                if(arrs.Length > 1)
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

        public static void GenerateCell(Grid grid, string filePath)
        {
            TextReader reader = new StreamReader(@"C:\Users\admin\Desktop\ddddd\CAB201_2020S2_ProjectPartA_nXXXXXXXX\Seeds\figure-eight_14x14.seed");
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.StartsWith("#")) continue;
                var arrs = line.Split(" ");
                grid.UpdateCell(int.Parse(arrs[0]), int.Parse(arrs[1]), CellState.Full);
            }
        }

        public static void StartLife()
        {

        }


        public void test1()
        {
            int rows = 7, columns = 9;
            int[,] cells = {
                { 5, 3 },
                { 4, 3 },
                { 5, 5 },
                { 4, 5 },
                { 2, 1 },
                { 1, 2 },
                { 1, 3 },
                { 1, 4 },
                { 1, 5 },
                { 1, 6 },
                { 2, 7 }
            };

            // Construct grid...
            Grid grid = new Grid(rows, columns);

            // Wait for user to press a key...
            Console.WriteLine("Press any key to start...");
            //Console.ReadKey();

            // Initialize the grid window (this will resize the window and buffer)
            grid.InitializeWindow();

            // Set the footnote (appears in the bottom left of the screen).
            grid.SetFootnote("Smiley");

            Stopwatch watch = new Stopwatch();

            // For each of the cells...
            for (int i = 0; i < cells.GetLength(0); i++)
            {
                watch.Restart();

                // Update grid with a new cell...
                grid.UpdateCell(cells[i, 0], cells[i, 1], CellState.Full);

                // Render updates to the console window...
                grid.Render();

                while (watch.ElapsedMilliseconds < 100) ;
            }


            // Set complete marker as true
            grid.IsComplete = true;

            // Render updates to the console window (grid should now display COMPLETE)...
            grid.Render();

            // Wait for user to press a key... 
            Console.ReadKey();

            // Revert grid window size and buffer to normal
            grid.RevertWindow();
        }

    }
}
