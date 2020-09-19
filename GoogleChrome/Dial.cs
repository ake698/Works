using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;

namespace GoogleChrome
{
    public partial class Work
    {
        private void ReDial()
        {
            PrintLogAction("正在断开连接...");
            Utils.ExecuteCommandWithResult($"rasdial {Setting.ADSL} /disconnect");

            Thread.Sleep(2000);
            PrintLogAction("正在重新获取IP...");
            Utils.ExecuteCommandWithResult($"rasdial {Setting.ADSL} {Setting.ADSLUser} {Setting.ADSLPassword}");
            Thread.Sleep(3000);
        }


        

        private bool CheckIP(string ip)
        {
            if (!Setting.CheckRepeatIP) return true;
            
            var ips = Utils.GetIPList(Setting.IPCheckDays);
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
                    ReDial();
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
            PrintLogAction("IP校验中...");
            var result = Utils.HttpGet("https://www.ip.cn/api/index?ip=&type=0");
            Debug.WriteLine(result);
            string pattern = @"\d+\.\d+\.\d+\.\d+";
            var match = Regex.Matches(result, pattern);
            Debug.WriteLine(match[0].Value);
            return match[0].Value.Trim();
        }
        

    }
}
