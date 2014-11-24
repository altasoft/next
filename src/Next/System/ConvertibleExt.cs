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
    }
}
