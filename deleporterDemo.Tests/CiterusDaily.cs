using System.Collections.Generic;
using deleporterDemo.Models;

namespace deleporterDemo.Tests
{
    public class CiterusDaily
    {
        public IEnumerable<NewsData> WriteNews()
        {
            return new List<NewsData>
                   {
                       new NewsData
                       {
                           Header = "Citerus are looking for new co-workers",
                           Body = "Citerus has announced that it's looking for driven and curious employees."
                       },
                       new NewsData
                       {
                           Header = "Great place to work announces Citerus as #1",
                           Body = "Citerus was ranked as #1 greatest place to work this year."
                       },
                       new NewsData
                       {
                           Header = "Citerus customers are pleased",
                           Body = "A recent survey shows that Citerus customers are pleased with the work done by the company's employees, appreciating extensive knowledge, working together and teaching agile methology."
                       }
                   };
        }
    }
}