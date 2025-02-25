using CityManagerApp1.Consts;
using CityManagerApp1.Repository.Abstract;

namespace CityManagerApp1.Repository.Concrete
{
    public class MovieSearchRepository : IMovieSearchRepository
    {
        public string GetMovieList(string searchPattern)
        {
            HttpClient client = new HttpClient();
            var result = client.GetAsync(WebSearchAPIConsts.WebAPISearchByTitle + searchPattern).Result;
            string resultString = result.Content.ReadAsStringAsync().Result;

            return resultString;
        }
    }
}
