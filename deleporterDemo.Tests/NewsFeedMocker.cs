using DeleporterCore.Client;
using deleporterDemo.Controllers;
using FakeItEasy;
using StructureMap;

namespace deleporterDemo.Tests
{
    public class NewsFeedMocker
    {
        public NewsFeedMocker()
        {
            ApplyMockResults();
        }

        private void ApplyMockResults()
        {
            var citerusNewsController = A.Fake<NewsController>();
            A.CallTo(citerusNewsController).CallsBaseMethod();
            A.CallTo(() => citerusNewsController.Feed()).ReturnsLazily(f => new CiterusDaily().WriteNews().ToViewResult());

            Deleporter.Run(() => ObjectFactory.Configure(expression => expression.For<NewsController>().Use(context => citerusNewsController)));
        }
    }
}