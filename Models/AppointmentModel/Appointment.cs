using ApointementSystem.Models.OfficerModel;
using ApointementSystem.Models.VisitorModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApointementSystem.Models.ApointmentModel
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int OfficerId { get; set; }
        [ForeignKey("OfficerId")]
        public Officer Officer { get; set; }
        public int VisitorId { get; set; }
        [ForeignKey("VisitorId")]
        public Visitor Visitor { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string Status { get; set; }
        
        
    }
}
