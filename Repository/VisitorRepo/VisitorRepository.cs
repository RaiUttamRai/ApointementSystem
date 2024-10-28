using ApointementSystem.Data;
using ApointementSystem.Models.ApointmentModel;
using ApointementSystem.Models.viewmodel;
using ApointementSystem.Models.VisitorModel;
using Microsoft.EntityFrameworkCore;

namespace ApointementSystem.Repository.VisitorRepo
{
    public class VisitorRepository : IVisitorRepository
    {
        private readonly ApplicationDbContext _context;

        public VisitorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Visitor>> GetAllVisitorsAsync()
        {
            return await _context.visitors.ToListAsync();
        }

        public async Task<Visitor> GetVisitorByIdAsync(int visitorId)
        {
            return await _context.visitors.FindAsync(visitorId);
        }
        public async Task<IEnumerable<Visitor>> GetAppointmentsByVisitorIdAsync(int visitorId)
        {
            return await _context.visitors.Where(a => a.VisitorId == visitorId).ToListAsync();
        }


        public async Task AddVisitorAsync(Visitor visitor)
        {
            await _context.visitors.AddAsync(visitor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateVisitorAsync(EditVisitor visitor, int visitorId)
        {

            var toEdit = await _context.visitors.FindAsync(visitorId);

            if (toEdit == null)
            {
                throw new KeyNotFoundException($"POST with ID {visitorId} not found.");

            }

            toEdit.Name = visitor.Name;
            toEdit.MobileNo = visitor.MobileNo;
            toEdit.Email = visitor.Email;
             

            await _context.SaveChangesAsync();

        }

        public async Task SetVisitorStatusAsync(int id, bool isActive)
        {
            var visitor = await _context.visitors.FindAsync(id);
            if (visitor != null)
            {
                visitor.Status = isActive ? false : true;
                await _context.SaveChangesAsync();
            }
        }

        
    }
}
