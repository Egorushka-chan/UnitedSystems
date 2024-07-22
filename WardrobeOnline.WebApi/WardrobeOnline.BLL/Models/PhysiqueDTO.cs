using WardrobeOnline.BLL.Models.Interfaces;
using WardrobeOnline.DAL.Entities;

namespace WardrobeOnline.BLL.Models
{
    public record PhysiqueDTO : IEntityDTO
    {
        public int ID { get; set; }
        public string? Description { get; init; }
        public int? Growth { get; init; }
        public int? Weight { get; init; }
        public int? Force { get; init; }
        public int? PersonID { get; init; }
        public IReadOnlyList<int>? SetIDs { get; init; }

        // Приведения прикольны(в структурах), но тут к сожалению не применимы (даже не наследуешь нормально). Мне в проекте требуется сложное приведение
        // Надо делать особые запросы в базу. Но даже хелпер сюда не засунешь, а в запись саму передавать - порнография

        //public static explicit operator Physique(PhysiqueDTO self)
        //{
        //    Physique physique = new Physique()
        //    {
        //        ID = self.ID,
        //        Description = self.Description,
        //        Growth = self.Growth,
        //        Weight = self.Weight,
        //        Force = self.Force,
        //        PersonID = self.PersonID
        //    };
        //    return physique;
        //}

        //internal Complection TranslateToDB(ITranslator translator)
        //{

        //}
    }
}
