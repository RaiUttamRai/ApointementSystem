using ApointementSystem.Models.OfficerModel;

namespace ApointementSystem.Models.PostModel
{
    public class Post
    {
        public int PostId { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; } 
        public ICollection<Officer> Officers { get; set; }
    }
}
