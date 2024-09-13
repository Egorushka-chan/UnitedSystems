namespace UnitedSystems.CommonLibrary.Queries
{
    public static class QueueEnumConverter
    {
        public static string GetChannelName(QueueType queueType)
        {
            return queueType switch
            {
                QueueType.MESToMDM => "FromMESToMDMQueue",
                QueueType.WOtoMDM => "FromWOToMDMQueue",
                _ => queueType.ToString(),
            };
        }
    }
}