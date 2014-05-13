using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;

namespace deleporterDemo.Tests
{
    public static class DriverProvider
    {
        public static IWebDriver Current { get; set; }

        static DriverProvider()
        {
            Current = new PhantomJSDriver();
        }
    }
}