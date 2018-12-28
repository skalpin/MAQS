﻿//--------------------------------------------------
// <copyright file="SeleniumConfig.cs" company="Magenic">
//  Copyright 2019 Magenic, All rights Reserved
// </copyright>
// <summary>Helper class for getting selenium specific configuration values</summary>
//--------------------------------------------------
using Magenic.Maqs.Utilities.Data;
using Magenic.Maqs.Utilities.Helper;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;

namespace Magenic.Maqs.BaseSeleniumTest
{
    /// <summary>
    /// Config class
    /// </summary>
    public static class SeleniumConfig
    {
        /// <summary>
        ///  Static name for the Selenium configuration section
        /// </summary>
        private const string SELENIUMSECTION = "SeleniumMaqs";

        /// <summary>
        /// Get the browser
        /// <para>If no browser is provide in the project configuration file we default to Chrome</para>
        /// <para>Browser are maximized by default</para>
        /// </summary>
        /// <returns>The web driver</returns>
        /// <example>
        /// <code source = "../SeleniumUnitTesting/SeleniumConfigTests.cs" region="GetBrowser" lang="C#" />
        /// </example>
        public static IWebDriver Browser()
        {
            return Browser(GetBrowserName());
        }

        /// <summary>
        /// Get the webdriver based for the provided browser
        /// <para>Browser are maximized by default</para>
        /// </summary>
        /// <param name="browser">The browser type we want to use</param>
        /// <returns>An IWebDriver</returns>
        /// <example>
        /// <code source = "../SeleniumUnitTesting/SeleniumConfigTests.cs" region="GetBrowserWithString" lang="C#" />
        /// </example>
        public static IWebDriver Browser(string browser)
        {
            IWebDriver webDriver = null;

            try
            {
                switch (browser.ToUpper())
                {
                    case "INTERNET EXPLORER":
                    case "INTERNETEXPLORER":
                    case "IE":
                        webDriver = InitializeIEDriver();
                        break;
                    case "FIREFOX":
                        webDriver = InitializeFirefoxDriver();
                        break;

                    case "CHROME":
                        webDriver = InitializeChromeDriver();
                        break;

                    case "HEADLESSCHROME":
                        webDriver = InitializeHeadlessChromeDriver();
                        break;

                    case "EDGE":
                        webDriver = InitializeEdgeDriver();
                        break;

                    case "PHANTOMJS":
                        throw new ArgumentException(StringProcessor.SafeFormatter("Selenium no longer supports PhantomJS", browser));

                    case "REMOTE":
                        webDriver = new RemoteWebDriver(new Uri(Config.GetValueForSection(SELENIUMSECTION, "HubUrl")), GetRemoteCapabilities(), GetCommandTimeout());
                        break;

                    default:
                        throw new ArgumentException(StringProcessor.SafeFormatter("Browser type '{0}' is not supported", browser));
                }

                SetBrowserSize(webDriver);
                return webDriver;
            }
            catch (Exception e)
            {
                if (e.GetType() == typeof(ArgumentException))
                {
                    throw e;
                }
                else
                {
                    try
                    {
                        // Try to cleanup
                        webDriver?.KillDriver();
                    }
                    catch (Exception quitExecption)
                    {
                        throw new Exception("Web driver setup and teardown failed. Your web driver may be out of date", quitExecption);
                    }
                }

                // Log that something went wrong
                throw new Exception("Your web driver may be out of date or unsupported.", e);
            }
        }

        /// <summary>
        /// Get the browser type name - Example: Chrome
        /// </summary>
        /// <returns>The browser type</returns>
        /// <example>
        /// <code source = "../SeleniumUnitTesting/SeleniumConfigTests.cs" region="GetBrowserName" lang="C#" />
        /// </example>
        public static string GetBrowserName()
        {
            return Config.GetValueForSection(SELENIUMSECTION, "Browser", "Chrome");
        }

