namespace MasterDominaSystem.RMQL.Extensions
{
    internal static class StringExtensions
    {
        internal static IEnumerable<int> AllIndexesOf(this string str, string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Значение которое нужно найти не может быть пустым", nameof(value));
            for (int index = 0; ; index += value.Length) {
                index = str.IndexOf(value, index);
                if (index == -1)
                    break;
                yield return index;
            }
        }

        internal static string GetArea(this string str, int offset, int size)
        {
            int startPartition = offset > (size / 2) ? offset - (size / 2) : 0;
            int endPartition = offset < str.Length - (size / 2) ? offset + (size / 2) : str.Length;
            string area = str[startPartition..endPartition];
            return area;
        }
    }
}
