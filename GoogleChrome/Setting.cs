using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleChrome
{
    public class Setting
    {

        #region 文件类配置
        public static string Dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.ToString(), "setting");

        public const string KeyFileName = "KeySetting.txt";
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


        public static bool Running { get; set; } = true;

        public static string ADSL { get; set; } = "adsl";
        public static string ADSLUser { get; set; } = "hh27ad113";
        public static string ADSLPassword { get; set; } = "294085";
        public const string ADSLFileName = "adsl.ini";
        public static string ADSLPath { get { return Path.Combine(Dir, ADSLFileName); } }


        public static string SearchFrom { get; set; } = "https://www.baidu.com/";
        public static int WebStayMin { get; set; } = 3;
        public static int WebStayMax { get; set; } = 5;
        public static int ClickLimit { get; set; } = 6;
        public static int SearchStayMin { get; set; } = 3;
        public static int SearchStayMax { get; set; } = 5;
        public static int AdStayMin { get; set; } = 3;
        public static int AdStayMax { get; set; } = 5;
        public static int AdClickMin { get; set; } = 2;
        public static int AdClickMax { get; set; } = 3;
        public static int SnapClickMin { get; set; } = 2;
        public static int SnapClickMax { get; set; } = 3;
        public static int SnapStayMin { get; set; } = 3;
        public static int SnapStayMax { get; set; } = 5;

        public static bool Normal { get; set; } = true;

    }
}
