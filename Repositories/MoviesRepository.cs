using CsvHelper;
using MovieAPI.Data.Models;
using MovieAPI.Extensions;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace MovieAPI.Repositories
{
    public class MoviesRepository : IMoviesRepository
    {
        private const string MetadataPath = "Data/Documents/metadata.csv";
        private const string StatsPath = "Data/Documents/stats.csv";

        private static readonly List<Metadata> Database = new List<Metadata>();

        private static CsvReader GetReader(string path)
        {
            var reader = new CsvReader(new StreamReader(path), CultureInfo.InvariantCulture);
            return reader;
        }

        private List<Metadata> GetMetadata()
        {
            using (var reader = GetReader(MetadataPath))
            {
                try
                {
                    var data = reader.GetRecords<Metadata>();
                    return data.ToList();
                }
                finally
                {
                    reader.Dispose();
                }
            }
        }

        private List<MovieWatchTime> GetMoviesWatchTime()
        {
            using (var reader = GetReader(StatsPath))
            {
                try
                {
                    var data = reader.GetRecords<MovieWatchTime>();
                    return data.ToList();
                }
                finally
                {
                    reader.Dispose();
                }
            }
        }

        public bool AddMetadata(Metadata metadata)
        {
            Database.Add(metadata);
            return true;
        }

        public List<Metadata> GetMovieMetadaById(int id)
        {
            var metadata = GetMetadata();
            var movieMetadata = metadata.Where(x => x.MovieId == id && x.IsValid())
                                        .GroupBy(x => x.Language)
                                        .Select(x => x.OrderByDescending(o => o.Id).FirstOrDefault())
                                        .OrderBy(x => x.Language)
                                        .ToList();
            return movieMetadata;
        }

        
        public List<MovieStat> GetMoviesStats()
        {
            // Gets movies from metadata , removes duplicates and pre-orders it.
            var metadata = GetMetadata().GroupBy(x => x.MovieId)
                .Select(x => x.OrderByDescending(o => o.Id))
                .Select(x => x.FirstOrDefault(f => f.Language == "EN"))
                .ToList();

            var moviesWatchTime = GetMoviesWatchTime();
            var moviesStats = new List<MovieStat>();

            // Calculate watch time for each movie
            moviesWatchTime.GroupBy(x => x.MovieId)
                           .ToList().ForEach(e =>
                           {
                               moviesStats.Add(new MovieStat
                               {
                                   MovieId = e.Key,
                                   Watches = e.Count(),
                                   AverageWatchDurationS = e.Sum(s => s.WatchDurationMs) / 1000
                               });
                           });

            // Creating movies stats
            // Using a reverse arry to allow dynamic removal
            for (var i = moviesStats.Count - 1; i >= 0; i--)
            {
                var movieInfo = metadata.Where(x => x.MovieId == moviesStats[i].MovieId).FirstOrDefault();
                if (movieInfo != null && movieInfo.IsValid())
                {
                    moviesStats[i].Title = movieInfo.Title;
                    moviesStats[i].AverageWatchDurationS /= moviesStats[i].Watches;
                    moviesStats[i].ReleaseYear = movieInfo.ReleaseYear;
                }
                else
                {
                    moviesStats.RemoveAt(i);
                }
            }
     
            return moviesStats;
        }
    }
}
