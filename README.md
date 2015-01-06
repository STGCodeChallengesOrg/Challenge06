Challenge06
===========


Let's define a new date type, called Perpetual 2014, as follows:

For dates 2014 and before, the dates will be the same as they are normally.
For dates in years 2015 and onward, the year will stay 2014, and the month will be the number it would be if the same month cycle in 2014 were to continue forever past month 12. So 02/08/2015 would be 14/08/2014, and 12/31/2020 would be 85/02/2014' Note that leap days are not accounted for because 2014 is not a leap year.
Your task is to build a program or function that will take a Julian astronomical date as input and return a string with the date corresponding to that Julian date in Perpetual 2014 notation, in either YYYY-MM-DD or DD/MM/YYYY format.

You may assume that the Julian day entered will always be an integer from 1721426 (January 1, 1) to 2914695 (January 23, 3268) inclusive. Years may contain leading zeros to pad to 4 digits or not, but months and days must always have leading zeros to pad to two digits (and years may not contain leading zeros to pad to any number of digits other than 4).

