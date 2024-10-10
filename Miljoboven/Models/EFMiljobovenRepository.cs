using Microsoft.EntityFrameworkCore;

namespace Miljoboven.Models
{
    public class EFMiljobovenRepository : IMiljobovenRepository
    {
        private readonly ApplicationDbContext _context;

        public EFMiljobovenRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Department> Departments => _context.Departments;
        public IQueryable<Errand> Errands => _context.Errands.Include(e => e.Samples).Include(e => e.Pictures);

        public IQueryable<ErrandStatus> ErrandStatuses => _context.ErrandStatuses;
        public IQueryable<Employee> Employees => _context.Employees;
        public IQueryable<Picture> Pictures => _context.Pictures;
        public IQueryable<Sample> Samples => _context.Samples;
        public IQueryable<Sequence> Sequences => _context.Sequences;

        // Get ErrandDetails by int id
        public Task<Errand> GetErrandDetails(int id)
        {
            return Task.FromResult(Errands.FirstOrDefault(e => e.ErrandId == id));
        }
    }
}