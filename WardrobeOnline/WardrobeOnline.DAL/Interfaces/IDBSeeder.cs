namespace WardrobeOnline.DAL.Interfaces
{
    internal interface IDBSeeder
    {
        /// <summary>
        /// Заполнить базу данных элементами
        /// </summary>
        Task Seed();
    }
}
