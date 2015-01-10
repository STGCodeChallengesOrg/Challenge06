using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 Week 6 Challenge 
 
Let's define a new date type, called Perpetual 2014, as follows:
 
For dates 2014 and before, the dates will be the same as they are normally.
For dates in years 2015 and onward, the year will stay 2014, and the month will be the number it would be if the same month cycle in 2014 were to 
 continue forever past month 12. So 02/08/2015 (Julian day  2457062) would be 14/08/2014, and 12/31/2020 (Julian day 2459215) would be 85/02/2014' Note that leap days are not accounted 
 for because 2014 is not a leap year.
Your task is to build a program or function that will take a Julian astronomical date as input and return a string with the date corresponding to that 
 Julian date in Perpetual 2014 notation, in either YYYY-MM-DD or DD/MM/YYYY format.
 
You may assume that the Julian day entered will always be an integer from 1721426 (January 1, 1) to 2914695 (January 23, 3268) inclusive. Years may 
 contain leading zeros to pad to 4 digits or not, but months and days must always have leading zeros to pad to two digits (and years may not contain 
 leading zeros to pad to any number of digits other than 4).*/
namespace CodeChallenge06_PerpetualCalendar
{
    class Program
    {
        public static void Main(string[] args)
        {
            string input;
			do
			{
				Console.WriteLine("\n\nEnter the Julian date (from 1721426 for January 1, 0001 to 2914695 for January 23, 3268) to convert to a Perptual 2014 date:");
				input = Console.ReadLine();
				Console.WriteLine(ConvertJulianDateToPerpetual(input, 2014));
			} while (input != "q");
        }

        public static string ConvertJulianDateToPerpetual(string input, int perpetualYear)
		{
            const int minAllowedJulianDate = 1721426;
            const int maxAllowedJulianDate = 2914695;
            int julianDayNumber;

            if (Int32.TryParse(input, out julianDayNumber) && julianDayNumber >= minAllowedJulianDate && julianDayNumber <= maxAllowedJulianDate)
            {
                //var julianCalendarObj = new JulianCalendar();

                // START core conversion algorithm courtesy of https://mikearnett.wordpress.com/2011/09/13/c-convert-julian-date/
                long L = julianDayNumber + 68569;
                long N = (long)((4 * L) / 146097);
                L = L - ((long)((146097 * N + 3) / 4));
                long I = (long)((4000 * (L + 1) / 1461001));
                L = L - (long)((1461 * I) / 4) + 31;
                long J = (long)((80 * L) / 2447);
                int day = (int)(L - (long)((2447 * J) / 80));
                L = (long)(J / 11);
                int month = (int)(J + 2 - 12 * L);
                int year = (int)(100 * (N - 49) + I + L);

                var convertedDate = new DateTime(year, month, day);
                // END core conversion algorithm

                var perpetualYearEndDate = new DateTime(perpetualYear, 12, 31);
                
                // do not account for leap year days if perpetualYear is not a leap year; otherwise, do account for them
                var leapYearsBetween = LeapYearsBetween(perpetualYear, convertedDate.Year);
                leapYearsBetween = DateTime.IsLeapYear(perpetualYear) ? leapYearsBetween * -1 : leapYearsBetween;
                if (convertedDate > perpetualYearEndDate)
                    convertedDate = convertedDate.AddDays(leapYearsBetween);

                var monthsElapsedSincePerpetualYearEnd = ((convertedDate.Year - perpetualYearEndDate.Year) * 12) + convertedDate.Month - perpetualYearEndDate.Month;
                var finalMonth = convertedDate > perpetualYearEndDate ? (perpetualYearEndDate.Month + monthsElapsedSincePerpetualYearEnd) : convertedDate.Month;
                var finalYear = convertedDate > perpetualYearEndDate ? perpetualYear : convertedDate.Year;
                
                //DD/MM/YYYY
                var finalDateFormatWithSlashes = String.Format("{0}/{1}/{2}",  
                                                                convertedDate.Day.ToString("D2"),
                                                                finalMonth.ToString("D2"), 
                                                                finalYear.ToString("D4"));
                //YYYY-MM-DD
                var finalDateFormatWithDashes = String.Format("{0}-{1}-{2}",
                                                                finalYear.ToString("D4"),
                                                                finalMonth.ToString("D2"),
                                                                convertedDate.Day.ToString("D2"));

                return String.Format("\nResult (YYYY-MM-DD): {0}\nResult(DD/MM/YYYY): {1}", finalDateFormatWithDashes, finalDateFormatWithSlashes);
            }
            else
            {
                return String.Format("Invalid Julian date entered.  Must be a valid number between {0} and {1}", minAllowedJulianDate, maxAllowedJulianDate);
            }
		}

        // algorithm below courtesy of: http://stackoverflow.com/questions/4587513/how-to-calculate-number-of-leap-years-between-two-years-in-c-sharp
        // NOTE: A year is a leap year if it can be divided by 4, but can't be divided by 100, except of case when it can be divided by 400
        public static int LeapYearsBetween(int startYear, int endYear)
        {
            var counter = 0;

            for (var year = startYear; year <= endYear; year++)
                counter += DateTime.IsLeapYear(year) ? 1 : 0;

            return counter;
        }

        // for testing purposes only: this function courtesy of https://mikearnett.wordpress.com/2011/09/13/c-convert-julian-date/
        //public static long ToJulian(DateTime dateTime)
        //{
        //    int day = dateTime.Day;
        //    int month = dateTime.Month;
        //    int year = dateTime.Year;

        //    if (month < 3)
        //    {
        //        month = month + 12;
        //        year = year - 1;
        //    }

        //    return day + (153 * month - 457) / 5 + 365 * year + (year / 4) - (year / 100) + (year / 400) + 1721119;
        //} 
    }
}
