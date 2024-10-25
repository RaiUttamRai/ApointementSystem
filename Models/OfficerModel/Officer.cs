using ApointementSystem.Models.ApointmentModel;
using ApointementSystem.Models.PostModel;
using ApointementSystem.Models.WorkdayModel;

namespace ApointementSystem.Models.OfficerModel
{
    public class Officer
    {
        public int OfficerId { get; set; }
        public string Name { get; set; }
        public int PostId { get; set; }
        public bool Status { get; set; }
        public DateTime WorkStartTime { get; set; }
        public DateTime WorkEndTime { get; set; }
        public Post Post { get; set; }
        public ICollection<WorkDay> WorkDays { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
