namespace MasterDominaSystem.BLL.Services.Extensions
{
    internal static class IntExtensions
    {
        internal static string ToSQLArray(this IEnumerable<int> array)
        {
            string result = "'{{";
            bool isBegin = true;

            foreach (int item in array) {
                if (isBegin) {
                    result += item.ToString();
                    isBegin = false;
                }
                else {
                    result += $",{item}";
                }
            }

            result += "}}'";

            return result;
        }
    }
}
