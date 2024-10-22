namespace Miljoboven.Models
{
    public interface IMiljobovenRepository

    {
        IQueryable<Errand> Errands { get; }
        IQueryable<ErrandStatus> ErrandStatuses { get; }
        IQueryable<Employee> Employees { get; }
        IQueryable<Department> Departments { get; }
        Task<Errand> GetErrandDetails(int id);

        string SaveErrand(Errand errand);
        void UpdateDepartment(int id, string department);
        void ManagerEdit(int id, string reason, bool noAction, string investigator);
        void InvestigatorEdit(int id, string events, string information, string status);
        void SaveSample(int id, string fileName);
        void SavePicture(int id, string fileName);
        IQueryable<MyErrand> CoordinatorErrands();
        IQueryable<MyErrand> investigatorErrand(string employee);
        IQueryable<MyErrand> GetErrands(string filter);
        Employee GetEmployeeDetails(string employeeId);
        IQueryable<EmployeeData> UserData();
    }
};