using WardrobeOnline.BLL.Models.Interfaces;
using WardrobeOnline.DAL.Entities;

namespace WardrobeOnline.BLL.Models
{
    public record ClothDTO: IEntityDTO
    {
        public int ID { get; set; }
        public string? Name { get; init; }
        public string? Description { get; init; }
        public int? Rating { get; init; }
        public string? Size { get; init; }
        public IReadOnlyList<string>? Materials { get; init; }
        public IReadOnlyList<string>? PhotoPaths { get; init; }

        // Приведения прикольны(в структурах), но тут к сожалению не применимы (даже не наследуешь нормально). Мне в проекте требуется сложное приведение
        // Надо делать особые запросы в базу. Но даже хелпер сюда не засунешь, а в запись саму передавать - порнография

        //public static explicit operator Cloth(ClothDTO self)
        //{
        //    Cloth cloth = new ()
        //    {
        //        ID = self.ID,
        //        Name = self.Name,
        //        Description = self.Description,
        //        Rating = self.Rating,
        //        Size = self.Size,
        //    };
        //    return cloth;
        //}

        //public static explicit operator ClothDTO(Cloth self)
        //{
        //    ClothDTO cloth = new (self.ID, self.Name)
        //    {
        //        Description = self.Description,
        //        Rating = self.Rating,
        //        Size = self.Size,
        //    };
        //    return cloth;
        //}

        //internal Cloth TranslateToDB(ITranslator translator)
        //{
        //Cloth cloth = new Cloth()
        //{
        //    ID = self.iD,
        //    Name = self.Name,
        //    Description = self.Description,
        //    Rating = self.Rating,
        //    Size = self.Size,
        //};
        // return cloth;
        //}
    }
}
