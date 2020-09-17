using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoogleChrome
{
    public class Work : IDisposable
    {
        private ChromeDriver _driver;
        private string[] _keys;
        // 点击快照数量
        private int _clickSnapCount = 1;
        // 快照页面停留时间
        private int _staySnapTime = 2;
        // 点击广告数量
        private int _clickADCount = 1;
        // 广告页面停留时间
        private int _stayADTime = 2;
        // 搜索页面停留时间
        private int _staySearchTie = 3;

        public Work(string[] keys)
        {
            

            _keys = keys;
        }

        public void Dispose()
        {
            _driver.Quit();
        }

        public void Start()
        {
            foreach (var key in _keys)
            {
                InitChrome();
                SearchKey(key);
                Dispose();
            }
        }

        private void InitChrome()
        {
            var option = new ChromeOptions();
            option.AddArgument("--incognito");
            option.AddArgument("--disable-infobars");
            string driverDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.ToString(), "driver2");
            ChromeDriverService service = ChromeDriverService.CreateDefaultService(driverDir);
            service.HideCommandPromptWindow = true;
            _driver = new ChromeDriver(service, option);
        }

        private void SearchKey(string key)
        {
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
            Thread.Sleep(_staySearchTie * 1000);
            var pmds = content.FindElements(By.ClassName("new-pmd"));
            var currentPage = _driver.CurrentWindowHandle;
            int currentAdCount = 0;
            int currentSnapCount = 0;
            foreach (var pmd in pmds)
            {
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


                var currentTag = pmd.Text;
                Debug.WriteLine(currentTag);

                IWebElement link = null;
                try
                {
                    link = pmd.FindElement(By.XPath("div/*/a"));
                }
                catch
                {
                    link = pmd.FindElement(By.PartialLinkText(key));
                }

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
