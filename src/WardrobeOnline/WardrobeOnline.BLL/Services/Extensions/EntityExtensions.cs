using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DTO;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;
using WardrobeOnline.BLL.Services.Interfaces;

namespace WardrobeOnline.BLL.Services.Extensions
{
    internal static class EntityExtensions
    {
        /// <summary>
        /// Метод расширения, переводящий объекты <see cref="IEntityDB"></see> в объекты <see cref="IEntityDTO"/>
        /// </summary>
        /// <remarks>Делает это не оптимально, т.к для приведения могут запрашиваться дополнительные запросы к базе данных</remarks>
        /// <typeparam name="Tdb">Один из типов, принадлежащих <see cref="IEntityDB"></see></typeparam>
        /// <typeparam name="Tdto">Один из типов, принадлежащих <see cref="IEntityDTO"></see></typeparam>
        /// <param name="resultDTO">Выведет null, если нет имплементации для выбранных объектов</param>
        internal static void TranslateToDTO<Tdb, Tdto>(this Tdb entityDB, out Tdto? resultDTO, ICastHelper translator) 
            where Tdb : class, IEntityDB
            where Tdto : class, IEntityDTO
        {
            //return entityDB switch
            //{
            //    Cloth cloth => Mapping(cloth),
            //    Set set => Mapping(set),
            //    _ => null,
            //};

            //return Mapping(entityDB);
            resultDTO = Mapping(entityDB);

            Tdto? Mapping(Tdb entity)
            {
                if(entity is Cloth cloth)
                {
                    var photoPaths = translator.GetPhotoPaths(cloth.Photos);
                    var materials = translator.GetClothMaterialNames(cloth);

                    return new ClothDTO()
                    {
                        ID = cloth.ID,
                        Name = cloth.Name,
                        Description = cloth.Description,
                        Rating = cloth.Rating,
                        Size = cloth.Size,
                        PhotoPaths = photoPaths,
                        Materials = materials
                    } as Tdto;
                }
                if(entity is Set set)
                {
                    SetDTO setDTO = new()
                    {
                        ID = set.ID,
                        Name = set.Name,
                        Season = set.Season?.Name,
                        PhysiqueID = set.PhysiqueID,
                        Description = set.Description,
                        ClothIDs = translator.GetSetClothesIDs(set)
                    };
                    return setDTO as Tdto;
                }
                if(entity is Person person)
                {
                    PersonDTO personDTO = new()
                    {
                        ID = person.ID,
                        Name = person.Name,
                        Type = person.Type,
                        PhysiqueIDs = translator.GetPersonPhysiqueIDs(person)
                    };
                    return personDTO as Tdto;
                }
                if(entity is Physique physique)
                {
                    PhysiqueDTO physiqueDTO = new()
                    {
                        ID=physique.ID,
                        Growth = physique.Growth,
                        Weight = physique.Weight,
                        PersonID = physique.PersonID,
                        Description = physique.Description,
                        Force = physique.Force,
                        SetIDs = translator.GetPhysiqueSetIDs(physique)
                    };
                    return physiqueDTO as Tdto;
                }

                return null;
            }
        }

        /// <summary>
        /// Метод расширения, переводящий объекты <see cref="IEntityDTO"></see> в объекты <see cref="IEntityDB"/>
        /// </summary>
        /// <remarks>Делает это не оптимально, т.к для приведения могут запрашиваться дополнительные запросы к базе данных</remarks>
        /// <typeparam name="Tdb">Один из типов, принадлежащих <see cref="IEntityDB"></see></typeparam>
        /// <typeparam name="Tdto">Один из типов, принадлежащих <see cref="IEntityDTO"></see></typeparam>
        /// <param name="entityDB"></param>
        /// <param name="resultDTO">Выведет null, если нет имплементации для выбранных объектов</param>
        /// <param name="translator"></param>
        internal static void TranslateToDB<Tdto, Tdb>(this Tdto entityDTO, out Tdb? resultDB)
            where Tdb : class, IEntityDB
            where Tdto : class, IEntityDTO
        {
            resultDB = entityDTO switch
            {
                ClothDTO clothDTO => GetCloth(clothDTO),
                SetDTO setDTO => GetSet(setDTO),
                PhysiqueDTO physiqueDTO => GetPhysique(physiqueDTO),
                PersonDTO personDTO => GetPerson(personDTO),
                _ => null
            };

            Tdb? GetCloth(ClothDTO self)
            {
                Cloth cloth = new()
                {
                    ID = self.ID,
                    Name = self.Name ?? "NULL",
                    Description = self.Description,
                    Rating = self.Rating.GetValueOrDefault(),
                    Size = self.Size,
                };
                return cloth as Tdb;
            }

            Tdb? GetSet(SetDTO self)
            {
                Set set = new()
                {
                    ID = self.ID,
                    Name = self.Name ?? "NULL",
                    Description = self.Description,
                    PhysiqueID = self.PhysiqueID ?? throw new InvalidCastException("SetDTO без PhysiqueID пытается преобразоваться в SetDB, что невозможно")
                };
                return set as Tdb;
            }

            Tdb? GetPhysique(PhysiqueDTO self)
            {
                Physique physique = new()
                {
                    ID = self.ID,
                    Description = self.Description,
                    Growth = self.Growth ?? 0,
                    Weight = self.Weight ?? 0,
                    Force = self.Force ?? 0,
                    PersonID = self.PersonID ?? throw new InvalidCastException("Physique без PersonID пытается преобразоваться в PhysiqueDB, что невозможно")
                };
                return physique as Tdb;
            }

            Tdb? GetPerson(PersonDTO self)
            {
                Person person = new()
                {
                    ID = self.ID,
                    Name = self.Name ?? "NULL",
                    Type = self.Type,
                };
                return person as Tdb;
            }
        }
    }
}
