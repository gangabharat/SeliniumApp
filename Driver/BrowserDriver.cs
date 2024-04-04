using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace SeliniumApp.Driver
{
    public interface IBrowserDriver
    {
        IWebDriver GetChromeDriver();
        IWebDriver GetFirefoxDriver();
        IWebDriver GetEdgeDriver();
        IWebDriver GetSafariDriver();

    }
    public class BrowserDriver : IBrowserDriver
    {
        public IWebDriver GetChromeDriver()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("--headless");
            return new ChromeDriver(chromeOptions);
        }

        public IWebDriver GetFirefoxDriver()
        {

            new DriverManager().SetUpDriver(new FirefoxConfig());
            FirefoxOptions firefoxOptions = new FirefoxOptions();
            firefoxOptions.AddArguments("--headless");
            return new FirefoxDriver(firefoxOptions);
        }

        public IWebDriver GetEdgeDriver()
        {

            new DriverManager().SetUpDriver(new EdgeConfig());
            EdgeOptions edgeOptions = new EdgeOptions();
            edgeOptions.AddArguments("--headless");
            return new EdgeDriver(edgeOptions);
        }

        public IWebDriver GetSafariDriver()
        {
            //new DriverManager().SetUpDriver(new SafariConfig());            
            SafariOptions safariOptions = new SafariOptions();
            //safariOptions.add("--headless");
            return new SafariDriver(safariOptions);
        }

    }
    public enum BrowserType
    {
        Firefox,
        Safari,
        Chrome,
        Edge
    }
}
