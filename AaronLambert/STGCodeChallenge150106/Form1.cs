using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace STGCodeChallenge150106
{
    public partial class Form1 : Form
    {
        const int JAN010001 = 1721426;
        const int JAN012014 = 2456659;
        const int DAYS_IN_2014 = 365;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            int jDate = Convert.ToInt32(txtInput.Text.Trim());
            DateTime CalendarStartDay = new DateTime(1, 1, 1);
            DateTime StartDate;
            
            StartDate = CalendarStartDay.AddDays(jDate - JAN010001);

            MessageBox.Show(string.Format("The date entered ({0}) is {1} in Perpetual 2014 format.",
                StartDate.ToString("yyyy-MM-dd"), JulianDateToPerpetual2014Date(jDate)));
        }

        private string JulianDateToPerpetual2014Date(int jDate)
        {
            DateTime CalendarStartDay = new DateTime(1, 1, 1);
            DateTime ResultDate;
            int ExtraYears = 0;

            // If the date entered is before 2014, just calculate as normal
            if (jDate <= JAN012014)
            {
                ResultDate = CalendarStartDay.AddDays(jDate - JAN010001);
            }
            else
            {
                int dx = jDate - JAN012014;

                ExtraYears = (dx / DAYS_IN_2014);
                dx = (dx % DAYS_IN_2014);
                ResultDate = CalendarStartDay.AddDays(JAN012014 + dx - JAN010001);
            }
            int Year = ResultDate.Year;
            int Month = ResultDate.Month;
            int Day = ResultDate.Day;

            Month += ExtraYears * 12;

            return string.Format("{0}-{1}-{2}", Year.ToString("D4"), Month.ToString("D2"), Day.ToString("D2"));
        }
    }
}
