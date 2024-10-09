using System.ComponentModel.DataAnnotations;

namespace Miljoboven.Models
{
    public class Errand
    {
        public string ErrandId { get; set; }

        [Required(ErrorMessage = "Du måste fylla i en Plats.")]
        public string Place { get; set; }

        [Required(ErrorMessage = "Du måste fylla i ett Brott.")]
        public string TypeOfCrime { get; set; }

        [Required(ErrorMessage = "Du måste fylla i ett Datum och Tid.")]
        [DisplayFormat(DataFormatString = @"{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime DateOfObservation { get; set; }

        [Display(Name = "Ditt namn (för- och efternamn):")]
        [Required(ErrorMessage = "Du måste fylla i ditt namn.")]
        public string InformerName { get; set; }

        // Regex for swedish telefone number
        [RegularExpression(@"^[0]{1}[0-9]{1,3}-[0-9]{5,9}$",
            ErrorMessage = "Formatet är riktnummer-telefonnummer: 070-1234567")]
        [Required(ErrorMessage = "Du måste fylla i ett Telefonnummer.")]
        public string InformerPhone { get; set; }

        public string Observation { get; set; }

        public string InvestigatorInfo { get; set; }
        public string InvestigatorAction { get; set; }

        public string StatusId { get; set; }

        public string DepartmentId { get; set; }
        public string EmployeeId { get; set; }
    }
}