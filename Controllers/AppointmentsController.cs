using ApointementSystem.Models.ApointmentModel;
using ApointementSystem.Repository.Appoinment;
using ApointementSystem.Repository.OfficerRepo;
using ApointementSystem.Repository.VisitorRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

        public async Task<IActionResult> Index()
        {
            var appointments = await _appointmentRepository.GetAllAppointmentsAsync();
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
            appointment.Status = true ;
            

            if (!await _appointmentRepository.IsOfficerAvailable(appointment.OfficerId, appointment.Date, appointment.StartTime, appointment.EndTime) ||
                !await _appointmentRepository.IsVisitorAvailable(appointment.VisitorId, appointment.Date, appointment.StartTime, appointment.EndTime))
            {
                ModelState.AddModelError("", "The officer or visitor is unavailable at the selected time.");
                return View(appointment);
            }
            if (ModelState.IsValid)
            {
               
                await _appointmentRepository.AddAppointmentAsync(appointment);
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
        public async Task<IActionResult> Edit(Appointment appointment, int id)
        {
            if (ModelState.IsValid)
            {
                await _appointmentRepository.UpdateAppointmentAsync(appointment,id);
                return RedirectToAction(nameof(Index));
            }
            var officer = await _officerRepository.GetActiveOfficerAsync();
            var visitor = await _visitorRepository.GetActiveVisitorAsync();
            ViewBag.Officer = new SelectList(officer, "OfficerId", "Name");
            ViewBag.Visitor = new SelectList(visitor, "VisitorId", "Name");
            return View(appointment);

        }

        public async Task<IActionResult> Cancel(int id)
        {
            await _appointmentRepository.CancelAppointmentAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