        /// <summary>
        /// Get the initialize Selenium timeout timespan
        /// </summary>
        /// <returns>The initialize timeout</returns>
        public static TimeSpan GetCommandTimeout()
        {
            string value = Config.GetValueForSection(SELENIUMSECTION, "SeleniumCommandTimeout", "60000");
            if (!int.TryParse(value, out int timeout))
            {
                throw new ArgumentException("SeleniumCommandTimeout should be a number but the current value is: " + value);
            }

            return TimeSpan.FromMilliseconds(timeout);
        }

        /// <summary>
        /// Get the hint path for the web driver
        /// </summary>
        /// <returns>The hint path for the web driver</returns>
        /// <example>
        /// <code source = "../SeleniumUnitTesting/SeleniumConfigTests.cs" region="GetDriverHintPath" lang="C#" />
        /// </example>
        public static string GetDriverHintPath()
        {
            string defaultPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return Config.GetValueForSection(SELENIUMSECTION, "WebDriverHintPath", defaultPath);
        }

        /// <summary>
        /// Get the remote browser type name
        /// </summary>
        /// <returns>The browser type being used on grid</returns>
        /// <example>
        /// <code source = "../SeleniumUnitTesting/SeleniumConfigTests.cs" region="GetRemoteName" lang="C#" />
        /// </example>
        public static string GetRemoteBrowserName()
        {
            return Config.GetValueForSection(SELENIUMSECTION, "RemoteBrowser", "Chrome");
        }

        /// <summary>
        /// Get the remote browser version
        /// </summary>
        /// <returns>The browser version to run against on grid</returns>
        /// <example>
        /// <code source = "../SeleniumUnitTesting/SeleniumConfigTests.cs" region="RemoteVersion" lang="C#" />
        /// </example>
        public static string GetRemoteBrowserVersion()
        {
            return Config.GetValueForSection(SELENIUMSECTION, "RemoteBrowserVersion");
        }

        /// <summary>
        /// Get the remote platform type name
        /// </summary>
        /// <returns>The platform (or OS) to run remote tests against</returns>
        /// <example>
        /// <code source = "../SeleniumUnitTesting/SeleniumConfigTests.cs" region="RemotePlatform" lang="C#" />
        /// </example>
        public static string GetRemotePlatform()
        {
            return Config.GetValueForSection(SELENIUMSECTION, "RemotePlatform");
        }

        /// <summary>
        /// Get the wait default wait driver
        /// </summary>
        /// <param name="driver">Brings in an IWebDriver</param>
        /// <returns>An WebDriverWait</returns>
        /// <example>
        /// <code source = "../SeleniumUnitTesting/SeleniumConfigTests.cs" region="WaitDriver" lang="C#" />
        /// </example>
        public static WebDriverWait GetWaitDriver(IWebDriver driver)
        {
            return new WebDriverWait(new SystemClock(), driver, GetTimeoutTime(), GetWaitTime());
        }

        /// <summary>
        /// Get the web site base url
        /// </summary>
        /// <returns>The web site base url</returns>
        /// <example>
        /// <code source = "../SeleniumUnitTesting/SeleniumConfigTests.cs" region="GetWebsiteBase" lang="C#" />
        /// </example>
        public static string GetWebSiteBase()
        {
            return Config.GetValueForSection(SELENIUMSECTION, "WebSiteBase");
        }

