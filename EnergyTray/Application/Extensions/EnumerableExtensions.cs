using System;
using System.Collections.Generic;

namespace EnergyTray.Application.Extensions
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> predicate)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            foreach (var item in source)
            {
                predicate(item);
            }
        }
    }
}