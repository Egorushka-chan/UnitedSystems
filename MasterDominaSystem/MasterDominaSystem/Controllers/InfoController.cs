using System.Net.Http;
using System.Net.NetworkInformation;

using MasterDominaSystem.BLL.Services.Abstractions;
using MasterDominaSystem.DAL;
using MasterDominaSystem.GRPC.Services.Interfaces;

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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ISessionInfoProvider))]
        public IResult GeneralInfo([FromServices] ISessionInfoProvider generalInfoProvider)
        {
            return TypedResults.Ok(generalInfoProvider);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IResult> AskForData([FromServices] IDatabaseDownloader downloader, CancellationToken token = default)
        {
            await downloader.DownloadDataAsync(token);
            return TypedResults.Ok();
        }

        [HttpPut("{id?}")]
        public async Task<IResult> CreateBaza([FromServices] MasterContext context, int? id)
        {
            return TypedResults.Ok(context.ReportCloths.ToList());
        }
    }
}
