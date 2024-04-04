using Microsoft.Extensions.Options;
using SeliniumApp.AppSettings;
using System.Net;
using System.Text;
using System.Text.Json;

namespace SeliniumApp.Services
{
    public interface IHttpClientService
    {
        CookieContainer CookieJar { get; }
        HttpClient Client { get; }
        void AddCookie(string key, string value);
        Task<HttpResponseMessage> Get(string apiUrl, CancellationToken token = default);
        Task<byte[]> GetByteArrayAsync(string apiUrl, CancellationToken token = default);
        Task<HttpResponseMessage> Post(string apiUrl, object postObject, CancellationToken token = default);
        Task<HttpResponseMessage> Put(string apiUrl, object putObject, CancellationToken token = default);
        Task<HttpResponseMessage> Delete(string apiUrl, CancellationToken token = default);
    }
    public class HttpClientService : IHttpClientService
    {
        private readonly CookieContainer _cookieJar;
        private readonly HttpClient _client;
        private readonly Uri _baseAddress;
        private readonly ApplicationOptions _options;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        public CookieContainer CookieJar { get { return _cookieJar; } }
        public HttpClient Client { get { return _client; } }
        public HttpClientService(IOptions<ApplicationOptions> options)
        {
            _options = options.Value;
            _cookieJar = new CookieContainer();
            _baseAddress = new(_options.Endpoint!.BaseUrl!);
            _jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

            _client = new(new HttpClientHandler() { CookieContainer = _cookieJar })
            {
                BaseAddress = _baseAddress
            };
        }
        public void AddCookie(string key, string value)
        {
            _cookieJar.Add(_baseAddress, new Cookie(key, value));
        }
        public async Task<HttpResponseMessage> Get(string apiUrl, CancellationToken token = default)
        {
            return await _client.GetAsync(apiUrl, token).ConfigureAwait(false);
        }
        public async Task<byte[]> GetByteArrayAsync(string apiUrl, CancellationToken token = default)
        {
            return await _client.GetByteArrayAsync(apiUrl, token).ConfigureAwait(false);
        }
        public async Task<HttpResponseMessage> Post(string apiUrl, object postObject, CancellationToken token = default)
        {
            var json = JsonSerializer.Serialize(postObject, _jsonSerializerOptions);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            return await _client.PostAsync(apiUrl, data, token).ConfigureAwait(false);
        }
        public async Task<HttpResponseMessage> Put(string apiUrl, object putObject, CancellationToken token = default)
        {
            var json = JsonSerializer.Serialize(putObject, _jsonSerializerOptions);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            return await _client.PutAsync(apiUrl, data, token).ConfigureAwait(false);
        }
        public async Task<HttpResponseMessage> Delete(string apiUrl, CancellationToken token = default)
        {
            return await _client.DeleteAsync(apiUrl, token).ConfigureAwait(false);
        }
    }
}
