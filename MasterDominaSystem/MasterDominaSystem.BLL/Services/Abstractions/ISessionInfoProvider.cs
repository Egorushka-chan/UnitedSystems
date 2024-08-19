using UnitedSystems.CommonLibrary.ManyEntitiesSender.IntegrationEvents;

namespace MasterDominaSystem.BLL.Services.Abstractions
{
    /// <summary>
    /// Тупо хранилка данных, для проверки работоспособности
    /// </summary>
    public interface ISessionInfoProvider
    {
        List<string> MessagePool { get; set; }
        // Many Entities Sender
        List<MESReturnedObjectsEvent> GetRequestsMES { get; set; }
        // Wardrobe Online
        List<string> PostRequestsWO { get; set; }
        List<string> PutRequestsWO { get; set; }
        List<string> DeleteRequestWO { get; set; }
    }
}
