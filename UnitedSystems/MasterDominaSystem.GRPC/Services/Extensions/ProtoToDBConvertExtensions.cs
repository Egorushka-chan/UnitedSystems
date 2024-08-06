using UnitedSystems.CommonLibrary.WardrobeOnline.Entities;

using WODownloaderClient;

namespace MasterDominaSystem.GRPC.Services.Extensions
{
    public static class ProtoToDBConvertExtensions
    {
        public static Person ConvertToDB(this PersonProto personProto)
        {
            return new()
            {
                ID = personProto.ID,
                Name = personProto.Name,
                Type = personProto.Type
            };
        }

        public static Physique ConvertToDB(this PhysiqueProto physiqueProto)
        {
            return new()
            {
                ID = physiqueProto.ID,
                Growth = physiqueProto.Growth,
                Weight = physiqueProto.Weight,
                Force = physiqueProto.Force,
                Description = physiqueProto.Description,
                PersonID = physiqueProto.PersonID
            };
        }

        public static Set ConvertToDB(this SetProto setProto)
        {
            return new()
            {
                ID = setProto.ID,
                Name = setProto.Name,
                Description = setProto.Description,
                PhysiqueID = setProto.PhysiqueID,
                SeasonID = setProto.SeasonID
            };
        }

        public static Season ConvertToDB(this SeasonProto seasonProto)
        {
            return new()
            {
                ID = seasonProto.ID,
                Name = seasonProto.Name
            };
        }

        public static SetHasClothes ConvertToDB(this SetHasClothesProto proto)
        {
            return new()
            {
                ID = proto.ID,
                ClothID = proto.ClothID,
                SetID = proto.SetID
            };
        }

        public static Cloth ConvertToDB(this ClothProto proto)
        {
            return new()
            {
                ID = proto.ID,
                Name = proto.Name,
                Description = proto.Description,
                Rating = proto.Rating,
                Size = proto.Size
            };
        }

        public static Photo ConvertToDB(this PhotoProto photoProto)
        {
            return new()
            {
                ID = photoProto.ID,
                Name = photoProto.Name,
                ClothID = photoProto.ClothID,
                HashCode = photoProto.HashCode,
                IsDBStored = photoProto.IsDBStored
            };
        }

        public static ClothHasMaterials ConvertToDB(this ClothHasMaterialsProto proto)
        {
            return new()
            {
                ID = proto.ID,
                MaterialID = proto.MaterialID,
                ClothID = proto.ClothID
            };
        }

        public static Material ConvertToDB(this MaterialProto materialProto)
        {
            return new()
            {
                ID = materialProto.ID,
                Name = materialProto.Name,
                Description = materialProto.Description
            };
        }
    }
}
