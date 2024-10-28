using ApointementSystem.Models.ApointmentModel;
using ApointementSystem.Models.PostModel;
using ApointementSystem.Models.WorkdayModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApointementSystem.Models.OfficerModel
{
    public class Officer
    {
        public int OfficerId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int PostId { get; set; }
        [ForeignKey("PostId")]
        [ValidateNever]
        public Post Post { get; set; }
        public bool Status { get; set; }
        [Required]
        public DateTime WorkStartTime { get; set; }
        [Required]
        public DateTime WorkEndTime { get; set; }
        [ValidateNever]
        public ICollection<WorkDay> WorkDays { get; set; }
        [ValidateNever]
        public ICollection<Appointment> Appointments { get; set; }
    }
}
