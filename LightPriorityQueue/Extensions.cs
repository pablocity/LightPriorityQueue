using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPQ
{
    public static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (T item in collection)
                action(item);
        }

        public static T Clamp<T>(this T value, T min, T max, T number) where T : IComparable<T>
        {
            if (number.CompareTo(min) < 0) return min;
            else if (number.CompareTo(max) > 0) return max;
            else return number;
        }
    }
}
