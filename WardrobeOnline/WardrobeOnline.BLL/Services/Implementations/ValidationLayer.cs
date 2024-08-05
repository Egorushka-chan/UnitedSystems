using System.Net;

using UnitedSystems.CommonLibrary.Messages;
using UnitedSystems.CommonLibrary.WardrobeOnline.DTO.Interfaces;

using WardrobeOnline.BLL.Services.Interfaces;

namespace WardrobeOnline.BLL.Services.Implementations
{
    public class ValidationLayer<TEntityDTO>(ICRUDProvider<TEntityDTO> _crudProvider) : IWrapperCRUDLayer<TEntityDTO> 
        where TEntityDTO : class, IEntityDTO
    {
        public virtual async Task<ErrorResponse?> Delete(int id)
        {
            if(IsNotCorrectID(id))
            {
                return new() {
                    Body = "ID sent by client is invalid",
                    Code = (int)HttpStatusCode.BadRequest
                };
            }

            bool passed = await _crudProvider.TryRemoveAsync(id);
            if (!passed)
            {
                ErrorResponse errorResponse = new() {
                    Body = $"Failed to delete {nameof(TEntityDTO)} {id}",
                    Code = (int)HttpStatusCode.BadRequest
                };
                return errorResponse;
            }

            return null;
        }

        public virtual async Task<(ErrorResponse?, TEntityDTO?)> Get(int id)
        {
            if (IsNotCorrectID(id))
            {
                ErrorResponse errorResponse = new() {
                    Body = "ID sent by client is invalid",
                    Code = (int)HttpStatusCode.BadRequest
                };
                return (errorResponse, null);
            }

            TEntityDTO? responseDTO = await _crudProvider.TryGetAsync(id);

            if (responseDTO == null)
            {
                ErrorResponse errorResponse = new() {
                    Body = "Entity with such ID wasn't found",
                    Code = (int)HttpStatusCode.BadRequest
                };
                return (errorResponse, null);
            }

            return (null, responseDTO);
        }

        public virtual async Task<(ErrorResponse?, IReadOnlyList<TEntityDTO>? entityDTOs)> GetPaged(int page, int pageQuantity)
        {
            if (page < 1)
            {
                ErrorResponse errorResponse = new() {
                    Body = "Page cannot be below 1",
                    Code = (int)HttpStatusCode.BadRequest
                };
                return (errorResponse, null);
            }

            if (pageQuantity < 2)
            {
                ErrorResponse errorResponse = new() {
                    Body = "Page quantity cannot be below 2",
                    Code = (int)HttpStatusCode.BadRequest
                };
                return (errorResponse, null);
            }

            IReadOnlyList<TEntityDTO> paged;

            try
            {
                paged = await _crudProvider.GetPagedQuantity(page, pageQuantity);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                ErrorResponse errorResponse = new() {
                    Body = $"{ex.Message}. ActualValue = {ex.ActualValue}",
                    Code = (int)HttpStatusCode.BadRequest
                };
                return (errorResponse, null);
            }

            if(paged.Count == 0)
            {
                ErrorResponse errorResponse = new() {
                    Body = $"By unknown error, resulted page was empty",
                    Code = (int)HttpStatusCode.InternalServerError
                };
                return (errorResponse, null);
            }

            return (null, paged);
        }

        public virtual async Task<(ErrorResponse?, TEntityDTO?)> Post(TEntityDTO entityDTO)
        {
            if (entityDTO is null)
            {
                ErrorResponse errorResponse = new() {
                    Body = "Body contains no info",
                    Code = (int)HttpStatusCode.BadRequest
                };
                return (errorResponse, null);
            }

            TEntityDTO? responseDTO = await _crudProvider.TryAddAsync(entityDTO);
            bool passed = responseDTO is not null;

            if (!passed)
            {
                ErrorResponse errorResponse = new() {
                    Body = "Failed to apply data",
                    Code = (int)HttpStatusCode.BadRequest
                };
                return (errorResponse, null);
            }

            return (null, responseDTO);
        }

        public virtual async Task<(ErrorResponse?, TEntityDTO?)> Put(int? id, TEntityDTO entityDTO)
        {
            bool hasID = id is not null || entityDTO.ID != default;

            if (!hasID)
            {
                ErrorResponse errorResponse = new() {
                    Body = "Request contains no ID, unable to proceed",
                    Code = (int)HttpStatusCode.BadRequest
                };
                return (errorResponse, null);
            }

            id = id is null ? entityDTO.ID : id; // выбираем ID; id в запросе приоритетен

            if (IsNotCorrectID(id.Value))
            {
                ErrorResponse errorResponse = new() {
                    Body = "ID sent by client is invalid",
                    Code = (int)HttpStatusCode.BadRequest
                };
                return (errorResponse, null);
            }

            entityDTO.ID = id.Value;

            TEntityDTO? responseDTO = await _crudProvider.TryUpdateAsync(entityDTO);
            bool passed = responseDTO is not null;

            if (!passed)
            {
                ErrorResponse errorResponse = new() {
                    Body = "Failed to update data",
                    Code = (int)HttpStatusCode.BadRequest
                };
                return (errorResponse, null);
            }

            return (null, responseDTO);
        }

        private static bool IsNotCorrectID(int id) => id < 1;
    }
}
