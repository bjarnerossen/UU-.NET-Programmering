namespace Miljoboven.Models
{
    public interface IMiljobovenRepository

    {
        // Queries
        IQueryable<Errand> Errands { get; }
        IQueryable<ErrandStatus> ErrandStatuses { get; }
        IQueryable<Employee> Employees { get; }
        IQueryable<Department> Departments { get; }
        Task<Errand> GetErrandDetails(int id);

        IQueryable<MyErrand> GetErrandListCoordinator();
        IQueryable<MyErrand> GetErrandListInvestigator(string userName);
        IQueryable<MyErrand> GetErrandListManager(string userName);
        IQueryable<EmployeeData> UserData();
        Employee GetEmployeeByUserName(string userName);
        string GetDepartmentIdByUserName(string userName);

        // CRUD Operations
        string SaveErrand(Errand errand);
        void UpdateDepartment(int id, string department);
        void ManagerEdit(int id, string reason, bool noAction, string investigator);
        void InvestigatorEdit(int id, string events, string information, string status);
        void SaveSample(int id, string fileName);
        void SavePicture(int id, string fileName);

        // Filtering Operations
        List<MyErrand> FilterErrands(List<MyErrand> errandList, string status, string department = null, string investigator = null, string caseNumber = null);
    }
};