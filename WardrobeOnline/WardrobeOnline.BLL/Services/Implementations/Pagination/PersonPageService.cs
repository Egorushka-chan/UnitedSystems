﻿using Microsoft.EntityFrameworkCore;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;
using WardrobeOnline.BLL.Services.Interfaces;
using WardrobeOnline.DAL.Interfaces;

namespace WardrobeOnline.BLL.Services.Implementations.Pagination
{
    public class PersonPageService(IWardrobeContext context, IGeneralInfoProvider generalInfoProvider) : GeneralPageService<Person>(context)
    {
        private readonly IGeneralInfoProvider _generalInfoProvider = generalInfoProvider;

        protected override Task<List<Person>> GetEntities(int pageIndex, int pageSize)
        {
            return _context.Persons
                .Skip((pageIndex-1) * pageSize)
                .Take(pageSize)
                .Include(p => p.Physiques)
                .OrderByDescending(p => p.ID)
                .ToListAsync();
        }

        protected override Task<int> GetTotalSize()
        {
            return _generalInfoProvider.GetTotalPersonCount();
        }
    }
}
