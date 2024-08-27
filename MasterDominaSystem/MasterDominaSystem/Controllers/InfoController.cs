using MasterDominaSystem.BLL.Services.Abstractions;
using MasterDominaSystem.DAL;
using MasterDominaSystem.GRPC.Services.Interfaces;

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
        [HttpGet("GeneralInfo")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ISessionInfoProvider))]
        public IResult GeneralInfo([FromServices] ISessionInfoProvider generalInfoProvider)
        {
            return TypedResults.Ok(generalInfoProvider);
        }

        [HttpGet("AskForData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IResult> AskForData([FromServices] IDatabaseDownloader downloader, CancellationToken token = default)
        {
            await downloader.DownloadDataAsync(token);
            return TypedResults.Ok();
        }
    }
}
