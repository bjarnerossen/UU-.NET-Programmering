using System.ComponentModel.DataAnnotations;

namespace Miljoboven.Models;

public class Errand
{
    public int ErrandId { get; set; }

    [Display(Name = "Var har brottet skett någonstans?")]
    [Required(ErrorMessage = "Du måste fylla i en Plats.")]
    public string Place { get; set; }

    [Display(Name = "Vilken typ av brott?")]
    [Required(ErrorMessage = "Du måste fylla i ett Brott.")]
    public string TypeOfCrime { get; set; }

    [Display(Name = "När skedde brottet?")]
    [Required(ErrorMessage = "Du måste fylla i ett Datum och Tid.")]
    [DisplayFormat(DataFormatString = @"{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    [CustomValidation(typeof(Errand), "ValidateDateOfObservation")]
    public DateTime DateOfObservation { get; set; }

    [Display(Name = "Ditt namn (för- och efternamn):")]
    [Required(ErrorMessage = "Du måste fylla i ett namn.")]
    public string InformerName { get; set; }

    // Validering (Regex) för svenskt eller internationellt telefonnummerformat
    [RegularExpression(@"^(\+46|0)[0-9]{1,3}-[0-9]{5,10}$",
        ErrorMessage = "Formatet är riktnummer-telefonnummer: 070-1234567 eller +46-70-1234567")]
    [Display(Name = "Ditt telefonnummer:")]
    [Required(ErrorMessage = "Du måste fylla i ett Telefonnummer.")]
    public string InformerPhone { get; set; }


    [MaxLength(500, ErrorMessage = "Observationen får inte överstiga 500 tecken.")]
    public string Observation { get; set; }

    public string InvestigatorInfo { get; set; }
    public string InvestigatorAction { get; set; }

    public string StatusId { get; set; }

    public string DepartmentId { get; set; }
    public string EmployeeId { get; set; }


    // Validerar att datumet inte ligger i framtiden
    public static ValidationResult ValidateDateOfObservation(DateTime date, ValidationContext context)
    {
        if (date > DateTime.Now)
        {
            return new ValidationResult("Datumet kan inte ligga i framtiden.");
        }
        return ValidationResult.Success;
    }
}