using UnitedSystems.CommonLibrary.Models.WardrobeOnline.Messages;

namespace MasterDominaSystem.BLL.Services.Abstractions
{
    public interface IGeneralInfoProvider
    {
        List<string> MessagePool { get; set; }
        List<GetWOInfo> GetRequestsWO { get; set; }
        List<PostWOInfo> PostRequestsWO { get; set; }
        List<PutWOInfo> PutRequestsWO {get; set; }
        List<DeleteWOInfo> DeleteRequestWO { get; set; }
        List<string> GetRequestsMES { get; set; }
        List<string> EnsuredRequestMES { get; set; }
        List<string> CacheStatusMES { get; set; }
    }
}
