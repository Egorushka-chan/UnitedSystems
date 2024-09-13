using Microsoft.EntityFrameworkCore;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB;
using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DTO;
using UnitedSystems.EventBus.Interfaces;

using WardrobeOnline.BLL.Services.Extensions;
using WardrobeOnline.BLL.Services.Interfaces;
using WardrobeOnline.DAL.Interfaces;

namespace WardrobeOnline.BLL.Services.Implementations.CRUD
{
    public class PersonProvider(IWardrobeContext context, IPaginationService<Person> pagination, ICastHelper castHelper, IImageProvider imageProvider, IEventBus eventBus) : CRUDProvider<PersonDTO, Person>(context, pagination, castHelper, imageProvider, eventBus)
    {
        protected override Task<Person?> AddTranslateToDB(PersonDTO entityDTO)
        {
            entityDTO.TranslateToDB(out Person? personDB);
            if (entityDTO.PhysiqueIDs != null && personDB != null)
            {
                _castHelper.AssertPersonPhysiques(entityDTO.PhysiqueIDs, personDB);
            }

            return Task.FromResult(personDB);
        }

        protected override Task<PersonDTO?> AddTranslateToDTO(Person entityDB)
        {
            entityDB.TranslateToDTO(out PersonDTO? resultDTO, _castHelper);
            return Task.FromResult(resultDTO);
        }

        protected override Task<Person?> GetFromDBbyID(int id)
        {
            return _context.Persons.Where(ent => ent.ID == id)
                .Include(ent => ent.Physiques).FirstOrDefaultAsync();
        }

        protected override Task<PersonDTO?> GetTranslateToDTO(Person entityDB)
        {
            entityDB.TranslateToDTO(out PersonDTO? resultDTO, _castHelper);
            if (resultDTO == null)
                return Task.FromResult(resultDTO);

            if (entityDB.Physiques.Count > 0) {
                List<int> physiqueIDs = (from Physique physique in entityDB.Physiques
                                         select physique.ID).ToList();
                var old = resultDTO;
                resultDTO = new() {
                    ID = old.ID,
                    PhysiqueIDs = physiqueIDs,
                    Name = old.Name,
                    Type = old.Type
                };
            }

            return Task.FromResult<PersonDTO?>(resultDTO);
        }

        protected override async Task<Person?> UpdateTranslateToDB(PersonDTO entityDTO)
        {
            Person? personDB = await GetFromDBbyID(entityDTO.ID);
            if (personDB == null)
                return null;

            if (entityDTO.Name is not null)
                personDB.Name = entityDTO.Name;

            if (entityDTO.Type is not null)
                personDB.Type = entityDTO.Type;

            if (entityDTO.PhysiqueIDs is not null)
                _castHelper.AssertPersonPhysiques(entityDTO.PhysiqueIDs, personDB);

            return personDB;
        }

        protected override Task<PersonDTO?> UpdateTranslateToDTO(Person entityDB)
        {
            entityDB.TranslateToDTO(out PersonDTO? resultDTO, _castHelper);
            return Task.FromResult(resultDTO);
        }
    }
}
