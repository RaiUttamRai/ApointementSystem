using ApointementSystem.Models.OfficerModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApointementSystem.Models.WorkdayModel
{
    public class WorkDay
    {
        public int WorkDayId { get; set; }
        public int OfficerId { get; set; }
        [ForeignKey("OfficerId")]
        public Officer Officer { get; set; }
        public string SelectedDays { get; set; }

    }
}
