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

        public string SaveErrand(Errand errand)
        {
            // Check if the errand object is null and throw an exception if it is.
            if (errand == null) throw new ArgumentNullException(nameof(errand));
            // If the errand already has an ID (meaning it's already saved), return its reference number.
            if (errand.ErrandId != 0) return errand.RefNumber;

            // Find the sequence with the ID of 1. If not found, throw an InvalidOperationException.
            var sequence = Sequences.FirstOrDefault(s => s.Id == 1) ?? throw new InvalidOperationException("Sequence not found");
            errand.RefNumber = $"{DateTime.Now.Year}-45-{sequence.CurrentValue++}";
            errand.StatusId = "S_A";

            _context.Errands.Add(errand);
            _context.SaveChanges();

            return errand.RefNumber;
        }

        public void UpdateDepartment(int id, string department)
        {
            var errand = Errands.FirstOrDefault(e => e.ErrandId == id);
            errand.DepartmentId = department;
            _context.SaveChanges();
        }

        public void ManagerEdit(int id, string reason, bool noAction, string investigator)
        {
            var errand = Errands.FirstOrDefault(e => e.ErrandId == id);
            if (errand == null) throw new InvalidOperationException("Errand not found");

            if (noAction) // If no action is required (true)
            {
                errand.StatusId = "S_B";
                errand.InvestigatorInfo = reason;
            }
            else // Choose investigator
            {
                errand.StatusId = "S_A";
                errand.EmployeeId = investigator;
            }

            _context.SaveChanges();
        }
    }
}