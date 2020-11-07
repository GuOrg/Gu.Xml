// ReSharper disable UnusedMember.Global
namespace Gu.Xml
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Extension methods that avoids allocations.
    /// </summary>
    internal static class EnumerableExt
    {
        /// <summary>
        /// Try getting the element at <paramref name="index"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="source"/></typeparam>
        /// <param name="source">The source collection, can be null.</param>
        /// <param name="index">The index.</param>
        /// <param name="result">The element at index if found, can be null.</param>
        /// <returns>True if an element was found.</returns>
        internal static bool TryElementAt<T>(this T[] source, int index, out T result)
        {
            result = default;
            if (source == null)
            {
                return false;
            }

            if (index < 0 ||
                source.Length <= index)
            {
                return false;
            }

            result = source[index];
            return true;
        }

        /// <summary>
        /// Try getting the single element in <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="source"/></typeparam>
        /// <param name="source">The source collection, can be null.</param>
        /// <param name="result">The single element, can be null.</param>
        /// <returns>True if an element was found.</returns>
        internal static bool TrySingle<T>(this T[] source, out T result)
        {
            result = default;
            if (source == null)
            {
                return false;
            }

            if (source.Length == 1)
            {
                result = source[0];
                return true;
            }

            return false;
        }

        /// <summary>
        /// Try getting the single element in <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="source"/></typeparam>
        /// <typeparam name="TResult">The type to filter by.</typeparam>
        /// <param name="source">The source collection, can be null.</param>
        /// <param name="result">The single element, can be null.</param>
        /// <returns>True if an element was found.</returns>
        internal static bool TrySingleOfType<T, TResult>(this T[] source, out TResult result)
            where TResult : T
        {
            result = default;
            if (source == null)
            {
                return false;
            }

            for (var i = 0; i < source.Length; i++)
            {
                if (source[i] is TResult item)
                {
                    for (int j = i + 1; j < source.Length; j++)
                    {
                        if (source[j] is TResult)
                        {
                            return false;
                        }
                    }

                    result = item;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Try getting the single element in <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="source"/></typeparam>
        /// <typeparam name="TResult">The type to filter by.</typeparam>
        /// <param name="source">The source collection, can be null.</param>
        /// <param name="predicate">The filter</param>
        /// <param name="result">The single element, can be null.</param>
        /// <returns>True if an element was found.</returns>
        internal static bool TrySingleOfType<T, TResult>(this T[] source, Func<TResult, bool> predicate, out TResult result)
            where TResult : T
        {
            result = default;
            if (source == null)
            {
                return false;
            }

            for (var i = 0; i < source.Length; i++)
            {
                if (source[i] is TResult item &&
                    predicate(item))
                {
                    for (var j = i + 1; j < source.Length; j++)
                    {
                        if (source[j] is TResult temp &&
                            predicate(temp))
                        {
                            return false;
                        }
                    }

                    result = item;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Try getting the single element in <paramref name="source"/> matching <paramref name="predicate"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="source"/></typeparam>
        /// <param name="source">The source collection, can be null.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="result">The single element matching the predicate, can be null.</param>
        /// <returns>True if an element was found.</returns>
        internal static bool TrySingle<T>(this T[] source, Func<T, bool> predicate, out T result)
        {
            result = default;
            if (source == null)
            {
                return false;
            }

            for (var i = 0; i < source.Length; i++)
            {
                var item = source[i];
                if (predicate(item))
                {
                    result = item;
                    for (var j = i + 1; j < source.Length; j++)
                    {
                        if (predicate(source[j]))
                        {
                            result = default;
                            return false;
                        }
                    }

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Try getting the first element in <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="source"/></typeparam>
        /// <param name="source">The source collection, can be null.</param>
        /// <param name="result">The first element, can be null.</param>
        /// <returns>True if an element was found.</returns>
        internal static bool TryFirst<T>(this T[] source, out T result)
        {
            result = default;
            if (source == null)
            {
                return false;
            }

            if (source.Length == 0)
            {
                return false;
            }

            result = source[0];
            return true;
        }

        /// <summary>
        /// Try getting the single element in <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="source"/></typeparam>
        /// <typeparam name="TResult">The type to filter by.</typeparam>
        /// <param name="source">The source collection, can be null.</param>
        /// <param name="result">The single element, can be null.</param>
        /// <returns>True if an element was found.</returns>
        internal static bool TryFirstOfType<T, TResult>(this T[] source, out TResult result)
            where TResult : T
        {
            result = default;
            if (source == null)
            {
                return false;
            }

            for (var i = 0; i < source.Length; i++)
            {
                if (source[i] is TResult item)
                {
                    result = item;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Try getting the single element in <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="source"/></typeparam>
        /// <typeparam name="TResult">The type to filter by.</typeparam>
        /// <param name="source">The source collection, can be null.</param>
        /// <param name="predicate">The filter</param>
        /// <param name="result">The single element, can be null.</param>
        /// <returns>True if an element was found.</returns>
        internal static bool TryFirstOfType<T, TResult>(this T[] source, Func<TResult, bool> predicate, out TResult result)
            where TResult : T
        {
            result = default;
            if (source == null)
            {
                return false;
            }

            for (var i = 0; i < source.Length; i++)
            {
                if (source[i] is TResult item &&
                    predicate(item))
                {
                    result = item;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Try getting the first element in <paramref name="source"/> matching <paramref name="predicate"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="source"/></typeparam>
        /// <param name="source">The source collection, can be null.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="result">The first element matching the predicate, can be null.</param>
        /// <returns>True if an element was found.</returns>
        internal static bool TryFirst<T>(this T[] source, Func<T, bool> predicate, out T result)
        {
            result = default;
            if (source == null)
            {
                return false;
            }

            foreach (var item in source)
            {
                if (predicate(item))
                {
                    result = item;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Try getting the last element in <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="source"/></typeparam>
        /// <param name="source">The source collection, can be null.</param>
        /// <param name="result">The last element if found, can be null.</param>
        /// <returns>True if an element was found.</returns>
        internal static bool TryLast<T>(this T[] source, out T result)
        {
            result = default;
            if (source == null)
            {
                return false;
            }

            if (source.Length == 0)
            {
                result = default;
                return false;
            }

            result = source[source.Length - 1];
            return true;
        }

        /// <summary>
        /// Try getting the single element in <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="source"/></typeparam>
        /// <typeparam name="TResult">The type to filter by.</typeparam>
        /// <param name="source">The source collection, can be null.</param>
        /// <param name="result">The single element, can be null.</param>
        /// <returns>True if an element was found.</returns>
        internal static bool TryLastOfType<T, TResult>(this T[] source, out TResult result)
            where TResult : T
        {
            result = default;
            if (source == null)
            {
                return false;
            }

            for (var i = source.Length - 1; i >= 0; i--)
            {
                if (source[i] is TResult item)
                {
                    result = item;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Try getting the single element in <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="source"/></typeparam>
        /// <typeparam name="TResult">The type to filter by.</typeparam>
        /// <param name="source">The source collection, can be null.</param>
        /// <param name="predicate">The filter</param>
        /// <param name="result">The single element, can be null.</param>
        /// <returns>True if an element was found.</returns>
        internal static bool TryLastOfType<T, TResult>(this T[] source, Func<TResult, bool> predicate, out TResult result)
            where TResult : T
        {
            result = default;
            if (source == null)
            {
                return false;
            }

            for (var i = source.Length - 1; i >= 0; i--)
            {
                if (source[i] is TResult item &&
                    predicate(item))
                {
                    result = item;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Try getting the last element in <paramref name="source"/> matching <paramref name="predicate"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="source"/></typeparam>
        /// <param name="source">The source collection, can be null.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="result">The last element matching the predicate, can be null.</param>
        /// <returns>True if an element was found.</returns>
        internal static bool TryLast<T>(this T[] source, Func<T, bool> predicate, out T result)
        {
            result = default;
            if (source == null)
            {
                return false;
            }

            for (var i = source.Length - 1; i >= 0; i--)
            {
                var item = source[i];
                if (predicate(item))
                {
                    result = item;
                    return true;
                }
            }

            result = default;
            return false;
        }

        /// <summary>
        /// Try getting the element at <paramref name="index"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="source"/></typeparam>
        /// <param name="source">The source collection, can be null.</param>
        /// <param name="index">The index.</param>
        /// <param name="result">The element at index if found, can be null.</param>
        /// <returns>True if an element was found.</returns>
        internal static bool TryElementAt<T>(this IReadOnlyList<T> source, int index, out T result)
        {
            result = default;
            if (source == null)
            {
                return false;
            }

            if (index < 0 ||
                source.Count <= index)
            {
                return false;
            }

            result = source[index];
            return true;
        }

        /// <summary>
        /// Try getting the single element in <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="source"/></typeparam>
        /// <param name="source">The source collection, can be null.</param>
        /// <param name="result">The single element, can be null.</param>
        /// <returns>True if an element was found.</returns>
        internal static bool TrySingle<T>(this IReadOnlyList<T> source, out T result)
        {
            result = default;
            if (source == null)
            {
                return false;
            }

            if (source.Count == 1)
            {
                result = source[0];
                return true;
            }

            return false;
        }

        /// <summary>
        /// Try getting the single element in <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="source"/></typeparam>
        /// <typeparam name="TResult">The type to filter by.</typeparam>
        /// <param name="source">The source collection, can be null.</param>
        /// <param name="result">The single element, can be null.</param>
        /// <returns>True if an element was found.</returns>
        internal static bool TrySingleOfType<T, TResult>(this IReadOnlyList<T> source, out TResult result)
            where TResult : T
        {
            result = default;
            if (source == null)
            {
                return false;
            }

            for (var i = 0; i < source.Count; i++)
            {
                if (source[i] is TResult item)
                {
                    for (int j = i + 1; j < source.Count; j++)
                    {
                        if (source[j] is TResult)
                        {
                            return false;
                        }
                    }

                    result = item;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Try getting the single element in <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="source"/></typeparam>
        /// <typeparam name="TResult">The type to filter by.</typeparam>
        /// <param name="source">The source collection, can be null.</param>
        /// <param name="predicate">The filter</param>
        /// <param name="result">The single element, can be null.</param>
        /// <returns>True if an element was found.</returns>
        internal static bool TrySingleOfType<T, TResult>(this IReadOnlyList<T> source, Func<TResult, bool> predicate, out TResult result)
            where TResult : T
        {
            result = default;
            if (source == null)
            {
                return false;
            }

            for (var i = 0; i < source.Count; i++)
            {
                if (source[i] is TResult item &&
                    predicate(item))
                {
                    for (var j = i + 1; j < source.Count; j++)
                    {
                        if (source[j] is TResult temp &&
                            predicate(temp))
                        {
                            return false;
                        }
                    }

                    result = item;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Try getting the single element in <paramref name="source"/> matching <paramref name="predicate"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="source"/></typeparam>
        /// <param name="source">The source collection, can be null.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="result">The single element matching the predicate, can be null.</param>
        /// <returns>True if an element was found.</returns>
        internal static bool TrySingle<T>(this IReadOnlyList<T> source, Func<T, bool> predicate, out T result)
        {
            result = default;
            if (source == null)
            {
                return false;
            }

            for (var i = 0; i < source.Count; i++)
            {
                var item = source[i];
                if (predicate(item))
                {
                    result = item;
                    for (var j = i + 1; j < source.Count; j++)
                    {
                        if (predicate(source[j]))
                        {
                            return false;
                        }
                    }

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Try getting the first element in <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="source"/></typeparam>
        /// <param name="source">The source collection, can be null.</param>
        /// <param name="result">The first element, can be null.</param>
        /// <returns>True if an element was found.</returns>
        internal static bool TryFirst<T>(this IReadOnlyList<T> source, out T result)
        {
            result = default;
            if (source == null)
            {
                return false;
            }

            if (source.Count == 0)
            {
                return false;
            }

            result = source[0];
            return true;
        }

        /// <summary>
        /// Try getting the single element in <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="source"/></typeparam>
        /// <typeparam name="TResult">The type to filter by.</typeparam>
        /// <param name="source">The source collection, can be null.</param>
        /// <param name="result">The single element, can be null.</param>
        /// <returns>True if an element was found.</returns>
        internal static bool TryFirstOfType<T, TResult>(this IReadOnlyList<T> source, out TResult result)
            where TResult : T
        {
            result = default;
            if (source == null)
            {
                return false;
            }

            for (var i = 0; i < source.Count; i++)
            {
                if (source[i] is TResult item)
                {
                    result = item;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Try getting the single element in <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="source"/></typeparam>
        /// <typeparam name="TResult">The type to filter by.</typeparam>
        /// <param name="source">The source collection, can be null.</param>
        /// <param name="predicate">The filter</param>
        /// <param name="result">The single element, can be null.</param>
        /// <returns>True if an element was found.</returns>
        internal static bool TryFirstOfType<T, TResult>(this IReadOnlyList<T> source, Func<TResult, bool> predicate, out TResult result)
            where TResult : T
        {
            result = default;
            if (source == null)
            {
                return false;
            }

            for (var i = 0; i < source.Count; i++)
            {
                if (source[i] is TResult item &&
                    predicate(item))
                {
                    result = item;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Try getting the first element in <paramref name="source"/> matching <paramref name="predicate"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="source"/></typeparam>
        /// <param name="source">The source collection, can be null.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="result">The first element matching the predicate, can be null.</param>
        /// <returns>True if an element was found.</returns>
        internal static bool TryFirst<T>(this IReadOnlyList<T> source, Func<T, bool> predicate, out T result)
        {
            result = default;
            if (source == null)
            {
                return false;
            }

            foreach (var item in source)
            {
                if (predicate(item))
                {
                    result = item;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Try getting the last element in <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="source"/></typeparam>
        /// <param name="source">The source collection, can be null.</param>
        /// <param name="result">The last element if found, can be null.</param>
        /// <returns>True if an element was found.</returns>
        internal static bool TryLast<T>(this IReadOnlyList<T> source, out T result)
        {
            result = default;
            if (source == null)
            {
                return false;
            }

            if (source.Count == 0)
            {
                result = default;
                return false;
            }

            result = source[source.Count - 1];
            return true;
        }

        /// <summary>
        /// Try getting the single element in <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="source"/></typeparam>
        /// <typeparam name="TResult">The type to filter by.</typeparam>
        /// <param name="source">The source collection, can be null.</param>
        /// <param name="result">The single element, can be null.</param>
        /// <returns>True if an element was found.</returns>
        internal static bool TryLastOfType<T, TResult>(this IReadOnlyList<T> source, out TResult result)
            where TResult : T
        {
            result = default;
            if (source == null)
            {
                return false;
            }

            for (var i = source.Count - 1; i >= 0; i--)
            {
                if (source[i] is TResult item)
                {
                    result = item;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Try getting the single element in <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="source"/></typeparam>
        /// <typeparam name="TResult">The type to filter by.</typeparam>
        /// <param name="source">The source collection, can be null.</param>
        /// <param name="predicate">The filter</param>
        /// <param name="result">The single element, can be null.</param>
        /// <returns>True if an element was found.</returns>
        internal static bool TryLastOfType<T, TResult>(this IReadOnlyList<T> source, Func<TResult, bool> predicate, out TResult result)
            where TResult : T
        {
            result = default;
            if (source == null)
            {
                return false;
            }

            for (var i = source.Count - 1; i >= 0; i--)
            {
                if (source[i] is TResult item &&
                    predicate(item))
                {
                    result = item;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Try getting the last element in <paramref name="source"/> matching <paramref name="predicate"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="source"/></typeparam>
        /// <param name="source">The source collection, can be null.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="result">The last element matching the predicate, can be null.</param>
        /// <returns>True if an element was found.</returns>
        internal static bool TryLast<T>(this IReadOnlyList<T> source, Func<T, bool> predicate, out T result)
        {
            result = default;
            if (source == null)
            {
                return false;
            }

            for (var i = source.Count - 1; i >= 0; i--)
            {
                var item = source[i];
                if (predicate(item))
                {
                    result = item;
                    return true;
                }
            }

            result = default;
            return false;
        }

        /// <summary>
        /// Returns <paramref name="before"/> then <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="source"/></typeparam>
        /// <param name="source">The source collection, can be null.</param>
        /// <param name="before">The item to retuns before <paramref name="source"/>.</param>
        /// <returns>An <see cref="IEnumerable{T}"/>.</returns>
        internal static IEnumerable<T> Prepend<T>(this IEnumerable<T> source, T before)
        {
            yield return before;
            foreach (var item in source)
            {
                yield return item;
            }
        }

        /// <summary>
        /// Try getting the element at <paramref name="index"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="source"/></typeparam>
        /// <param name="source">The source collection, can be null.</param>
        /// <param name="index">The index.</param>
        /// <param name="result">The element at index if found, can be null.</param>
        /// <returns>True if an element was found.</returns>
        internal static bool TryElementAt<T>(this IEnumerable<T> source, int index, out T result)
        {
            result = default;
            if (source == null)
            {
                return false;
            }

            var current = 0;
            using var e = source.GetEnumerator();
            while (e.MoveNext())
            {
                if (current == index)
                {
                    result = e.Current;
                    return true;
                }

                current++;
            }

            return false;
        }

        /// <summary>
        /// Try getting the single element in <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="source"/></typeparam>
        /// <param name="source">The source collection, can be null.</param>
        /// <param name="result">The single element, can be null.</param>
        /// <returns>True if an element was found.</returns>
        internal static bool TrySingle<T>(this IEnumerable<T> source, out T result)
        {
            result = default;
            if (source == null)
            {
                return false;
            }

            using var e = source.GetEnumerator();
            if (e.MoveNext())
            {
                result = e.Current;
                if (!e.MoveNext())
                {
                    return true;
                }
            }

            result = default;
            return false;
        }

        /// <summary>
        /// Try getting the single element in <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="source"/></typeparam>
        /// <typeparam name="TResult">The type to filter by.</typeparam>
        /// <param name="source">The source collection, can be null.</param>
        /// <param name="result">The single element, can be null.</param>
        /// <returns>True if an element was found.</returns>
        internal static bool TrySingleOfType<T, TResult>(this IEnumerable<T> source, out TResult result)
            where TResult : T
        {
            result = default;
            if (source == null)
            {
                return false;
            }

            using var e = source.GetEnumerator();
            if (e.MoveNext())
            {
                if (e.Current is TResult item)
                {
                    while (e.MoveNext())
                    {
                        if (e.Current is TResult)
                        {
                            return false;
                        }
                    }

                    result = item;
                    return true;
                }

                return false;
            }

            result = default;
            return false;
        }

        /// <summary>
        /// Try getting the single element in <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="source"/></typeparam>
        /// <typeparam name="TResult">The type to filter by.</typeparam>
        /// <param name="source">The source collection, can be null.</param>
        /// <param name="predicate">The filter</param>
        /// <param name="result">The single element, can be null.</param>
        /// <returns>True if an element was found.</returns>
        internal static bool TrySingleOfType<T, TResult>(this IEnumerable<T> source, Func<TResult, bool> predicate, out TResult result)
            where TResult : T
        {
            result = default;
            if (source == null)
            {
                return false;
            }

            using var e = source.GetEnumerator();
            while (e.MoveNext())
            {
                if (e.Current is TResult item &&
                    predicate(item))
                {
                    while (e.MoveNext())
                    {
                        if (e.Current is TResult temp &&
                            predicate(temp))
                        {
                            return false;
                        }
                    }

                    result = item;
                    return true;
                }
            }

            result = default;
            return false;
        }

        /// <summary>
        /// Try getting the single element in <paramref name="source"/> matching <paramref name="predicate"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="source"/></typeparam>
        /// <param name="source">The source collection, can be null.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="result">The single element matching the predicate, can be null.</param>
        /// <returns>True if an element was found.</returns>
        internal static bool TrySingle<T>(this IEnumerable<T> source, Func<T, bool> predicate, out T result)
        {
            result = default;
            if (source == null)
            {
                return false;
            }

            using (var e = source.GetEnumerator())
            {
                while (e.MoveNext())
                {
                    result = e.Current;
                    if (predicate(result))
                    {
                        while (e.MoveNext())
                        {
                            if (predicate(e.Current))
                            {
                                result = default;
                                return false;
                            }
                        }

                        return true;
                    }
                }
            }

            result = default;
            return false;
        }

        /// <summary>
        /// Try getting the first element in <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="source"/></typeparam>
        /// <param name="source">The source collection, can be null.</param>
        /// <param name="result">The first element, can be null.</param>
        /// <returns>True if an element was found.</returns>
        internal static bool TryFirst<T>(this IEnumerable<T> source, out T result)
        {
            result = default;
            if (source == null)
            {
                return false;
            }

            using var e = source.GetEnumerator();
            if (e.MoveNext())
            {
                result = e.Current;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Try getting the first element in <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="source"/></typeparam>
        /// <typeparam name="TResult">The type to filter by.</typeparam>
        /// <param name="source">The source collection, can be null.</param>
        /// <param name="result">The first element, can be null.</param>
        /// <returns>True if an element was found.</returns>
        internal static bool TryFirstOfType<T, TResult>(this IEnumerable<T> source, out TResult result)
            where TResult : T
        {
            result = default;
            if (source == null)
            {
                return false;
            }

            using var e = source.GetEnumerator();
            if (e.MoveNext() &&
                e.Current is TResult item)
            {
                result = item;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Try getting the first element in <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="source"/></typeparam>
        /// <typeparam name="TResult">The type to filter by.</typeparam>
        /// <param name="source">The source collection, can be null.</param>
        /// <param name="predicate">The filter</param>
        /// <param name="result">The first element, can be null.</param>
        /// <returns>True if an element was found.</returns>
        internal static bool TryFirstOfType<T, TResult>(this IEnumerable<T> source, Func<TResult, bool> predicate, out TResult result)
            where TResult : T
        {
            result = default;
            if (source == null)
            {
                return false;
            }

            using var e = source.GetEnumerator();
            while (e.MoveNext())
            {
                if (e.Current is TResult item &&
                    predicate(item))
                {
                    result = item;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Try getting the first element in <paramref name="source"/> matching <paramref name="predicate"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="source"/></typeparam>
        /// <param name="source">The source collection, can be null.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="result">The first element matching the predicate, can be null.</param>
        /// <returns>True if an element was found.</returns>
        internal static bool TryFirst<T>(this IEnumerable<T> source, Func<T, bool> predicate, out T result)
        {
            if (source == null)
            {
                result = default;
                return false;
            }

            using (var e = source.GetEnumerator())
            {
                while (e.MoveNext())
                {
                    if (predicate(e.Current))
                    {
                        result = e.Current;
                        return true;
                    }
                }
            }

            result = default;
            return false;
        }

        /// <summary>
        /// Try getting the first element in <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="source"/></typeparam>
        /// <param name="source">The source collection, can be null.</param>
        /// <param name="result">The first element, can be null.</param>
        /// <returns>True if an element was found.</returns>
        internal static bool TryLast<T>(this IEnumerable<T> source, out T result)
        {
            result = default;
            if (source == null)
            {
                return false;
            }

            using var e = source.GetEnumerator();
            if (!e.MoveNext())
            {
                return false;
            }

            while (e.MoveNext())
            {
                result = e.Current;
            }

            return true;
        }

        /// <summary>
        /// Try getting the first element in <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="source"/></typeparam>
        /// <param name="source">The source collection, can be null.</param>
        /// <param name="predicate">The filter</param>
        /// <param name="result">The first element, can be null.</param>
        /// <returns>True if an element was found.</returns>
        internal static bool TryLast<T>(this IEnumerable<T> source, Func<T, bool> predicate, out T result)
        {
            result = default;
            if (source == null)
            {
                return false;
            }

            using var e = source.GetEnumerator();
            if (!e.MoveNext())
            {
                return false;
            }

            var found = false;
            do
            {
                if (e.Current is T item &&
                    predicate(item))
                {
                    result = item;
                    found = true;
                }
            }
            while (e.MoveNext());
            return found;
        }

        /// <summary>
        /// Try getting the first element in <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="source"/></typeparam>
        /// <typeparam name="TResult">The type to filter by.</typeparam>
        /// <param name="source">The source collection, can be null.</param>
        /// <param name="result">The first element, can be null.</param>
        /// <returns>True if an element was found.</returns>
        internal static bool TryLastOfType<T, TResult>(this IEnumerable<T> source, out TResult result)
            where TResult : T
        {
            result = default;
            if (source == null)
            {
                return false;
            }

            using var e = source.GetEnumerator();
            if (!e.MoveNext())
            {
                return false;
            }

            var found = false;
            do
            {
                if (e.Current is TResult item)
                {
                    result = item;
                    found = true;
                }
            }
            while (e.MoveNext());
            return found;
        }

        /// <summary>
        /// Try getting the first element in <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements in <paramref name="source"/></typeparam>
        /// <typeparam name="TResult">The type to filter by.</typeparam>
        /// <param name="source">The source collection, can be null.</param>
        /// <param name="predicate">The filter</param>
        /// <param name="result">The first element, can be null.</param>
        /// <returns>True if an element was found.</returns>
        internal static bool TryLastOfType<T, TResult>(this IEnumerable<T> source, Func<TResult, bool> predicate, out TResult result)
            where TResult : T
        {
            result = default;
            if (source == null)
            {
                return false;
            }

            using var e = source.GetEnumerator();
            if (!e.MoveNext())
            {
                return false;
            }

            var found = false;
            do
            {
                if (e.Current is TResult item &&
                    predicate(item))
                {
                    result = item;
                    found = true;
                }
            }
            while (e.MoveNext());
            return found;
        }
    }
}