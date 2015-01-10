using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace STGCodeChallenge6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Julian astronomical date as input and return a string with the date corresponding to that Julian date in Perpetual 2014 notation, in either YYYY-MM-DD or DD/MM/YYYY format.
        /// </summary>
        private void btnProcess_Click_1(object sender, RoutedEventArgs e)
        {
            lblPerpetualDate.Content = "";
            lblDate.Content = "";
            lblErrorMessage.Content = "";
            lblPerpetualDateLabel.Visibility = System.Windows.Visibility.Hidden;
            lblNormalDateLabel.Visibility = System.Windows.Visibility.Hidden;
            int day = 0;
            if (isValidJulianAstronomicalDate(txtText.Text, ref day))
            {
                day = day - 1721426;
                DateTime originalDate = new DateTime(1, 1, 1);
                DateTime convertedDate = originalDate.AddDays(day);
                if (convertedDate.Year > 2014)
                {
                    int yearDifference = convertedDate.Year - 2014;
                    int perpetualMonth = yearDifference * 12;
                    //DateTime perpetual2014Date = new DateTime(2014, perpetualMonth, convertedDate.Day);
                    displayDate(convertedDate.Year, convertedDate.Month, convertedDate.Day, false);
                    displayDate(2014, perpetualMonth, convertedDate.Day, true);
                }
                else
                {
                    displayDate(convertedDate.Year, convertedDate.Month, convertedDate.Day, true);
                }
            }
            else
            {
                lblErrorMessage.Content = "Sorry, invalid input. Please enter a Julian astronomical date from \n1721426 (January 1, 1) to 2914695 (January 23, 3268) inclusive.";
            }
        }
                
        /// <summary>
        /// Display a string with the date corresponding to that Julian date in Perpetual 2014 notation, in YYYY-MM-DD format.
        /// </summary>
        /// <param name="Year">Year of date to display</param>
        /// <param name="Month">Month of date to display</param>
        /// <param name="Day">Day of date to display</param>
        /// <param name="isPerpetual">Bool specifying which label to display the date in (perpetual 2014 label: true, normal date: false)</param>
        private void displayDate(int Year, int Month, int Day, bool isPerpetual)
        {
            string year = "" + Year;
            string month = "" + Month;
            string day = "" + Day;
            if (isPerpetual)
            {
                lblPerpetualDateLabel.Visibility = System.Windows.Visibility.Visible;
                lblPerpetualDate.Content = string.Format("{0}-{1}-{2}", year.PadLeft(4, '0'), month.PadLeft(2, '0'), day.PadLeft(2, '0'));
            }
            else
            {
                lblNormalDateLabel.Visibility = System.Windows.Visibility.Visible;
                lblDate.Content = string.Format("{0}-{1}-{2}", year.PadLeft(4, '0'), month.PadLeft(2, '0'), day.PadLeft(2, '0'));
            }
        }

        //You may assume that the Julian day entered will always be an integer from 1721426 (January 1, 1) to 2914695 (January 23, 3268) inclusive.
        private bool isValidJulianAstronomicalDate(string julianDay, ref int day)
        {
            return int.TryParse(julianDay, out day) && day >= 1721426 && day <= 2914695;
        }
    }
}
