using ApointementSystem.Models.viewmodel;
using ApointementSystem.Models.WorkdayModel;

namespace ApointementSystem.Repository.DayOfWeek
{
    public interface IWorkDayRepository
    {
        Task<IEnumerable<WorkDay>> GetAllWorkDaysAsync();
        Task<WorkDay> GetWorkDayByIdAsync(int id);
        Task AddWorkDayAsync(WorkDay workDay);
        
         
    }
}
