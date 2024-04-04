using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using SeliniumApp.AppSettings;
using SeliniumApp.Driver;

namespace SeliniumApp.Services
{
    public interface IRunService
    {
        public Task Run();
    }
    public class RunService : IRunService
    {
        private readonly ApplicationOptions _options;
        private readonly IBaseService _service;
        private readonly IHttpClientService _httpClientService;
        private readonly IDriverFixture _webDriver;
        private readonly ITempFileService _tempFileService;

        public RunService(IBaseService baseService, IHttpClientService httpClientService, IDriverFixture driverFixture, ITempFileService tempFileService)
        {
            _options = baseService.Options;
            _service = baseService;
            _httpClientService = httpClientService;
            _webDriver = driverFixture;
            //driverFixture.
            _tempFileService = tempFileService;
        }
        public async Task Run()
        {
            _webDriver.Driver.Url = "https://sarigamamusicmasti.blogspot.com/";
            Thread.Sleep(1000 * 3);
            _webDriver.Driver.Quit();

            await Task.CompletedTask;
        }


    }
}
