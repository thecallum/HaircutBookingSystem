using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HaircutBookingSystem.ViewModels;

namespace HaircutBookingSystem.ViewModels.book
{
    public class SelectDateViewModel
    {
        public static readonly int NUMBER_OF_CALENDAR_DAYS = 28;

        private int NumberOfAppointmentsPerDay;
        private Dictionary<DateTime, int> AppointmentsGroupedByCount;

        public CalendarDayViewModel[] Dates { get; set; }

        public SelectDateViewModel(Dictionary<DateTime, int> appointmentsGroupedByCount, DateTime startDate, int numberOfAppointmentsPerDay)
        {
            NumberOfAppointmentsPerDay = numberOfAppointmentsPerDay;
            AppointmentsGroupedByCount = appointmentsGroupedByCount;

            Dates = new CalendarDayViewModel[NUMBER_OF_CALENDAR_DAYS];

            for (int i = 0; i < NUMBER_OF_CALENDAR_DAYS; i++)
            {
                DateTime date = startDate.AddDays(i);
                Dates[i] = BuildCalendarDayViewModel(date);
            }
        }

        private CalendarDayViewModel BuildCalendarDayViewModel(DateTime date)
        {
            CalendarDayViewModel calendarDay = new CalendarDayViewModel()
            {
                Date = date,
                Availability = GetAvailableAppointmentsOutOfTen(date),
                EnableLink = false
            };

            if (date.DayOfWeek != DayOfWeek.Sunday)
                calendarDay.EnableLink = true;

            return calendarDay;
        }

        private int GetAvailableAppointmentsOutOfTen(DateTime date)
        {
            int defaultAvailability = 10;

            if (!AppointmentsGroupedByCount.ContainsKey(date))
                return defaultAvailability;

            double availabilityFraction = (double)AppointmentsGroupedByCount[date] / (double)NumberOfAppointmentsPerDay;
            int availabilityOutOfTen = defaultAvailability - (int)Math.Floor(availabilityFraction * 10);

            return availabilityOutOfTen;
        }

        public static DateTime GetCalendarStartDate()
        {
            DateTime today = DateTime.Today;

            if (today.DayOfWeek == DayOfWeek.Sunday)
                return today;

            int todayWeekNum = (int)today.DayOfWeek;
            int numberOfDaysAfterSunday = todayWeekNum;

            return today.AddDays(-numberOfDaysAfterSunday);
        }

        public static DateTime GetCalendarEndDate()
        {
            return GetCalendarStartDate().AddDays(NUMBER_OF_CALENDAR_DAYS - 1);
        }
    }
}