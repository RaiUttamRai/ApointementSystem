using ApointementSystem.Models.OfficerModel;
using ApointementSystem.Models.VisitorModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApointementSystem.Models.ApointmentModel
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int OfficerId { get; set; }
        [ForeignKey("OfficerId")]
        [ValidateNever]
        public Officer Officer { get; set; }
        public int VisitorId { get; set; }
        [ForeignKey("VisitorId")]
        [ValidateNever]
        public Visitor Visitor { get; set; }
        [ValidateNever]
        public string Name { get; set; }
        [ValidateNever]
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public bool Status { get; set; }
        
        
    }
}
