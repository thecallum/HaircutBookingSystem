using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HaircutBookingSystem.Models
{
    public class Appointment
    {
        public int ID { get; set; }

        [Required]
        public Barber Barber { get; set; }

        public byte BarberID { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Range(0, 8)]
        public int Slot { get; set; }
    }
}