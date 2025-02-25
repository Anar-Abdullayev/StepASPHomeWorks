using CityManagerApp1.Entities;

namespace CityManagerApp1.Repository.Abstract
{
    public interface IMovieRepository
    {
        Task<bool> AddMovie(Movie movie);
    }
}
