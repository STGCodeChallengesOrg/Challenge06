using System;
using System.Threading;

namespace ConsoleApp1
{
    public class Program
    {
        private delegate bool tryParse<T>(string input, out T result);

        public void Main()
        {
            // Run the main process loop
            while (RunLoop())
            {
                // Just to prevent the "key-down" CPU eater
                Thread.Sleep(1);
            }
        }

        private bool RunLoop()
        {
            // Attempt to gather user input and convert it
            Convert(GetUserInput());

            // Ask the user if they want to keep going
            return GetUserRunAgainPreference();
        }

        private string GetUserInput()
        {
            Console.Write("Please enter a Julian date then press enter: ");
            return Console.ReadLine();
        }

        private bool GetUserRunAgainPreference()
        {
            var keepRunning = false;
            var userInputIsInvalid = true;

            do
            {
                Console.Write("Try another value (Y/n)? ");
                var yn = Console.ReadKey();
                Console.WriteLine();

                // Parse the Yes/No response
                switch (yn.Key)
                {
                    case ConsoleKey.Y:
                    case ConsoleKey.Enter:
                        keepRunning = true;
                        userInputIsInvalid = false;
                        break;
                    case ConsoleKey.N:
                        userInputIsInvalid = false;
                        break;
                    default:
                        break;
                }
            } while (userInputIsInvalid);

            return keepRunning;
        }

        private void Convert(string value)
        {
            // Try parsing the input as a Julian day number, try as a date/time if Julian day number parsing fails
            var converted = ConvertFromInt(value) ?? ConvertFromDateTime(value);

            if (!string.IsNullOrEmpty(converted))
            {
                Console.WriteLine("Converted value: " + converted);
            }
            else
            {
                Console.WriteLine("Unable to parse specified date: " + value);
            }
        }

        private string ConvertFromInt(string value)
        {
            return ConvertFromType<int>(value, int.TryParse, x => new Perpetual2014(x), string.Format("The Julian date specified is out of range. Only consider values >= {0} and <= {1}.", Perpetual2014.START, Perpetual2014.END));
        }

        private string ConvertFromDateTime(string value)
        {
            return ConvertFromType<DateTime>(value, DateTime.TryParse, x => new Perpetual2014(x), string.Format("The Julian date specified is out of range. Only consider values >= {0} and <= {1}.", Perpetual2014.StartDate, Perpetual2014.EndDate));
        }

        private string ConvertFromType<T>(string value, tryParse<T> tryParse, Func<T, Perpetual2014> constructor, string errorMessage)
        {
            T date;
            string result = null;

            // Attempt to parse the input
            if (tryParse(value, out date))
            {
                try
                {
                    // Try constructing Perpetual2014 data type
                    Perpetual2014 parsed = constructor(date);
                    result = parsed.GetValue();
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine(errorMessage);
                }
            }

            return result;
        }
    }
}
