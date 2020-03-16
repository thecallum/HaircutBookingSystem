using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaircutBookingSystem.ViewModels.book
{
    public class CalendarDayViewModel
    {
        public DateTime Date { get; set; }
        public bool EnableLink { get; set; } = false;
        public int Index { get; set; }
    }
}