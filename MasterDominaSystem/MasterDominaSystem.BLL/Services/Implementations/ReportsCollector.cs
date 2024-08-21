using System.Reflection;

using MasterDominaSystem.BLL.Services.Abstractions;
using MasterDominaSystem.DAL.Reports;

namespace MasterDominaSystem.BLL.Services.Implementations
{
    /// <inheritdoc cref="IReportsCollector"/>
    internal class ReportsCollector : IReportsCollector
    {
        Dictionary<Type, ReportAttribute> Types = [];

        public ReportsCollector()
        {
            var queries = from assembly in AppDomain.CurrentDomain.GetAssemblies().AsParallel()
                          from type in assembly.GetTypes()
                          let attributes = type.GetCustomAttributes<ReportAttribute>(true)
                          where attributes != null && attributes.Any()
                          select new { Type = type, Attributes = attributes.Cast<ReportAttribute>() };
            foreach(var qu in queries) {
                Types.Add(qu.Type, qu.Attributes.Single());
            }
        }

        public Dictionary<Type, ReportAttribute> GetReports() => Types;
    }
}
