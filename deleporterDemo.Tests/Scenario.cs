using deleporterDemo.Tests.Page;
using NUnit.Framework;

namespace deleporterDemo.Tests
{
    [TestFixture]
    public class Scenario<TPage, TMocker>
        where TPage : IPage, new()
        where TMocker : new()
    {
        protected TPage Page;

        [TestFixtureSetUp]
        public void StartBrowser()
        {
            Page = new TPage();
            Page.SetDriver(DriverProvider.Current);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            DriverProvider.Current.Quit();
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