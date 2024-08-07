using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;

namespace WardrobeOnline.BLL.Services.Interfaces
{
    public interface ICastHelper
    {
        IReadOnlyList<string> GetPhotoPaths(ICollection<Photo> photos);
        IReadOnlyList<string> GetClothMaterialNames(Cloth cloth);
        void AssertClothMaterials(IEnumerable<string> materials, Cloth cloth);
        IReadOnlyList<int> GetPersonPhysiqueIDs(Person person);
        IReadOnlyList<int> GetPhysiqueSetIDs(Physique physique);
        IReadOnlyList<int> GetSetClothesIDs(Set set);
        void AssertPersonPhysiques(IReadOnlyList<int> physiqueIDs, Person personDB);
        void AssertPhysiqueSets(IReadOnlyList<int> setIDs, Physique? physiqueDB);
        void AssertSetSeason(string season, Set setDB);
        void AssertSetClothes(IReadOnlyList<int> clothIDs, Set setDB);
    }
}
