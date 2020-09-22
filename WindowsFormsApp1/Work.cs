using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace WindowsFormsApp1
{
    public partial class Work : IDisposable
    {

        public Action<bool> UpdateButtonAction;
        public Action<string> PrintLogAction;
        public Action<int, int, bool> TaskListViewAction;


        private ChromeDriver _driver;
        private List<string> _keys;

        // 广告页面停留时间
        private int _stayADTime = 2;
        // 搜索页面停留时间
        private int _siteStayTime = 3;
        public Work(List<string> keys)
        {
            _keys = keys;
            RandomSetting();
        }

        public void Start()
        {
            for (int i = 0; i < _keys.Count; i++)
            {
                for (int p = 0; p < Setting.TaskInput; p++)
                {
                    // 更新IP
                    ChangeIP();
                    // 随机更新参数
                    RandomSetting();
                    // 初始化Chrome
                    InitChrome();
                    //Setting.Running = Utils.CheckAuth();
                    Doit(_keys[i]);
                    Dispose();
                    TaskListViewAction(i, p + 1, false);
                    if (!Setting.Running) break;
                }
                TaskListViewAction(i, Setting.TaskInput, true);
                PrintLogAction($"{_keys[i]} 点击完成");
            }
            UpdateButtonAction(false);
            PrintLogAction("所有任务完成...");
        }

        private void RandomSetting()
        {
            Random rnd = new Random();
            _stayADTime = rnd.Next(Setting.AdStayMin, Setting.AdStayMax + 1);
            _siteStayTime = rnd.Next(Setting.SiteStayMin, Setting.SiteStayMax + 1);
        }

        private void InitChrome()
        {
            var option = new ChromeOptions();
            option.AddArgument("--incognito");
            option.AddArgument("--disable-infobars");
            option.AddArgument(string.Format("--user-agent={0}", Utils.GetRandomUA()));
            string driverDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.ToString(), "driver");
            ChromeDriverService service = ChromeDriverService.CreateDefaultService(driverDir);
            service.HideCommandPromptWindow = true;
            _driver = new ChromeDriver(service, option);
        }


        private void Doit(string url)
        {
            PrintLogAction($"开始进入广告主页 {url} ...");
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            try
            {
                _driver.Navigate().GoToUrl(url);
            }
            catch (Exception)
            {
                return;
            }
            Thread.Sleep(500);
            //PrintLogAction($"页面停留{_siteStayTime}秒");
            //Thread.Sleep(_siteStayTime * 1000);
            var adtags = _driver.FindElementsByClassName("img");

            Debug.WriteLine(adtags.Count);

            var currentPage = _driver.CurrentWindowHandle;
            foreach (var ad in adtags)
            {
                try
                {
                    ad.Click();
                }
                catch (Exception)
                {
                    continue;
                }
                StayAdPage();
                _driver.SwitchTo().Window(currentPage);
            }

        }


        private void StayAdPage()
        {
            PrintLogAction($"进入广告界面...");
            Thread.Sleep(1000);
            _driver.SwitchTo().Window(_driver.WindowHandles.Last());
            for (int i = 0; i < _stayADTime; i++)
            {
                try
                {
                    ExecutorJs($"window.scrollTo(0, {300 * i});");
                    Thread.Sleep(1000);
                }
                catch (Exception)
                {

                    PrintLogAction("该页面超时...");
                    Thread.Sleep(2000);
                    break;
                }

            }
            Thread.Sleep(1000);
        }


        private void ExecutorJs(string js, params object[] args)
        {
            IJavaScriptExecutor executor = _driver;
            executor.ExecuteScript(js, args);
        }


        public void Dispose()
        {
            if (_driver != null) _driver.Quit();
            PrintLogAction("程序关闭!");
        }
    }
        

    
}
