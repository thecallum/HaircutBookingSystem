using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HaircutBookingSystem.Models
{
    public class Barber
    {
        public int ID { get; set; }

        [Required]
        [Range(1, 50)]
        public string Name { get; set; }
    }
}