using ApointementSystem.Models.OfficerModel;
using ApointementSystem.Models.VisitorModel;

namespace ApointementSystem.Models.ApointmentModel
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int OfficerId { get; set; }
        public int VisitorId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string Status { get; set; } // E.g., "Active", "Cancelled", "Completed"
        public Officer Officer { get; set; }
        public Visitor Visitor { get; set; }
    }
}
