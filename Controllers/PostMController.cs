using ApointementSystem.Models.PostModel;
using ApointementSystem.Models.viewmodel;
using ApointementSystem.Repository.PostRepo;
using Microsoft.AspNetCore.Mvc;

namespace ApointementSystem.Controllers
{
    public class PostMController : Controller
    {
        private readonly IPostRepository _postRepository;
        public PostMController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
            
        }
         public async Task<IActionResult> Index()
         {
            var post = await _postRepository.GetAllPostAsync();
            return View(post);
         }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.PostModel.Post post)
        {
             
                await _postRepository.AddPostAsync(post);
                return RedirectToAction("Index");

            
            return View(post);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if (post == null)
                return NotFound();
            return View(post);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditPost post,int id)
        {
            if(ModelState.IsValid)
            {
                await _postRepository.UpdatePostAsync(post,id);
                return RedirectToAction("Index");
            }
            return View(post);
        }
        public async Task<IActionResult> Activate(int id)
        {
            await _postRepository.SetPostStatusAsync(id, false);
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Deactive(int id)
        {
            await _postRepository.SetPostStatusAsync(id, true);
            return RedirectToAction("Index");
        }

    }
}
