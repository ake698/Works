using Life2;
using System;
using System.Collections.Generic;
using System.IO;

namespace Life
{
    public class LifeParams
    {
        private bool paramBuild = true;

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
        // --ghost
        public bool Ghost = false;
        // --neighbour type order center
        public Neighbourhood Neighbourhood = Neighbourhood.MOORE;
        public int Order = 1;
        public bool Center = false;
        // --survival
        public int[] Survival = new int[] { 2, 3 };
        // -- birth
        public int[] Birth = new int[] { 3 };
        // survival birth display
        public string Rules = "S( 2...3 ) B( 3 )";
        // --memory
        public int Memory = 16;
        // --output
        public string OutPutFilePath = null;

        public LifeParams(string[] args)
        {
            ParamsParse(args);

            PopulateParms();

            if (paramBuild) ConsoleSuccessMsg("Command line arguments processed.");

        }

        private List<string> args = new List<string>();
        private Dictionary<string, List<string>> dic = new Dictionary<string, List<string>>();


        private void ParamsParse(string[] command)
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
            if (!string.IsNullOrEmpty(key)) dic.Add(key, args);
        }

        private void PopulateParms()
        {
            if (dic.ContainsKey("--seed"))
            {
                FilePath = dic.ContainsKey("--seed") ? dic["--seed"][0] : null;

                if (!FilePath.EndsWith(".seed"))
                {
                    ConsoleErrorMsg("Input File: Valid paths with a .seed file extension ");
                    FilePath = null;
                    paramBuild = false;
                }
                else if (!File.Exists(FilePath))
                {
                    ConsoleErrorMsg("Input File: No such file ");
                    FilePath = null;
                    paramBuild = false;
                }
            }

            if (dic.ContainsKey("--dimensions"))
            {
                if (dic["--dimensions"].Count == 2)
                {
                    int tempRows = Rows, tempColums = Colums;
                    int.TryParse(dic["--dimensions"][0], out tempRows);
                    int.TryParse(dic["--dimensions"][1], out tempColums);
                    if (tempRows < 4 || tempRows > 48 || tempColums < 4 || tempColums > 48)
                    {
                        ConsoleErrorMsg("Dimensions: Integer values between 4 and 48 (inclusive)");
                        paramBuild = false;
                    }
                    else
                    {
                        Rows = tempRows;
                        Colums = tempColums;
                    }
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
                    paramBuild = false;
                }
                else
                {
                    Random = tempRandom;
                }
            }

            if (dic.ContainsKey("--generations"))
            {
                int tempgen;
                int.TryParse(dic["--generations"][0], out tempgen);
                if (tempgen < 0)
                {
                    ConsoleErrorMsg("Generations: Integer values above 0");
                    paramBuild = false;
                }
                else
                {
                    Generations = tempgen;
                }
            }

            if (dic.ContainsKey("--max-update"))
            {
                int tempRate;
                int.TryParse(dic["--max-update"][0], out tempRate);
                if (tempRate < 1 || tempRate > 30)
                {
                    ConsoleErrorMsg("Update Rate: Floating point values between 1 and 30 (inclusive)");
                    paramBuild = false;
                }
                else
                {
                    Rate = tempRate;
                }
            }

            Step = dic.ContainsKey("--step") ? true : false;

            if (dic.ContainsKey("--neighbour"))
            {
                string type = dic["--neighbour"][0].ToUpper();
                Neighbourhood tempNeighbourhood;
                if(!Enum.TryParse(type, out tempNeighbourhood))
                {
                    tempNeighbourhood = Neighbourhood;
                    paramBuild = false;
                    ConsoleErrorMsg("Neighbourhood: The neighbourhood type must be one of two strings, either 'moore' or 'vonNeumann', case insensitive");
                }
                int tempOrder;
                int.TryParse(dic["--neighbour"][1], out tempOrder);
                if(!(tempOrder >= 1 && tempOrder <= 10 && tempOrder < Math.Min(Rows,Colums)*2))
                {
                    tempOrder = Order;
                    paramBuild = false;
                    ConsoleErrorMsg("Neighbourhood: The order must be an integer between 1 and 10 (inclusive) and less than half of the smallest dimensions (rows or columns).");
                }

                bool tempCenter;
                if(!bool.TryParse(dic["--neighbour"][2], out tempCenter))
                {
                    tempCenter = Center;
                    paramBuild = false;
                    ConsoleErrorMsg("Neighbourhood: The center-count must be one of two strings, either 'true' or 'false'.");
                }
                Neighbourhood = tempNeighbourhood;
                Order = tempOrder;
                Center = tempCenter;
            }

            if (dic.ContainsKey("--survival"))
            {
                string value = dic["--survival"][0];
                value = value.Replace("...", "-");
                var values = value.Split('-');
                int[] tempSurvival = new int[values.Length];
                for (int i = 0; i < values.Length; i++)
                {
                    string temp = values[i];
                    int tempValue;
                    if(!int.TryParse(temp, out tempValue))
                    {
                        ConsoleErrorMsg("Survival: The survival must be integer.");
                        tempSurvival = Survival;
                        paramBuild = false;
                        break;
                    }
                    if (tempValue <= 0)
                    {
                        tempSurvival = Survival;
                        paramBuild = false;
                        ConsoleErrorMsg("Survival: The birth must greate than 0.");
                        break;
                    }
                    tempSurvival[i] = tempValue;
                }
                Survival = tempSurvival;
            }

            if (dic.ContainsKey("--birth"))
            {
                string value = dic["--birth"][0];
                value = value.Replace("...", "-");
                var values = value.Split('-');
                int[] tempBirth = new int[values.Length];
                for (int i = 0; i < values.Length; i++)
                {
                    string temp = values[i];
                    int tempValue;
                    if (!int.TryParse(temp, out tempValue))
                    {
                        ConsoleErrorMsg("Birth: The birth must be integer.");
                        paramBuild = false;
                        tempBirth = Birth;
                        break;
                    }
                    if (tempValue <= 0)
                    {
                        tempBirth = Birth;
                        ConsoleErrorMsg("Birth: The birth must greate than 0.");
                        paramBuild = false;
                        break;
                    }
                    tempBirth[i] = tempValue;
                }
                Birth = tempBirth;
            }
            Rules = $"S( {string.Join("...", Survival)} ) B( {string.Join("...", Birth)} )";

            if (dic.ContainsKey("--memory"))
            {
                int tempMemory;
                int.TryParse(dic["--memory"][0], out tempMemory);
                if (tempMemory < 4 || tempMemory > 512)
                {
                    ConsoleErrorMsg("Memory: Integer values between 4 and 512(inclusive).");
                    paramBuild = false;
                    tempMemory = Memory;
                }
                Memory = tempMemory;
            }

            if (dic.ContainsKey("--output"))
            {

            }

            Ghost = dic.ContainsKey("--ghost") ? true : false;

        }

        private void ConsoleErrorMsg(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[{DateTime.Now.ToString("HH:MM:ss:fff")}] Warning: {msg}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void ConsoleSuccessMsg(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[{DateTime.Now.ToString("HH:MM:ss:fff")}] Success: {msg}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
