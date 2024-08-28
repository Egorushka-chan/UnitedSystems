using System.Xml.Linq;

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
        ILogger<ProcedureBaker> logger) : IProcedureBaker
    {
        List<string> _bakedList = [];
        public bool IsBaked(string reportKey)
        {
            return _bakedList.Contains(reportKey);
        }

        public async Task<string> BakeDefaultAsync(string? reportKey = default)
        {
            List<string> reports;
            if (reportKey == null) {
                logger.LogInformation("Запрос на запечку процедур");
                reports = reportsCollector.GetReports()
                    .Select(r => r.Key.GetKey())
                    .ToList();
            }
            else {
                logger.LogInformation("Запрос на запечку процедуры {reportKey}", reportKey);
                reports = reportsCollector.GetReports()
                    .Where(r => r.Key.GetKey() == reportKey)
                    .Select(r => r.Key.GetKey())
                    .ToList();
            }

            logger.LogInformation("Кол-во отчетов на запекание: {count}", reports.Count);
            string scriptsPath = Path.Combine(hostEnvironment.ContentRootPath, "ScriptFiles");
            logger.LogInformation("Путь к файлам скриптов: {scriptsPath}", scriptsPath);

            string finalScript = string.Empty;
            foreach(string report in reports) {
                string reportPath = Path.Combine(scriptsPath, report.Split(".").Last());
                var files = Directory.GetFiles(reportPath).Where(file => file.EndsWith(".sql"));
                logger.LogInformation("У отчёта {report} найдено {count} файлов", report, files.Count());
                foreach (var file in files) {
                    logger.LogInformation("Чтение файла {file}", file);
                    string script = await File.ReadAllTextAsync(file);
                    if (script.StartsWith("create or replace procedure", StringComparison.CurrentCultureIgnoreCase)
                        || script.StartsWith("create procedure", StringComparison.CurrentCultureIgnoreCase)) {

                        logger.LogInformation("Процедура {report} добавлена в лист запечённых объектов.\n" +
                            "Её файл - {file}", report, file);

                        finalScript += script;
                    }
                }
                _bakedList.Add(report);
            }
            return finalScript;
        }

        public async Task<string> AssertBaked(string reportKey)
        {
            if (!IsBaked(reportKey))
                return await BakeDefaultAsync(reportKey);
            else
                return string.Empty;
        }
    }
}
