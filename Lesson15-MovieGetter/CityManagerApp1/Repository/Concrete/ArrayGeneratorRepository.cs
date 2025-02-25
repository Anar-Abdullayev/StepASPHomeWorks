using CityManagerApp1.Repository.Abstract;

namespace CityManagerApp1.Repository.Concrete
{
    public class ArrayGeneratorRepository : IArrayGeneratorRepository
    {
        public string[] GenerateArrayFromFile(string filePath)
        {
            var patternArray = File.ReadLines(filePath).ToArray();
            return patternArray;
        }

    }
}
