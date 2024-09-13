namespace MasterDominaSystem.BLL.Services.Extensions
{
    internal static class StringExtensions
    {
        internal static string InSQLStringQuotes(this string value)
        {
            return "'" + value + "'";
        }

        internal static IEnumerable<int> AllIndexesOf(this string str, string value)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentException("the string to find may not be empty", nameof(value));
            for (int index = 0; ; index += value.Length) {
                index = str.IndexOf(value, index);
                if (index == -1)
                    break;
                yield return index;
            }
        }
    }
}
