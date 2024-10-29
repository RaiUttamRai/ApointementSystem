using ApointementSystem.Models.viewmodel;
using ApointementSystem.Models.WorkdayModel;
using ApointementSystem.Repository.DayOfWeek;
using ApointementSystem.Repository.OfficerRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ApointementSystem.Controllers
{
    public class WorkDayController : Controller
    {
        
            private readonly IWorkDayRepository _workDayRepository;
            private readonly IOfficerRepository _officerRepository;

            public WorkDayController(IWorkDayRepository workDayRepository, IOfficerRepository officerRepository)
            {
                _workDayRepository = workDayRepository;
                _officerRepository = officerRepository;
            }

            public async Task<IActionResult> Index()
            {
                var workDays = await _workDayRepository.GetAllWorkDaysAsync();
                return View(workDays);
            }

            public async Task<IActionResult> Create()
            {
                  var officer = await _officerRepository.GetAllOfficersAsync();
                  ViewBag.Officer = new SelectList(officer, "OfficerId", "Name");
                  return View();

            }

        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> Create(int OfficerId, List<DayOfWeek> SelectedDays)
        {
            if (SelectedDays == null || !SelectedDays.Any())
            {
                ModelState.AddModelError("", "Please select at least one day.");
            }

            if (ModelState.IsValid)
            {
                var officerWorkDays = new WorkDay
                {
                    OfficerId = OfficerId,
                    SelectedDays = string.Join(",", SelectedDays) // Save selected days as a comma-separated string
                };
                await _workDayRepository.AddWorkDayAsync(officerWorkDays);
                return RedirectToAction(nameof(Index));
            }

            var officer = await _officerRepository.GetAllOfficersAsync();
            ViewBag.Officer = new SelectList(officer, "OfficerId", "Name");
            return View();
        }



        public async Task<IActionResult> Edit(int id)
            {
                var workDay = await _workDayRepository.GetWorkDayByIdAsync(id);
                if (workDay == null)
                {
                    return NotFound();
                }

            var officer = await _officerRepository.GetAllOfficersAsync();
            ViewBag.Officer = new SelectList(officer, "OfficerId", "Name");
            return View(workDay);
        }

             
    }
}
 