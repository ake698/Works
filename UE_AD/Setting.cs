using System;
using System.Collections.Generic;
using System.IO;

namespace UE_AD
{
    public class Setting
    {
        #region 文件类配置
        public static string Dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.ToString(), "setting");

        public const string UAFileName = "UA.txt";
        public static string UAPath { get { return Path.Combine(Dir, UAFileName); } }
        public static List<string> UAs = null;

        public static string IPFileName = $"{DateTime.Now:yyyy-MM-dd}IP.txt";
        public static string IPPath { get { return Path.Combine(Dir, IPFileName); } }
        #endregion

        public const string ReadMe = "软件说明.txt";
        public static string ReadMePath { get { return Path.Combine(Dir, ReadMe); } }

        public const string ProcessName = "chrome";

        public static string ADSL { get; set; } = "adsl";
        public static string ADSLUser { get; set; } = "710001";
        public static string ADSLPassword { get; set; } = "344321";
        public const string ADSLFileName = "config.ini";
        public static string ADSLPath { get { return Path.Combine(Dir, ADSLFileName); } }

        public static int IPCheckDays { get; set; } = 1;

        public static int AdStayMin { get; set; } = 6;
        public static int AdStayMax { get; set; } = 15;

        //public const string BaseUrl = "http://www.icanfly.club/tools/bullshit.php";
        public const string BaseUrl = "http://www.icanfly.club/tools/bullshit2.php";

        public static int Count = 0;
    }
}
