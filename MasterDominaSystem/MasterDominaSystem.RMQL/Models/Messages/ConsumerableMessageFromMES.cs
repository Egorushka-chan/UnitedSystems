using MasterDominaSystem.BLL.Services.Abstractions;

using Microsoft.Extensions.DependencyInjection;

using UnitedSystems.CommonLibrary.Models.MasterDominaSystem.Messages;
using UnitedSystems.CommonLibrary.Models.ManyEntitiesSender.Messages.Headers;

namespace MasterDominaSystem.RMQL.Models.Messages
{
    public class ConsumerableMessageFromMES : MessageFromMES, IConsumerableMessage
    {
        public void Handle(IServiceProvider services)
        {
            IGeneralInfoProvider generalInfoProvider = services.GetRequiredService<IGeneralInfoProvider>();
            switch (Type)
            {
                case MessageHeaderFromMES.NotSpecified:
                    generalInfoProvider.MessagePool.Add("MES (not specified): " + Body);
                    break;
                case MessageHeaderFromMES.EnsuredRequestStart:
                    generalInfoProvider.EnsuredRequestMES.Add(Body);
                    break;
                case MessageHeaderFromMES.EnsuredRequestEnd:
                    generalInfoProvider.EnsuredRequestMES.Add(Body);
                    break;
                case MessageHeaderFromMES.GetRequestInfo:
                    generalInfoProvider.GetRequestsMES.Add(Body);
                    break;
                case MessageHeaderFromMES.AppStarting:
                    generalInfoProvider.MessagePool.Add("MES (app start): " + Body);
                    break;
                case MessageHeaderFromMES.AppEnd:
                    generalInfoProvider.MessagePool.Add("MES (app end): + Body");
                    break;
            }
        }
    }
}