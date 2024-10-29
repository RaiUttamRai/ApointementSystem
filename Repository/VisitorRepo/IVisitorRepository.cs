using ApointementSystem.Models.ApointmentModel;
using ApointementSystem.Models.PostModel;
using ApointementSystem.Models.viewmodel;
using ApointementSystem.Models.VisitorModel;
using Microsoft.EntityFrameworkCore;

namespace ApointementSystem.Repository.VisitorRepo
{
    public interface IVisitorRepository
    {
        Task<IEnumerable<Visitor>> GetAllVisitorsAsync();

        Task<Visitor> GetVisitorByIdAsync(int visitorId);
        Task<IEnumerable<Visitor>> GetAppointmentsByVisitorIdAsync(int visitorId);
        

        Task AddVisitorAsync(Visitor visitor);
        Task UpdateVisitorAsync(EditVisitor visitor,int visitorId);
        Task SetVisitorStatusAsync(int id, bool isActive);
        Task<IEnumerable<Visitor>> GetActiveVisitorAsync();
        Task<bool> IsActive(int officerId);
    }
}
