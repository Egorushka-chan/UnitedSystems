using Microsoft.EntityFrameworkCore;

using UnitedSystems.CommonLibrary.WardrobeOnline.Entities;

using WardrobeOnline.BLL.Services.Interfaces;
using WardrobeOnline.DAL.Interfaces;

namespace WardrobeOnline.BLL.Services.Implementations.Pagination
{
    public class GeneralPersonPageService : GeneralPageService<Person>
    {
        private IGeneralInfoProvider _generalInfoProvider;
        public GeneralPersonPageService(IWardrobeContext context, IGeneralInfoProvider generalInfoProvider) : base(context)
        {
            _generalInfoProvider = generalInfoProvider;
        }

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
