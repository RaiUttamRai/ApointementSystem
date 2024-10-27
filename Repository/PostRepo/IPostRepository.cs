using ApointementSystem.Models.PostModel;
using ApointementSystem.Models.viewmodel;

namespace ApointementSystem.Repository.PostRepo
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllPostAsync();
        Task<Post> GetPostByIdAsync(int id);
        Task AddPostAsync(Post post);   
        Task UpdatePostAsync(EditPost post,int id);
        Task SetPostStatusAsync(int id, bool isActive);
    }
}
