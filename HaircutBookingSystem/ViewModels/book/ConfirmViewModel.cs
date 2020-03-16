using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaircutBookingSystem.ViewModels.book
{
    public class ConfirmViewModel
    {
        public DateTime Date { get; set; }
        public string Barber { get; set; }
        public string Slot { get; set; }

        public ConfirmParametersViewModel Parameters { get; set; }
    }

    public class ConfirmParametersViewModel
    {
        public string Date { get; set; }
        public int Barber { get; set; }
        public int Slot { get; set; }

    }
}
