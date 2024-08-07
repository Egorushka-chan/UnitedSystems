﻿using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

namespace WardrobeOnline.BLL.Services.Interfaces
{
    public interface IPaginationService<TEntity>
        where TEntity : class, IEntityDB
    {
        public Task<List<TEntity>> GetPagedQuantityOf(int pageIndex, int pageSize);
    }
}
