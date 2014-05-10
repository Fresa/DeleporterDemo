using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;

namespace deleporterDemo.Tests
{
    public static class Driver
    {
        public static IWebDriver Current { get; set; }

        static Driver()
        {
            Current = new PhantomJSDriver();
        }
    }
}