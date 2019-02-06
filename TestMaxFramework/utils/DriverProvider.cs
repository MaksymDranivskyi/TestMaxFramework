using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using TestMaxFramework.utils;

namespace TestMaxFramework
{
    public class DriverProvider
    {
        private static IWebDriver webDriver;
        public static string baseURL = ConfigurationManager.AppSettings["base_url"];
        public static string browser = ConfigurationManager.AppSettings["browser"];
        private static FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(Directory.GetParent(TestContext.CurrentContext.TestDirectory).Parent.FullName + "/drivers"); // location of the geckdriver.exe file


        public static void Init()
        {
            switch (browser)
            {
                case "Chrome":
                    webDriver = new ChromeDriver();
                break;
                case "IE":
                    webDriver = new InternetExplorerDriver();
                break;
                case "Firefox":
                    webDriver = new FirefoxDriver(service);
                break;
            }
            webDriver.Manage().Window.Maximize();
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        }

        /// Returns title of the page.

        public static string Title
        {
            get { return webDriver.Title; }
        }

        public static IWebDriver getDriver
        {
            get { return webDriver; }
        }

        public static void Goto(string url)
        {
            webDriver.Url = url;
        }

        public static void Close()
        {
            webDriver.Quit();
        }

       
        /// Returns type of current OS
        public static string GetOS()
        {
            OperatingSystem os = Environment.OSVersion;
            PlatformID pid = os.Platform;
            switch (pid)
            {
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                    return "Windows";
                case PlatformID.Unix:
                    return "Linux";
                case PlatformID.MacOSX:
                    return "Mac";
                default:
                    return "Mac";
            }
        }
    }
}
