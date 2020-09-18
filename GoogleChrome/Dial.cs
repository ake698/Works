using System.Diagnostics;
using System.Text.RegularExpressions;

namespace GoogleChrome
{
    public partial class Work
    {
        private void ReDial()
        {

        }


        

        private bool CheckIP(string ip)
        {
            var ips = Utils.GetIPList();
            if (!ips.Contains(ip.Trim()))
            {
                return true;
            }
            return false;
        }

        public void ChangeIP()
        {
            bool hasIP = false;
            string ip = null;
            while (!hasIP)
            {
                try
                {
                    ip = GetIP();
                    hasIP = true;
                    PrintLogAction($"获取到IP {ip}");
                }
                catch
                {
                    continue;
                }
                if (!CheckIP(ip))
                {
                    PrintLogAction($"{ip}已经被使用");
                    hasIP = false;
                }
                
            }

            // 记录IP
            Utils.AddUsedIP(ip);
        }


        public string GetIP()
        {
            var result = Utils.HttpGet("https://www.ip.cn/api/index?ip=&type=0");
            Debug.WriteLine(result);
            string pattern = @"\d+\.\d+\.\d+\.\d+";
            var match = Regex.Matches(result, pattern);
            Debug.WriteLine(match[0].Value);
            return match[0].Value.Trim();
        }
        

    }
}
