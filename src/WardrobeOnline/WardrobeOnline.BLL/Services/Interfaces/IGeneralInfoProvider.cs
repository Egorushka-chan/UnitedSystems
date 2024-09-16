namespace WardrobeOnline.BLL.Services.Interfaces
{
    /// <summary>
    /// Работает с информацией,которая может хранится в кэше
    /// </summary>
    public interface IGeneralInfoProvider
    {
        public Task<int> GetPersonSetCount(int id, CancellationToken token = default);
        public Task<int> GetTotalPersonCount(CancellationToken token = default);
        public Task<int> GetPersonClothCount(int id, CancellationToken token = default);
    }
}
