using System.Collections.Generic;

namespace deleporterDemo.Models
{
    public interface INewsProvider
    {
        IEnumerable<NewsData> GetTheNews();
    }
}