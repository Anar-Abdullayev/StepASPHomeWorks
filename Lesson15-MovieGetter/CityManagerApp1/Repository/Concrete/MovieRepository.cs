using CityManagerApp1.Data;
using CityManagerApp1.Entities;
using CityManagerApp1.Repository.Abstract;

namespace CityManagerApp1.Repository.Concrete
{
    public class MovieRepository(AppDataContext context) : IMovieRepository
    {
        public async Task<bool> AddMovie(Movie movie)
        {
            var result = await context.Movies.AddAsync(movie);
            return (await context.SaveChangesAsync()) > 0;
        }
    }
}
