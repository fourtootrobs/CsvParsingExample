using System.Threading.Tasks;

namespace CsvParsingExample
{
    public interface IFootballDataService
    {
        Task<HttpStreamCsvReaderContext> BeginReadingCsvAsync(string requestUri);
    }
}
