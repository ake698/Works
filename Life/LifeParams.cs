using System;
using System.Collections.Generic;
using System.Text;

namespace Life
{
    public class LifeParams
    {
        // 4- 48 --dimensions rows columns
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
                int.TryParse(dic["--dimensions"][0], out Rows);
                int.TryParse(dic["--dimensions"][1], out Colums);
            }

            Periodic = dic.ContainsKey("--periodic") ? true : false;

            if (dic.ContainsKey("--random"))
            {
                decimal.TryParse(dic["--random"][0], out Random);
                if (Random > 1) throw new ArgumentOutOfRangeException("random error");
            }

            if (dic.ContainsKey("--seed"))
            {
                FilePath = dic.ContainsKey("--seed") ? dic["--seed"][0] : null;
                if (!FilePath.EndsWith(".seed")) throw new ArgumentOutOfRangeException("File Error");
            }

            if (dic.ContainsKey("--generations"))
            {
                int.TryParse(dic["--generations"][1], out Generations);
                if (Generations < 0) throw new ArgumentOutOfRangeException("Generations Error");
            }

            if (dic.ContainsKey("--max-update"))
            {
                int.TryParse(dic["--max-update"][1], out Rate);
                if (Rate < 1 || Rate > 30) throw new ArgumentOutOfRangeException("Rate Error");
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
    }
}
