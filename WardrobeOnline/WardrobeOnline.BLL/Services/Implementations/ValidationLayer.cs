using System.Net;

using WardrobeOnline.BLL.Models;
using WardrobeOnline.BLL.Models.Interfaces;
using WardrobeOnline.BLL.Services.Interfaces;
using WardrobeOnline.DAL.Interfaces;

namespace WardrobeOnline.BLL.Services.Implementations
{
    public class ValidationLayer<TEntityDTO>(
        ICRUDProvider<TEntityDTO> _crudProvider) : IValidationLayer<TEntityDTO> where TEntityDTO : class, IEntityDTO
    {
        public async Task<ErrorResponse?> Delete(int id)
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
                ErrorResponse errorResponse = new ErrorResponse();
                errorResponse.Body = $"Failed to delete {nameof(TEntityDTO)} {id}";
                errorResponse.Code = (int)HttpStatusCode.BadRequest;
                return errorResponse;
            }

            return null;
        }

        public async Task<(ErrorResponse?, TEntityDTO?)> Get(int id)
        {
            if (IsNotCorrectID(id))
            {
                ErrorResponse errorResponse = new ErrorResponse();
                errorResponse.Body = "ID sent by client is invalid";
                errorResponse.Code = (int)HttpStatusCode.BadRequest;
                return (errorResponse, null);
            }

            TEntityDTO? responseDTO = await _crudProvider.TryGetAsync(id);

            if (responseDTO == null)
            {
                ErrorResponse errorResponse = new ErrorResponse();
                errorResponse.Body = "Entity with such ID wasn't found";
                errorResponse.Code = (int)HttpStatusCode.BadRequest;
                return (errorResponse, null);
            }

            return (null, responseDTO);
        }

        public async Task<(ErrorResponse?, IReadOnlyList<TEntityDTO>? entityDTOs)> GetPaged(int page, int pageQuantity)
        {
            if (page < 1)
            {
                ErrorResponse errorResponse = new ErrorResponse();
                errorResponse.Body = "Page cannot be below 1";
                errorResponse.Code = (int)HttpStatusCode.BadRequest;
                return (errorResponse, null);
            }

            if (pageQuantity < 2)
            {
                ErrorResponse errorResponse = new ErrorResponse();
                errorResponse.Body = "Page quantity cannot be below 2";
                errorResponse.Code = (int)HttpStatusCode.BadRequest;
                return (errorResponse, null);
            }

            IReadOnlyList<TEntityDTO> paged;

            try
            {
                paged = await _crudProvider.GetPagedQuantity(page, pageQuantity);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                ErrorResponse errorResponse = new ErrorResponse();
                errorResponse.Body = $"{ex.Message}. ActualValue = {ex.ActualValue}";
                errorResponse.Code = (int)HttpStatusCode.BadRequest;
                return (errorResponse, null);
            }

            if(paged.Count == 0)
            {
                ErrorResponse errorResponse = new ErrorResponse();
                errorResponse.Body = $"By unknown error, resulted page was empty";
                errorResponse.Code = (int)HttpStatusCode.InternalServerError;
                return (errorResponse, null);
            }

            return (null, paged);
        }

        public async Task<(ErrorResponse?, TEntityDTO?)> Post(TEntityDTO entityDTO)
        {
            if (entityDTO is null)
            {
                ErrorResponse errorResponse = new ErrorResponse();
                errorResponse.Body = "Body contains no info";
                errorResponse.Code = (int)HttpStatusCode.BadRequest;
                return (errorResponse, null);
            }

            TEntityDTO? responseDTO = await _crudProvider.TryAddAsync(entityDTO);
            bool passed = responseDTO is not null;

            if (!passed)
            {
                ErrorResponse errorResponse = new ErrorResponse();
                errorResponse.Body = "Failed to apply data";
                errorResponse.Code = (int)HttpStatusCode.BadRequest;
                return (errorResponse, null);
            }

            return (null, responseDTO);
        }

        public async Task<(ErrorResponse?, TEntityDTO?)> Put(int? id, TEntityDTO entityDTO)
        {
            bool hasID = id is not null || entityDTO.ID != default;

            if (!hasID)
            {
                ErrorResponse errorResponse = new ErrorResponse();
                errorResponse.Body = "Request contains no ID, unable to proceed";
                errorResponse.Code = (int)HttpStatusCode.BadRequest;
                return (errorResponse, null);
            }

            id = id is null ? entityDTO.ID : id; // выбираем ID; id в запросе приоритетен

            if (IsNotCorrectID(id.Value))
            {
                ErrorResponse errorResponse = new ErrorResponse();
                errorResponse.Body = "ID sent by client is invalid";
                errorResponse.Code = (int)HttpStatusCode.BadRequest;
                return (errorResponse, null);
            }

            entityDTO.ID = id.Value;

            TEntityDTO? responseDTO = await _crudProvider.TryUpdateAsync(entityDTO);
            bool passed = responseDTO is not null;

            if (!passed)
            {
                ErrorResponse errorResponse = new ErrorResponse();
                errorResponse.Body = "Failed to update data";
                errorResponse.Code = (int)HttpStatusCode.BadRequest;
                return (errorResponse, null);
            }

            return (null, responseDTO);
        }

        private bool IsNotCorrectID(int id) => id < 1;
    }
}
