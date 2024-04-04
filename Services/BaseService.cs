using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SeliniumApp.AppSettings;

namespace SeliniumApp.Services
{
    public interface IBaseService
    {
        public ApplicationOptions Options { get; }
        public CustomCommandLineOptions CommandLineOptions { get; }
        public ILogger<BaseService> Logger { get; }
    }
    public class BaseService : IBaseService
    {
        private readonly ApplicationOptions _options;

        private readonly CustomCommandLineOptions _commandLineOptions;

        private readonly ILogger<BaseService> _logger;
        public BaseService(ILogger<BaseService> logger, IOptions<ApplicationOptions> options, CustomCommandLineOptions commandLineOptions)
        {
            _options = options.Value;

            Directory.CreateDirectory(_options.GetApplicationDataPath());
            Directory.CreateDirectory(_options.GetApplicationTempPath());

            _logger = logger;
            _commandLineOptions = commandLineOptions;

            _logger.LogDebug("Application Data Path {0}", _options.GetApplicationDataPath());
            _logger.LogDebug("Application Temp Path {0}", _options.GetApplicationTempPath());

            _logger.LogDebug("Input appsettings.json {0}", _options.ToJson());

            _options.ApplicationInputPath = _commandLineOptions.Input ?? _options.GetApplicationTempPath();
            _options.ApplicationOutputPath = _commandLineOptions.Output ?? _options.GetApplicationDataPath();

            _logger.LogInformation("Application Input Path {0}", _options.ApplicationInputPath);
            _logger.LogInformation("Application Output Path {0}", _options.ApplicationOutputPath);

            Directory.CreateDirectory(_options.ApplicationInputPath);
            Directory.CreateDirectory(_options.ApplicationOutputPath);

        }

        public ApplicationOptions Options { get => _options; }
        public CustomCommandLineOptions CommandLineOptions { get => _commandLineOptions; }
        public ILogger<BaseService> Logger { get => _logger; }

    }
}
