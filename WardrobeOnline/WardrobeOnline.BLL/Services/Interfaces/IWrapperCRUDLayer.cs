using UnitedSystems.CommonLibrary.Models.WardrobeOnline;
using UnitedSystems.CommonLibrary.Models.WardrobeOnline.Interfaces;

namespace WardrobeOnline.BLL.Services.Interfaces
{
    public interface IWrapperCRUDLayer<TEntityDTO> where TEntityDTO : class, IEntityDTO
    {
        Task<(ErrorResponse?, TEntityDTO?)> Post(TEntityDTO entityDTO);
        Task<(ErrorResponse?, TEntityDTO?)> Get(int id);
        Task<(ErrorResponse?, TEntityDTO?)> Put(int? id, TEntityDTO entityDTO);
        Task<ErrorResponse?> Delete(int id);
        Task<(ErrorResponse?, IReadOnlyList<TEntityDTO>? entityDTOs)> GetPaged(int page, int pageQuantity);
    }
}
