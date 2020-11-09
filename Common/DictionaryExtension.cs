using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Common
{
    public static class DictionaryExtension
    {
        public static void Deconstruct<T1, T2>(this KeyValuePair<T1, T2> tuple, out T1 key, out T2 value)
        {
            key = tuple.Key;
            value = tuple.Value;
        }

        public static IEnumerable<(T, int)> WithIndex<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Select((value, index) => (value, index));
        }
    }
}
