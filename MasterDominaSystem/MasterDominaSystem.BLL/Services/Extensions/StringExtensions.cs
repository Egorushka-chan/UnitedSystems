namespace MasterDominaSystem.BLL.Services.Extensions
{
    internal static class StringExtensions
    {
        internal static string InSQLStringQuotes(this string value)
        {
            return "'" + value + "'";
        }
    }
}
