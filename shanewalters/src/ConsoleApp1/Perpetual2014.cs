using System;

namespace ConsoleApp1
{
    /// <summary>
    /// A new data type, defined by a concoction from an infernal pit of code challenges.
    /// </summary>
    public class Perpetual2014
    {
        public const int START = 1721426;
        public const int END = 2914695;
        public static readonly DateTime StartDate = new DateTime(1, 1, 1);
        public static readonly DateTime EndDate = new DateTime(3268, 1, 23);
        public static readonly DateTime Cutoff = new DateTime(2014, 1, 1);
        private int value;
        private const string FORMAT_YYYYMMDD = "{2:0000}-{0:00}-{1:00} (YYYY-MM-DD)";
        private const string FORMAT_DDMMYYYY = "{1:00}/{0:00}/{2:0000} (DD/MM/YYYY)";

        public Perpetual2014(DateTime dateTime)
            : this(Convert(dateTime)) // Convert to JDN
        {
        }

        public Perpetual2014(int julianDate)
        {
            // Validate JDN range
            if (julianDate < START || julianDate > END)
            {
                throw new ArgumentOutOfRangeException("julianDate");
            }

            this.value = julianDate;
        }

        /// <summary>
        /// Gets the printed format of the Perpetual2014 data type with a default format in YYYY-MM-DD.
        /// </summary>
        /// <returns>The printed format of the Perpetual2014 data type with a default format in YYYY-MM-DD.</returns>
        public string GetValue()
        {
            return GetValue(DateFormat.YYYYMMDD);
        }

        /// <summary>
        /// Gets the printed format of the Perpetual2014 data type with the specified format.
        /// </summary>
        /// <param name="format">The format to use when producing the output string.</param>
        /// <returns>The printed format of the Perpetual2014 data type with the specified format.</returns>
        public string GetValue(DateFormat format)
        {
            string result = null;
            DateTime date = Convert(this.value);

            if (date.Year <= Cutoff.Year)
            {
                result = GetDateString(date, format);
            }
            else
            {
                // Determine the number of JDs since 1/1/2014
                var julianDaysSinceCutoff = Convert(date) - Convert(Cutoff);

                // How many years worth of months to add later
                var yearsToAdd = (julianDaysSinceCutoff / 365);

                // How many days we'll add since the cutoff
                var daysToAdd = (julianDaysSinceCutoff % 365);

                // Produce the new date (just adding number of days since cutoff)
                var adjusted = Cutoff.AddDays(daysToAdd);

                // Determine the month number
                var month = adjusted.Month + (yearsToAdd * 12);

                // Get the output result string
                result = GetDateString(month, adjusted.Day, Cutoff.Year, format);
            }

            return result;
        }

        /// <summary>
        /// Gets the output string using the specified DateTime object and specified format.
        /// </summary>
        /// <param name="value">The <see cref="DateTime"/> object to use.</param>
        /// <param name="format">The desired output format string.</param>
        /// <returns>The output string using the specified DateTime object and specified format.</returns>
        private string GetDateString(DateTime value, DateFormat format)
        {
            return GetDateString(value.Month, value.Day, value.Year, format);
        }

        /// <summary>
        /// Gets the output string using the specified month, day, year, and format.
        /// </summary>
        /// <param name="month">The month to use in the output.</param>
        /// <param name="day">The day to use in the output.</param>
        /// <param name="year">The year to use in the output.</param>
        /// <param name="format">The format to use when the output string is generated.</param>
        /// <returns></returns>
        private string GetDateString(int month, int day, int year, DateFormat format)
        {
            string result;

            if (format == DateFormat.DDMMYYYY)
            {
                result = string.Format(FORMAT_DDMMYYYY, month, day, year);
            }
            else // Default to YYYYMMDD
            {
                result = string.Format(FORMAT_YYYYMMDD, month, day, year);
            }

            return result;
        }

        /// <summary>
        /// Converts a Julian day number into a valid DateTime object.
        /// </summary>
        /// <param name="julianDays">The Julian day number to use to create the resulting DateTime object.</param>
        /// <returns>The resulting DateTime object after converting the Julian day number.</returns>
        private static DateTime Convert(int julianDays)
        {
            var jdEnd = (START + (EndDate - StartDate).TotalDays);

            return StartDate.AddDays(julianDays - START);
        }

        /// <summary>
        /// Converts a DateTime object into a Julian day number.
        /// </summary>
        /// <param name="date">The DateTime object to convert.</param>
        /// <returns>The resulting Julian day number after converting the DateTime object.</returns>
        private static int Convert(DateTime date)
        {
            return ((int)((date - StartDate).TotalDays + START));
        }
    }
}