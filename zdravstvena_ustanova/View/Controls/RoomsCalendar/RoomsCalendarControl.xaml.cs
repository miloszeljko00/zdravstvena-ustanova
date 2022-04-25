using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using zdravstvena_ustanova.View.Pages.ManagerPages;

namespace zdravstvena_ustanova.View.Controls.RoomsCalendar
{
    public partial class RoomsCalendarControl : UserControl
    {
        public DateTime DisplayedMonth { get; set; }
        public DateTime FirstDayOfMonth { get; set; }
        public DateTime LastDayOfMonth { get; set; }
        public long RoomId { get; set; }
        public RoomCalendarOverview RoomCalendarOverview { get; set; }

        public RoomsCalendarControl(long roomId, RoomCalendarOverview roomCalendarOverview)
        {
            InitializeComponent();

            RoomId = roomId;
            RoomCalendarOverview = roomCalendarOverview;

            UpdateDisplayedMonth();

            DisplayCalendarForMonth(DisplayedMonth);
        }

        private void UpdateDisplayedMonth()
        {
            DisplayedMonth = DateTime.Now;
            FirstDayOfMonth = new DateTime(DisplayedMonth.Year, DisplayedMonth.Month, 1, 8, 0, 0);
            LastDayOfMonth = new DateTime(DisplayedMonth.Year, DisplayedMonth.Month,
                DateTime.DaysInMonth(DisplayedMonth.Year, DisplayedMonth.Month), 21, 0, 0);
        }
        private void DisplayCalendarForMonth(DateTime date)
        {
            daysContainer.Children.Clear();

            MonthLabel.Content = date.ToString("MMMM");
            YearLabel.Content = date.ToString("yyyy.");

            int dayCounter = 0;
            dayCounter += GenerateDaysFromPreviousMonth(date);
            dayCounter += GenerateDaysFromCurrentMonth(date);
            dayCounter += GenerateDaysFromNextMonth(date, dayCounter);

            if (dayCounter != 42) Console.WriteLine("ERROR GENERATING DAYS IN CALENDAR");
        }

        private int GetDayOfWeekWithMondayAsFirstDay(DateTime date)
        {
            int dayOfTheWeek = (int)date.DayOfWeek;

            if (dayOfTheWeek == 0)
            {
                dayOfTheWeek = 7;
            }
            else
            {
                dayOfTheWeek++;
            }
            return dayOfTheWeek;
        }

        private DateTime GetPreviousMonth(DateTime date)
        {
            if (date.Month > 1) return new DateTime(date.Year, date.Month - 1, date.Day);
            return new DateTime(date.Year - 1, 12, date.Day);
        }
        private DateTime GetNextMonth(DateTime date)
        {
            if (date.Month < 12) return new DateTime(date.Year, date.Month + 1, date.Day);
            return new DateTime(date.Year + 1, 1, date.Day);
        }

        private int GenerateDaysFromPreviousMonth(DateTime date)
        {
            DateTime previousMonth = GetPreviousMonth(date);
            int daysInPreviousMonth = DateTime.DaysInMonth(previousMonth.Year, previousMonth.Month);

            DateTime startDateOfMonth = new DateTime(date.Year, date.Month, 1);
            int dayOfTheWeekForFirstDayInMonth = GetDayOfWeekWithMondayAsFirstDay(startDateOfMonth);

            int daysGenerated = 0;
            for (var i = 0; i < dayOfTheWeekForFirstDayInMonth; i++)
            {
                DayFieldCalendar dayFieldCalendar = new DayFieldCalendar(new DateTime(previousMonth.Year, 
                                                                                      previousMonth.Month,
                                                                                      daysInPreviousMonth - dayOfTheWeekForFirstDayInMonth + i + 1),
                                                                                      false,
                                                                                      RoomId,
                                                                                      RoomCalendarOverview);
                daysContainer.Children.Add(dayFieldCalendar);
                daysGenerated++;
            }

            return daysGenerated;
        }
        private int GenerateDaysFromCurrentMonth(DateTime date)
        {
            int daysInCurrentMonth = DateTime.DaysInMonth(date.Year, date.Month);

            int daysGenerated = 0;
            for (var i = 0; i < daysInCurrentMonth; i++)
            {
                DayFieldCalendar dayFieldCalendar = new DayFieldCalendar(new DateTime(date.Year,
                                                                                      date.Month,
                                                                                      i + 1),
                                                                                      true,
                                                                                      RoomId,
                                                                                      RoomCalendarOverview);
                daysContainer.Children.Add(dayFieldCalendar);
                daysGenerated++;
            }

            return daysGenerated;
        }
        private int GenerateDaysFromNextMonth(DateTime date, int dayCounter)
        {
            DateTime nextMonth = GetNextMonth(date);
            int daysInNextMonth = DateTime.DaysInMonth(nextMonth.Year, nextMonth.Month);

            int daysGenerated = 0;
            for (var i = 0; i < daysInNextMonth; i++)
            {
                if (dayCounter >= 42) break;
                DayFieldCalendar dayFieldCalendar = new DayFieldCalendar(new DateTime(nextMonth.Year,
                                                                                      nextMonth.Month,
                                                                                      i + 1),
                                                                                      false,
                                                                                      RoomId,
                                                                                      RoomCalendarOverview);
                daysContainer.Children.Add(dayFieldCalendar);
                daysGenerated++;
                dayCounter++;
            }

            return daysGenerated;
        }

        private void PreviousMonthArrowIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(DisplayedMonth.Month > 1)
            {
                DisplayedMonth = new DateTime(DisplayedMonth.Year, DisplayedMonth.Month - 1, DisplayedMonth.Day);
            }
            else
            {
                DisplayedMonth = new DateTime(DisplayedMonth.Year - 1, 12, DisplayedMonth.Day);
            }
            DisplayCalendarForMonth(DisplayedMonth);
        }
        private void NextMonthArrowIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DisplayedMonth.Month < 12)
            {
                DisplayedMonth = new DateTime(DisplayedMonth.Year, DisplayedMonth.Month + 1, DisplayedMonth.Day);
            }
            else
            {
                DisplayedMonth = new DateTime(DisplayedMonth.Year + 1, 1, DisplayedMonth.Day);
            }
            DisplayCalendarForMonth(DisplayedMonth);
        }
    }
}
