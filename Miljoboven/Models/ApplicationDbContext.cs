using Microsoft.EntityFrameworkCore;
using Miljoboven.Models;

namespace Miljoboven.Models
{
    // The ApplicationDbContext class is the database context for this application.
    // It inherits from DbContext, which provides functionality for interacting with the database.
    public class ApplicationDbContext : DbContext
    {
        // Constructor that accepts DbContextOptions to configure the context (e.g., specifying the database connection)
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSet properties define the tables in the database that will be created for each entity.
        // Each DbSet corresponds to a table and allows CRUD operations (Create, Read, Update, Delete).
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Errand> Errands { get; set; }
        public DbSet<ErrandStatus> ErrandStatuses { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Sample> Samples { get; set; }
        public DbSet<Sequence> Sequences { get; set; }
    }
}