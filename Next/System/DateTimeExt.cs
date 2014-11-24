using System.Collections.Generic;
// ReSharper disable once CheckNamespace


namespace System
{
    /// <summary>
    /// DateTime extension methods
    /// </summary>
    public static class DateTimeExt
    {
        /// <summary>
        /// Gets end of month for specified date
        /// </summary>
        /// <param name="this">Specified date</param>
        /// <returns>Date which is end of month for specified date</returns>
        public static DateTime EndOfMonth(this DateTime @this)
        {
            return new DateTime(@this.Year,
                @this.Month,
                DateTime.DaysInMonth(@this.Year, @this.Month));
        }

        /// <summary>
        /// Checks if specified date is end of month
        /// </summary>
        /// <param name="this">Specified date</param>
        /// <returns>True if specified date is end of month, otherwise false</returns>
        public static bool IsEndOfMonth(this DateTime @this)
        {
            return @this.Day == DateTime.DaysInMonth(@this.Year, @this.Month);
        }

        /// <summary>
        /// Generates infinite sequence of folowing months from specified date
        /// </summary>
        /// <param name="this">Specified date</param>
        /// <param name="step">Step length</param>
        /// <returns>Infinite sequence of following months from specified date</returns>
        public static IEnumerable<DateTime> FollowingMonths(this DateTime @this,
            int step = 1)
        {
            var i = 0;

            while (true)
            {
                yield return @this.AddMonths(++i * step);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        /// <summary>
        /// Generate infinite sequence of following end of months for specified date
        /// </summary>
        /// <param name="this">Specified date</param>
        /// <param name="step">Step length</param>
        /// <returns>Infinite sequence of following end of months for specified date</returns>
        public static IEnumerable<DateTime> FollowingEndOfMonths(this DateTime @this,
            int step = 1)
        {
            var i = 0;

            if (step == 1 && !@this.IsEndOfMonth())
            {
                yield return @this.EndOfMonth();
            }

            while (true)
            {
                yield return @this.AddMonths(++i * step).EndOfMonth();
            }
            // ReSharper disable once FunctionNeverReturns
        }
    }
}
