using ApointementSystem.Models.OfficerModel;
using ApointementSystem.Models.PostModel;
using ApointementSystem.Models.viewmodel;
using ApointementSystem.Repository.OfficerRepo;
using ApointementSystem.Repository.PostRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ApointementSystem.Controllers
{
    public class OfficerController : Controller
    {
        private readonly IOfficerRepository _officerRepository;
        private readonly IPostRepository _postRepository;

        public OfficerController(IOfficerRepository officerRepository, IPostRepository postRepository)
        {
            _officerRepository = officerRepository;
            _postRepository = postRepository;
        }

        public async Task<IActionResult> Index()
        {
           
            var officers = await _officerRepository.GetAllOfficersAsync();
            return View(officers);
        }


        public async Task<IActionResult> Create()
        {
            var posts = await _postRepository.GetActivePostsAsync();
            
            ViewBag.Posts = new SelectList(posts, "PostId", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Officer officer)
        {
            officer.Status = true;
             
            if (ModelState.IsValid)
            {
                await _officerRepository.AddOfficerAsync(officer);
                return RedirectToAction(nameof(Index));
            }


            var posts = await _postRepository.GetActivePostsAsync();
            ViewBag.Posts = new SelectList(posts, "PostId", "Name");
            return View(officer);
        }



        public async Task<IActionResult> Edit(int id)
        {
            
            var officer = await _officerRepository.GetOfficerByIdAsync(id);
            if(officer == null)
            {
                NotFound("NO date found");
            }
            var posts = await _postRepository.GetActivePostsAsync();

            ViewBag.Posts = new SelectList(posts, "PostId", "Name");
            return View(officer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditOfficer officer,int id)
        {
            
            if (ModelState.IsValid)
            {
                await _officerRepository.UpdateOfficerAsync(officer,id);
                return RedirectToAction(nameof(Index));
            }
            var posts = await _postRepository.GetActivePostsAsync();
            ViewBag.Posts = new SelectList(posts, "PostId", "Name");

            return View(officer);
        }

        public async Task<IActionResult> Activate(int id)
        {
            await _officerRepository.SetOfficerStatusAsync(id, false);
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Deactive(int id)
        {
            await _officerRepository.SetOfficerStatusAsync(id, true);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Appointments(int id)
        {
            var visitor = await _officerRepository.GetOfficerByIdAsync(id);
            if (visitor.Status == false)
            {
                return NotFound("visitor was deactive please activate");
            }

            var appointments = await _officerRepository.GetAppointmentsByOfficerIdAsync(id);
            return View(appointments);
        }
    }
}
