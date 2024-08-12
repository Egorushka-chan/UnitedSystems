namespace MasterDominaSystem.BLL.Services.Extensions
{
    internal static class TypeExtensions
    {
        /// <summary>
        /// Создаёт ключ из имени типа
        /// </summary>
        /// <remarks>
        /// Т.к имя очереди получается из имени класса ивента, следует учитывать существование множества вариаций приведения Type в string.
        /// Чтобы избежать проблем, приводить нужно через этот метод
        /// </remarks>
        internal static string GetKey(this Type type)
        {
            // return type.Name - не работает с generic
            return type.ToString();
        }
    }
}
