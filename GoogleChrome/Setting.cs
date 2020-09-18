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
        public static string KeyDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.ToString(), "setting");
        public const string KeyFileName = "KeySetting.txt";
        public static string KeyPath { get { return Path.Combine(KeyDir, KeyFileName); } }
        public static bool Running { get; set; } = true;

        public static string ADSL { get; set; } = "adsl";

        public static int ConnectStay { get; set; } = 3;
        public static int DisConnectStay { get; set; } = 3;
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