        /// <summary>
        /// Get if we should save page source on fail
        /// </summary>
        /// <returns>True if we want to save page source on fail</returns>
        public static bool GetSavePagesourceOnFail()
        {
            return Config.GetValueForSection(SELENIUMSECTION, "SavePagesourceOnFail").Equals("Yes", StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// Get if we should save screenshots on soft alert fails
        /// </summary>
        /// <returns>True if we want to save screenshots on soft alert fails</returns>
        public static bool GetSoftAssertScreenshot()
        {
            return Config.GetValueForSection(SELENIUMSECTION, "SoftAssertScreenshot").Equals("Yes", StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// Get the format we want to capture screenshots with
        /// </summary>
        /// <returns>The desired format</returns>
        public static string GetImageFormat()
        {
            return Config.GetValueForSection(SELENIUMSECTION, "ImageFormat", "PNG");
        }

        /// <summary>
        /// Set the script and page timeouts
        /// </summary>
        /// <param name="driver">Brings in an IWebDriver</param>
        /// <example>
        /// <code source = "../SeleniumUnitTesting/SeleniumConfigTests.cs" region="SetTimeouts" lang="C#" />
        /// </example>
        public static void SetTimeouts(IWebDriver driver)
        {
            TimeSpan timeoutTime = GetTimeoutTime();
            driver.Manage().Timeouts().PageLoad = timeoutTime;
            driver.Manage().Timeouts().AsynchronousJavaScript = timeoutTime;
        }

        /// <summary>
        /// Get the web driver location
        /// </summary>
        /// <param name="driverFile">The web drive file, including extension</param>
        /// <param name="defaultHintPath">The default location for the specific driver</param>
        /// <param name="mustExist">Do we need to know where this drive is located, if this is true and the file is not found an error will be thrown</param>
        /// <returns>The path to the web driver</returns>
        private static string GetDriverLocation(string driverFile, string defaultHintPath = "", bool mustExist = true)
        {
            // Get the hint path from the app.config
            string hintPath = GetDriverHintPath();

            // Try the hintpath first
            if (!string.IsNullOrEmpty(hintPath) && File.Exists(Path.Combine(hintPath, driverFile)))
            {
                return hintPath;
            }

            // Try the default hit path next
            if (!string.IsNullOrEmpty(defaultHintPath) && File.Exists(Path.Combine(defaultHintPath, driverFile)))
            {
                return Path.Combine(defaultHintPath).ToString();
            }

            // Get the test dll location
            UriBuilder uri = new UriBuilder(Assembly.GetExecutingAssembly().Location);
            string testLocation = Path.GetDirectoryName(Uri.UnescapeDataString(uri.Path));

            // Try the test dll location
            if (File.Exists(Path.Combine(testLocation, driverFile)))
            {
                return testLocation;
            }

            // We didn't find the web driver so throw an error if we need to know where it is
            if (mustExist)
            {
                throw new FileNotFoundException(StringProcessor.SafeFormatter("Unable to find driver for '{0}'", driverFile));
            }

            return string.Empty;
        }

        /// <summary>
        /// Get the programs file folder which contains given file
        /// </summary>
        /// <param name="folderName">The programs file sub folder</param>
        /// <param name="file">The file we are looking for</param>
        /// <returns>The parent folder of the given file or the empty string if the file is not found</returns>
        private static string GetProgramFilesFolder(string folderName, string file)
        {
            // Handle 64 bit systems first
            if (IntPtr.Size == 8 || (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
            {
                string path = Path.Combine(Environment.GetEnvironmentVariable("ProgramW6432"), folderName, file);
                if (File.Exists(path))
                {
                    return Path.GetDirectoryName(path);
                }

                path = Path.Combine(Environment.GetEnvironmentVariable("ProgramFiles(x86)"), folderName, file);
                if (File.Exists(path))
                {
                    return Path.GetDirectoryName(path);
                }
            }
            else
            {
                string path = Path.Combine(Environment.GetEnvironmentVariable("ProgramFiles"), folderName, file);
                if (File.Exists(path))
                {
                    return Path.GetDirectoryName(path);
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Get the remote desired capability
        /// </summary>
        /// <returns>The remote desired capability</returns>
        private static ICapabilities GetRemoteCapabilities()
        {
            DriverOptions options = null;
            string remoteBrowser = GetRemoteBrowserName();
            string remotePlatform = GetRemotePlatform();
            string remoteBrowserVersion = GetRemoteBrowserVersion();

            switch (remoteBrowser.ToUpper())
            {
                case "INTERNET EXPLORER":
                case "INTERNETEXPLORER":
                case "IE":
                    options = new InternetExplorerOptions();
                    break;

                case "FIREFOX":
                    options = new FirefoxOptions();
                    break;

                case "CHROME":
                    options = new ChromeOptions();
                    break;

                case "EDGE":
                    options = new EdgeOptions();
                    break;

                case "SAFARI":
                    options = new SafariOptions();
                    break;

                default:
                    throw new ArgumentException(StringProcessor.SafeFormatter("Remote browser type '{0}' is not supported", remoteBrowser));
            }

            // Add a platform setting if one was provided
            if (remotePlatform.Length > 0)
            {
                options.AddAdditionalCapability("platform", remotePlatform);
            }

            // Add a remote browser setting if one was provided
            if (remoteBrowserVersion.Length > 0)
            {
                options.AddAdditionalCapability("version", remoteBrowserVersion);
            }

            // Add RemoteCapabilites section if it exists
            options.SetDriverOptions();

            return options.ToCapabilities();
        }

        /// <summary>
        /// Reads the RemoteSeleniumCapsMaqs section and appends to the driver options
        /// </summary>
        /// <param name="driverOptions">The driver options to make this an extension method</param>
        /// <returns>The altered <see cref="DriverOptions"/> driver options</returns>
        private static DriverOptions SetDriverOptions(this DriverOptions driverOptions)
        {
            Dictionary<string, string> remoteCapabilitySection = Config.GetSection(ConfigSection.RemoteSeleniumCapsMaqs);

            if (remoteCapabilitySection == null)
            {
                return driverOptions;
            }

            foreach (KeyValuePair<string, string> keyValue in remoteCapabilitySection)
            {
                if (remoteCapabilitySection[keyValue.Key].Length > 0)
                {
                    if (driverOptions is ChromeOptions)
                    {
                        ((ChromeOptions)driverOptions).AddAdditionalCapability(keyValue.Key, keyValue.Value, true);
                    }
                    else if (driverOptions is FirefoxOptions)
                    {
                        ((FirefoxOptions)driverOptions).AddAdditionalCapability(keyValue.Key, keyValue.Value, true);
                    }
                    else if (driverOptions is InternetExplorerOptions)
                    {
                        ((InternetExplorerOptions)driverOptions).AddAdditionalCapability(keyValue.Key, keyValue.Value, true);
                    }
                    else
                    {
                        driverOptions.AddAdditionalCapability(keyValue.Key, keyValue.Value);
                    }
                }
            }

            return driverOptions;
        }

        /// <summary>
        /// Get the timeout timespan
        /// </summary>
        /// <returns>The timeout time span</returns>
        private static TimeSpan GetTimeoutTime()
        {
            int timeoutTime = Convert.ToInt32(Config.GetValueForSection(SELENIUMSECTION, "BrowserTimeout", "0"));
            return TimeSpan.FromMilliseconds(timeoutTime);
        }

        /// <summary>
        /// Get the wait timespan
        /// </summary>
        /// <returns>The wait time span</returns>
        private static TimeSpan GetWaitTime()
        {
            int waitTime = Convert.ToInt32(Config.GetValueForSection(SELENIUMSECTION, "BrowserWaitTime", "0"));
            return TimeSpan.FromMilliseconds(waitTime);
        }

        /// <summary>
        /// get the browser size
        /// </summary>
        /// <returns>string of desired browser size</returns>
        private static string GetBrowserSize()
        {
            return Config.GetValueForSection(SELENIUMSECTION, "BrowserSize", "MAXIMIZE").ToUpper();
        }

        /// <summary>
        /// Sets the browser size based on selector in app.config
        /// </summary>
        /// <param name="webDriver">the webDriver from the Browser method</param>
        private static void SetBrowserSize(IWebDriver webDriver)
        {
            string size = GetBrowserSize();

            if (size == "MAXIMIZE")
            {
                webDriver.Manage().Window.Maximize();
            }
            else if (size == "DEFAULT")
            {
                // do nothing to have the browser window come up unchanged.
            }
            else
            {
                GetWindowSizeString(size, out int width, out int height);

                webDriver.Manage().Window.Size = new Size(width, height);
            }
        }

        /// <summary>
        /// Get the browser/browser size as a string
        /// </summary>
        /// <returns>The browser size as a string - Specifically for Chrome options</returns>
        private static string GetWindowSizeString()
        {
            string size = GetBrowserSize();

            if (size == "MAXIMIZE" || size == "DEFAULT")
            {
                return "window-size=1920,1080";
            }
            else
            {
                GetWindowSizeString(size, out int width, out int height);

                return string.Format("window-size={0},{1}", width, height);
            }
        }

        /// <summary>
        /// Get the window size as a string
        /// </summary>
        /// <param name="size">The size in a #X# format</param>
        /// <param name="width">The browser width</param>
        /// <param name="height">The browser height</param>
        private static void GetWindowSizeString(string size, out int width, out int height)
        {
            string[] sizes = size.Split('X');

            if (!size.Contains("X") || sizes.Length != 2)
            {
                throw new ArgumentException("Browser size is expected to be in an expected format: 1920x1080");
            }

            if (!int.TryParse(sizes[0], out width) || !int.TryParse(sizes[1], out height))
            {
                throw new InvalidCastException("Length and Width must be a string that is an integer value: 400x400");
            }
        }

        /// <summary>
        /// Initialize a new IE driver
        /// </summary>
        /// <returns>A new IE driver</returns>
        private static IWebDriver InitializeIEDriver()
        {
            var options = new InternetExplorerOptions
            {
                IgnoreZoomLevel = true
            };
            return new InternetExplorerDriver(GetDriverLocation("IEDriverServer.exe"), options, GetCommandTimeout());
        }

        /// <summary>
        /// Initialize a new Firefox driver
        /// </summary>
        /// <returns>A new Firefox driver</returns>
        private static IWebDriver InitializeFirefoxDriver()
        {
            FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(GetDriverLocation("geckodriver.exe"), "geckodriver.exe");
            FirefoxOptions firefoxOptions = new FirefoxOptions
            {
                Profile = new FirefoxProfile()
            };

            // Add support for encoding 437 that was removed in .net core
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            return new FirefoxDriver(service, firefoxOptions, GetCommandTimeout());
        }

        /// <summary>
        /// Initialize a new Chrome driver
        /// </summary>
        /// <returns>A new Chrome driver</returns>
        private static IWebDriver InitializeChromeDriver()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("test-type");
            chromeOptions.AddArguments("--disable-web-security");
            chromeOptions.AddArguments("--allow-running-insecure-content");
            chromeOptions.AddArguments("--disable-extensions");
            return new ChromeDriver(GetDriverLocation("chromedriver.exe"), chromeOptions, GetCommandTimeout());
        }

        /// <summary>
        /// Initialize a new headless Chrome driver
        /// </summary>
        /// <returns>A new headless Chrome driver</returns>
        private static IWebDriver InitializeHeadlessChromeDriver()
        {
            ChromeOptions headlessChromeOptions = new ChromeOptions();
            headlessChromeOptions.AddArgument("test-type");
            headlessChromeOptions.AddArguments("--disable-web-security");
            headlessChromeOptions.AddArguments("--allow-running-insecure-content");
            headlessChromeOptions.AddArguments("--disable-extensions");
            headlessChromeOptions.AddArguments("--headless");
            headlessChromeOptions.AddArguments(GetWindowSizeString());
            return new ChromeDriver(GetDriverLocation("chromedriver.exe"), headlessChromeOptions, GetCommandTimeout());
        }

        /// <summary>
        /// Initialize a new Edge driver
        /// </summary>
        /// <returns>A new Edge driver</returns>
        private static IWebDriver InitializeEdgeDriver()
        {
            EdgeOptions edgeOptions = new EdgeOptions
            {
                PageLoadStrategy = PageLoadStrategy.Normal
            };

            return new EdgeDriver(GetDriverLocation("MicrosoftWebDriver.exe", GetProgramFilesFolder("Microsoft Web Driver", "MicrosoftWebDriver.exe")), edgeOptions, GetCommandTimeout());
        }
    }
}