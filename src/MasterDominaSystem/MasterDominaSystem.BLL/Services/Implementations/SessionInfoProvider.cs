using MasterDominaSystem.BLL.Services.Abstractions;

using UnitedSystems.CommonLibrary.ManyEntitiesSender.IntegrationEvents;

namespace MasterDominaSystem.BLL.Services.Implementations
{
    /// <inheritdoc cref="ISessionInfoProvider"/>
    public class SessionInfoProvider : ISessionInfoProvider
    {
        public List<string> MessagePool { get; set; } = [];
        // Many Entities Sender
        public List<MESReturnedObjectsEvent> GetRequestsMES { get; set; } = [];
        public List<string> PostRequestsWO { get; set; } = [];
        public List<string> PutRequestsWO { get; set; } = [];
        public List<string> DeleteRequestWO { get; set; } = [];
        // Wardrobe Online
        //public List<GetWOInfo> GetRequestsWO { get; set; } = [];
        //public List<PostWOInfo> PostRequestsWO { get; set; } = [];
        //public List<PutWOInfo> PutRequestsWO { get; set; } = [];
        //public List<DeleteWOInfo> DeleteRequestWO { get; set; } = [];
        //public List<ErrorWOInfo> ErrorRequestWO { get; set; } = [];
        //public List<PagedGetWOInfo> PagedRequestWO { get; set; } = [];
    }
}
