using Microsoft.EntityFrameworkCore;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DTO;
using UnitedSystems.EventBus.Interfaces;

using WardrobeOnline.BLL.Services.Extensions;
using WardrobeOnline.BLL.Services.Interfaces;
using WardrobeOnline.DAL.Interfaces;

namespace WardrobeOnline.BLL.Services.Implementations.CRUD
{
    public class PhysiqueProvider(IWardrobeContext context, IPaginationService<Physique> pagination, ICastHelper castHelper, IImageProvider imageProvider, IEventBus eventBus) 
        : CRUDProvider<PhysiqueDTO, Physique>(context, pagination, castHelper, imageProvider, eventBus)
    {
        protected override async Task<Physique?> AddTranslateToDB(PhysiqueDTO entityDTO)
        {
            entityDTO.TranslateToDB(out Physique? physiqueDB, _castHelper);

            if (entityDTO.SetIDs != null)
            {
                _castHelper.AssertPhysiqueSets(entityDTO.SetIDs, physiqueDB);
            }

            return physiqueDB;
        }

        protected override async Task<PhysiqueDTO?> AddTranslateToDTO(Physique entityDB)
        {
            entityDB.TranslateToDTO(out PhysiqueDTO? physiqueDTO, _castHelper);
            return physiqueDTO;
        }

        protected override Task<Physique?> GetFromDBbyID(int id)
        {
            return _context.Physiques.Where(ent => ent.ID == id)
                .Include(ent => ent.Sets).FirstOrDefaultAsync();
        }

        protected override async Task<PhysiqueDTO?> GetTranslateToDTO(Physique entityDB)
        {
            entityDB.TranslateToDTO(out PhysiqueDTO? resultDTO, _castHelper);
            if (resultDTO == null)
                return null;

            if (entityDB.Sets.Count > 0)
            {
                List<int> setIDs = (from Set set in entityDB.Sets
                                    select set.ID).ToList();
                resultDTO = resultDTO with { SetIDs = setIDs };
            }

            return resultDTO;
        }

        protected override async Task<Physique?> UpdateTranslateToDB(PhysiqueDTO entityDTO)
        {
            Physique? physiqueDB = await GetFromDBbyID(entityDTO.ID);
            if (physiqueDB == null)
                return null;

            if (entityDTO.Weight is not null)
                physiqueDB.Weight = entityDTO.Weight.Value;

            if (entityDTO.Force is not null)
                physiqueDB.Force = entityDTO.Force.Value;

            if (entityDTO.Growth is not null)
                physiqueDB.Growth = entityDTO.Growth.Value;

            if (entityDTO.Description is not null)
                physiqueDB.Description = entityDTO.Description;

            if (entityDTO.PersonID is not null)
                physiqueDB.PersonID = entityDTO.PersonID.Value;

            if (entityDTO.SetIDs is not null)
                _castHelper.AssertPhysiqueSets(entityDTO.SetIDs, physiqueDB);

            return physiqueDB;
        }

        protected override async Task<PhysiqueDTO?> UpdateTranslateToDTO(Physique entityDB)
        {
            entityDB.TranslateToDTO(out PhysiqueDTO? physiqueDTO, _castHelper);
            return physiqueDTO;
        }
    }
}
