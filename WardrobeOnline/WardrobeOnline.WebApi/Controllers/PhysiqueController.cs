using Microsoft.AspNetCore.Mvc;

using UnitedSystems.CommonLibrary.Messages;
using UnitedSystems.CommonLibrary.WardrobeOnline.DTO;

using WardrobeOnline.BLL.Services.Interfaces;

namespace WardrobeOnline.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhysiqueController : ControllerBase
    {
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PhysiqueDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [HttpGet("{id}")]
        public async Task<IResult> GetPersonPhysique(int id, [FromServices] IWrapperCRUDLayer<PhysiqueDTO> validationLayer)
        {
            (ErrorResponse? errorResponse, PhysiqueDTO? dto) = await validationLayer.Get(id);

            if (errorResponse != null)
                return TypedResults.BadRequest(errorResponse);
            else
                return TypedResults.Ok(dto);
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PhysiqueDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [HttpPost]
        public async Task<IResult> CreatePhysique([FromBody] PhysiqueDTO physiqueDTO, [FromServices] IWrapperCRUDLayer<PhysiqueDTO> validationLayer)
        {
            (ErrorResponse? errorResponse, PhysiqueDTO? dto) = await validationLayer.Post(physiqueDTO);

            if (errorResponse != null)
                return TypedResults.BadRequest(errorResponse);
            else
                return TypedResults.Ok(dto);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [HttpDelete("{id}")]
        public async Task<IResult> DeletePhysique(int id, [FromServices] IWrapperCRUDLayer<PhysiqueDTO> validationLayer)
        {
            ErrorResponse? errorResponse = await validationLayer.Delete(id);
            if (errorResponse != null)
                return TypedResults.BadRequest(errorResponse);

            return TypedResults.NoContent();
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PhysiqueDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [HttpPut("{id}")]
        public async Task<IResult> UpdatePhysique(int? id, [FromBody] PhysiqueDTO physiqueDTO, [FromServices] IWrapperCRUDLayer<PhysiqueDTO> validationLayer)
        {
            (ErrorResponse? errorResponse, PhysiqueDTO? dto) = await validationLayer.Put(id, physiqueDTO);

            if (errorResponse != null)
                return TypedResults.BadRequest(errorResponse);
            else
                return TypedResults.Ok(dto);
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PhysiqueDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [HttpGet("page/{pageIndex}/{pageSize}")]
        public async Task<IResult> GetPage(int pageIndex, int pageSize, [FromServices] IWrapperCRUDLayer<PhysiqueDTO> validationLayer)
        {
            (ErrorResponse? errorResponse, IReadOnlyList<PhysiqueDTO>? list) = await validationLayer.GetPaged(pageIndex, pageSize);

            if (errorResponse != null)
                return TypedResults.BadRequest(errorResponse);
            else
                return TypedResults.Ok(list);
        }
    }
}
