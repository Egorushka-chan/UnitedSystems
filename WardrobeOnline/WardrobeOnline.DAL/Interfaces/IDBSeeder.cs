namespace WardrobeOnline.DAL.Interfaces
{
    public interface IDBSeeder
    {
        /// <summary>
        /// Заполнить базу данных элементами
        /// </summary>
        Task Seed();
    }
}
