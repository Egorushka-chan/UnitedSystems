using Microsoft.EntityFrameworkCore;

using WardrobeOnline.BLL.Models;
using WardrobeOnline.BLL.Services.Extensions;
using WardrobeOnline.BLL.Services.Interfaces;
using WardrobeOnline.DAL.Entities;
using WardrobeOnline.DAL.Interfaces;
using WardrobeOnline.DAL.Repositories.Interfaces;
using System.Linq;

namespace WardrobeOnline.BLL.Services.Implementations.CRUD
{
    public class PersonProvider : CRUDProvider<PersonDTO, Person>
    {
        public PersonProvider(IWardrobeContext context, IPaginationService<Person> pagination, ICastHelper castHelper, IImageProvider imageProvider)
            : base(context, pagination, castHelper, imageProvider)
        {

        }

        protected override async Task<Person?> AddTranslateToDB(PersonDTO entityDTO)
        {
            entityDTO.TranslateToDB(out Person? personDB, _castHelper);
            if (entityDTO.PhysiqueIDs != null)
            {
                _castHelper.AssertPersonPhysiques(entityDTO.PhysiqueIDs, personDB);
            }

            return personDB;
        }

        protected override async Task<PersonDTO?> AddTranslateToDTO(Person entityDB)
        {
            entityDB.TranslateToDTO(out PersonDTO? resultDTO, _castHelper);
            return resultDTO;
        }

        protected override Task<Person?> GetFromDBbyID(int id)
        {
            return _context.Persons.Where(ent => ent.ID == id)
                .Include(ent => ent.Physiques).FirstOrDefaultAsync();
        }

        protected override async Task<PersonDTO?> GetTranslateToDTO(Person entityDB)
        {
            entityDB.TranslateToDTO(out PersonDTO? resultDTO, _castHelper);
            if (resultDTO == null)
                return null;

            if (entityDB.Physiques.Count > 0)
            {
                List<int> physiqueIDs = (from Physique physique in entityDB.Physiques
                                         select physique.ID).ToList();
                resultDTO = resultDTO with { PhysiqueIDs = physiqueIDs };
            }

            return resultDTO;
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

        protected override async Task<PersonDTO?> UpdateTranslateToDTO(Person entityDB)
        {
            entityDB.TranslateToDTO(out PersonDTO? resultDTO, _castHelper);
            return resultDTO;
        }
    }
}
