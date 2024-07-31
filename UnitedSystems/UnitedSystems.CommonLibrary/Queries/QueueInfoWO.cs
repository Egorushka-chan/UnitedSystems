using UnitedSystems.CommonLibrary.Queries;
using UnitedSystems.CommonLibrary.Queries.Interfaces;

namespace UnitedSystems.CommonLibrary.Queries
{
    public class QueueInfoWO : IQueueInfo
    {
        public static string GetQueueKey()
        {
            return QueueEnumConverter.GetChannelName(QueueType.WOtoMDM);
        }
    }
}
