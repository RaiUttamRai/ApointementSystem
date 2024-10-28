
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using ApointementSystem.Models.VisitorModel;
    using ApointementSystem.Repository;
    using ApointementSystem.Repository.VisitorRepo;
    using ApointementSystem.Models.viewmodel;

    namespace ApointementSystem.Controllers
    {
        public class VisitorController : Controller
        {
            private readonly IVisitorRepository _visitorRepository;

            public VisitorController(IVisitorRepository visitorRepository)
            {
                _visitorRepository = visitorRepository;
            }

            public async Task<IActionResult> Index()
            {
                var visitors = await _visitorRepository.GetAllVisitorsAsync();
                return View(visitors);
            }
        public async Task<IActionResult> ViewAppointments(int id)
        {
            var visitor = await _visitorRepository.GetVisitorByIdAsync(id);
            if (visitor.Status == false)
            {
                return NotFound("visitor was deactive please activate");
            }

            var appointments = await _visitorRepository.GetAppointmentsByVisitorIdAsync(id);
            return View(appointments);
        }


        public IActionResult Create()
            {
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(Visitor visitor)
            {
            visitor.Status = true;
             
                    await _visitorRepository.AddVisitorAsync(visitor);
                    return RedirectToAction(nameof(Index));
            
                return View(visitor);
            }

            [HttpGet]
            public async Task<IActionResult> Edit(int id)
            {
                var visitor = await _visitorRepository.GetVisitorByIdAsync(id);
                if (visitor == null)
                {
                    return NotFound();
                }
                return View(visitor);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(EditVisitor visitor, int visitorId)
            {
                if (ModelState.IsValid)
                {
                    await _visitorRepository.UpdateVisitorAsync(visitor,visitorId);
                    return RedirectToAction(nameof(Index));
                }
                return View(visitor);
            }

            public async Task<IActionResult> Activate(int id)
            {
             
                await _visitorRepository.SetVisitorStatusAsync(id, false);
                return RedirectToAction("Index");

            }
            public async Task<IActionResult> Deactive(int id)
            {
                await _visitorRepository.SetVisitorStatusAsync(id, true);
                return RedirectToAction("Index");
            }
        }
    }
