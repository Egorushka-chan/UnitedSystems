namespace UnitedSystems.CommonLibrary.Models.ManyEntitiesSender.Messages.Produced
{
    public class HandCacheSnapshot
    {
        public int TotalCount { get; set; }
        public Dictionary<string, int> KeyCount { get; set; } = [];
    }
}
