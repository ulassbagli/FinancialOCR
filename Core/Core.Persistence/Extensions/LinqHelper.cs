namespace Core.Persistence.Extensions;

public static class LinqHelper
{
    private static object findAllLock = new Object();
    public static IEnumerable<int> FindAllIndex<T>(this IEnumerable<T> items, Func<T, bool> predicate)
    {
        lock (findAllLock)
        {
            if (items == null) throw new ArgumentNullException("items");
            if (predicate == null) throw new ArgumentNullException("predicate");

            int retVal = 0;
            List<int> indexes = new List<int>();
            foreach (var item in items)
            {
                if (predicate(item)) yield return retVal;
                retVal++;
            }
        }
    }
    public static IEnumerable<T> GetUniqueItems<T>(this IEnumerable<T> items)
    {
        HashSet<T> hashSet = new HashSet<T>();
        lock (findAllLock)
        {
            if (items == null) throw new ArgumentNullException("items");

            foreach (var item in items)
            {
                if(hashSet.Add(item)) yield return item;
            }
        }
    }
}