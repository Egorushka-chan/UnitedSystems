using Microsoft.EntityFrameworkCore;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;
using WardrobeOnline.BLL.Services.Interfaces;
using WardrobeOnline.DAL.Interfaces;

namespace WardrobeOnline.BLL.Services.Implementations
{

    public class CastHelper(IWardrobeContext _context, IImageProvider _imageProvider) : ICastHelper
    {
        /// <summary>
        /// Возвращает булево значение в зависимости от существования такого сезона
        /// </summary>
        /// <param name="seasonName">Строковое представление названия сезона</param>
        /// <param name="id">Возвращает 0, если Season не найден</param>
        /// <returns></returns>

        public IReadOnlyList<string> GetClothMaterialNames(Cloth cloth)
        {
            var materials = from clothMaterials in cloth.ClothHasMaterials
                                   select clothMaterials.Material;

            return (from material in materials
                    select material.Name).ToList();
        }

        public void AssertSetSeason(string season, Set setDB)
        {
            var searchedSeason = _context.Seasons.Where(ent => ent.Name == season)
                .FirstOrDefault();
            if(searchedSeason != null) {
                setDB.SeasonID = searchedSeason.ID;
                
                //TODO: непонятный баг добавляет новую сущность, а не указывает на текущую
                //setDB.Season = searchedSeason;
            }
            else
            {
                setDB.Season = new Season() {Name = season};
            }
        }
        public void AssertSetClothes(IReadOnlyList<int> clothIDs, Set setDB)
        {
            var dbClothes = from SetHasClothes setClothes in setDB.SetHasClothes
                              select setClothes.Cloth;

            var notMatchedDBCloths = from dbCloth in dbClothes
                                    where !clothIDs.Contains(dbCloth.ID)
                                    select dbCloth;

            IEnumerable<int> notMatchedDTOCloths;
            if (dbClothes.Count() == 0)
            {
                notMatchedDTOCloths = clothIDs;
            }
            else
            {
                 notMatchedDTOCloths = from clothID in clothIDs
                                          let existedIDs = from dbCloth in dbClothes
                                                           select dbCloth.ID
                                          where !existedIDs.Contains(clothID)
                                          select clothID;
            }

            foreach (Cloth nonMatched in notMatchedDBCloths)
            {
                setDB.SetHasClothes.Remove(setDB.SetHasClothes.Where(cm => cm.ClothID == nonMatched.ID).First());
            }

            foreach (int nonMatched in notMatchedDTOCloths)
            {
                SetHasClothes setHasClothes = new()
                {
                    ClothID = nonMatched,
                    SetID = setDB.ID
                };
                setDB.SetHasClothes.Add(setHasClothes);
            }
        }

        public void AssertPhysiqueSets(IReadOnlyList<int> setIDs, Physique? physiqueDB)
        {
            var dbSets = from set in physiqueDB.Sets
                         select set;

            var notMatchedDBSets = from dbSet in dbSets
                                        where !setIDs.Contains(dbSet.ID)
                                        select dbSet;

            var notMatchedDTOSets = from setID in setIDs
                                         let existedIDs = from dbSet in dbSets
                                                            select dbSet.ID
                                         where !existedIDs.Contains(setID)
                                         select setID;
            
            foreach (var nonMatched in notMatchedDBSets)
            {
                physiqueDB.Sets.Remove(nonMatched);
            }

            if (notMatchedDTOSets.Count() > 0)
            {
                throw new FormatException("Can't add physique sets through PhysiqueDTO");
            }
        }

        public void AssertPersonPhysiques(IReadOnlyList<int> physiqueIDs, Person personDB)
        {
            var dbPhysiques = from physique in personDB.Physiques
                              select physique;

            var notMatchedDBPhysiques = from dbPhysique in dbPhysiques
                                    where !physiqueIDs.Contains(dbPhysique.ID)
                                    select dbPhysique;

            var notMatchedDTOPhysiques = from physiqueID in physiqueIDs
                                         let existedIDs = from dbPhysique in dbPhysiques
                                                            select dbPhysique.ID
                                         where !existedIDs.Contains(physiqueID)
                                         select physiqueID;

            foreach (var nonMatched in notMatchedDBPhysiques)
            {
                personDB.Physiques.Remove(nonMatched);
            }

            if (notMatchedDTOPhysiques.Count() > 0)
            {
                throw new FormatException("Can't add person physiques through PersonDTO");
            }
        }

        public void AssertClothMaterials(IEnumerable<string> materials, Cloth cloth)
        {
            var dbMaterials = from ClothHasMaterials clothMaterials in cloth.ClothHasMaterials
                              select clothMaterials.Material;

            var notMatchedDBMaterials = from dbMaterial in dbMaterials
                                    where !materials.Contains(dbMaterial.Name)
                                    select dbMaterial;

            var notMatchedDTOMaterials = from materialDTO in materials
                                         let existedNames = from dbMaterial in dbMaterials
                                                            select dbMaterial.Name
                                         where !existedNames.Contains(materialDTO)
                                         select materialDTO;

            foreach (Material nonMatched in notMatchedDBMaterials)
            {
                cloth.ClothHasMaterials.Remove(cloth.ClothHasMaterials.Where(cm => cm.MaterialID == nonMatched.ID).First());
            }

            foreach (string nonMatched in notMatchedDTOMaterials)
            {
                Material newMaterial = new Material()
                {
                    Name = nonMatched
                };
                _context.Materials.Add(newMaterial);
                cloth.ClothHasMaterials.Add(new ClothHasMaterials()
                {
                    ClothID = cloth.ID,
                    Material = newMaterial
                });
            }
        }
        
        public IReadOnlyList<int> GetPhysiqueSetIDs(Physique physique)
        {
            return (from set in physique.Sets
                    select set.ID).ToList();
        }

        public IReadOnlyList<int> GetPersonPhysiqueIDs(Person person)
        {
            return  (from physique in  person.Physiques
                     select physique.ID).ToList();
        }

        public IReadOnlyList<string> GetPhotoPaths(ICollection<Photo> photos)
        {
            return (from Photo photo in photos
                    select _imageProvider.GetImageLink(photo.ID)).ToList();
        }

        public IReadOnlyList<int> GetSetClothesIDs(Set set)
        {
            return (from setClothes in set.SetHasClothes
                   select setClothes.ClothID).ToList();
        }
    }
}
