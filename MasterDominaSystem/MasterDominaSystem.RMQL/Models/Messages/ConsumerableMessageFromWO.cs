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
                    PostWOInfo? postWOInfo = JsonSerializer.Deserialize<PostWOInfo>(Body);
                    if (postWOInfo is null)
                    {
                        generalInfoProvider.MessagePool.Add("WO: POST info consumed, but body can't be handled");
                    }
                    else
                    {
                        generalInfoProvider.MessagePool.Add("WO: POST info consumed");
                        generalInfoProvider.PostRequestsWO.Add(postWOInfo);
                    }
                    break;
                case MessageHeaderFromWO.PutRequestInfo:
                    PutWOInfo? putWOInfo = JsonSerializer.Deserialize<PutWOInfo>(Body);
                    if (putWOInfo is null)
                    {
                        generalInfoProvider.MessagePool.Add("WO: PUT info consumed, but body can't be handled");
                    }
                    else
                    {
                        generalInfoProvider.MessagePool.Add("WO: PUT info consumed");
                        generalInfoProvider.PutRequestsWO.Add(putWOInfo);
                    }
                    break;
                case MessageHeaderFromWO.GetRequestInfo:
                    GetWOInfo? getWOInfo = JsonSerializer.Deserialize<GetWOInfo>(Body);
                    if (getWOInfo is null)
                    {
                        generalInfoProvider.MessagePool.Add("WO: GET info consumed, but body can't be handled");
                    }
                    else
                    {
                        generalInfoProvider.MessagePool.Add("WO: GET info consumed");
                        generalInfoProvider.GetRequestsWO.Add(getWOInfo);
                    }
                    break;
                case MessageHeaderFromWO.DeleteRequestInfo:
                    DeleteWOInfo? deleteWOInfo = JsonSerializer.Deserialize<DeleteWOInfo>(Body);
                    if (deleteWOInfo is null)
                    {
                        generalInfoProvider.MessagePool.Add("WO: DELETE info consumed, but body can't be handled");
                    }
                    else
                    {
                        generalInfoProvider.MessagePool.Add("WO: DELETE info consumed");
                        generalInfoProvider.DeleteRequestWO.Add(deleteWOInfo);
                    }
                    break;
            }
        }
    }
}
