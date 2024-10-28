using ApointementSystem.Models.ApointmentModel;
using System.ComponentModel.DataAnnotations;

namespace ApointementSystem.Models.VisitorModel
{
    public class Visitor
    {
        public int VisitorId { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Mobile number is required.")]
        [RegularExpression(@"^9\d{9}$", ErrorMessage = "Mobile number must start with 9 and be 10 digits long.")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Email must be a valid Gmail address.")]
        public string Email { get; set; }
        public bool Status { get; set; } 
        public ICollection<Appointment> Appointments { get; set; }
    }
}
