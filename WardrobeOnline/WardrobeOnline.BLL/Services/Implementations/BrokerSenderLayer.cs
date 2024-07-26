using UnitedSystems.CommonLibrary.Models.WardrobeOnline;
using UnitedSystems.CommonLibrary.Models.WardrobeOnline.Interfaces;
using UnitedSystems.CommonLibrary.Models.WardrobeOnline.Messages;
using UnitedSystems.CommonLibrary.Models.WardrobeOnline.Messages.Headers;

using WardrobeOnline.BLL.Services.Interfaces;
using WardrobeOnline.BPL.Abstractions;

namespace WardrobeOnline.BLL.Services.Implementations
{
    public class BrokerSenderLayer<TEntityDTO>(ICRUDProvider<TEntityDTO> _crudProvider, IMDMSender mdmSender) : ValidationLayer<TEntityDTO>(_crudProvider)
        where TEntityDTO : class, IEntityDTO
    {
        public override async Task<ErrorResponse?> Delete(int id)
        {
            var result = await base.Delete(id);
            string entityName = typeof(TEntityDTO).Name;

            if (result is null) {
                var delete = new DeleteWOInfo() {
                    StatusCode = 204,
                    Summary = $"Successful delete {entityName} with id {id}"
                };
                delete.RequestQuery.Add(nameof(id), id.ToString());
                mdmSender.Send(delete, MessageHeaderFromWO.DeleteRequestInfo);
            }
            else {
                var error = new ErrorWOInfo() {
                    Header = "DELETE",
                    Summary = $"Error during deleting {entityName} with id {id}",
                    ErrorResponse = result
                };
                error.Query.Add(nameof(id), id.ToString());
                mdmSender.Send(error, MessageHeaderFromWO.ErrorResponseInfo);
            }
            return result;
        }

        public override async Task<(ErrorResponse?, TEntityDTO?)> Get(int id)
        {
            var result = await base.Get(id);
            string entityName = typeof(TEntityDTO).Name;

            if (result.Item1 is null) {
                var get = new GetWOInfo() {
                    StatusCode = 200,
                    Summary = $"Successful Get {entityName} with id {id}"
                };
                get.RequestQuery.Add(nameof(id), id.ToString());
                get.ObjectDTO = result.Item2;
                mdmSender.Send(get, MessageHeaderFromWO.GetRequestInfo);
            }
            else {
                var error = new ErrorWOInfo() {
                    Header = "Get",
                    Summary = $"Error during getting {entityName} with id {id}",
                    ErrorResponse = result.Item1
                };
                error.Query.Add(nameof(id), id.ToString());
                mdmSender.Send(error, MessageHeaderFromWO.ErrorResponseInfo);
            }
            return result;
        }

        public override async Task<(ErrorResponse?, IReadOnlyList<TEntityDTO>? entityDTOs)> GetPaged(int page, int pageQuantity)
        {
            var result = await base.GetPaged(page, pageQuantity);
            string entityName = typeof(TEntityDTO).Name;

            if (result.Item1 is null) {
                var paged = new PagedGetWOInfo() {
                    StatusCode = 200,
                    Summary = $"Successful Get paged quantity of {entityName}"
                };
                paged.RequestQuery.Add(nameof(page), page.ToString());
                paged.RequestQuery.Add(nameof(pageQuantity), pageQuantity.ToString());

                paged.ResponseIDs.AddRange(from entityDTO in result.Item2
                                           select entityDTO.ID);

                mdmSender.Send(paged, MessageHeaderFromWO.PagedGetInfo);
            }
            else {
                var error = new ErrorWOInfo() {
                    Header = "GET",
                    Summary = $"Error during getting paged quantity of {entityName}",
                    ErrorResponse = result.Item1
                };
                error.Query.Add(nameof(page), page.ToString());
                error.Query.Add(nameof(pageQuantity), pageQuantity.ToString());

                mdmSender.Send(error, MessageHeaderFromWO.ErrorResponseInfo);
            }
            return result;
        }

        public override async Task<(ErrorResponse?, TEntityDTO?)> Post(TEntityDTO entityDTO)
        {
            var result = await base.Post(entityDTO);
            string entityName = typeof(TEntityDTO).Name;

            if (result.Item1 is null) {
                var paged = new PostWOInfo() {
                    StatusCode = 200,
                    Summary = $"Successful Post {entityName} with id {entityDTO.ID}"
                };
                paged.RequestQuery.Add(nameof(entityDTO.ID), entityDTO.ID.ToString());

                mdmSender.Send(paged, MessageHeaderFromWO.PostRequestInfo);
            }
            else {
                var error = new ErrorWOInfo() {
                    Header = "POST",
                    Summary = $"Error during post {entityName}",
                    ErrorResponse = result.Item1
                };
                error.Query.Add(nameof(entityDTO.ID), entityDTO.ID.ToString());

                mdmSender.Send(error, MessageHeaderFromWO.ErrorResponseInfo);
            }
            return result;
        }

        public override async Task<(ErrorResponse?, TEntityDTO?)> Put(int? id, TEntityDTO entityDTO)
        {
            var result = await base.Put(id, entityDTO);
            string entityName = typeof(TEntityDTO).Name;

            if (result.Item1 is null) {
                var put = new PutWOInfo() {
                    StatusCode = 200,
                    Summary = $"Successful update of {entityName} with id {id}"
                };
                put.RequestQuery.Add(nameof(id), (id ?? entityDTO.ID).ToString());

                mdmSender.Send(put, MessageHeaderFromWO.PutRequestInfo);
            }
            else {
                var error = new ErrorWOInfo() {
                    Header = "PUT",
                    Summary = $"Error during put {entityName}",
                    ErrorResponse = result.Item1
                };
                error.Query.Add(nameof(id), (id ?? entityDTO.ID).ToString());

                mdmSender.Send(error, MessageHeaderFromWO.ErrorResponseInfo);
            }
            return result;
        }
    }
}
