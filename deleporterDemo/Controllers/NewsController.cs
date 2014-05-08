using System.Web.Mvc;
using deleporterDemo.Models;

namespace deleporterDemo.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsProvider _newsProvider;

        public NewsController(INewsProvider newsProvider)
        {
            _newsProvider = newsProvider;
        }

        public ActionResult Feed()
        {
            
            var theNews = _newsProvider.GetTheNews();
            return View(theNews);
        }
    }
}