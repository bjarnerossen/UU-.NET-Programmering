namespace Miljoboven.Models
{
    public class Errand
    {
        public string ErrandId { get; set; }
        public string Place { get; set; }
        public string TypeOfCrime { get; set; }
        public DateTime DateOfObservation { get; set; }
        public string InformerName { get; set; }
        public string InformerPhone { get; set; }
        public string Observation { get; set; }
        public string InvestigatorInfo { get; set; }
        public string InvestigatorAction { get; set; }
        public string StatusId { get; set; }
        public string DepartmentId { get; set; }
        public string EmployeeId { get; set; }
    }
}