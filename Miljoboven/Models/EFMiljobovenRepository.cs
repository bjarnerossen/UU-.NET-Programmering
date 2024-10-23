using Microsoft.EntityFrameworkCore;

namespace Miljoboven.Models
{
    public class EFMiljobovenRepository : IMiljobovenRepository
    {
        private readonly ApplicationDbContext context;

        public EFMiljobovenRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Department> Departments => context.Departments;
        public IQueryable<Errand> Errands => context.Errands.Include(e => e.Samples).Include(e => e.Pictures);

        public IQueryable<ErrandStatus> ErrandStatuses => context.ErrandStatuses;
        public IQueryable<Employee> Employees => context.Employees;
        public IQueryable<Picture> Pictures => context.Pictures;
        public IQueryable<Sample> Samples => context.Samples;
        public IQueryable<Sequence> Sequences => context.Sequences;

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

            context.Errands.Add(errand);
            context.SaveChanges();

            return errand.RefNumber;
        }

        public void UpdateDepartment(int id, string department)
        {
            var errand = Errands.FirstOrDefault(e => e.ErrandId == id);
            errand.DepartmentId = department;
            context.SaveChanges();
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

            context.SaveChanges();
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
            context.SaveChanges();
        }

        public void SavePicture(int id, string fileName)
        {
            var picture = new Picture();
            picture.ErrandId = id;
            picture.PictureName = fileName;
            context.Pictures.Add(picture);
            context.SaveChanges();
        }

        public void SaveSample(int id, string fileName)
        {
            var sample = new Sample();
            sample.ErrandId = id;
            sample.SampleName = fileName;
            context.Samples.Add(sample);
            context.SaveChanges();
        }
        public IQueryable<MyErrand> GetErrandListCoordinator()
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

        public IQueryable<MyErrand> GetErrandListInvestigator(string userName)
        {
            Employee emp = GetEmployee(userName);

            var errandList = from err in Errands

                             join stat in ErrandStatuses on err.StatusId equals stat.StatusId
                             join dep in Departments on err.DepartmentId equals dep.DepartmentId
                             into departmentErrand
                             from deptE in departmentErrand.DefaultIfEmpty()

                             join em in Employees on err.EmployeeId equals em.EmployeeId into employeeErrand
                             from empE in employeeErrand
                             where empE.EmployeeId == emp.EmployeeId
                             orderby err.RefNumber descending

                             select new MyErrand
                             {
                                 DateOfObservation = err.DateOfObservation,
                                 ErrandId = err.ErrandId,
                                 RefNumber = err.RefNumber,
                                 TypeOfCrime = err.TypeOfCrime,
                                 StatusName = stat.StatusName,
                                 DepartmentName = (err.DepartmentId == null ? "ej tillsatt" : deptE.DepartmentName),
                                 EmployeeName = (err.EmployeeId == null ? "ej tillsatt" : empE.EmployeeName)
                             };
            return errandList;

        }

        public IQueryable<MyErrand> GetErrandListManager(string userName)
        {
            Employee emp = GetEmployee(userName);


            var errandList = from err in Errands

                             join stat in ErrandStatuses on err.StatusId equals stat.StatusId
                             join dep in Departments on err.DepartmentId equals dep.DepartmentId
                             into departmentErrand
                             from deptE in departmentErrand
                             where deptE.DepartmentId == emp.DepartmentId

                             join em in Employees on err.EmployeeId equals em.EmployeeId into employeeErrand
                             from empE in employeeErrand.DefaultIfEmpty()
                             orderby err.RefNumber descending

                             select new MyErrand
                             {
                                 DateOfObservation = err.DateOfObservation,
                                 ErrandId = err.ErrandId,
                                 RefNumber = err.RefNumber,
                                 TypeOfCrime = err.TypeOfCrime,
                                 StatusName = stat.StatusName,
                                 DepartmentName = (err.DepartmentId == null ? "ej tillsatt" : deptE.DepartmentName),
                                 EmployeeName = (err.EmployeeId == null ? "ej tillsatt" : empE.EmployeeName)
                             };
            return errandList;

        }

        public Employee GetEmployee(string userName)
        {
            Employee emp = new Employee();
            foreach (Employee em in Employees)
            {
                if (em.EmployeeId == userName)
                {
                    emp = em;
                }
            }
            return emp;

        }

        public string GetDepartmentFromEmployee(string user)
        {
            Employee em = new Employee();

            foreach (Employee em2 in Employees)
            {
                if (em2.EmployeeId == user)
                {
                    em = em2;
                }
            }
            String depart = em.DepartmentId;
            return depart;

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