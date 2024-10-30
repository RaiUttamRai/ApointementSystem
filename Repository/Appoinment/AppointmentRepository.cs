using ApointementSystem.Data;
using ApointementSystem.Models.ApointmentModel;
using ApointementSystem.Models.OfficerModel;
using ApointementSystem.Repository.OfficerRepo;
using ApointementSystem.Repository.VisitorRepo;
using Microsoft.EntityFrameworkCore;
using static ApointementSystem.Models.ApointmentModel.Appointment;

namespace ApointementSystem.Repository.Appoinment
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IOfficerRepository _officer;
        private readonly IVisitorRepository _visitor;

        public AppointmentRepository(ApplicationDbContext context,IVisitorRepository visitor, IOfficerRepository officer)
        {
            _context = context;
            _officer = officer;
            _visitor = visitor;
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
        {
            var appointmetns= await _context.appointments
                .Include(a => a.Officer)
                .Include(a => a.Visitor)
                .ToListAsync();
            foreach (var appointment in appointmetns)
            {
                if(appointment.Date<DateTime.Today)
                {
                    if (appointment.Status == AppointmentStatus.Active)
                    {
                        appointment.Status = AppointmentStatus.Completed;
                    }
                    else if(appointment.Status==AppointmentStatus.Deactivated)
                    {
                        appointment.Status = AppointmentStatus.Cancelled;
                    }
                }
            }
            await _context.SaveChangesAsync();
            return appointmetns;
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

        public async Task UpdateAppointmentAsync(Appointment appointment, int id, int officerId, int visitorId)
        {
            appointment.LastUpdatedOn = DateTime.UtcNow;

            var toEdit = await _context.appointments.FindAsync(id);
            var officerExists = await _context.officers.AnyAsync(o => o.OfficerId == officerId);
            var visitorExists = await _context.visitors.AnyAsync(v => v.VisitorId == visitorId);

            if (toEdit == null)
            {
                throw new KeyNotFoundException($"Appointment with ID {id} not found.");
            }
            if (!officerExists)
            {
                throw new KeyNotFoundException($"Officer with ID {officerId} not found.");
            }
            if (!visitorExists)
            {
                throw new KeyNotFoundException($"Visitor with ID {visitorId} not found.");
            }

             
            toEdit.OfficerId = officerId;
            toEdit.VisitorId = visitorId;
            toEdit.Name = appointment.Name;
            toEdit.Date = appointment.Date;
            toEdit.StartTime = appointment.StartTime;
            toEdit.EndTime = appointment.EndTime;
            toEdit.Status = appointment.Status;
            toEdit.LastUpdatedOn = DateTime.UtcNow;

            _context.appointments.Update(toEdit);
            await _context.SaveChangesAsync();
        }


        //public async Task CancelAppointmentAsync(int id)
        //{
        //    var appointment = await GetAppointmentByIdAsync(id);
        //    if (appointment != null)
        //    {
        //        appointment.Status = false;
        //        appointment.LastUpdatedOn = DateTime.UtcNow;
        //        _context.appointments.Update(appointment);
        //        await _context.SaveChangesAsync();
        //    }
        //}
        public async Task SetAppointsStatusAsync(int id, Appointment.AppointmentStatus status)
        {
            var appointment = await _context.appointments.FindAsync(id);
            if (appointment != null)
            {
                appointment.Status = status;
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
                a.Status==AppointmentStatus.Active==true);
        }

        public async Task<bool> IsVisitorAvailable(int visitorId, DateTime date, DateTime startTime, DateTime endTime)
        {
            return !await _context.appointments.AnyAsync(a =>
                a.VisitorId == visitorId &&
                a.Date == date &&
                ((a.StartTime <= startTime && a.EndTime > startTime) ||
                 (a.StartTime < endTime && a.EndTime >= endTime)) &&
                a.Status == AppointmentStatus.Active==true);
        }
        public async Task<bool> IsActive(int id)
        {
            var officer = await _context.appointments.FindAsync(id);
            return officer?.Status == AppointmentStatus.Active == true;
        }
    }
}
