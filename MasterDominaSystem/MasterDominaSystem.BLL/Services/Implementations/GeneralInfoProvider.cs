using MasterDominaSystem.BLL.Services.Abstractions;

namespace MasterDominaSystem.BLL.Services.Implementations
{
    public class GeneralInfoProvider : IGeneralInfoProvider
    {
        public List<string> MessagePool { get; set; } = [];
        public List<string> GetRequestsWO { get; set; } = [];
        public List<string> PostRequestsWO { get; set; } = [];
        public List<string> PutRequestsWO { get; set; } = [];
        public List<string> DeleteRequestWO { get; set; } = [];
        public List<string> GetRequestsMES { get; set; } = [];
        public List<string> EnsuredRequestMES { get; set; } = [];
        public List<string> CacheStatusMES { get; set; } = [];
    }
}
