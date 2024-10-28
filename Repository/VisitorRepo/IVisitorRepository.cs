using ApointementSystem.Models.viewmodel;
using ApointementSystem.Models.VisitorModel;

namespace ApointementSystem.Repository.VisitorRepo
{
    public interface IVisitorRepository
    {
        Task<IEnumerable<Visitor>> GetAllVisitorsAsync();
        Task<Visitor> GetVisitorByIdAsync(int visitorId);
        Task AddVisitorAsync(Visitor visitor);
        Task UpdateVisitorAsync(EditVisitor visitor,int visitorId);
        Task SetVisitorStatusAsync(int id, bool isActive);
    }
}
