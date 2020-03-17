using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaircutBookingSystem.ViewModels.book
{
    public class SlotViewModel
    {
        public string Time { get; set; }
        public bool Available { get; set; } = true;
    }
}