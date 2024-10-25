using ApointementSystem.Models.OfficerModel;

namespace ApointementSystem.Models.WorkdayModel
{
    public class WorkDay
    {
        public int WorkDayId { get; set; }
        public int OfficerId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public Officer Officer { get; set; }
    }
}
