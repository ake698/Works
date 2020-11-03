using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace UE_AD
{
    public class Work
    {
        public Action<bool> UpdateButtonAction;
        public Action<string> PrintLogAction;
        // 广告页面停留时间
        private int _stayADTime = 5;

        private ChromeDriver _driver;

        public Work()
        {
        }

        public void Start()
        {
            PrintLogAction("程序启动...");
            while (true)
            {
                try
                {
                    // 更新IP
                    ChangeIP();
                    // 随机更新参数
                    RandomSetting();
                    InitChrome();
                    Doit();
                    PrintLogAction($"{++Setting.Count} 次点击完成");
                }
                catch(ThreadAbortException)
                {
                    continue;
                }catch(WebDriverException)
                {
                    continue;
                }
                catch(Exception e)
                {
                    PrintLogAction(e.ToString() + "\n" + e.Message);
                }
                finally
                {
                    Dispose();
                }
            }
        }

        private void RandomSetting()
        {
            Random rnd = new Random();
            _stayADTime = rnd.Next(Setting.AdStayMin, Setting.AdStayMax + 1);
        }

        private void InitChrome()
        {
            var option = new ChromeOptions();
            option.AddArgument("--incognito");
            option.AddArgument("--disable-infobars");
            //option.AddArgument("--headless");
            option.AddArgument(string.Format("--user-agent={0}", Utils.GetRandomUA()));
            string driverDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.ToString(), "driver");
            ChromeDriverService service = ChromeDriverService.CreateDefaultService(driverDir);
            service.HideCommandPromptWindow = true;
            _driver = new ChromeDriver(service, option);
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(20);
        }

        private void Doit()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            try
            {
                _driver.Navigate().GoToUrl(Setting.BaseUrl);
            }
            catch (Exception)
            {
                PrintLogAction("打开目标页面超时...");
                StopPage();
            }

            var currentPage = _driver.CurrentWindowHandle;

            StopPage();
            // 等待广告加载
            //ReadOnlyCollection<IWebElement> adtags = null;
            var adtags = new List<IWebElement>();

            adtags.Add(_driver.FindElementById("dl1"));
            adtags.Add(_driver.FindElementById("dl2"));


            foreach (var ad in adtags)
            {
                ad.Click();

                StayAdPage();

                _driver.SwitchTo().Window(currentPage);
            }

        }

        private void StayAdPage()
        {
            PrintLogAction("进入广告...");
            _driver.SwitchTo().Window(_driver.WindowHandles.Last());
            StopPage();
            for (int i = 0; i < _stayADTime; i++)
            {
                try
                {
                    ExecutorJs($"window.scrollTo(0, {300 * i});");
                    Thread.Sleep(1000);
                }
                catch (Exception)
                {
                    Debug.WriteLine("Thief超时");
                    Thread.Sleep(2000);
                    break;
                }
            }
            Thread.Sleep(1000);
        }

        private void StopPage()
        {
            ExecutorJs("window.stop()");
        }

        private void ExecutorJs(string js, params object[] args)
        {
            IJavaScriptExecutor executor = _driver;
            executor.ExecuteScript(js, args);
        }

        public void Dispose()
        {
            if (_driver != null) _driver.Quit();
            Utils.KillProcess(Setting.ProcessName);
            PrintLogAction("程序关闭!");
        }


        #region IP
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
            Utils.AddUsedIP(ip);
        }

        private void ReDial()
        {
            PrintLogAction("正在断开连接...");
            var result = Utils.ExecuteCommandWithResult($"rasdial {Setting.ADSL} /disconnect");
            //PrintLogAction(result);
            Thread.Sleep(4000);
            PrintLogAction("正在重新获取IP...");
            result = Utils.ExecuteCommandWithResult($"rasdial {Setting.ADSL} {Setting.ADSLUser} {Setting.ADSLPassword}");
            //PrintLogAction(result);
            Thread.Sleep(2000);
        }

        private bool CheckIP(string ip)
        {
            var ips = Utils.GetIPList(Setting.IPCheckDays);
            if (!ips.Contains(ip.Trim()))
            {
                return true;
            }
            return false;
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

        #endregion
    }
}
