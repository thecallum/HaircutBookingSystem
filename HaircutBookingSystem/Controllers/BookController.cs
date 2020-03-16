using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HaircutBookingSystem.Models;
using HaircutBookingSystem.ViewModels;
using HaircutBookingSystem.ViewModels.book;

namespace HaircutBookingSystem.Controllers
{

    public class BookController : Controller
    {
        private ApplicationDbContext _context;

        private string[] TimeSlots;

        public BookController()
        {
            _context = new ApplicationDbContext();

            TimeSlots = new String[9]
            {
                "09:00",
                "10:00",
                "11:00",
                "12:00",
                "13:00",
                "14:00",
                "15:00",
                "16:00",
                "17:00"
            };
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }

        // GET: Book
        public ActionResult Index(int? slot, int? barber, string date = null)
        {
            if (date != null && slot != null && barber != null)
            {
                var barberInstance = _context.Barbers.Single(x => x.ID == (int)barber);

                return View("SelectAppointmentConfirm", BuildConfirmViewModel(date, (int)slot, barberInstance));
            }

            if (date != null)
                return View("SelectAppointmentTime", BuildSelectSlotViewModel(date));

            return View("SelectAppointmentDate", new SelectDateViewModel());
        }


        public ActionResult Confirm(int slot, int barber, string date)
        {



            return View();
        }


        private ConfirmViewModel BuildConfirmViewModel(string date, int slot, Barber barber)
        {
            var parameters = new ConfirmParametersViewModel()
            {
                Date = date,
                Slot = slot,
                Barber = barber.ID
            };


            return new ConfirmViewModel()
            {
                Date = DateTime.ParseExact(date, "ddMMyy", System.Globalization.CultureInfo.InvariantCulture),
                Slot = TimeSlots[slot],
                Barber = barber.Name,
                Parameters = parameters
            };
        }

        private SelectSlotViewModel BuildSelectSlotViewModel(string Date)
        {
            var viewModel = new SelectSlotViewModel()
            {
                Date = Date,
                Barbers = LoadBarbers()
            };

            return viewModel;
        }

        private List<BarberViewModel> LoadBarbers()
        {
            var Barbers = new List<BarberViewModel>();
            var timeSlots = TimeSlots;

            var barbers = _context.Barbers.ToList();

            foreach (var barber in barbers)
            {
                var newBarber = new BarberViewModel()
                {
                    Barber = barber,
                    TimeSlots = new List<SlotViewModel>() {
                    new SlotViewModel() { Time = timeSlots[0], Available = true },
                    new SlotViewModel() { Time = timeSlots[1], Available = true },
                    new SlotViewModel() { Time = timeSlots[2], Available = false },
                    new SlotViewModel() { Time = timeSlots[3], Available = false },
                    new SlotViewModel() { Time = timeSlots[4], Available = true },
                    new SlotViewModel() { Time = timeSlots[5], Available = true },
                    new SlotViewModel() { Time = timeSlots[6], Available = false },
                    new SlotViewModel() { Time = timeSlots[7], Available = true },
                    new SlotViewModel() { Time = timeSlots[8], Available = true },
                }
                };
                Barbers.Add(newBarber);

            }

            return Barbers;
        }
    }
}