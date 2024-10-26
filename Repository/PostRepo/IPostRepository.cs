using ApointementSystem.Models.PostModel;

namespace ApointementSystem.Repository.PostRepo
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllPostAsync();
        Task<Post> GetPostByIdAsync(int id);
        Task AddPostAsync(Post post);   
        Task UpdatePostAsync(Post post,int id);
        Task SetPostStatusAsync(int id, bool isActive);
    }
}
