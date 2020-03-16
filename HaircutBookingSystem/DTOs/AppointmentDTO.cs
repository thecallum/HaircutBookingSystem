using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using HaircutBookingSystem.Models;

namespace HaircutBookingSystem.DTOs
{
    public class AppointmentDTO
    {
        public byte BarberID { get; set; }

        public DateTime Date { get; set; }

        [Range(0, 8)]
        public int Slot { get; set; }
    }
}