using MasterDominaSystem.DAL.Reports;

namespace MasterDominaSystem.BLL.Services.Abstractions
{
    /// <summary>
    /// Считывает с помощью рефлексии классы с отчетами, и может выдавать о них информацию
    /// </summary>
    public interface IReportsCollector
    {
        Dictionary<Type, ReportAttribute> GetReports();
    }
}
