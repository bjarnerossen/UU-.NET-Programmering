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

        public void InvestigatorEdit(int id, string events, string information, string status)
        {
            var errand = Errands.FirstOrDefault(e => e.ErrandId == id);

            if (errand == null) throw new InvalidOperationException("Errand not found.");

            // Append events if not empty or whitespace
            if (!string.IsNullOrWhiteSpace(events))
            {
                errand.InvestigatorAction += events.Trim() + " ";
            }

            // Append information if not empty or whitespace
            if (!string.IsNullOrWhiteSpace(information))
            {
                errand.InvestigatorInfo += information.Trim() + " ";
            }

            // Update status if a valid option is selected
            if (!string.IsNullOrWhiteSpace(status) && status != "Välj")
            {
                errand.StatusId = status;
            }
            _context.SaveChanges();
        }

        public void SavePicture(int id, string fileName)
        {
            var picture = new Picture();
            picture.ErrandId = id;
            picture.PictureName = fileName;
            _context.Pictures.Add(picture);
            _context.SaveChanges();
        }

        public void SaveSample(int id, string fileName)
        {
            var sample = new Sample();
            sample.ErrandId = id;
            sample.SampleName = fileName;
            _context.Samples.Add(sample);
            _context.SaveChanges();
        }
        public IQueryable<MyErrand> CoordinatorErrands()
        {
            var errandList = from errand in Errands
                             join status in ErrandStatuses on errand.StatusId equals status.StatusId
                             join department in Departments on errand.DepartmentId equals department.DepartmentId into departments
                             from dep in departments.DefaultIfEmpty()
                             join employee in Employees on errand.EmployeeId equals employee.EmployeeId into employees
                             from emp in employees.DefaultIfEmpty()
                             orderby errand.RefNumber descending
                             select new MyErrand
                             {
                                 DateOfObservation = errand.DateOfObservation,
                                 ErrandId = errand.ErrandId,
                                 RefNumber = errand.RefNumber,
                                 TypeOfCrime = errand.TypeOfCrime,
                                 StatusName = status.StatusName,
                                 DepartmentName = dep != null ? dep.DepartmentName : "Ej tillsatt",
                                 EmployeeName = emp != null ? emp.EmployeeName : "Ej tillsatt"
                             };
            return errandList;
        }

        public IQueryable<MyErrand> GetErrands(string filter)
        {
            var errand = CoordinatorErrands();

            if (string.IsNullOrEmpty(filter))
            {
                return errand;
            }
            else if (filter.StartsWith("DepartmentName:"))
            {
                string departmentName = filter.Substring("DepartmentName:".Length);
                return errand.Where(e => e.DepartmentName == departmentName);
            }
            else if (filter.StartsWith("EmployeeName:"))
            {
                string employeeName = filter.Substring("EmployeeName:".Length);
                return errand.Where(e => e.EmployeeName == employeeName);
            }
            else
            {
                return errand;
            }
        }

        public IQueryable<MyErrand> investigatorErrand(string employee)
        {
            var errands = GetErrands("EmployeeName:" + employee);
            var investigatorErrands = errands.Where(errand => errand.EmployeeName == employee);
            return investigatorErrands;
        }

        public Employee GetEmployeeDetails(string employeeId)
        {
            return Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
        }

        public IQueryable<EmployeeData> UserData()
        {
            var joinedData = Employees.Join(
                Departments,
                user => user.DepartmentId,
                department => department.DepartmentId,
                (user, department) => new EmployeeData()
                {
                    DepartmentId = department.DepartmentId,
                    DepartmentName = department.DepartmentName,
                    EmployeeName = user.EmployeeName,
                    EmployeeId = user.EmployeeId
                }
            );

            var orderedData = joinedData.OrderBy(user => user.EmployeeId);
            return orderedData;
        }

    }
}