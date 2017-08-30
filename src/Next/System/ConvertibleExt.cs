using System.Globalization;
// ReSharper disable once CheckNamespace


namespace System
{
    /// <summary>
    /// IConvertible extension methods
    /// </summary>
    public static class ConvertibleExt
    {
        /// <summary>
        /// Converts value to string using invariant culture
        /// </summary>
        /// <param name="this">Source value</param>
        /// <returns>Culture invariant representation of source value</returns>
        public static string ToStringInvariant(this IConvertible @this)
        {
            return @this.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts nullable value to string using invariant culture
        /// </summary>
        /// <typeparam name="TValue">Type of source value</typeparam>
        /// <param name="this">Source value</param>
        /// <returns>Culture invariant representation of source value</returns>
        public static string ToStringInvariant<TValue>(this TValue? @this)
            where TValue : struct, IConvertible
        {
            return @this?.ToStringInvariant();
        }
    }
}
