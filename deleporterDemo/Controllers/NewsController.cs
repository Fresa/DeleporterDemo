using System.Web.Mvc;
using deleporterDemo.Models;

namespace deleporterDemo.Controllers
{
    public class NewsController : Controller
    {
        private readonly NewsProvider _newsProvider;

        public NewsController()
        {
            
        }
        public NewsController(NewsProvider newsProvider)
        {
            _newsProvider = newsProvider;
        }

        public virtual ActionResult Feed()
        {
            
            var theNews = _newsProvider.GetTheNews();
            return View(theNews);
        }
    }
}