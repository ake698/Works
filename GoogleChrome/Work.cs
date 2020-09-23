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
        public Action<int, int, string> AddTaskListViewAction;


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
        // 一次点击的数量
        private int _adClickCount = 0;
        // 已经点击的数量
        private int _gloablCount = 0;


        public Work(List<string> keys)
        {
            _keys = keys;
            RandomSetting();
        }

        public void Dispose()
        {
            if (_driver != null) _driver.Quit();
            PrintLogAction("程序关闭!");
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

            for (int i = 1; i < int.MaxValue; i++)
            {
                // 更新IP
                ChangeIP();
                // 随机更新参数
                RandomSetting();
                // 初始化Chrome
                InitChrome();
                Setting.Running = Utils.CheckAuth();
                string key = _keys[i % _keys.Count];
                if (!Setting.Normal)
                {
                    key = _keys[Utils.GetRandomNumber(_keys.Count)];
                }
                SearchKey(key);
                Dispose();
                AddTaskListViewAction(i, _adClickCount, key);
                _adClickCount = 0;
                if (!Setting.Running) break;
                PrintLogAction($"{key} 点击完成");
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

            _driver.Navigate().GoToUrl(Setting.SearchFrom);
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

                string type  = "快照";
                if (flag.Value)
                {
                    type = "广告";
                    _adClickCount++;
                    _gloablCount++;
                }
                if(_gloablCount >= Setting.GlobalCount)
                {
                    Setting.Running = false;
                    break;
                }

                PrintLogAction($"匹配到{type},{link.Text},准备进入...");
                try
                {
                    link.Click();
                }
                catch (Exception)
                {
                    ExecutorJs("arguments[0].click();", link);
                    PrintLogAction("Exception click...");
                }
                try
                {
                    StayAdPage(flag.Value);
                }catch(Exception e)
                {
                    PrintLogAction(e.ToString());
                    PrintLogAction(e.Message);
                }
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
            IJavaScriptExecutor executor = (IJavaScriptExecutor)_driver;
            executor.ExecuteScript(js,args);
        }
    }
}
