using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaircutBookingSystem.ViewModels.book
{
    public class SelectSlotViewModel
    {
        public List<BarberViewModel> Barbers { get; set; }
        public string Date { get; set; }
    }
}