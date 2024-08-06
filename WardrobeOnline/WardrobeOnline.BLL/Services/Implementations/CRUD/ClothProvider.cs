using Microsoft.EntityFrameworkCore;

using UnitedSystems.CommonLibrary.WardrobeOnline.DTO;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities;
using UnitedSystems.EventBus.Interfaces;

using WardrobeOnline.BLL.Services.Extensions;
using WardrobeOnline.BLL.Services.Interfaces;
using WardrobeOnline.DAL.Interfaces;

namespace WardrobeOnline.BLL.Services.Implementations.CRUD
{
    public class ClothProvider(IWardrobeContext context, IPaginationService<Cloth> pagination, ICastHelper castHelper, IImageProvider imageProvider, IEventBus eventBus) 
        : CRUDProvider<ClothDTO, Cloth>(context, pagination, castHelper, imageProvider, eventBus)
    {
        protected override Task<Cloth?> GetFromDBbyID(int id)
        {
            return _context.Clothes.Where(ent => ent.ID == id)
                .Include(ent => ent.ClothHasMaterials).ThenInclude(ent => ent.Material).FirstOrDefaultAsync();
        }

        protected override async Task<ClothDTO?> GetTranslateToDTO(Cloth clothDB)
        {
            return new ClothDTO()
            {
                ID = clothDB.ID,
                Name = clothDB.Name,
                Materials = (from clothMaterial in clothDB.ClothHasMaterials
                             select clothMaterial.Material.Name).ToList(),
                Rating = clothDB.Rating,
                Size = clothDB.Size,
                PhotoPaths = _castHelper.GetPhotoPaths(clothDB.Photos)
            };
        }

        protected override async Task<Cloth?> AddTranslateToDB(ClothDTO clothDTO)
        {
            clothDTO.TranslateToDB(out Cloth? clothDB, _castHelper);
            if (clothDTO.Materials != null)
            {
                _castHelper.AssertClothMaterials(clothDTO.Materials, clothDB);
            }

            return clothDB;
        }

        protected override async Task<ClothDTO?> AddTranslateToDTO(Cloth clothDB)
        {
            clothDB.TranslateToDTO(out ClothDTO? clothDTO, _castHelper);
            return clothDTO;
        }

        protected override async Task<Cloth?> UpdateTranslateToDB(ClothDTO clothDTO)
        {
            Cloth? cloth = await GetFromDBbyID(clothDTO.ID);
            if (cloth == null)
                return null;

            if (clothDTO.Name is not null)
                cloth.Name = clothDTO.Name;

            if (clothDTO.Description is not null)
                cloth.Description = clothDTO.Description;

            if (clothDTO.Rating is not null)
                cloth.Rating = clothDTO.Rating.Value;

            if (clothDTO.Size is not null)
                cloth.Size = clothDTO.Size;

            if (clothDTO.Materials is not null)
                _castHelper.AssertClothMaterials(clothDTO.Materials, cloth);

            return cloth;
        }

        protected override async Task<ClothDTO?> UpdateTranslateToDTO(Cloth clothDB)
        {
            clothDB.TranslateToDTO(out ClothDTO? clothDTO, _castHelper);
            return clothDTO;
        }
    }
}
