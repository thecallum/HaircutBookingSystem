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
    public class BarbersController : Controller
    {
        private ApplicationDbContext _context;

        public BarbersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }


        [HttpGet]
        public ActionResult Index()
        {
            var barbers = _context.Barbers.ToList();

            var viewModel = new BarbersListViewModel()
            {
                Barbers = barbers
            };

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var barber = _context.Barbers.SingleOrDefault(x => x.ID == id);

            var barberDTO = new BarberDTO()
            {
                ID = barber.ID,
                Name = barber.Name
            };

            if (barber == null)
                return HttpNotFound();

            var viewModel = new BarberViewModel()
            {
                Barber = barberDTO
            };

            return View("BarberForm", viewModel);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var barberDTO = new BarberDTO()
            {
                ID = 0
            };

            var viewModel = new BarberViewModel()
            {
                Barber = barberDTO
            };

            return View("BarberForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(BarberDTO barberDTO)
        {
            var viewModel = new BarberViewModel()
            {
                Barber = barberDTO
            };

            if (!ModelState.IsValid)
                return View("BarberForm", viewModel);

            var barber = new Barber()
            {
                ID = barberDTO.ID,
                Name = barberDTO.Name
            };

            if (barber.ID == 0)
                AddNewBarber(barber);
            else
            {
                var existingBarber = _context.Barbers.SingleOrDefault(x => x.ID == barber.ID);
                if (existingBarber == null)
                    return HttpNotFound();

                UpdateBarber(existingBarber);
            }


            _context.SaveChanges();

            return RedirectToAction("Barbers", "Admin");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var barber = _context.Barbers.SingleOrDefault(x => x.ID == id);
            if (barber == null)
                return HttpNotFound();

            _context.Barbers.Remove(barber);
            _context.SaveChanges();

            return RedirectToAction("Index", "Barbers");
        }

        private void AddNewBarber(Barber barber)
        {
            _context.Barbers.Add(barber);
        }

        private void UpdateBarber(Barber barber)
        {
            barber.Name = barber.Name;
        }
    }
}