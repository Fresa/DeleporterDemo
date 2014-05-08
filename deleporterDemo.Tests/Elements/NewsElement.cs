using OpenQA.Selenium;

namespace deleporterDemo.Tests.Elements
{
    public class NewsElement
    {
        private readonly IWebElement _element;

        public NewsElement(IWebElement element)
        {
            _element = element;
        }

        public string Header
        {
            get
            {
                return _element.FindElement(By.Name("h1")).Text;
            }
        }

        public string Body
        {
            get
            {
                return _element.FindElement(By.Name("p")).Text;
            }
        }
    }
}