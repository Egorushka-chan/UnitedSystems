namespace UnitedSystems.CommonLibrary.Queries
{
    public static class QueueEnumConverter
    {
        public static string GetChannelName(QueueType queueType)
        {
            switch (queueType) {
                case QueueType.MESToMDM:
                    return "FromMESToMDMQueue";
                case QueueType.WOtoMDM:
                    return "FromWOToMDMQueue";
                default:
                    return queueType.ToString();
            }
        }
    }
}