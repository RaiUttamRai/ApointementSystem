using ApointementSystem.Models.ApointmentModel;
using ApointementSystem.Models.OfficerModel;
using ApointementSystem.Models.PostModel;
using ApointementSystem.Models.viewmodel;
using ApointementSystem.Models.VisitorModel;
using ApointementSystem.Models.WorkdayModel;

namespace ApointementSystem.Repository.OfficerRepo
{
    public interface IOfficerRepository
    {
        Task<Officer> GetOfficerByIdAsync(int officerId);
        Task<IEnumerable<Officer>> GetAllOfficersAsync();
        Task AddOfficerAsync(Officer officer);
        Task UpdateOfficerAsync(EditOfficer officer ,int id);
        Task<IEnumerable<Officer>> GetAppointmentsByOfficerIdAsync(int officerId);
        Task SetOfficerStatusAsync(int id, bool isActive);
        Task<IEnumerable<Officer>> GetActiveOfficerAsync();


        Task<IEnumerable<Appointment>> GetOfficerAppointmentsAsync(int officerId);
        Task UpdateWorkDaysAsync(int officerId, ICollection<WorkDay> workDays);
    }
}
