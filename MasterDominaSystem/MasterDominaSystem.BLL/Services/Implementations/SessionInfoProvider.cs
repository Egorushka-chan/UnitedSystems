using MasterDominaSystem.BLL.Services.Abstractions;

using UnitedSystems.CommonLibrary.ManyEntitiesSender.IntegrationEvents;

namespace MasterDominaSystem.BLL.Services.Implementations
{
    public class SessionInfoProvider : ISessionInfoProvider
    {
        public List<string> MessagePool { get; set; } = [];
        // Many Entities Sender
        public List<MESReturnedObjectsEvent> GetRequestsMES { get; set; } = [];
        public List<string> EnsuredRequestMES { get; set; } = [];
        public List<string> CacheStatusMES { get; set; } = [];
        // Wardrobe Online
        //public List<GetWOInfo> GetRequestsWO { get; set; } = [];
        //public List<PostWOInfo> PostRequestsWO { get; set; } = [];
        //public List<PutWOInfo> PutRequestsWO { get; set; } = [];
        //public List<DeleteWOInfo> DeleteRequestWO { get; set; } = [];
        //public List<ErrorWOInfo> ErrorRequestWO { get; set; } = [];
        //public List<PagedGetWOInfo> PagedRequestWO { get; set; } = [];
    }
}
