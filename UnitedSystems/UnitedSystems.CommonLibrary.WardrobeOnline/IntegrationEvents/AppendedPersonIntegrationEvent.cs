using UnitedSystems.CommonLibrary.Messages;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.IntegrationEvents
{
    public class AppendedPersonIntegrationEvent(Person person) : IntegrationEvent 
    {
        public Person Person { get; private set; } = person;
        public ICollection<Physique> Physiques { get; set; } = [];
    }
}
