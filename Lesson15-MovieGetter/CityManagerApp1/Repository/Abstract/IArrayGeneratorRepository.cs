namespace CityManagerApp1.Repository.Abstract
{
    public interface IArrayGeneratorRepository
    {
        string[] GenerateArrayFromFile(string filePath);
    }
}
