using System.Web.Mvc;

namespace deleporterDemo.Tests
{
    public static class ViewResultExtensions
    {
        public static ViewResult ToViewResult(this object model)
        {
            return new ViewResult { ViewData = { Model = model } };
        }
    }
}