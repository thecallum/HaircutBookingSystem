using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HaircutBookingSystem.DTOs;
using HaircutBookingSystem.Models;

namespace HaircutBookingSystem.ViewModels.book
{
    public class ConfirmViewModel
    {
        public AppointmentDTO AppointmentDTO { get; set; }
        public Barber Barber { get; set; }

        public string ErrorMessage { get; set; }
    }
}
