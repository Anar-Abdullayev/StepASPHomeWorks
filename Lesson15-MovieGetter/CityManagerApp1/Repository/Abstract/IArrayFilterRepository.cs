namespace CityManagerApp1.Repository.Abstract
{
    public interface IArrayFilterRepository
    {
        string[] GenerateRandomArrayFrom(string[] array, int generationCount);
    }
}
