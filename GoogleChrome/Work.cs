using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace GoogleChrome
{
    public partial class Work : IDisposable
    {

        public Action<bool> UpdateButtonAction;
        public Action<string> PrintLogAction;
        public Action<int> FinishTaskViewAction;
        public Action<int, int, int> UpdateTaskViewCountAction;


        private ChromeDriver _driver;
        private List<string> _keys;
        // 点击快照数量
        private int _clickSnapCount = 1;
        // 快照页面停留时间
        private int _staySnapTime = 2;
        // 点击广告数量
        private int _clickADCount = 1;
        // 广告页面停留时间
        private int _stayADTime = 2;
        // 搜索页面停留时间
        private int _staySearchTime = 3;


        public Work(List<string> keys)
        {
            _keys = keys;
            RandomSetting();
        }

        public void Dispose()
        {
            _driver.Quit();
            
        }

        private void RandomSetting()
        {
            Random rnd = new Random();
            _clickADCount = rnd.Next(Setting.AdClickMin, Setting.AdClickMax + 1);
            _stayADTime = rnd.Next(Setting.AdStayMin, Setting.AdStayMax + 1);
            _clickSnapCount = rnd.Next(Setting.SnapClickMin, Setting.SnapClickMax + 1);
            _staySnapTime = rnd.Next(Setting.SnapStayMin, Setting.SnapStayMax + 1);
            _staySearchTime = rnd.Next(Setting.SearchStayMin, Setting.SearchStayMax + 1);
        }

        public void Start()
        {
            for (int i = 0; i < _keys.Count; i++)
            {
                if (!Setting.Running) break;
                // 随机更新参数
                RandomSetting();
                // 更新点击数量
                UpdateTaskViewCountAction(i, _clickADCount, _clickSnapCount);
                InitChrome();
                Setting.Running = Utils.CheckAuth();
                SearchKey(_keys[i]);
                FinishTaskViewAction(i);
                Dispose();
                PrintLogAction($"{_keys[i]} 点击完成");
            }
            UpdateButtonAction(false);
            PrintLogAction("所有任务完成...");
        }

        private void InitChrome()
        {
            var option = new ChromeOptions();
            option.AddArgument("--incognito");
            option.AddArgument("--disable-infobars");
            option.AddArgument(string.Format("--user-agent={0}", Utils.GetRandomUA()));
            string driverDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.ToString(), "driver2");
            ChromeDriverService service = ChromeDriverService.CreateDefaultService(driverDir);
            service.HideCommandPromptWindow = true;
            _driver = new ChromeDriver(service, option);
        }

        private void SearchKey(string key)
        {
            PrintLogAction($"开始关键词 {key} 操作...");
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            _driver.Navigate().GoToUrl("https://www.baidu.com/");
            var input = _driver.FindElementById("kw");
            input.Click();
            input.SendKeys(key);
            _driver.FindElementById("su").Click();

            // wait it until page done
            wait.Until(ExpectedConditions.ElementExists((By.Id("content_left"))));

            // wait it until page done
            var content = wait.Until((d) =>
            {
                return _driver.FindElementById("content_left");
            });
            PrintLogAction($"搜索页面停留 {_staySearchTime}秒...");
            Thread.Sleep(_staySearchTime * 1000);
            var pmds = content.FindElements(By.ClassName("new-pmd"));
            var currentPage = _driver.CurrentWindowHandle;
            int currentAdCount = 0;
            int currentSnapCount = 0;
            foreach (var pmd in pmds)
            {
                if (!Setting.Running) break;
                bool? flag = null;
                try
                {
                    pmd.FindElement(By.PartialLinkText("广告"));
                    flag = true;
                }
                catch (Exception)
                {
                    Debug.WriteLine("Not ad");
                }
                try
                {
                    pmd.FindElement(By.PartialLinkText("百度快照"));
                    flag = false;
                }
                catch (Exception)
                {
                    Debug.WriteLine("Not snap");
                }

                if (!flag.HasValue) continue;
                if (flag.Value && ++currentAdCount > _clickADCount) continue;
                if (!flag.Value && ++currentSnapCount > _clickSnapCount) continue;
                //PrintLogAction($"ad--{currentAdCount},snap--{currentSnapCount}");

                var currentTag = pmd.Text;
                Debug.WriteLine(currentTag);

                IWebElement link = null;
                //Debug.WriteLine(pmd.GetAttribute("outerHTML"));

                try
                {
                    link = pmd.FindElement( flag.Value == true ? By.XPath("div/*/a"): By.XPath("h3/a"));
                }
                catch
                {
                    link = pmd.FindElement(By.PartialLinkText(key));
                }
                string type = flag == true ? "广告" : "快照";

                PrintLogAction($"匹配到{type},{link.Text},准备进入...");
                try
                {
                    link.Click();
                }
                catch (Exception)
                {
                    ExecutorJs("arguments[0].click();", link);
                }
                StayAdPage(flag.Value);
                _driver.SwitchTo().Window(currentPage);
            }
        }


        private void StayAdPage(bool flag)
        {
            int stayTime = _staySnapTime;
            if (flag) stayTime = _stayADTime;
            Thread.Sleep(1000);
            _driver.SwitchTo().Window(_driver.WindowHandles.Last());
            for (int i = 0; i < stayTime; i++)
            {
                ExecutorJs($"window.scrollTo(0, {300 * i});");
                Thread.Sleep(1000);
            }
            Thread.Sleep(1000);
        }


        private void ExecutorJs(string js, params object[] args)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)_driver;
            executor.ExecuteScript(js,args);
        }
    }
}
