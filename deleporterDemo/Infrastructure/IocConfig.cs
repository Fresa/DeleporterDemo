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

            DependencyResolver.SetResolver(new StructureMapDependencyResolver(container));
        }
    }
}