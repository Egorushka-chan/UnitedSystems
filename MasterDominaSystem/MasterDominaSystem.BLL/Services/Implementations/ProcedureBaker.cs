using MasterDominaSystem.BLL.Services.Abstractions;
using MasterDominaSystem.BLL.Services.Extensions;
using MasterDominaSystem.DAL;

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MasterDominaSystem.BLL.Services.Implementations
{
    internal class ProcedureBaker(IReportsCollector reportsCollector,
        IWebHostEnvironment hostEnvironment,
        MasterContext context,
        ILogger<ProcedureBaker> logger) : IProcedureBaker
    {
        List<string> _bakedList = [];
        public bool IsBaked(string reportKey)
        {
            return _bakedList.Contains(reportKey);
        }

        public async Task BakeDefaultAsync(string? reportKey = default)
        {
            List<string> reports;
            if (reportKey != null) {
                logger.LogTrace("Запрос на запечку процедур");
                reports = reportsCollector.GetReports()
                    .Select(r => r.Key.GetKey())
                    .ToList();
            }
            else {
                logger.LogTrace("Запрос на запечку процедуры {reportKey}", reportKey);
                reports = reportsCollector.GetReports()
                    .Where(r => r.Key.GetKey() == reportKey)
                    .Select(r => r.Key.GetKey())
                    .ToList();
            }

            logger.LogTrace("Кол-во отчетов на запекание: {count}", reports.Count);
            string scriptsPath = Path.Combine(hostEnvironment.ContentRootPath, "ScriptFiles");
            logger.LogTrace("Путь к файлам скриптов: {scriptsPath}", scriptsPath);

            foreach(string report in reports) {
                string reportPath = Path.Combine(scriptsPath, report);
                var files = Directory.GetFiles(reportPath).Where(file => file.EndsWith(".sql"));
                logger.LogTrace("Кол-во файлов скриптов: {files.Count}", files.Count());
                foreach (var file in files) {
                    string script = await File.ReadAllTextAsync(file);
                    if (script.StartsWith("create or replace procedure", StringComparison.CurrentCultureIgnoreCase)
                        || script.StartsWith("create procedure", StringComparison.CurrentCultureIgnoreCase)) {

                        logger.LogDebug("Выполняется скрипт процедуры: {script}", script);
                        int count = await context.Database.ExecuteSqlRawAsync(script);
                        if (count != 1)
                            logger.LogWarning("Непонятное поведение: количество затронутых строк != 1: {count}", count);
                        _bakedList.Add(report);
                    }
                }
            }
        }

        public Task AssertBaked(string reportKey)
        {
            if (!IsBaked(reportKey))
                return BakeDefaultAsync(reportKey);
            else
                return Task.CompletedTask;
        }
    }
}
