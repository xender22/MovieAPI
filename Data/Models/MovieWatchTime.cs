using CsvHelper.Configuration.Attributes;

namespace MovieAPI.Data.Models
{
    public class MovieWatchTime
    {
        [Name("movieId")]
        public int MovieId { get; set; }
        [Name("watchDurationMs")]
        public long WatchDurationMs { get; set; }
    }
}
