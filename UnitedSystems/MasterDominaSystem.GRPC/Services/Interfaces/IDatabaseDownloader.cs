namespace MasterDominaSystem.GRPC.Services.Interfaces
{
    public interface IDatabaseDownloader
    {
        Task DownloadDataAsync(CancellationToken cancellationToken = default);
    }
}
