﻿using System;
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
        [MaxLength(255)]
        [MinLength(1)]
        public string Name { get; set; }
    }
}