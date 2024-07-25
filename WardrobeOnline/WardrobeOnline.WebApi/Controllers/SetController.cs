using Microsoft.AspNetCore.Mvc;

using UnitedSystems.CommonLibrary.Models.WardrobeOnline;

using WardrobeOnline.BLL.Services.Interfaces;

namespace WardrobeOnline.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SetController : ControllerBase
    {
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SetDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [HttpGet("{id}")]
        public async Task<IResult> Get(int id, [FromServices] IValidationLayer<SetDTO> validationLayer)
        {
            (ErrorResponse? errorResponse, SetDTO? dto) = await validationLayer.Get(id);

            if (errorResponse != null)
                return TypedResults.BadRequest(errorResponse);
            else
                return TypedResults.Ok(dto);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [HttpDelete("{id}")]
        public async Task<IResult> Delete(int id, [FromServices] IValidationLayer<SetDTO> validationLayer)
        {
            ErrorResponse? errorResponse = await validationLayer.Delete(id);
            if (errorResponse != null)
                return TypedResults.BadRequest(errorResponse);

            return TypedResults.NoContent();
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SetDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [HttpPost]
        public async Task<IResult> Create([FromBody] SetDTO setDTO, [FromServices] IValidationLayer<SetDTO> validationLayer)
        {
            (ErrorResponse? errorResponse, SetDTO? dto) = await validationLayer.Post(setDTO);

            if (errorResponse != null)
                return TypedResults.BadRequest(errorResponse);
            else
                return TypedResults.Ok(dto);
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SetDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [HttpPut("{id?}")]
        public async Task<IResult> Update(int? id, [FromBody] SetDTO setDTO, [FromServices] IValidationLayer<SetDTO> validationLayer)
        {
            (ErrorResponse? errorResponse, SetDTO? dto) = await validationLayer.Put(id, setDTO);

            if (errorResponse != null)
                return TypedResults.BadRequest(errorResponse);
            else
                return TypedResults.Ok(dto);
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SetDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [HttpGet("page/{pageIndex}/{pageSize}")]
        public async Task<IResult> GetPage(int pageIndex, int pageSize, [FromServices] IValidationLayer<SetDTO> validationLayer)
        {
            (ErrorResponse? errorResponse, IReadOnlyList<SetDTO>? list) = await validationLayer.GetPaged(pageIndex, pageSize);

            if (errorResponse != null)
                return TypedResults.BadRequest(errorResponse);
            else
                return TypedResults.Ok(list);
        }
    }
}
