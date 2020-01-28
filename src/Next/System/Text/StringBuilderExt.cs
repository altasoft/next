using System.Collections.Generic;
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

        /// <summary>
        /// Appends text computed by <param name="getValue"/> if condition is true
        /// </summary>
        /// <param name="this">Source string builder</param>
        /// <param name="getValue">Function which returns text to add</param>
        /// <param name="condition">Condition to check</param>
        /// <returns>Instance of source string builder</returns>
        public static StringBuilder AppendIf(this StringBuilder @this,
            Func<string> getValue,
            bool condition)
        {
            return condition ? @this.Append(getValue()) : @this;
        }

        /// <summary>
        /// Appends string values separated with specified separator
        /// </summary>
        /// <param name="this">Source string builder</param>
        /// <param name="values">String values</param>
        /// <param name="separator">Separator</param>
        /// <returns>Instance of source string builder</returns>
        public static StringBuilder AppendSeparated(this StringBuilder @this, IEnumerable<string> values,
            string separator)
        {
            using var iterator = values.GetEnumerator();

            if (iterator.MoveNext())
            {
                @this.Append(iterator.Current);

                while (iterator.MoveNext())
                {
                    @this.Append(separator).Append(iterator.Current);
                }
            }

            return @this;
        }

        public static StringBuilder AppendLines(this StringBuilder @this, IEnumerable<string> values) =>
            @this.AppendSeparated(values, Environment.NewLine);
    }
}