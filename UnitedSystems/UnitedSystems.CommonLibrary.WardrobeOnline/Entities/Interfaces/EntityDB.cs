using System.Runtime.CompilerServices;

using Microsoft.AspNetCore.Mvc;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces
{
    public abstract class EntityDB : EntityS, IEntityDB
    {
        public abstract int ID { get; set; }
        public static implicit operator EntityDB(EntityDTO entityDB)
        {
            return entityDB.GeneralConvertToDB(entityDB);
        }

        public static implicit operator EntityDB(EntityProto entityProto)
        {
            return entityProto.GeneralConvertToDB(entityProto);
        }

        internal abstract EntityDTO GeneralConvertToDTO(EntityDTO entityDTO);
        internal abstract EntityProto GeneralConvertToProto(EntityProto entityProto);
    }

    // Из-за тупняка С# (нельзя абстрактные методы вызвать в следующем абстрактом классе) у нас два одинаковых метода конвертации

    public abstract class EntityDB<TEntity> : EntityDB, IEntityDB
        where TEntity : class, IEntityS
    {
        public static implicit operator EntityDB<TEntity>(EntityDTO<TEntity> entityDB)
        {
            return entityDB.GenericConvertToDB(entityDB);
        }

        public static implicit operator EntityDB<TEntity>(EntityProto<TEntity> entityProto)
        {
            return entityProto.GenericConvertToDB(entityProto);
        }

        internal abstract EntityDTO<TEntity> GenericConvertToDTO(EntityDTO<TEntity> entityDTO);
        internal abstract EntityProto<TEntity> GenericConvertToProto(EntityProto<TEntity> entityProto);
    }
}
