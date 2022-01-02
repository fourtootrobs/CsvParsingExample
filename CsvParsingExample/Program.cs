using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Extensions.Http;

namespace CsvParsingExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();

            services.AddLogging(
                loggingBuilder =>
                {
                    loggingBuilder.AddConsole();
                    loggingBuilder.AddDebug();
                    loggingBuilder.SetMinimumLevel(LogLevel.Debug);
                });

            services.AddHttpClient<IFootballDataService, FootballDataService>(client =>
            {
                client.BaseAddress = new Uri("https://www.football-data.co.uk");
            })
            .AddPolicyHandler(
                HttpPolicyExtensions.HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                .WaitAndRetryAsync(
                    Backoff.DecorrelatedJitterBackoffV2(
                        medianFirstRetryDelay: TimeSpan.FromSeconds(1),
                        retryCount: 5)));

            using var serviceProvider = services.BuildServiceProvider();

            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

            var footballDataService = serviceProvider.GetRequiredService<IFootballDataService>();

            using var readerContext = await footballDataService.BeginReadingCsvAsync(
                "/mmz4281/1920/E0.csv");

            await foreach (var row in readerContext.CsvReader.GetRecordsAsync<FdHistoricalDataRowDto>())
            {
                logger.LogInformation(
                    $"{row.HomeTeam} {row.FullTimeHomeGoals}:{row.FullTimeAwayGoals} {row.AwayTeam}");
            }
        }
    }
}
