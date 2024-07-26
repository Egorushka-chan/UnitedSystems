using UnitedSystems.CommonLibrary.Queries;
using UnitedSystems.CommonLibrary.Queries.Interfaces;

namespace MasterDominaSystem.RMQL.Models.Queues
{
    public class QueueInfoWO : IQueueInfo
    {
        public static string GetQueueKey()
        {
            return QueueEnumConverter.GetChannelName(QueueType.WOtoMDM);
        }
    }
}
