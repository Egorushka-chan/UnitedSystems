using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;
using WOSenderDB;

namespace WardrobeOnline.GRPC.Services.Extensions
{
    /// <summary>
    /// Все преобразования происходят в самих классах объектов, благодаря невероятным извращениям с наследованием
    /// </summary>
    internal static class DBToProtoExtensions
    {
        //internal static PersonProto ConvertToProto(this Person person)
        //{
        //    return new() {
        //        ID = person.ID,
        //        Name = person.Name,
        //        Type = person.Type
        //    };
        //}
        //internal static PhysiqueProto ConvertToProto(this Physique physique)
        //{
        //    return new() {
        //        ID = physique.ID,
        //        Growth = physique.Growth,
        //        Weight = physique.Weight,
        //        Force = physique.Force,
        //        Description = physique.Description,
        //        PersonID = physique.PersonID
        //    };
        //}
        //internal static SetProto ConvertToProto(this Set set)
        //{
        //    return new() {
        //        ID = set.ID,
        //        Name = set.Name,
        //        Description = set.Description,
        //        PhysiqueID = set.PhysiqueID,
        //        SeasonID = set.SeasonID
        //    };
        //}
        //internal static SeasonProto ConvertToProto(this Season season)
        //{
        //    return new() {
        //        ID = season.ID,
        //        Name = season.Name
        //    };
        //}
        //internal static ClothProto ConvertToProto(this Cloth cloth)
        //{
        //    return new() {
        //        ID = cloth.ID,
        //        Name = cloth.Name,
        //        Description = cloth.Description,
        //        Rating = cloth.Rating.GetValueOrDefault(),
        //        Size = cloth.Size
        //    };
        //}
        //internal static SetHasClothesProto ConvertToProto(this SetHasClothes setHasClothes)
        //{
        //    return new() {
        //        ID = setHasClothes.ID,
        //        ClothID = setHasClothes.ClothID,
        //        SetID = setHasClothes.SetID
        //    };
        //}
        //internal static MaterialProto ConvertToProto(this Material material)
        //{
        //    return new() {
        //        ID = material.ID,
        //        Name = material.Name,
        //        Description = material.Description
        //    };
        //}
        //internal static ClothHasMaterialsProto ConvertToProto(this ClothHasMaterials clothHasMaterials)
        //{
        //    return new() {
        //        ID = clothHasMaterials.ID,
        //        ClothID = clothHasMaterials.ClothID,
        //        MaterialID = clothHasMaterials.MaterialID
        //    };
        //}
        //internal static PhotoProto ConvertToProto(this Photo photo)
        //{
        //    return new() {
        //        ID = photo.ID,
        //        Name = photo.Name,
        //        HashCode = photo.HashCode,
        //        IsDBStored = photo.IsDBStored,
        //        ClothID = photo.ClothID
        //    };
        //}
    }
}
