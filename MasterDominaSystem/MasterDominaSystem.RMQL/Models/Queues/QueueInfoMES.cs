using MasterDominaSystem.RMQL.Models.Queues.Interface;

using UnitedSystems.CommonLibrary.Queries;

namespace MasterDominaSystem.RMQL.Models.Queues
{
    internal class QueueInfoMES : IQueueInfo
    {
        public static string GetQueueKey()
        {
            return QueueEnumConverter.GetChannelName(QueueType.MESToMDM);
        }
    }
}
