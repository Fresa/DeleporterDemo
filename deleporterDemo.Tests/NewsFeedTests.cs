using System.Collections.Generic;
using System.Linq;
using deleporterDemo.Tests.Elements;
using deleporterDemo.Tests.Page;
using NUnit.Framework;
using Shouldly;

namespace deleporterDemo.Tests
{
    [TestFixture]
    public class When_showing_news : Scenario<NewsFeedPage, NewsFeedMocker>
    {
        private IEnumerable<NewsElement> _news;

        public override void When()
        {
            Page.GoToNewsFeed();
            _news = Page.GetNews();
        }

        [Test]
        public void It_should_have_3_news()
        {
            _news.Count().ShouldBe(3);
        }


    }
}