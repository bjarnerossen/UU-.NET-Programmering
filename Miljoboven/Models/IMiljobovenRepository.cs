namespace Miljoboven.Models
{
    public interface IMiljobovenRepository
    {
        IQueryable<Errand> GetErrands();
        IQueryable<string> GetStatuses();
        IQueryable<string> GetDepartments();
        IQueryable<string> GetInvestigators();

        Errand GetErrandById(string errandId);
    }
}