using System.Globalization;
// ReSharper disable once CheckNamespace


namespace System.Text
{
    /// <summary>
    /// StringBuilder extension methods
    /// </summary>
    public static class StringBuilderExt
    {
        /// <summary>
        /// Appends formated text with invariant culture
        /// </summary>
        /// <param name="this">Source string builder</param>
        /// <param name="format">Format</param>
        /// <param name="args">Arguments</param>
        /// <returns>Instance of source string builder</returns>
        public static StringBuilder AppendFormatInvariant(this StringBuilder @this,
            string format,
            params object[] args)
        {
            return @this.AppendFormat(CultureInfo.InvariantCulture, format, args);
        }

        /// <summary>
        /// Appends text if condition is true
        /// </summary>
        /// <param name="this">Source string builder</param>
        /// <param name="value">Text</param>
        /// <param name="condition">Condition to check</param>
        /// <returns>Instance of source string builder</returns>
        public static StringBuilder AppendIf(this StringBuilder @this,
            string value,
            bool condition)
        {
            return condition ? @this.Append(value) : @this;
        }
    }
}
