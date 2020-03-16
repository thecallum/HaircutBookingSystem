using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HaircutBookingSystem.ViewModels;

namespace HaircutBookingSystem.ViewModels.book
{
    public class SelectDateViewModel
    {
        private readonly int NUMBER_OF_CALENDAR_DAYS = 28;
        private readonly int SUNDAY = 7;

        public CalendarDayViewModel[] Dates { get; set; }

        public SelectDateViewModel()
        {
            Dates = BuildCalendarDayViewModel();
        }


        private CalendarDayViewModel[] BuildCalendarDayViewModel()
        {
            DateTime calendarStartDate = GetCalendarStartDate();
            CalendarDayViewModel[] dates = new CalendarDayViewModel[NUMBER_OF_CALENDAR_DAYS];

            for (int i = 0; i < 28; i++)
            {
                CalendarDayViewModel calendarDay = new CalendarDayViewModel()
                {
                    Date = calendarStartDate.AddDays(i),
                    Index = i
                };

                if ((int)calendarDay.Date.DayOfWeek != SUNDAY)
                    calendarDay.EnableLink = true;

                dates[i] = calendarDay;
            }

            return dates;
        }

        private DateTime GetCalendarStartDate()
        {
            DateTime today = DateTime.Today;

            int todayWeekNum = (int)today.DayOfWeek;

            if (todayWeekNum == SUNDAY)
                return today;

            int numberOfDaysAfterSunday = todayWeekNum;

            return today.AddDays(-numberOfDaysAfterSunday);
        }
    }
}