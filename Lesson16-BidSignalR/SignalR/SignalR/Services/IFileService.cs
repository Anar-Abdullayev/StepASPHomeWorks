namespace SignalR.Services
{
    public interface IFileService
    {
        Task<double> Read();
        Task Write(double data);
    }
}
