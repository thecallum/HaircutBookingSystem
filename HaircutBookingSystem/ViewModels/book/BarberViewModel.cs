using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HaircutBookingSystem.Models;

namespace HaircutBookingSystem.ViewModels.book
{
    public class BarberViewModel
    {
        public Barber Barber { get; set; }
        public int BarberID { get; set; }
        public List<SlotViewModel> TimeSlots { get; set; }
    }
}