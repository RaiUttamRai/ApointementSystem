using ApointementSystem.Data;
using ApointementSystem.Models.viewmodel;
using ApointementSystem.Models.VisitorModel;
using ApointementSystem.Models.WorkdayModel;
using Microsoft.EntityFrameworkCore;

namespace ApointementSystem.Repository.DayOfWeek
{
    public class WorkDayRepository : IWorkDayRepository
    {
        private readonly ApplicationDbContext _context;

        public WorkDayRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WorkDay>> GetAllWorkDaysAsync()
        {
            return await _context.workDays.Include(w => w.Officer).ToListAsync();
        }

        public async Task<WorkDay> GetWorkDayByIdAsync(int id)
        {
            return await _context.workDays.Include(w => w.Officer).FirstOrDefaultAsync(w => w.WorkDayId == id);
        }

        public async Task AddWorkDayAsync(WorkDay workDay)
        {
            _context.workDays.Add(workDay);
            await _context.SaveChangesAsync();
        }

       

         
    }
}
