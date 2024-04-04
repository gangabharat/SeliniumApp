using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using SeliniumApp.AppSettings;

namespace SeliniumApp.Driver
{
    public interface IDriverFixture
    {
        IWebDriver Driver { get; }
    }
    public class DriverFixture : IDriverFixture
    {
        //IWebDriver driver;
        private readonly BrowserParameters options;
        private readonly IBrowserDriver browserDriver;

        readonly IWebDriver _driver;

        public IWebDriver Driver
        {
            get { return _driver; }
            //set { _driver = value; }
        }

        //DI is happening
        public DriverFixture(IOptions<ApplicationOptions> options, IBrowserDriver browserDriver)
        {
            this.options = options.Value.Browser!;
            this.browserDriver = browserDriver;
            _driver = GetWebDriver();
        }
        //public IWebDriver Driver => driver;
        private IWebDriver GetWebDriver()
        {
            return options.BrowserType switch
            {
                BrowserType.Chrome => browserDriver.GetChromeDriver(),
                BrowserType.Firefox => browserDriver.GetFirefoxDriver(),
                BrowserType.Edge => browserDriver.GetEdgeDriver(),
                BrowserType.Safari => browserDriver.GetSafariDriver(),
                _ => browserDriver.GetChromeDriver()
            };
        }
        public void Dispose()
        {
            _driver.Quit();
        }
    }

}
