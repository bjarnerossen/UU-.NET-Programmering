using System.Collections.Generic;

namespace Miljoboven.Models
{
    public interface IMiljobovenRepository
    {
        List<Errand> GetErrands();
        List<string> GetStatuses();
        List<string> GetDepartments();
        List<string> GetInvestigators();
    }

    public class MiljobovenRepository : IMiljobovenRepository
    {
        private List<Errand> _errands = new List<Errand>
        {
            new Errand
            {
                ErrandId = "2023-45-0001",
                Place = "Skogslunden vid Jensens gård",
                TypeOfCrime = "Sopor",
                DateOfObservation = new DateTime(2023,04,24),
                Observation = "Anmälaren var på promenad i skogslunden när hon upptäckte soporna",
                InvestigatorInfo = "Undersökning har gjorts och bland soporna hittades bl.a ett brev till Gösta Olsson",
                InvestigatorAction = "Brev har skickats till Gösta Olsson om soporna och anmälan har gjorts till polisen 2023-05-01",
                InformerName = "Ada Bengtsson",
                InformerPhone = "0432-5545522",
                StatusId = "Klar",
                DepartmentId = "Renhållning och avfall",
                EmployeeId = "Susanne Strid"
            },
            new Errand
            {
                ErrandId = "2023-45-0002",
                Place = "Småstadsjön",
                TypeOfCrime = "Oljeutsläpp",
                DateOfObservation = new DateTime(2023,04,29),
                Observation = "Jag såg en oljefläck på vattnet när jag var där för att fiska",
                InvestigatorInfo = "Undersökning har gjorts på plats, ingen fläck har hittas",
                InvestigatorAction = "",
                InformerName = "Bengt Svensson",
                InformerPhone = "0432-5152255",
                StatusId = "Ingen åtgärd",
                DepartmentId = "Natur och Skogsvård",
                EmployeeId = "Oskar Jansson"
            },
            new Errand
            {
                ErrandId = "2023-45-0003",
                Place = "Ödehuset",
                TypeOfCrime = "Skrot",
                DateOfObservation = new DateTime(2023,05,02),
                Observation = "Anmälaren körde förbi ödehuset och upptäcker ett antal bilar och annat skrot",
                InvestigatorInfo = "Undersökning har gjorts och bilder har tagits",
                InvestigatorAction = "",
                InformerName = "Olle Pettersson",
                InformerPhone = "0432-5255522",
                StatusId = "Påbörjad",
                DepartmentId = "Miljö och Hälsoskydd",
                EmployeeId = "Lena Kristersson"
            },
            new Errand
            {
                ErrandId = "2023-45-0004",
                Place = "Restaurang Krögaren",
                TypeOfCrime = "Buller",
                DateOfObservation = new DateTime(2023,06,04),
                Observation = "Restaurangen hade för högt ljud på så man inte kunde sova",
                InvestigatorInfo = "Bullermätning har gjorts. Man håller sig inom riktvärden",
                InvestigatorAction = "Meddelat restaurangen att tänka på ljudet i fortsättning",
                InformerName = "Roland Jönsson",
                InformerPhone = "0432-5322255",
                StatusId = "Klar",
                DepartmentId = "Miljö och Hälsokydd",
                EmployeeId = "Martin Bäck"
            },
            new Errand
            {
                ErrandId = "2023-45-0005",
                Place = "Torget",
                TypeOfCrime = "Klotter",
                DateOfObservation = new DateTime(2023,07,10),
                Observation = "Samtliga skräpkorgar och bänkar är nedklottrade",
                InvestigatorInfo = "",
                InvestigatorAction = "",
                InformerName = "Peter Svensson",
                InformerPhone = "0432-5322555",
                StatusId = "Inrapporterad",
                DepartmentId = "Ej tillsatt",
                EmployeeId = "Ej tillsatt"
            }
        };

        // Return all errands
        public List<Errand> GetErrands()
        {
            return _errands;
        }

        // Get unique statuses from list of errands
        public List<string> GetStatuses()
        {
            return _errands.Select(e => e.StatusId).Distinct().ToList();
        }

        // Get unique departments from the list of errands
        public List<string> GetDepartments()
        {
            return _errands.Select(e => e.DepartmentId).Distinct().ToList();
        }

        // Get unique Investigator from the list of errands
        public List<string> GetInvestigators()
        {
            return _errands.Select(e => e.EmployeeId).Distinct().ToList();
        }
    }
}
