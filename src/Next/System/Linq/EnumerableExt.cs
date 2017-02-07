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

        /// <summary>
        /// Returns first element in sequence or fails with specified exception
        /// </summary>
        /// <typeparam name="TSource">Source sequence element type</typeparam>
        /// <param name="source">Source sequnce</param>
        /// <param name="failWith">Exception factory</param>
        /// <returns>First element</returns>
        public static TSource FirstOrFailWith<TSource>(this IEnumerable<TSource> source,
            Func<Exception> failWith)
        {            
            foreach (var item in source)
            {
                return item;
            }

            throw failWith();
        }

        /// <summary>
        /// Returns first element in sequence that satisfies a specified condition or fails with specified exception
        /// </summary>
        /// <typeparam name="TSource">Source sequence element type</typeparam>
        /// <param name="source">Source sequnce</param>
        /// <param name="predicate">Condition</param>
        /// <param name="failWith">Exception factory</param>
        /// <returns>First element</returns>
        public static TSource FirstOrFailWith<TSource>(this IEnumerable<TSource> source,
            Func<TSource, bool> predicate,
            Func<Exception> failWith) => FirstOrFailWith(source.Where(predicate), failWith);

        public static TSource LastOrFailWith<TSource>(this IEnumerable<TSource> source,
            Func<Exception> failWith)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var list = source as IList<TSource>;

            if (list != null)
            {
                var count = list.Count;

                if (count > 0)
                {
                    return list[count - 1];
                }
            }
            else
            {
                using (var e = source.GetEnumerator())
                {
                    if (e.MoveNext())
                    {
                        TSource result;

                        do
                        {
                            result = e.Current;
                        } while (e.MoveNext());

                        return result;
                    }
                }
            }

            throw failWith();
        }

        public static TSource LastOrFailWith<TSource>(this IEnumerable<TSource> source,
            Func<TSource, bool> predicate,
            Func<Exception> failWith) => LastOrFailWith(source.Where(predicate), failWith);

        public static TSource SingleOrFailWith<TSource>(this IEnumerable<TSource> source,
            Func<Exception> failWith)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            var list = source as IList<TSource>;

            if (list != null)
            {
                if (list.Count == 1)
                {
                    return list[0];
                }
            }
            else
            {
                using (var e = source.GetEnumerator())
                {
                    if (e.MoveNext())
                    {
                        var result = e.Current;

                        if (!e.MoveNext())
                        {
                            return result;
                        }
                    }
                }
            }

            throw failWith();
        }

        public static TSource SingleOrFailWith<TSource>(this IEnumerable<TSource> source,
            Func<TSource, bool> predicate,
            Func<Exception> failWith) => SingleOrFailWith(source.Where(predicate), failWith);

    }
}
