namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces
{
    public abstract class EntityProto(dynamic proto) : EntityS, IEntityProtoWrapper
    {
        private readonly dynamic _proto = proto;
        public dynamic Value => _proto;

        public static implicit operator EntityProto(EntityDB entityDB)
        {
            return entityDB.GeneralConvertToProto(entityDB);
        }
        internal abstract EntityDB GeneralConvertToDB(EntityDB entityDB);
    }

    // Из-за тупняка С# (нельзя абстрактные методы вызвать в следующем абстрактом классе) у нас два одинаковых метода конвертации

    public abstract class EntityProto<TEntity>(dynamic proto) : EntityProto(proto), IEntityProtoWrapper
        where TEntity : class, IEntityS
    {
        public static implicit operator EntityProto<TEntity>(EntityDB<TEntity> entityDB)
        {
            return entityDB.GenericConvertToProto(entityDB);
        }
        internal abstract EntityDB<TEntity> GenericConvertToDB(EntityDB<TEntity> entityDB);
    }

    // Выбор небольшой - или использовать второй Generic, который нормально не вписать, и который ломает весь смысл преобразования через классы
    // Или использовать dynamic, ибо sealed классы protobuf всё ломают

    //public abstract class EntityProto<TEntity, TProto> : EntityProto<TEntity>, IEntityProtoWrapper
    //    where TEntity : class, IEntityS
    //    where TProto : class, new()
    //{
    //    private TProto _proto;
    //    public EntityProto()
    //    {
    //        _proto = new TProto();
    //    }
    //    public EntityProto(TProto proto)
    //    {
    //        _proto = proto;
    //    }
    //    public TProto Value => _proto;
    //}
}
