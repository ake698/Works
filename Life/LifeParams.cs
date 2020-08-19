using System;
using System.Collections.Generic;
using System.Text;

namespace Life
{
    public class LifeParams
    {
        // 4- 48 --dimensions rows columns  start by 0
        public int Rows = 16;
        public int Colums = 16;
        // --periodic
        public bool Periodic = false;
        // --random random valid : 0-1
        public decimal Random = new decimal(0.5);
        // --seed filepath valid : real filepath && endwith .seed
        public string FilePath = null;
        // --generations number  valid : > 0
        public int Generations = 50;
        // --max-update rate  valid: 1-30
        public int Rate = 5;
        // --step
        public bool Step = false;

        public LifeParams(string[] args)
        {
            ParamsParse(args);
            
            if (dic.ContainsKey("--dimensions"))
            {
                int tempRows, tempColums;
                int.TryParse(dic["--dimensions"][0], out tempRows);
                int.TryParse(dic["--dimensions"][1], out tempColums);
                if (tempRows < 4 || tempRows > 48 || tempColums < 4 || tempColums > 48)
                {
                    ConsoleErrorMsg("Dimensions: Integer values between 4 and 48 (inclusive)");
                }
                else
                {
                    Rows = tempRows;
                    Colums = tempColums;
                }
            }
            

            Periodic = dic.ContainsKey("--periodic") ? true : false;

            if (dic.ContainsKey("--random"))
            {
                decimal tempRandom;
                decimal.TryParse(dic["--random"][0], out tempRandom);
                if (tempRandom > 1)
                {
                    ConsoleErrorMsg("Random Factor: Floating point values between 0 and 1 (inclusive)");
                }
                else
                {
                    Random = tempRandom;
                }
            }

            if (dic.ContainsKey("--seed"))
            {
                FilePath = dic.ContainsKey("--seed") ? dic["--seed"][0] : null;
                if (!FilePath.EndsWith(".seed"))
                {
                    ConsoleErrorMsg("Input File: Valid paths with a .seed file extension ");
                }
            }

            if (dic.ContainsKey("--generations"))
            {
                int tempgen;
                int.TryParse(dic["--generations"][1], out tempgen);
                if (tempgen < 0)
                {
                    ConsoleErrorMsg("Generations: Integer values above 0");
                }
                else
                {
                    Generations = tempgen;
                }
            }

            if (dic.ContainsKey("--max-update"))
            {
                int tempRate;
                int.TryParse(dic["--max-update"][1], out tempRate);
                if (tempRate < 1 || tempRate > 30)
                {
                    ConsoleErrorMsg("Update Rate: Floating point values between 1 and 30 (inclusive)");
                }
                else
                {
                    Rate = tempRate;
                }
            }


            Step = dic.ContainsKey("--step") ? true : false;
        }

        List<string> args = new List<string>();
        Dictionary<string, List<string>> dic = new Dictionary<string, List<string>>();

        public void ParamsParse(string[] command)
        {
            string key = null;
            for (int i = 0; i < command.Length; i++)
            {
                if (command[i].Trim() == "") continue;
                if (command[i].StartsWith("-"))
                {
                    if (key != null) dic.Add(key, args);
                    key = command[i];
                    args = new List<string>();
                }
                else
                {
                    args.Add(command[i].Trim());
                }
            }
            if(!string.IsNullOrEmpty(key)) dic.Add(key, args);
        }

        private void ConsoleErrorMsg(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Warning  :{msg}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
