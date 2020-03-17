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

            return View("SelectAppointmentDate", BuildSelectDateViewModel());
        }

        private SelectDateViewModel BuildSelectDateViewModel()
        {
            DateTime startDate = SelectDateViewModel.GetCalendarStartDate();
            DateTime endDate = SelectDateViewModel.GetCalendarEndDate();

            int numberOfBarbers = _context.Barbers.Count();
            int numberOfAppointmentsPerDay = numberOfBarbers * TimeSlots.Count();

            var appointmentsGroupedByCount = _context.Appointments
                .Where(appointment => appointment.Date >= startDate)
                .Where(appointment => appointment.Date <= endDate)
                .GroupBy(appointment => appointment.Date)
                .ToDictionary(appointment => appointment.Key, appointment => appointment.Count());

            return new SelectDateViewModel(appointmentsGroupedByCount, startDate, numberOfAppointmentsPerDay);
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

        private SelectSlotViewModel BuildSelectSlotViewModel(string date)
        {
            var viewModel = new SelectSlotViewModel()
            {
                Date = date,
                Barbers = LoadBarbers(date)
            };

            return viewModel;
        }

        private List<BarberViewModel> LoadBarbers(string date)
        {
            var Barbers = new List<BarberViewModel>();
            DateTime selectedDate = DateTime.ParseExact(date, "ddMMyy", System.Globalization.CultureInfo.InvariantCulture);

            var barbers = _context.Barbers.ToList();

            foreach (var barber in barbers)
            {
                var appointments = _context.Appointments
                    .Where(appointment => appointment.Barber.ID == barber.ID)
                    .Where(appointment => appointment.Date == selectedDate)
                   .GroupBy(appointment => appointment.Slot)
                   .ToDictionary(appointment => appointment.Key);

                var newBarber = new BarberViewModel()
                {
                    Barber = barber,
                    TimeSlots = new List<SlotViewModel>()
                };

                for (int i = 0; i < TimeSlots.Count(); i++)
                {
                    newBarber.TimeSlots.Add(
                        new SlotViewModel() { Time = TimeSlots[i], Available = !appointments.ContainsKey(i) }
                    );
                }

                Barbers.Add(newBarber);
            }

            return Barbers;
        }
    }
}