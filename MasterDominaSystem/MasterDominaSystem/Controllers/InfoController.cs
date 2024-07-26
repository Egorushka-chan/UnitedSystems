using MasterDominaSystem.BLL.Services.Abstractions;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MasterDominaSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        /// <summary>
        /// Просто вывод по быстрому, для проверки
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IGeneralInfoProvider))]
        public IResult GeneralInfo([FromServices] IGeneralInfoProvider generalInfoProvider)
        {
            return TypedResults.Ok(generalInfoProvider);
        }
    }
}
