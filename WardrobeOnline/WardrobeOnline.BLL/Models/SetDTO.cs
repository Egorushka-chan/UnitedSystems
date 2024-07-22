using WardrobeOnline.BLL.Models.Interfaces;
using WardrobeOnline.DAL.Entities;

namespace WardrobeOnline.BLL.Models
{
    public record SetDTO : IEntityDTO
    {
        public int ID { get; set; }
        public string? Name { get; init; }
        public string? Description { get; init; }
        public string? Season { get; init; }
        public int? PhysiqueID { get; init; }
        public IReadOnlyList<int>? ClothIDs { get; init; }

        // Приведения прикольны(в структурах), но тут к сожалению не применимы (даже не наследуешь нормально). Мне в проекте требуется сложное приведение
        // Надо делать особые запросы в базу. Но даже хелпер сюда не засунешь, а в запись саму передавать - порнография

        /// <summary>
        /// Это преобразование требует после себя обязательного определения Session/SessionID
        /// Требование использовать именно такой определитель
        /// И CastHelper не может помочь тут
        /// </summary>
        //public static explicit operator Set(SetDTO self)
        //{
        //    Set set = new()
        //    {
        //        ID = self.ID,
        //        Name = self.Name,
        //        Description = self.Description,
        //        PhysiqueID = self.PhysiqueID
        //    };
        //    return set;
        //}

        //public static explicit operator SetDTO(Set self)
        //{
        //    SetDTO set = new(self.ID)
        //    {
        //        ID = self.ID,
        //        Name = self.Name,
        //        Description = self.Description,
        //        PhysiqueID = self.PhysiqueID
        //    };
        //    return set;
        //}

        //internal Set TranslateToDB(ITranslator translator)
        //{
        //    int id;
        //    if (translator.TryFindSeasonID(Season, out id))
        //    {
        //        Set set = new()
        //        {
        //            ID = ID,
        //            Name = Name,
        //            Description = Description,
        //            SeasonID = id,
        //            ComplectionID = ComplectionID
        //        };
        //        return set;
        //    }
        //    else
        //    {
        //        Set set = new()
        //        {
        //            ID = ID,
        //            Name = Name,
        //            Description = Description,
        //            Season = new Season() { Name = Season},
        //            ComplectionID = ComplectionID
        //        };
        //        return set;
        //    }
        //}
    }
}
