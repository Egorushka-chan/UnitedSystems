using Microsoft.EntityFrameworkCore;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DTO;
using UnitedSystems.EventBus.Interfaces;

using WardrobeOnline.BLL.Services.Extensions;
using WardrobeOnline.BLL.Services.Interfaces;
using WardrobeOnline.DAL.Interfaces;

namespace WardrobeOnline.BLL.Services.Implementations.CRUD
{
    public class SetProvider(IWardrobeContext context, IPaginationService<Set> pagination, ICastHelper castHelper, IImageProvider imageProvider, IEventBus eventBus) : CRUDProvider<SetDTO, Set>(context, pagination, castHelper, imageProvider, eventBus)
    {
        protected override Task<Set?> AddTranslateToDB(SetDTO entityDTO)
        {
            entityDTO.TranslateToDB(out Set? setDB);
            _castHelper.AssertSetSeason(entityDTO.Season, setDB);

            if (entityDTO.ClothIDs != null)
            {
                _castHelper.AssertSetClothes(entityDTO.ClothIDs, setDB);
            }

            return Task.FromResult<Set?>(setDB);
        }

        protected override Task<SetDTO?> AddTranslateToDTO(Set entityDB)
        {
            entityDB.TranslateToDTO(out SetDTO? resultDTO, _castHelper);
            return Task.FromResult(resultDTO);
        }

        protected override Task<Set?> GetFromDBbyID(int id)
        {
            return _context.Sets.Where(ent => ent.ID == id)
                .Include(ent => ent.Season)
                .Include(ent => ent.SetHasClothes)
                .ThenInclude(ent => ent.Cloth)
                .FirstOrDefaultAsync();
        }

        protected override Task<SetDTO?> GetTranslateToDTO(Set entityDB)
        {
            entityDB.TranslateToDTO(out SetDTO? resultDTO, _castHelper);
            return Task.FromResult(resultDTO);
        }

        protected override async Task<Set?> UpdateTranslateToDB(SetDTO entityDTO)
        {
            Set? setDB = await GetFromDBbyID(entityDTO.ID);
            if (setDB == null)
                return null;

            if (entityDTO.Name is not null)
                setDB.Name = entityDTO.Name;

            if (entityDTO.Description is not null)
                setDB.Description = entityDTO.Description;

            if (entityDTO.Season is not null)
                _castHelper.AssertSetSeason(entityDTO.Season, setDB);

            if (entityDTO.PhysiqueID is not null)
                setDB.PhysiqueID = entityDTO.PhysiqueID.Value;

            if (entityDTO.ClothIDs is not null)
                _castHelper.AssertSetClothes(entityDTO.ClothIDs, setDB);

            return setDB;
        }

        protected override Task<SetDTO?> UpdateTranslateToDTO(Set entityDB)
        {
            entityDB.TranslateToDTO(out SetDTO? resultDTO, _castHelper);
            return Task.FromResult(resultDTO);
        }
    }
}
