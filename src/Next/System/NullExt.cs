// ReSharper disable once CheckNamespace
namespace System
{
    public static class NullExt
    {
        /// <summary>
        /// Checks if instance is not null and invokes func
        /// </summary>
        /// <typeparam name="T">Type of instance</typeparam>
        /// <typeparam name="TResult">Result</typeparam>
        /// <param name="self">Instance</param>
        /// <param name="func">Func</param>
        /// <returns>Result</returns>
        public static TResult IfNotNull<T, TResult>(this T self, Func<T, TResult> func)
            where T : class
            where TResult : class
        {
            return self != null ? func(self) : null;
        }

        /// <summary>
        /// Checks if instance is not null and invokes func
        /// </summary>
        /// <typeparam name="T">Type of instance</typeparam>
        /// <typeparam name="TResult">Result</typeparam>
        /// <param name="self">Instance</param>
        /// <param name="func">Func</param>
        /// <returns>Result</returns>
        public static TResult? IfNotNull<T, TResult>(this T self, Func<T, TResult?> func)
            where T : class
            where TResult : struct
        {
            return self != null ? func(self) : null;
        }

        /// <summary>
        /// Checks if instance is not null and invokes func
        /// </summary>
        /// <typeparam name="T">Type of instance</typeparam>
        /// <typeparam name="TResult">Result</typeparam>
        /// <param name="self">Instance</param>
        /// <param name="func">Func</param>
        /// <returns>Result</returns>
        public static TResult IfHasValue<T, TResult>(this T? self, Func<T, TResult> func)
            where T : struct
            where TResult : class
        {
            return self != null ? func(self.Value) : null;
        }

        /// <summary>
        /// Checks if instance is not null and invokes func
        /// </summary>
        /// <typeparam name="T">Type of instance</typeparam>
        /// <typeparam name="TResult">Result</typeparam>
        /// <param name="self">Instance</param>
        /// <param name="func">Func</param>
        /// <returns>Result</returns>
        public static TResult? IfHasValue<T, TResult>(this T? self, Func<T, TResult?> func)
            where T : struct
            where TResult : struct
        {
            return self != null ? func(self.Value) : null;
        }
    }
}
