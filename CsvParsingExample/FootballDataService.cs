using System.Net.Http;
using System.Threading.Tasks;

namespace CsvParsingExample
{
    public class FootballDataService
        : IFootballDataService
    {
        private readonly HttpClient _httpClient;

        public FootballDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpStreamCsvReaderContext> BeginReadingCsvAsync(string requestUri)
        {
            var responseMessage = await _httpClient.GetAsync(
                requestUri, 
                HttpCompletionOption.ResponseHeadersRead);

            responseMessage.EnsureSuccessStatusCode();

            var stream = await responseMessage.Content.ReadAsStreamAsync();

            return new HttpStreamCsvReaderContext(responseMessage, stream);
        }
    }
}
