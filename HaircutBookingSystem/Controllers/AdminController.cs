using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HaircutBookingSystem.Models;
using HaircutBookingSystem.ViewModels;
using HaircutBookingSystem.ViewModels.admin;
using HaircutBookingSystem.DTOs;
using System.Data.Entity;

namespace HaircutBookingSystem.Controllers
{
    [Authorize(Roles = RoleName.Admin)]
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}