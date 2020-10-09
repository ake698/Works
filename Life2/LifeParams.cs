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
        public string SurvivalArg = "2...3";
        // -- birth
        public int[] Birth = new int[] { 3 };
        public string BirthArg = "3";
        // survival birth display
        public string Rules = "S( 2...3 ) B( 3 )";
        // --memory
        public int Memory = 16;
        // --output
        public string OutPutFilePath = null;
        public string OutPutFileFullPath = null;

        public LifeParams(string[] args)
        {
            ParamsParse(args);

            PopulateParms();

            if (paramBuild) Utils.ConsoleSuccessMsg("Command line arguments processed.");

        }

        private List<string> args = new List<string>();
        private Dictionary<string, List<string>> dic = new Dictionary<string, List<string>>();


        private void ParamsParse(string[] command)
        {
            string key = null;
            for (int i = 0; i < command.Length; i++)
            {
                if (command[i].Trim() == "") continue;
                if (command[i].StartsWith("--"))
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
                    Utils.ConsoleErrorMsg("Input File: Valid paths with a .seed file extension ");
                    FilePath = null;
                    paramBuild = false;
                }
                else if (!File.Exists(FilePath))
                {
                    Utils.ConsoleErrorMsg("Input File: No such file ");
                    FilePath = null;
                    paramBuild = false;
                }
            }

            if (dic.ContainsKey("--dimensions"))
            {
                if (dic["--dimensions"].Count == 2)
                {
                    int.TryParse(dic["--dimensions"][0], out int tempRows);
                    int.TryParse(dic["--dimensions"][1], out int tempColums);
                    if (tempRows < 4 || tempRows > 48 || tempColums < 4 || tempColums > 48)
                    {
                        Utils.ConsoleErrorMsg("Dimensions: Integer values between 4 and 48 (inclusive)");
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
                decimal.TryParse(dic["--random"][0], out decimal tempRandom);
                if (tempRandom > 1)
                {
                    Utils.ConsoleErrorMsg("Random Factor: Floating point values between 0 and 1 (inclusive)");
                    paramBuild = false;
                }
                else
                {
                    Random = tempRandom;
                }
            }

            if (dic.ContainsKey("--generations"))
            {
                int.TryParse(dic["--generations"][0], out int tempgen);
                if (tempgen < 0)
                {
                    Utils.ConsoleErrorMsg("Generations: Integer values above 0");
                    paramBuild = false;
                }
                else
                {
                    Generations = tempgen;
                }
            }

            if (dic.ContainsKey("--max-update"))
            {
                int.TryParse(dic["--max-update"][0], out int tempRate);
                if (tempRate < 1 || tempRate > 30)
                {
                    Utils.ConsoleErrorMsg("Update Rate: Floating point values between 1 and 30 (inclusive)");
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
                if (!Enum.TryParse(type, out Neighbourhood tempNeighbourhood))
                {
                    tempNeighbourhood = Neighbourhood;
                    paramBuild = false;
                    Utils.ConsoleErrorMsg("Neighbourhood: The neighbourhood type must be one of two strings, either 'moore' or 'vonNeumann', case insensitive");
                }
                int.TryParse(dic["--neighbour"][1], out int tempOrder);
                if (!(tempOrder >= 1 && tempOrder <= 10 && tempOrder < Math.Min(Rows,Colums)*2))
                {
                    tempOrder = Order;
                    paramBuild = false;
                    Utils.ConsoleErrorMsg("Neighbourhood: The order must be an integer between 1 and 10 (inclusive) and less than half of the smallest dimensions (rows or columns).");
                }

                if (!bool.TryParse(dic["--neighbour"][2], out bool tempCenter))
                {
                    tempCenter = Center;
                    paramBuild = false;
                    Utils.ConsoleErrorMsg("Neighbourhood: The center-count must be one of two strings, either 'true' or 'false'.");
                }
                Neighbourhood = tempNeighbourhood;
                Order = tempOrder;
                Center = tempCenter;
            }

            if (dic.ContainsKey("--survival"))
            {
                var survivalList = new List<int>();
                var survivalValues = dic["--survival"];
                bool survivalPass = true;
                foreach(var x in survivalValues)
                {
                    string value = x.Replace("...", "*");
                    if (value.Contains("*"))
                    {
                        var valueArrs = value.Split("*");
                        if (valueArrs[0].IntParamCheck("Survival", (x) => x > 0, out int result) && valueArrs[1].IntParamCheck("Survival", (x) => x > 0, out int result2))
                        {
                            survivalList.Add(result);
                            survivalList.Add(result2);
                        }
                        else
                        {
                            Utils.ConsoleErrorMsg("Survival: The survival must greate than 0.");
                            paramBuild = false;
                            survivalPass = false;
                            break;
                        }
                    }
                    else
                    {
                        if(value.IntParamCheck("Survival", (x) => x > 0, out int result))
                            survivalList.Add(result);
                        else
                        {
                            Utils.ConsoleErrorMsg("Survival: The survival must greate than 0.");
                            survivalPass = false;
                            paramBuild = false;
                            break;
                        }
                    }
                };
                if (survivalList.Count > 0 && survivalPass)
                { 
                    Survival = survivalList.ToArray();
                    SurvivalArg = string.Join(" ", survivalValues.ToArray());
                }
            }

            if (dic.ContainsKey("--birth"))
            {
                var birthList = new List<int>();
                var birthValues = dic["--birth"];
                var birthPass = true;
                foreach(var x in birthValues)
                {
                    string value = x.Replace("...", "*");
                    if (value.Contains("*"))
                    {
                        var valueArrs = value.Split("*");
                        if (valueArrs[0].IntParamCheck("Birth", (x) => x > 0, out int result) && valueArrs[1].IntParamCheck("Birth", (x) => x > 0, out int result2))
                        {
                            birthList.Add(result);
                            birthList.Add(result2);
                        }
                        else
                        {
                            paramBuild = false;
                            birthPass = false;
                            Utils.ConsoleErrorMsg("Birth: The birth must greate than 0.");
                            break;
                        }
                    }
                    else
                    {
                        if (value.IntParamCheck("Birth", (x) => x > 0, out int result))
                            birthList.Add(result);
                        else
                        {
                            paramBuild = false;
                            birthPass = false;
                            Utils.ConsoleErrorMsg("Birth: The birth must greate than 0.");
                            break;
                        }
                    }
                };
                if (birthList.Count > 0 && birthPass)
                {
                    Birth = birthList.ToArray();
                    BirthArg = string.Join(" ", birthValues.ToArray());
                }
            }
            Rules = $"S( {SurvivalArg} ) B( {BirthArg} )";

            if (dic.ContainsKey("--memory"))
            {
                int.TryParse(dic["--memory"][0], out int tempMemory);
                if (tempMemory < 4 || tempMemory > 512)
                {
                    Utils.ConsoleErrorMsg("Memory: Integer values between 4 and 512(inclusive).");
                    paramBuild = false;
                    tempMemory = Memory;
                }
                Memory = tempMemory;
            }

            if (dic.ContainsKey("--output"))
            {
                string filePath = dic["--output"][0];
                if (filePath.EndsWith(".seed"))
                {
                    string directory = Path.GetDirectoryName(filePath);
                    Directory.CreateDirectory(directory);
                }
                OutPutFilePath = filePath;
                OutPutFileFullPath = Path.GetFullPath(filePath);
            }

            Ghost = dic.ContainsKey("--ghost") ? true : false;

        }

        
    }
}
