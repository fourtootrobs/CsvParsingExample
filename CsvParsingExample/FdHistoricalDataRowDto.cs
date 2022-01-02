using CsvHelper.Configuration.Attributes;

namespace CsvParsingExample
{
    public class FdHistoricalDataRowDto
    {
        [Name("Div")]
        public string Division { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        [Name("FTHG")]
        public int FullTimeHomeGoals { get; set; }
        [Name("FTAG")]
        public int FullTimeAwayGoals { get; set; }
        [Name("FTR")]
        public string FullTimeResult { get; set; }
    }
}
