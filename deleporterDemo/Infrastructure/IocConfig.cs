using System.Web.Mvc;
using deleporterDemo.DependencyResolution;
using deleporterDemo.Models;
using StructureMap;

namespace deleporterDemo.Infrastructure
{
    public class IocConfig
    {
        public static void Register()
        {
            IContainer container = new Container();

            container.Configure(x => x.For<INewsProvider>().Use<NewsProvider>());

            DependencyResolver.SetResolver(new StructureMapDependencyResolver(container));
        }
    }
}