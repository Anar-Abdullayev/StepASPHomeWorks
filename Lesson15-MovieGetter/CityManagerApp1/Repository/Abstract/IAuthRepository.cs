using CityManagerApp1.Entities;

namespace CityManagerApp1.Repository.Abstract
{
    public interface IAuthRepository
    {
        Task<User> Register(User user,string password);
        Task<User> Login(string username,string password);
        Task<bool> UserExists(string username);
    }
}
