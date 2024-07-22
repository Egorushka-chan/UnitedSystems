using WardrobeOnline.DAL.Entities;

namespace WardrobeOnline.BLL.Services.Interfaces
{
    public interface IImageProvider
    {
        string GetImageLink(int imageID);
        bool Exist(string imagePath);
        internal int GetPhotoID(string imagePath);
        void Delete(string imagePath);
        void Add(string imagePath);
    }
}
