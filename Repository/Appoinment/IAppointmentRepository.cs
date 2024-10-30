using ApointementSystem.Models.ApointmentModel;
using static ApointementSystem.Models.ApointmentModel.Appointment;

namespace ApointementSystem.Repository.Appoinment
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
        Task<Appointment> GetAppointmentByIdAsync(int id);
        Task AddAppointmentAsync(Appointment appointment);
        Task UpdateAppointmentAsync(Appointment appointment, int id, int officerId, int visitorId);
        //Task CancelAppointmentAsync(int id);
        Task<bool> IsActive(int officerId);
        Task SetAppointsStatusAsync(int id, AppointmentStatus status);
        Task<bool> IsOfficerAvailable(int officerId, DateTime date, DateTime startTime, DateTime endTime);
        Task<bool> IsVisitorAvailable(int visitorId, DateTime date, DateTime startTime, DateTime endTime);
    }
}
