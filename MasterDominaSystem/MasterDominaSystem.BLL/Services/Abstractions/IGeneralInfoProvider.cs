namespace MasterDominaSystem.BLL.Services.Abstractions
{
    public interface IGeneralInfoProvider
    {
        List<string> MessagePool { get; set; }
        List<string> GetRequestsWO { get; set; }
        List<string> PostRequestsWO { get; set; }
        List<string> PutRequestsWO {get; set; }
        List<string> DeleteRequestWO { get; set; }
        List<string> GetRequestsMES { get; set; }
        List<string> EnsuredRequestMES { get; set; }
        List<string> CacheStatusMES { get; set; }
    }
}
