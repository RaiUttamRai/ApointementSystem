using ApointementSystem.Data;
using ApointementSystem.Models.PostModel;
using ApointementSystem.Models.viewmodel;
using Microsoft.EntityFrameworkCore;

namespace ApointementSystem.Repository.PostRepo
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<IEnumerable<Post>> GetAllPostAsync()
        {
            return await _context.posts.ToListAsync();
        }
        public async Task<Post> GetPostByIdAsync(int id)
        {
            return await _context.posts.FindAsync(id);
        }

        public async Task AddPostAsync(Post post)
        {
            post.Status = true;
            _context.posts.Add(post);
            await _context.SaveChangesAsync();
        }
        public async Task UpdatePostAsync(EditPost post, int id)
        {

            var toEdit = await _context.posts.FindAsync(id);

            if (toEdit== null)
            {
                throw new KeyNotFoundException($"POST with ID {id} not found.");

            }

            toEdit.Name = post.Name;
           // toEdit.Status = post.Status;

            await _context.SaveChangesAsync();

        }

        public async Task SetPostStatusAsync(int id, bool isActive)
        {
            var post = await _context.posts.FindAsync(id);
            if (post != null)
            {
                post.Status = isActive ? false : true;
                await _context.SaveChangesAsync();
            }
        }


    }
}
