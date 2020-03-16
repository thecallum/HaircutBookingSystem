using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HaircutBookingSystem.Models;
using HaircutBookingSystem.ViewModels;
using HaircutBookingSystem.ViewModels.book;
using HaircutBookingSystem.DTOs;

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
        [HttpGet]
        public ActionResult Index(int? slot, int? barber, string date = null)
        {
            if (date != null && slot != null && barber != null)
            {
                var appointmentDTO = new AppointmentDTO
                {
                    BarberID = (byte)barber,
                    Date = DateTime.ParseExact(date, "ddMMyy", System.Globalization.CultureInfo.InvariantCulture),
                    Slot = (int)slot,
                };

                return View("SelectAppointmentConfirm", BuildConfirmViewModel(appointmentDTO));
            }

            if (date != null)
                return View("SelectAppointmentTime", BuildSelectSlotViewModel(date));

            return View("SelectAppointmentDate", new SelectDateViewModel());
        }

        [HttpPost]
        public ActionResult Confirm(AppointmentDTO appointmentDTO)
        {
            if (!ModelState.IsValid)
                return View("SelectAppointmentConfirm", BuildConfirmViewModel(appointmentDTO));

            var barber = _context.Barbers.SingleOrDefault(x => x.ID == appointmentDTO.BarberID);

            if (barber == null)
                return View("SelectAppointmentConfirm", BuildConfirmViewModel(appointmentDTO, "Barber Not Found"));

            var appointment = new Appointment()
            {
                Barber = barber,
                Date = appointmentDTO.Date,
                Slot = appointmentDTO.Slot,
            };


            _context.Appointments.Add(appointment);
            _context.SaveChanges();

            return View();
        }

        private ConfirmViewModel BuildConfirmViewModel(AppointmentDTO appointmentDTO, string errorMessage = null)
        {
            return new ConfirmViewModel()
            {
                AppointmentDTO = appointmentDTO,
                Barber = _context.Barbers.SingleOrDefault(x => x.ID == appointmentDTO.BarberID),
                ErrorMessage = errorMessage
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