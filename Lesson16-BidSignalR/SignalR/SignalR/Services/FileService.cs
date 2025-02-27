
namespace SignalR.Services
{
    public class FileService : IFileService
    {
        public async Task<double> Read()
        {
            var data = await File.ReadAllTextAsync("data.txt");
            return double.Parse(data);
        }

        public async Task Write(double data)
        {
            await File.WriteAllTextAsync("data.txt", data.ToString());
        }
    }
}
