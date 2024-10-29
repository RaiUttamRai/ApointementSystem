using ApointementSystem.Data;
using ApointementSystem.Models.ApointmentModel;
using ApointementSystem.Models.OfficerModel;
using ApointementSystem.Models.viewmodel;
using ApointementSystem.Models.VisitorModel;
using ApointementSystem.Models.WorkdayModel;
using Microsoft.EntityFrameworkCore;

namespace ApointementSystem.Repository.OfficerRepo
{
    public class OfficerRepository : IOfficerRepository
    {
        private readonly ApplicationDbContext _context;

        public OfficerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Officer> GetOfficerByIdAsync(int officerId)
        {
            return await _context.officers
                .Include(o => o.WorkDays)
                .Include(o => o.Appointments)
                .FirstOrDefaultAsync(o => o.OfficerId == officerId);
        }

        public async Task<IEnumerable<Officer>> GetAllOfficersAsync()
        {
            return await _context.officers
                .Include(o => o.Post)
                .Include(o => o.WorkDays)
                .ToListAsync();
        }

        public async Task AddOfficerAsync(Officer officer)
        {
            await _context.officers.AddAsync(officer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOfficerAsync(EditOfficer officer,int id)
        {
            var toEdit = await _context.officers.FindAsync(id);

            if (toEdit == null)
            {
                throw new KeyNotFoundException($"POST with ID {id} not found.");

            }

            toEdit.Name = officer.Name;
            toEdit.PostId = officer.PostId;
            toEdit.WorkStartTime = officer.WorkStartTime;
            toEdit.WorkEndTime = officer.WorkEndTime;
            await _context.SaveChangesAsync();
        }
       



        public async Task<IEnumerable<Appointment>> GetOfficerAppointmentsAsync(int officerId)
        {
            var officer = await _context.officers
                .Include(o => o.Appointments)
                .FirstOrDefaultAsync(o => o.OfficerId == officerId);
            return officer?.Appointments;
        }

        public async Task UpdateWorkDaysAsync(int officerId, ICollection<WorkDay> workDays)
        {
            var officer = await _context.officers
                .Include(o => o.WorkDays)
                .FirstOrDefaultAsync(o => o.OfficerId == officerId);

            if (officer != null)
            {
                officer.WorkDays = workDays;
                await _context.SaveChangesAsync();
            }
        }

        public async Task SetOfficerStatusAsync(int id, bool isActive)
        {
            var officer = await _context.officers.FindAsync(id);
            if (officer != null)
            {
                officer.Status = isActive ? false : true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Officer>> GetAppointmentsByOfficerIdAsync(int officerId)
        {

            return await _context.officers
        .Include(o => o.Post)
        .Where(u => u.OfficerId == officerId)
        .ToListAsync();
        }
    }
}
