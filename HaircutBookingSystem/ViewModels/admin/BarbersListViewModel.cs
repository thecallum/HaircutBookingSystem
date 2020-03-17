using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HaircutBookingSystem.Models;
using HaircutBookingSystem.ViewModels;
using HaircutBookingSystem.ViewModels.admin;
using HaircutBookingSystem.DTOs;

namespace HaircutBookingSystem.ViewModels.admin
{
    public class BarbersListViewModel
    {
        public List<Barber> Barbers { get; set; }
    }
}