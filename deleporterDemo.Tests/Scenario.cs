using deleporterDemo.Tests.Page;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;

namespace deleporterDemo.Tests
{
    [TestFixture]
    public class Scenario<TPage, TMocker>
        where TPage : IPage, new()
        where TMocker : new()
    {
        private IWebDriver _driver;
        protected TPage Page;

        [TestFixtureSetUp]
        public void StartBrowser()
        {
            _driver = new PhantomJSDriver();
            Page = new TPage();
            Page.SetDriver(_driver);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            _driver.Quit();
        }

        [SetUp]
        public void Setup()
        {
            Given();
            When();
        }

        public virtual void Given()
        {
            new TMocker();
        }

        public virtual void When() { }

    }
}