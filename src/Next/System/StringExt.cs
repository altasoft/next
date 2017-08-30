// ReSharper disable once CheckNamespace
namespace System
{
    /// <summary>
    /// String extension methods
    /// </summary>
    public static class StringExt
    {
        /// <summary>        
        /// Splits source string by specified separator and removes empty entries.
        /// </summary>
        /// <param name="this">Source string</param>
        /// <param name="separator">Separator</param>
        /// <returns>Array of strings</returns>
        public static string[] SplitAndRemoveEmptyEntries(this string @this, params char[] separator)
        {
            return @this.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Splits source string by specified separator and removes empty entries.
        /// </summary>
        /// <param name="this">Source string</param>
        /// <param name="separator">Separator</param>
        /// <returns>Array of strings</returns>
        public static string[] SplitAndRemoveEmptyEntries(this string @this, params string[] separator)
        {
            return @this.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}