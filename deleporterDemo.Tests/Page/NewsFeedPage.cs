using System;
using System.Collections.Generic;
using System.Linq;
using deleporterDemo.Tests.Elements;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace deleporterDemo.Tests.Page
{
    public class NewsFeedPage : IPage
    {
        private IWebDriver _driver;

        public void SetDriver(IWebDriver driver)
        {
            _driver = driver;
        }

        public void GoToNewsFeed()
        {
            _driver.Navigate().GoToUrl("~/News/Feed");
        }

        public IEnumerable<NewsElement> GetNews()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            return wait.Until(d => d.FindElements(By.ClassName("news-post"))).Select(element => new NewsElement(element));
        }
    }
}