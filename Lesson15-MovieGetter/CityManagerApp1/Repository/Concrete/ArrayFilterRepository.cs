using CityManagerApp1.Repository.Abstract;

namespace CityManagerApp1.Repository.Concrete
{
    public class ArrayFilterRepository : IArrayFilterRepository
    {
        public string[] GenerateRandomArrayFrom(string[] array, int generationCount)
        {
            Random rand = new Random();
            string[] newGeneratedString = new string[generationCount];
            if (generationCount > array.Length)
                throw new ArgumentException();

            for (int i = 0; i < generationCount; i++)
            {
                newGeneratedString[i] = array[rand.Next(array.Length)];
            }

            return newGeneratedString;
        }
    }
}
