using MasterDominaSystem.BLL.Services.Abstractions;

using Microsoft.Extensions.DependencyInjection;

using UnitedSystems.CommonLibrary.Models.MasterDominaSystem.Messages;
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
                    break;
                case MessageHeaderFromWO.AppStarting:
                    break;
                case MessageHeaderFromWO.AppClose:
                    break;
                case MessageHeaderFromWO.PostRequestInfo:
                    break;
                case MessageHeaderFromWO.PutRequestInfo:
                    break;
                case MessageHeaderFromWO.GetRequestInfo:
                    break;
                case MessageHeaderFromWO.DeleteRequestInfo:
                    break;
            }
        }
    }
}
