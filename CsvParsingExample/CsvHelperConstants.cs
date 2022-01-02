using System.Globalization;
using CsvHelper.Configuration;

namespace CsvParsingExample
{
    public static class CsvHelperConstants
    {
        public static readonly CsvConfiguration DefaultCsvConfig = new CsvConfiguration(
            CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            HeaderValidated = null,
            MissingFieldFound = null,
            SanitizeForInjection = true
        };
    }
}
