using System.Runtime.CompilerServices;

using Microsoft.AspNetCore.Mvc;

namespace UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces
{
    public abstract class EntityDB : EntityS, IEntityDB
    {
        public static implicit operator EntityDB(EntityDTO entityDB)
        {
            return entityDB.GeneralConvertToDB();
        }

        public static implicit operator EntityDB(EntityProto entityProto)
        {
            return entityProto.GeneralConvertToDB();
        }

        internal abstract EntityDTO GeneralConvertToDTO();
        internal abstract EntityProto GeneralConvertToProto();
    }

    // Из-за тупняка С# (нельзя абстрактные методы вызвать в следующем абстрактом классе) у нас два одинаковых метода конвертации

    public abstract class EntityDB<TEntity> : EntityDB, IEntityDB
        where TEntity : class, IEntityS
    {
        public static implicit operator EntityDB<TEntity>(EntityDTO<TEntity> entityDB)
        {
            return entityDB.GenericConvertToDB();
        }

        public static implicit operator EntityDB<TEntity>(EntityProto<TEntity> entityProto)
        {
            return entityProto.GenericConvertToDB();
        }

        internal abstract EntityDTO<TEntity> GenericConvertToDTO();
        internal abstract EntityProto<TEntity> GenericConvertToProto();
    }
}
