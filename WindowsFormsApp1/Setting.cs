using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Setting
    {
        #region 文件类配置
        public static string Dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.ToString(), "setting");

        public const string KeyFileName = "Task.txt";
        public static string KeyPath { get { return Path.Combine(Dir, KeyFileName); } }
        public static int KeyCount { get; set; } = 0;

        public const string UAFileName = "UA.txt";
        public static string UAPath { get { return Path.Combine(Dir, UAFileName); } }
        public static List<string> UAs = null;

        public static string IPFileName = $"{DateTime.Now:yyyy-MM-dd}IP.txt";
        public static string IPPath { get { return Path.Combine(Dir, IPFileName); } }
        #endregion
        public const string Auth = "http://119.29.79.210/";
        public static int IPCheckDays { get; set; } = 1;
        public static bool CheckRepeatIP { get; set; } = true;

        public const string ReadMe = "软件说明.txt";
        public static string ReadMePath { get { return Path.Combine(Dir, ReadMe); } }


        public static bool Running { get; set; } = true;

        public static string ADSL { get; set; } = "adsl";
        public static string ADSLUser { get; set; } = "hh27ad113";
        public static string ADSLPassword { get; set; } = "294085";
        public const string ADSLFileName = "config.ini";
        public static string ADSLPath { get { return Path.Combine(Dir, ADSLFileName); } }



        public static int SiteStayMin { get; set; } = 3;
        public static int SiteStayMax { get; set; } = 5;
        public static int AdStayMin { get; set; } = 3;
        public static int AdStayMax { get; set; } = 5;

    }
}
