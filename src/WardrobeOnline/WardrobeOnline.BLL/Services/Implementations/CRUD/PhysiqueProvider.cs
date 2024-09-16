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
        protected override Task<Physique?> AddTranslateToDB(PhysiqueDTO entityDTO)
        {
            entityDTO.TranslateToDB(out Physique? physiqueDB);

            if (entityDTO.SetIDs != null && physiqueDB != null)
            {
                _castHelper.AssertPhysiqueSets(entityDTO.SetIDs, physiqueDB);
            }

            return Task.FromResult(physiqueDB);
        }

        protected override Task<PhysiqueDTO?> AddTranslateToDTO(Physique entityDB)
        {
            entityDB.TranslateToDTO(out PhysiqueDTO? physiqueDTO, _castHelper);
            return Task.FromResult(physiqueDTO);
        }

        protected override Task<Physique?> GetFromDBbyID(int id)
        {
            return _context.Physiques.Where(ent => ent.ID == id)
                .Include(ent => ent.Sets).FirstOrDefaultAsync();
        }

        protected override Task<PhysiqueDTO?> GetTranslateToDTO(Physique entityDB)
        {
            entityDB.TranslateToDTO(out PhysiqueDTO? resultDTO, _castHelper);
            if (resultDTO == null)
                return Task.FromResult(resultDTO);

            if (entityDB.Sets.Count > 0)
            {
                List<int> setIDs = (from Set set in entityDB.Sets
                                    select set.ID).ToList();

                var old = resultDTO;
                resultDTO = new() {
                    ID = old.ID,
                    Growth = old.Growth,
                    Weight = old.Weight,
                    Force = old.Force,
                    Description = old.Description,
                    PersonID = old.PersonID,
                    SetIDs = setIDs
                };
            }

            return Task.FromResult<PhysiqueDTO?>(resultDTO);
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

        protected override Task<PhysiqueDTO?> UpdateTranslateToDTO(Physique entityDB)
        {
            entityDB.TranslateToDTO(out PhysiqueDTO? physiqueDTO, _castHelper);
            return Task.FromResult(physiqueDTO);
        }
    }
}
