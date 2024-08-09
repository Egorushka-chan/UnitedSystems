using System.Net.Http;
using System.Net.NetworkInformation;

using MasterDominaSystem.BLL.Services.Abstractions;
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

        [HttpPut]
        public async Task<IResult> Ping([FromServices] IHttpClientFactory clientFactory)
        {
            var client = clientFactory.CreateClient("tester");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get,
                "http://WardrobeWebApi:8088/api/clothes/1");
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            if (httpResponseMessage.IsSuccessStatusCode) {
                using var body = await httpResponseMessage.Content.ReadAsStreamAsync();
                using var reader = new StreamReader(body); 
                return TypedResults.Ok(await reader.ReadToEndAsync());
            }
            else {
                using var body = await httpResponseMessage.Content.ReadAsStreamAsync();
                using var reader = new StreamReader(body);
                string result = await reader.ReadToEndAsync();
                return Results.Ok(httpResponseMessage.StatusCode + result);   
            }
        }
    }
}
