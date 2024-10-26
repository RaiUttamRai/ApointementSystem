using ApointementSystem.Models.ApointmentModel;

namespace ApointementSystem.Models.VisitorModel
{
    public class Visitor
    {
        public int VisitorId { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; } 
        public ICollection<Appointment> Appointments { get; set; }
    }
}
