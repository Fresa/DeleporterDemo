using System.Collections.Generic;

namespace deleporterDemo.Models
{
    public class NewsProvider
    {
        public IEnumerable<NewsData> GetTheNews()
        {
            return new List<NewsData>
            {
                new NewsData
                {
                    Header = "Car crash downtown!",
                    Body = "I car slammed into a water post causing flooding in the local swimsuit shop."
                },
                new NewsData
                {
                    Header = "People are getting lazier...",
                    Body = "Survey shows that people whom are sitting all the day may be lazy."
                }
            };
        }
    }
}