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
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IResult> AskForData([FromServices] IDatabaseDownloader downloader, CancellationToken token = default)
        {
            try {
                await downloader.DownloadDataAsync(token);
            }
            catch(Grpc.Core.RpcException ex) {
                if(ex.InnerException?.Message.Contains("Connection refused") ?? true) {
                    return Results.Forbid();
                }
            }
            return TypedResults.Ok();
        }
    }
}
