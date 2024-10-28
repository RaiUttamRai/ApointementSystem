
using ApointementSystem.Models.PostModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApointementSystem.Models.viewmodel
{
    public class EditOfficer
    {
        public string Name { get; set; }
        public int PostId { get; set; }
        [ForeignKey("PostId")]
        [ValidateNever]
        public Post Post { get; set; }
        public DateTime WorkStartTime { get; set; }
        public DateTime WorkEndTime { get; set; }
    }
}
