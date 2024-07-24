namespace UnitedSystems.CommonLibrary.Models.ManyEntitiesSender.Messages.Produced
{
    public class LegCacheSnapshot
    {
        public int TotalCount { get; set; }
        public Dictionary<string, int> KeyCount { get; set; } = [];
    }
}
