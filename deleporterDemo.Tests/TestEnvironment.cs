using System.Diagnostics;
using System.IO;
using System.Net;
using NUnit.Framework;

namespace deleporterDemo.Tests
{
    [SetUpFixture]
    public class TestEnvironment
    {
        IISExpressInstance _iis;

        [SetUp]
        public void Start()
        {
            Debug.WriteLine("Starting tests in " + Directory.GetCurrentDirectory());

            _iis = BootstrapIisExpress();
            InitializeDeleporter();
        }

        private IISExpressInstance BootstrapIisExpress()
        {
            var sitepath = Path.GetFullPath(Directory.GetCurrentDirectory() + @"\..\..\..\deleporterDemo");

            if (File.Exists(Directory.GetCurrentDirectory() + "//WebDev.WebHost20.dll"))
            {
                File.Delete(Directory.GetCurrentDirectory() + "//WebDev.WebHost20.dll");
            }

            return new IISExpressInstance(sitepath, 9090, Directory.GetCurrentDirectory());
        }

        private static void InitializeDeleporter()
        {
            WebRequest.CreateHttp("http://localhost:9090").GetResponse();
        }

        [TearDown]
        public void TearDown()
        {
            _iis.Dispose();
            Driver.Current.Dispose();
        }
    }
}