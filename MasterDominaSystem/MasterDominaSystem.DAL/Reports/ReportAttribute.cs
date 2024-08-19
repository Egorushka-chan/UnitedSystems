using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

namespace MasterDominaSystem.DAL.Reports
{
    /// <summary>
    /// Обозначает отчет как доступный для генерации кода
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ReportAttribute : Attribute
    {
        /// <summary>
        /// Конструктор определяет, какие таблицы есть у отчета 
        /// </summary>
        /// <param name="types">Типы должны быть наследованы от <see cref="IEntityDB"/></param>
        public ReportAttribute(params Type[] types) => Types = types;

        public Type[] Types { get; set; }
    }
}
