using System.Text.Json;

using MasterDominaSystem.BLL.Services.Abstractions;

using Microsoft.Extensions.DependencyInjection;

using UnitedSystems.CommonLibrary.Models.MasterDominaSystem.Messages;
using UnitedSystems.CommonLibrary.Models.WardrobeOnline.Messages;
using UnitedSystems.CommonLibrary.Models.WardrobeOnline.Messages.Headers;

namespace MasterDominaSystem.RMQL.Models.Messages
{
    public class ConsumerableMessageFromWO : MessageFromWO, IConsumerableMessage
    {
        public void Handle(IServiceProvider services)
        {
            IGeneralInfoProvider generalInfoProvider = services.GetRequiredService<IGeneralInfoProvider>();
            switch (Type) {
                case MessageHeaderFromWO.NotSpecified:
                    generalInfoProvider.MessagePool.Add("WO (not specified): " + Body);
                    break;
                case MessageHeaderFromWO.AppStarting:
                    generalInfoProvider.MessagePool.Add("WO (app start): " + Body);
                    break;
                case MessageHeaderFromWO.AppClose:
                    generalInfoProvider.MessagePool.Add("WO (app close): " + Body);
                    break;
                case MessageHeaderFromWO.PostRequestInfo:
                    PostWOInfo postWOInfo = JsonSerializer.Deserialize<PostWOInfo>(Body) ?? throw new JsonException($"Failed to deserialize {nameof(PostWOInfo)}");
                    generalInfoProvider.PostRequestsWO.Add(postWOInfo);
                    break;
                case MessageHeaderFromWO.PutRequestInfo:
                    PutWOInfo putWOInfo = JsonSerializer.Deserialize<PutWOInfo>(Body) ?? throw new JsonException($"Failed to deserialize {nameof(PutWOInfo)}");
                    generalInfoProvider.PutRequestsWO.Add(putWOInfo);
                    break;
                case MessageHeaderFromWO.GetRequestInfo:
                    GetWOInfo getWOInfo = JsonSerializer.Deserialize<GetWOInfo>(Body) ?? throw new JsonException($"Failed to deserialize {nameof(GetWOInfo)}");
                    generalInfoProvider.GetRequestsWO.Add(getWOInfo);
                    break;
                case MessageHeaderFromWO.DeleteRequestInfo:
                    DeleteWOInfo deleteWOInfo = JsonSerializer.Deserialize<DeleteWOInfo>(Body) ?? throw new JsonException($"Failed to deserialize {nameof(DeleteWOInfo)}");
                    generalInfoProvider.DeleteRequestWO.Add(deleteWOInfo);
                    break;
                case MessageHeaderFromWO.ErrorResponseInfo:
                    ErrorWOInfo errorWOInfo = JsonSerializer.Deserialize<ErrorWOInfo>(Body) ?? throw new JsonException($"Failed to deserialize {nameof(ErrorWOInfo)}");
                    generalInfoProvider.ErrorRequestWO.Add(errorWOInfo);
                    break;
                case MessageHeaderFromWO.PagedGetInfo:
                    PagedGetWOInfo pagedWOInfo = JsonSerializer.Deserialize<PagedGetWOInfo>(Body) ?? throw new JsonException($"Failed to deserialize {nameof(PagedGetWOInfo)}");
                    generalInfoProvider.PagedRequestWO.Add(pagedWOInfo);
                    break;
            }
        }
    }
}
