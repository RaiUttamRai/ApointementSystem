using ApointementSystem.Models.ApointmentModel;
using ApointementSystem.Models.OfficerModel;
using ApointementSystem.Models.PostModel;
using ApointementSystem.Models.VisitorModel;
using ApointementSystem.Models.WorkdayModel;
using Microsoft.EntityFrameworkCore;

namespace ApointementSystem.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Appointment> appointments { get; set; }
        public DbSet<Officer> officers { get; set; }
        public DbSet<Post> posts { get; set; }
        public DbSet<WorkDay> workDays { get; set; }
        public DbSet<Visitor> visitors { get; set; }
        

    }
}
