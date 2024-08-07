namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces
{
    public abstract class EntityProto : EntityS, IEntityProto
    {
        public static implicit operator EntityProto(EntityDB entityDB)
        {
            return entityDB.GeneralConvertToProto();
        }

        public static implicit operator EntityProto(EntityDTO entityDTO)
        {
            return entityDTO.GeneralConvertToProto();
        }

        internal abstract EntityDTO GeneralConvertToDTO();
        internal abstract EntityDB GeneralConvertToDB();
    }

    // Из-за тупняка С# (нельзя абстрактные методы вызвать в следующем абстрактом классе) у нас два одинаковых метода конвертации

    public abstract class EntityProto<TEntity> : EntityProto, IEntityProto
        where TEntity : class, IEntityS
    {
        public static implicit operator EntityProto<TEntity>(EntityDB<TEntity> entityDB)
        {
            return entityDB.GenericConvertToProto();
        }

        public static implicit operator EntityProto<TEntity>(EntityDTO<TEntity> entityDTO)
        {
            return entityDTO.GenericConvertToProto();
        }
        internal abstract EntityDTO<TEntity> GenericConvertToDTO();
        internal abstract EntityDB<TEntity> GenericConvertToDB();
    }

    public abstract class EntityProto<TEntity, TProto> : EntityProto<TEntity>, IEntityProto
        where TEntity : class, IEntityS
        where TProto : class, new()
    {
        private TProto _proto;
        public EntityProto()
        {
            _proto = new TProto();
        }
        public EntityProto(TProto proto)
        {
            _proto = proto;
        }
        public TProto Value => _proto;
    }
}
