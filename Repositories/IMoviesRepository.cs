using MovieAPI.Data.Models;
using System.Collections.Generic;

namespace MovieAPI.Repositories
{
    public interface IMoviesRepository
    {
        bool AddMetadata(Metadata metadata);
        List<Metadata> GetMovieMetadaById(int id);
        List<MovieStat> GetMoviesStats();
    }
}
