using UnitedSystems.CommonLibrary.Models.ManyEntitiesSender.Messages.Produced;
using UnitedSystems.CommonLibrary.Models.WardrobeOnline.Messages;

namespace MasterDominaSystem.BLL.Services.Abstractions
{
    public interface IGeneralInfoProvider
    {
        List<string> MessagePool { get; set; }
        // Many Entities Sender
        List<GetMESInfo> GetRequestsMES { get; set; }
        List<string> EnsuredRequestMES { get; set; }
        List<string> CacheStatusMES { get; set; }
        // Wardrobe Online
        List<GetWOInfo> GetRequestsWO { get; set; }
        List<PostWOInfo> PostRequestsWO { get; set; }
        List<PutWOInfo> PutRequestsWO { get; set; }
        List<DeleteWOInfo> DeleteRequestWO { get; set; }
        List<ErrorWOInfo> ErrorRequestWO { get; set; }
        List<PagedGetWOInfo> PagedRequestWO { get; set; }
    }
}
