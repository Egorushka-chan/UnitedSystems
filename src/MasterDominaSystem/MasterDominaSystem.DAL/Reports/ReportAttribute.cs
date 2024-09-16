using UnitedSystems.CommonLibrary.WardrobeOnline.Entities.Interfaces;

namespace MasterDominaSystem.DAL.Reports
{
    /// <summary>
    /// Обозначает отчет как доступный для генерации кода
    /// </summary>
    /// <remarks>
    /// Конструктор определяет, какие таблицы есть у отчета 
    /// </remarks>
    /// <param name="types">Типы должны быть наследованы от <see cref="IEntityDB"/></param>
    [AttributeUsage(AttributeTargets.Class)]
    public class ReportAttribute(params Type[] types) : Attribute
    {
        public Type[] Types { get; set; } = types;
    }
}
