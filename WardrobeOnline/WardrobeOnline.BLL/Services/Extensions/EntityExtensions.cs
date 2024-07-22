using System.Data;
using System.Threading.Channels;
using WardrobeOnline.BLL.Models;
using WardrobeOnline.BLL.Models.Interfaces;
using WardrobeOnline.BLL.Services.Interfaces;
using WardrobeOnline.DAL.Entities;
using WardrobeOnline.DAL.Interfaces;

namespace WardrobeOnline.BLL.Services.Extensions
{
    internal static class EntityExtensions
    {
        /// <summary>
        /// Метод расширения, переводящий объекты <see cref="IEntity"></see> в объекты <see cref="IEntityDTO"/>
        /// </summary>
        /// <remarks>Делает это не оптимально, т.к для приведения могут запрашиваться дополнительные запросы к базе данных</remarks>
        /// <typeparam name="Tdb">Один из типов, принадлежащих <see cref="IEntity"></see></typeparam>
        /// <typeparam name="Tdto">Один из типов, принадлежащих <see cref="IEntityDTO"></see></typeparam>
        /// <param name="entityDB"></param>
        /// <param name="resultDTO">Выведет null, если нет имплементации для выбранных объектов</param>
        /// <param name="translator"></param>
        internal static void TranslateToDTO<Tdb, Tdto>(this Tdb entityDB, out Tdto? resultDTO, ICastHelper translator) 
            where Tdb : class, IEntity
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
                    SetDTO setDTO = new SetDTO()
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
                    PersonDTO personDTO = new PersonDTO()
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
                    PhysiqueDTO physiqueDTO = new PhysiqueDTO()
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
        /// Метод расширения, переводящий объекты <see cref="IEntityDTO"></see> в объекты <see cref="IEntity"/>
        /// </summary>
        /// <remarks>Делает это не оптимально, т.к для приведения могут запрашиваться дополнительные запросы к базе данных</remarks>
        /// <typeparam name="Tdb">Один из типов, принадлежащих <see cref="IEntity"></see></typeparam>
        /// <typeparam name="Tdto">Один из типов, принадлежащих <see cref="IEntityDTO"></see></typeparam>
        /// <param name="entityDB"></param>
        /// <param name="resultDTO">Выведет null, если нет имплементации для выбранных объектов</param>
        /// <param name="translator"></param>
        internal static void TranslateToDB<Tdto, Tdb>(this Tdto entityDTO, out Tdb? resultDB, ICastHelper castHelper)
            where Tdb : class, IEntity
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
                    Name = self.Name,
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
                    Name = self.Name,
                    Description = self.Description,
                    PhysiqueID = self.PhysiqueID.Value
                };
                return set as Tdb;
            }

            Tdb? GetPhysique(PhysiqueDTO self)
            {
                Physique physique = new Physique()
                {
                    ID = self.ID,
                    Description = self.Description,
                    Growth = self.Growth.Value,
                    Weight = self.Weight.Value,
                    Force = self.Force.Value,
                    PersonID = self.PersonID.Value
                };
                return physique as Tdb;
            }

            Tdb? GetPerson(PersonDTO self)
            {
                Person person = new()
                {
                    ID = self.ID,
                    Name = self.Name,
                    Type = self.Type,
                };
                return person as Tdb;
            }
        }

        //internal static ClothDTO TranslateToDTO(this Cloth cloth, ICastHelper translator)
        //{
        //    var photoPaths = translator.GetPhotoPaths(cloth.Photos);

        //    var materials = translator.GetClothMaterialNames(cloth);

        //    ClothDTO clothDTO = new(cloth.ID, cloth.Name)
        //    {
        //        Description = cloth.Description,
        //        Rating = cloth.Rating,
        //        Size = cloth.Size,
        //        PhotoPaths = photoPaths,
        //        Materials = materials
        //    };
        //    return clothDTO;
        //}

        //internal static PersonDTO TranslateToDTO(this Person person, ICastHelper translator) 
        //{
        //    PersonDTO personDTO = new PersonDTO(person.ID, person.Name)
        //    {
        //        Type = person.Type,
        //        PhysiqueIDs = translator.GetPersonPhysiqueIDs(person)
        //    };
        //    return personDTO;
        //}

        //internal static PhysiqueDTO TranslateToDTO(this Physique physique, ICastHelper translator)
        //{
        //    PhysiqueDTO physiqueDTO = new PhysiqueDTO(physique.ID, physique.Growth, physique.Weight, physique.PersonID)
        //    {
        //        Description = physique.Description,
        //        Force = physique.Force,
        //        SetIDs = translator.GetPhysiqueSetIDs(physique)
        //    };
        //    return physiqueDTO;
        //}

        //internal static SetDTO TranslateToDTO(this Set set, ICastHelper translator)
        //{
        //    SetDTO setDTO = new SetDTO(set.ID, set.Name, translator.GetSeasonName(set.SeasonID), set.PhysiqueID)
        //    {
        //        Description = set.Description,
        //        ClothIDs = translator.GetSetClothesIDs(set)
        //    };
        //    return setDTO;
        //}

        //internal static void TranslateToDTO(this IEntity entityDB, out IEntityDTO? resultDTO, ICastHelper translator)
        //{
        //    resultDTO = Mapping(entityDB);

        //    IEntityDTO? Mapping(IEntity entity)
        //    {
        //        if (entity is Cloth cloth)
        //        {
        //            var photoPaths = translator.GetPhotoPaths(cloth.Photos);
        //            var materials = translator.GetClothMaterialNames(cloth);

        //            return new ClothDTO(cloth.ID, cloth.Name)
        //            {
        //                Description = cloth.Description,
        //                Rating = cloth.Rating,
        //                Size = cloth.Size,
        //                PhotoPaths = photoPaths,
        //                Materials = materials
        //            };
        //        }
        //        if (entity is Set set)
        //        {
        //            SetDTO setDTO = new SetDTO(set.ID, set.Name, translator.GetSeasonName(set.SeasonID), set.PhysiqueID)
        //            {
        //                Description = set.Description,
        //                ClothIDs = translator.GetSetClothesIDs(set)
        //            };
        //            return setDTO;
        //        }
        //        if (entity is Person person)
        //        {
        //            PersonDTO personDTO = new PersonDTO(person.ID, person.Name)
        //            {
        //                Type = person.Type,
        //                PhysiqueIDs = translator.GetPersonPhysiqueIDs(person)
        //            };
        //            return personDTO;
        //        }
        //        if (entity is Physique physique)
        //        {
        //            PhysiqueDTO physiqueDTO = new PhysiqueDTO(physique.ID, physique.Growth, physique.Weight, physique.PersonID)
        //            {
        //                Description = physique.Description,
        //                Force = physique.Force,
        //                SetIDs = translator.GetPhysiqueSetIDs(physique)
        //            };
        //            return physiqueDTO;
        //        }

        //        return null;
        //    }
        //}
    }
}
