namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces
{
    public abstract class EntityDTO : EntityS, IEntityDTO
    {
        public static implicit operator EntityDTO(EntityDB entityDB)
        {
            return entityDB.GeneralConvertToDTO();
        }

        public static implicit operator EntityDTO(EntityProto entityProto)
        {
            return entityProto.GeneralConvertToDTO();
        }

        internal abstract EntityDB GeneralConvertToDB();
        internal abstract EntityProto GeneralConvertToProto();
    }

    // Из-за тупняка С# (нельзя абстрактные методы вызвать в следующем абстрактом классе) у нас два одинаковых метода конвертации
    public abstract class EntityDTO<TEntity> : EntityDTO, IEntityDTO
        where TEntity : class, IEntityS
    {
        public static implicit operator EntityDTO<TEntity>(EntityDB<TEntity> entityDB)
        {
            return entityDB.GenericConvertToDTO();
        }

        public static implicit operator EntityDTO<TEntity>(EntityProto<TEntity> entityProto)
        {
            return entityProto.GenericConvertToDTO();
        }

        internal abstract EntityDB<TEntity> GenericConvertToDB();
        internal abstract EntityProto<TEntity> GenericConvertToProto();
    }
}
