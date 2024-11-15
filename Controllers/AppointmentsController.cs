using ApointementSystem.Models.ApointmentModel;
using ApointementSystem.Repository.Appoinment;
using ApointementSystem.Repository.OfficerRepo;
using ApointementSystem.Repository.VisitorRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static ApointementSystem.Models.ApointmentModel.Appointment;

namespace ApointementSystem.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IOfficerRepository _officerRepository;
        private readonly IVisitorRepository _visitorRepository;

        public AppointmentsController(IAppointmentRepository appointmentRepository, IOfficerRepository officerRepository, IVisitorRepository visitorRepository)
        {
            _appointmentRepository = appointmentRepository;
            _officerRepository = officerRepository;
            _visitorRepository = visitorRepository;
        }

        public async Task<IActionResult> Index(
            string generalSearch = null,
        DateTime? fromDate = null,
        DateTime? toDate = null,
        TimeSpan? fromTime = null,
        TimeSpan? toTime = null
            )
        {
            var appointments = await _appointmentRepository.GetAllAppointmentsAsync();
            //apply filters
            if (!string.IsNullOrEmpty(generalSearch))
            {
                appointments = appointments.Where(a =>
                    a.Name.Contains(generalSearch, StringComparison.OrdinalIgnoreCase) ||
                    a.Status.ToString().Contains(generalSearch, StringComparison.OrdinalIgnoreCase) ||
                    a.Officer.Name.Contains(generalSearch, StringComparison.OrdinalIgnoreCase) ||
                    a.Visitor.Name.Contains(generalSearch, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }

            // Date range filter
            if (fromDate.HasValue)
            {
                appointments = appointments.Where(a => a.Date >= fromDate.Value).ToList();
            }
            if (toDate.HasValue)
            {
                appointments = appointments.Where(a => a.Date <= toDate.Value).ToList();
            }

            // Time range filter
            if (fromTime.HasValue)
            {
                appointments = appointments.Where(a => a.StartTime.TimeOfDay >= fromTime.Value).ToList();
            }
            if (toTime.HasValue)
            {
                appointments = appointments.Where(a => a.EndTime.TimeOfDay <= toTime.Value).ToList();
            }


            return View(appointments);
        }

        public async Task<IActionResult> Create()
        {
            var officer = await _officerRepository.GetActiveOfficerAsync();
            var visitor = await _visitorRepository.GetActiveVisitorAsync();
            ViewBag.Officer = new SelectList(officer, "OfficerId", "Name");
            ViewBag.Visitor = new SelectList(visitor, "VisitorId", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Appointment appointment)
        {
            appointment.Status = AppointmentStatus.Active;



            if (!await _appointmentRepository.IsOfficerAvailable(appointment.OfficerId, appointment.Date, appointment.StartTime, appointment.EndTime) ||
                !await _appointmentRepository.IsVisitorAvailable(appointment.VisitorId, appointment.Date, appointment.StartTime, appointment.EndTime))
            {
                ModelState.AddModelError("", "The officer or visitor is unavailable at the selected time.");
                return View(appointment);
            }
            if (ModelState.IsValid)
            {
               
                await _appointmentRepository.AddAppointmentAsync(appointment);
                TempData["SuccessMessage"] = "Appointment was successfully created!";
                return RedirectToAction(nameof(Index));

            }
            var officer = await _officerRepository.GetActiveOfficerAsync();
            var visitor = await _visitorRepository.GetActiveVisitorAsync();
            ViewBag.Officer = new SelectList(officer, "OfficerId", "Name");
            ViewBag.Visitor = new SelectList(visitor, "VisitorId", "Name");
            return View(appointment);
        }

        public async Task<IActionResult> Edit(int id)
            {
            var app = await _appointmentRepository.GetAppointmentByIdAsync(id);
            if (id == null) return NotFound();

            var officer = await _officerRepository.GetActiveOfficerAsync();
            var visitor = await _visitorRepository.GetActiveVisitorAsync();
            ViewBag.Officer = new SelectList(officer, "OfficerId", "Name");
            ViewBag.Visitor = new SelectList(visitor, "VisitorId", "Name");
            return View(app);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Appointment appointment, int id, int officerId, int visitorId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _appointmentRepository.UpdateAppointmentAsync(appointment, id, officerId, visitorId);
                    TempData["SuccessMessage"] = "Appointment Was Updated !";
                    return RedirectToAction(nameof(Index));
                }
                catch (KeyNotFoundException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the appointment.");
                }
            }

            var officers = await _officerRepository.GetActiveOfficerAsync();
            var visitors = await _visitorRepository.GetActiveVisitorAsync();
            ViewBag.Officer = new SelectList(officers, "OfficerId", "Name", officerId);
            ViewBag.Visitor = new SelectList(visitors, "VisitorId", "Name", visitorId);
            return View(appointment);
        }
  
        public async Task<IActionResult> Activate(int id)
        {

            await _appointmentRepository.SetAppointsStatusAsync(id, AppointmentStatus.Active);
            TempData["SuccessMessage"] = "Appointment was activated";
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Deactivate(int id)
        {
            await _appointmentRepository.SetAppointsStatusAsync(id, AppointmentStatus.Deactivated);
            TempData["SuccessMessage"] = "Appointment was Deactivated!";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Cancel(int id)
        {

            await _appointmentRepository.SetAppointsStatusAsync(id, AppointmentStatus.Cancelled);
            TempData["SuccessMessage"] = "Appoinment was Cancel !";
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Complete(int id)
        {
            await _appointmentRepository.SetAppointsStatusAsync(id, AppointmentStatus.Completed);
            TempData["SuccessMessage"] = "Appoinment was Completed";
            return RedirectToAction("Index");
        }
    }
}
