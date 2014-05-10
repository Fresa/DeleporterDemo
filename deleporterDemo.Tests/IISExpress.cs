using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using System.Xml.XPath;
using EnvDTE80;

namespace deleporterDemo.Tests
{
    internal class NativeMethods
    {
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr GetTopWindow(IntPtr hWnd);
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern uint GetWindowThreadProcessId(IntPtr hwnd, out uint lpdwProcessId);
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool PostMessage(HandleRef hWnd, uint msg, IntPtr wParam, IntPtr lParam);
        public static void SendStopMessageToProcess(int pid)
        {
            try
            {
                var intPtr = GetTopWindow(IntPtr.Zero);
                while (intPtr != IntPtr.Zero)
                {
                    uint num;
                    GetWindowThreadProcessId(intPtr, out num);
                    if (pid == num)
                    {
                        var hWnd = new HandleRef(null, intPtr);
                        PostMessage(hWnd, 18u, IntPtr.Zero, IntPtr.Zero);
                        break;
                    }
                    intPtr = GetWindow(intPtr, 2u);
                }
            }
            catch (ArgumentException)
            {
            }
        }
    }

    public class IISExpressInstance : IDisposable
    {
        readonly Process _process;
        readonly string _shadowpath;

        public IISExpressInstance(string path, int port, params string[] additionalPaths)
        {
            var apphost = XElement.Load(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"IISExpress\config\applicationhost.config"));
            var pathInfo = new DirectoryInfo(path);

            var rootPath = Path.GetTempFileName();
            File.Delete(rootPath);
            Directory.CreateDirectory(rootPath);

            _shadowpath = Path.Combine(rootPath, pathInfo.Name);

            var siteElement = apphost.XPathSelectElement("./system.applicationHost/sites");
            foreach (var site in siteElement.Elements("site").ToList())
            {
                site.Remove();
            }

            var siteTemplate = new SiteTemplate { Port = port, SiteName = pathInfo.Name, SitePath = _shadowpath }.TransformText();
            siteElement.Add(XElement.Parse(siteTemplate));

            apphost.Save(rootPath + @"\applicationhost.config");

            CopyDirectory(path, _shadowpath);
            foreach (var additionalPath in additionalPaths)
                CopyDirectory(additionalPath, _shadowpath + "\\bin");
            
            Debug.WriteLine("Starting iis express in path " + _shadowpath);
            _process = Process.Start(new ProcessStartInfo
            {
                Arguments = string.Format("/config:\"{0}\\applicationhost.config\" /trace:error /systray:false", Path.GetFullPath(rootPath)),
                FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), @"IIS Express\iisexpress.exe"),
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                UseShellExecute = false
            });

            _process.OutputDataReceived += (o, e) => { };
            _process.BeginOutputReadLine();

            if ( Debugger.IsAttached)
            {
                var dte = (DTE2)Marshal.GetActiveObject("VisualStudio.DTE.12.0");
                var engine = (dte.Debugger as EnvDTE100.Debugger5).Transports.Item("Default").Engines.Item("Managed (v4.5, v4.0");
                var iisExpressProcess = dte.Debugger.LocalProcesses.OfType<Process2>().ToArray().FirstOrDefault(p => p.Name.Contains("iisexpress.exe"));
                if (iisExpressProcess != null)
                {
                    iisExpressProcess.Attach2(engine);
                }
            }
        }

        public void CopyDirectory(string source, string destination)
        {
            Debug.WriteLine("Copying " + source + " to " + destination);
            var p = Process.Start(new ProcessStartInfo
            {
                FileName = "xcopy.exe",
                Arguments = string.Format("\"{0}\" \"{1}\" /e /i /y", source, destination),
                CreateNoWindow = true,
                UseShellExecute = false
            });

            p.OutputDataReceived += (o, e) => Debug.WriteLine(e.Data);
            p.WaitForExit();
        }

        public void Dispose()
        {
            NativeMethods.SendStopMessageToProcess(_process.Id);
            _process.WaitForExit();
            try
            {
                Directory.Delete(_shadowpath, true);
            }
            catch
            {
                Debug.WriteLine("Some files could not be deleted");
            }
        }
    }
}
