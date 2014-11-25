using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace System.Linq
{
    /// <summary>
    /// Extension methods for IEnumerable
    /// </summary>
    public static class EnumerableExt
    {
        /// <summary>
        /// Follows sequence with new element
        /// </summary>
        /// <typeparam name="TSource">Source sequence element type</typeparam>
        /// <param name="sequence">Source sequence</param>
        /// <param name="value">New element value</param>
        /// <returns>Sequence with new last element</returns>
        public static IEnumerable<TSource> Follow<TSource>(this IEnumerable<TSource> sequence, TSource value)
        {
            foreach (var item in sequence)
            {
                yield return item;
            }

            yield return value;
        }

        /// <summary>
        /// Converts sole element to enumerable
        /// </summary>
        /// <typeparam name="TSource">Type of element</typeparam>
        /// <param name="this">Value</param>
        /// <returns>Sequence of one element</returns>
        public static IEnumerable<TSource> ToEnumerable<TSource>(this TSource @this)
        {
            yield return @this;
        }

        /// <summary>
        /// Leads sequence with new element
        /// </summary>
        /// <typeparam name="TSource">Source sequence element type</typeparam>
        /// <param name="sequence">Source sequnce</param>
        /// <param name="value">New element</param>
        /// <returns>Sequence with new leading element</returns>
        public static IEnumerable<TSource> Lead<TSource>(this IEnumerable<TSource> sequence, TSource value)
        {
            yield return value;

            foreach (var item in sequence)
            {
                yield return item;
            }
        }
    }
}
