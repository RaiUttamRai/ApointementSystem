using ApointementSystem.Data;
using ApointementSystem.Models.ApointmentModel;
using ApointementSystem.Models.OfficerModel;
using Microsoft.EntityFrameworkCore;

namespace ApointementSystem.Repository.Appoinment
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
        {
            return await _context.appointments
                .Include(a => a.Officer)
                .Include(a => a.Visitor)
                .ToListAsync();
        }

        public async Task<Appointment> GetAppointmentByIdAsync(int id)
        {
            return await _context.appointments
                .Include(a => a.Officer)
                .Include(a => a.Visitor)
                .FirstOrDefaultAsync(a => a.AppointmentId == id);
        }

        public async Task AddAppointmentAsync(Appointment appointment)
        {
            appointment.AddedOn = DateTime.UtcNow;
            appointment.LastUpdatedOn = DateTime.UtcNow;
            await _context.appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAppointmentAsync(Appointment appointment, int id)
        {
            appointment.LastUpdatedOn = DateTime.UtcNow;
            _context.appointments.Update(appointment);
            await _context.SaveChangesAsync();
            //    var toEdit = await _context.appointments.FindAsync(id);

            //    if (toEdit == null)
            //    {
            //        throw new KeyNotFoundException($"POST with ID {id} not found.");
            //    }


            //    toEdit.OfficerId = appointment.OfficerId;  
            //    toEdit.VisitorId = appointment.VisitorId; 
            //    toEdit.Name = appointment.Name;
            //    toEdit.Date = appointment.Date;  
            //    toEdit.StartTime = appointment.StartTime;
            //    toEdit.EndTime = appointment.EndTime;


        }


        public async Task CancelAppointmentAsync(int id)
        {
            var appointment = await GetAppointmentByIdAsync(id);
            if (appointment != null)
            {
                appointment.Status = false;
                appointment.LastUpdatedOn = DateTime.UtcNow;
                _context.appointments.Update(appointment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> IsOfficerAvailable(int officerId, DateTime date, DateTime startTime, DateTime endTime)
        {
            return !await _context.appointments.AnyAsync(a =>
                a.OfficerId == officerId &&
                a.Date == date &&
                ((a.StartTime <= startTime && a.EndTime > startTime) ||
                 (a.StartTime < endTime && a.EndTime >= endTime)) &&
                a.Status == true);
        }

        public async Task<bool> IsVisitorAvailable(int visitorId, DateTime date, DateTime startTime, DateTime endTime)
        {
            return !await _context.appointments.AnyAsync(a =>
                a.VisitorId == visitorId &&
                a.Date == date &&
                ((a.StartTime <= startTime && a.EndTime > startTime) ||
                 (a.StartTime < endTime && a.EndTime >= endTime)) &&
                a.Status == true);
        }
        public async Task<bool> IsActive(int id)
        {
            var officer = await _context.appointments.FindAsync(id);
            return officer?.Status == true;
        }
    }
}
