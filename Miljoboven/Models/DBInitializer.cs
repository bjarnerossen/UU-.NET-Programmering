using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Miljoboven.Models;

namespace Miljoboven.Models
{
    public class DBInitializer
    {
        // The EnsurePopulated method initializes the database with seed data if the specified tables are empty.
        // It checks for content in the Departments, ErrandStatuses, Sequences, Employees, and Errands tables.
        // If any of these tables are empty, it populates them with predefined values, and then saves the changes
        // to the database. This method ensures the application has the necessary initial data to operate properly.
        public static void EnsurePopulated(IServiceProvider services)
        {
            var ctx = services.GetRequiredService<ApplicationDbContext>();

            // Kontrollerar om tabellen Departments har något innehåll
            if (!ctx.Departments.Any())
            {
                ctx.Departments.AddRange(
                    new Department
                    {
                        DepartmentId = "D00",
                        DepartmentName = "Småstads kommun"
                    },
                    new Department
                    {
                        DepartmentId = "D01",
                        DepartmentName = "Tekniska Avloppshanteringen"
                    },
                    new Department
                    {
                        DepartmentId = "D02",
                        DepartmentName = "Klimat och Energi"
                    },
                    new Department
                    {
                        DepartmentId = "D03",
                        DepartmentName = "Miljö och Hälsoskydd"
                    },
                    new Department
                    {
                        DepartmentId = "D04",
                        DepartmentName = "Natur och Skogsvård"
                    },
                    new Department
                    {
                        DepartmentId = "D05",
                        DepartmentName = "Renhållning och Avfall"
                    }
                );
                ctx.SaveChanges();
            }

            // Kontrollerar om tabellen ErrandStatuses har något innehåll
            if (!ctx.ErrandStatuses.Any())
            {
                ctx.ErrandStatuses.AddRange(
                    new ErrandStatus { StatusId = "S_A", StatusName = "Inrapporterad" },
                    new ErrandStatus { StatusId = "S_B", StatusName = "Ingen åtgärd" },
                    new ErrandStatus { StatusId = "S_C", StatusName = "Påbörjad" },
                    new ErrandStatus { StatusId = "S_D", StatusName = "Klar" }
                );
                ctx.SaveChanges();
            }

            // Kontrollerar om tabellen Sequences har något innehåll
            if (!ctx.Sequences.Any())
            {
                ctx.Sequences.Add(new Sequence { CurrentValue = 200 });
                ctx.SaveChanges();
            }

            // Kontrollerar om tabellen Employees har något innehåll
            if (!ctx.Employees.Any())
            {
                ctx.Employees.AddRange(
                    new Employee
                    {
                        EmployeeId = "E001",
                        EmployeeName = "Östen Ärling",
                        RoleTitle = "Coordinator",
                        DepartmentId = "D00"
                    },
                    new Employee
                    {
                        EmployeeId = "E100",
                        EmployeeName = "Anna Åkerman",
                        RoleTitle = "Manager",
                        DepartmentId = "D01"
                    },
                    new Employee
                    {
                        EmployeeId = "E101",
                        EmployeeName = "Fredrik Roos",
                        RoleTitle = "Investigator",
                        DepartmentId = "D01"
                    },
                    new Employee
                    {
                        EmployeeId = "E102",
                        EmployeeName = "Gösta Qvist",
                        RoleTitle = "Investigator",
                        DepartmentId = "D01"
                    },
                    new Employee
                    {
                        EmployeeId = "E103",
                        EmployeeName = "Hilda Persson",
                        RoleTitle = "Investigator",
                        DepartmentId = "D01"
                    },
                    new Employee
                    {
                        EmployeeId = "E200",
                        EmployeeName = "Bengt Viik",
                        RoleTitle = "Manager",
                        DepartmentId = "D02"
                    },
                    new Employee
                    {
                        EmployeeId = "E201",
                        EmployeeName = "Ivar Oscarsson",
                        RoleTitle = "Investigator",
                        DepartmentId = "D02"
                    },
                    new Employee
                    {
                        EmployeeId = "E202",
                        EmployeeName = "Jenny Nordström",
                        RoleTitle = "Investigator",
                        DepartmentId = "D02"
                    },
                    new Employee
                    {
                        EmployeeId = "E203",
                        EmployeeName = "Kurt Mild",
                        RoleTitle = "Investigator",
                        DepartmentId = "D02"
                    },
                    new Employee
                    {
                        EmployeeId = "E300",
                        EmployeeName = "Cecilia Unosson",
                        RoleTitle = "Manager",
                        DepartmentId = "D03"
                    },
                    new Employee
                    {
                        EmployeeId = "E301",
                        EmployeeName = "Lena Larsson",
                        RoleTitle = "Investigator",
                        DepartmentId = "D03"
                    },
                    new Employee
                    {
                        EmployeeId = "E302",
                        EmployeeName = "Martin Kvist",
                        RoleTitle = "Investigator",
                        DepartmentId = "D03"
                    },
                    new Employee
                    {
                        EmployeeId = "E303",
                        EmployeeName = "Nina Jansson",
                        RoleTitle = "Investigator",
                        DepartmentId = "D03"
                    },
                    new Employee
                    {
                        EmployeeId = "E400",
                        EmployeeName = "David Trastlund",
                        RoleTitle = "Manager",
                        DepartmentId = "D04"
                    },
                    new Employee
                    {
                        EmployeeId = "E401",
                        EmployeeName = "Oskar Ivarsson",
                        RoleTitle = "Investigator",
                        DepartmentId = "D04"
                    },
                    new Employee
                    {
                        EmployeeId = "E402",
                        EmployeeName = "Petra Hermansdotter",
                        RoleTitle = "Investigator",
                        DepartmentId = "D04"
                    },
                    new Employee
                    {
                        EmployeeId = "E403",
                        EmployeeName = "Rolf Gunnarsson",
                        RoleTitle = "Investigator",
                        DepartmentId = "D04"
                    },
                    new Employee
                    {
                        EmployeeId = "E500",
                        EmployeeName = "Emma Svanberg",
                        RoleTitle = "Manager",
                        DepartmentId = "D05"
                    },
                    new Employee
                    {
                        EmployeeId = "E501",
                        EmployeeName = "Susanne Fred",
                        RoleTitle = "Investigator",
                        DepartmentId = "D05"
                    },
                    new Employee
                    {
                        EmployeeId = "E502",
                        EmployeeName = "Torsten Embjörn",
                        RoleTitle = "Investigator",
                        DepartmentId = "D05"
                    },
                    new Employee
                    {
                        EmployeeId = "E503",
                        EmployeeName = "Ulla Davidsson",
                        RoleTitle = "Investigator",
                        DepartmentId = "D05"
                    }
                );
                ctx.SaveChanges();
            }

            // Kontrollerar om tabellen Errands har något innehåll
            if (!ctx.Errands.Any())
            {
                ctx.Errands.AddRange(
                    new Errand
                    {
                        RefNumber = "2023-45-195",
                        Place = "Skogslunden vid Jensens gård",
                        TypeOfCrime = "Sopor",
                        DateOfObservation = new DateTime(2023, 04, 24),
                        Observation = "Anmälaren var på promenad i skogslunden när hon upptäckte soporna",
                        InvestigatorInfo = "Undersökning har gjorts och bland soporna hittades bl.a. ett brev till Gösta Olsson",
                        InvestigatorAction = "Brev har skickats till Gösta Olsson om soporna och anmälan har gjorts till polisen 2023-05-01",
                        InformerName = "Ada Bengtsson",
                        InformerPhone = "0432-5545522",
                        StatusId = "S_D",
                        DepartmentId = "D05",
                        EmployeeId = "E501"
                    },
                    new Errand
                    {
                        RefNumber = "2023-45-196",
                        Place = "Småstadsjön",
                        TypeOfCrime = "Oljeutsläpp",
                        DateOfObservation = new DateTime(2023, 04, 29),
                        Observation = "Jag såg en oljefläck på vattnet när jag var där för att fiska",
                        InvestigatorInfo = "Undersökning har gjorts på plats, ingen fläck har hittats",
                        InvestigatorAction = "",
                        InformerName = "Bengt Svensson",
                        InformerPhone = "0432-5152255",
                        StatusId = "S_B",
                        DepartmentId = "D04",
                        EmployeeId = "E401"
                    },
                    new Errand
                    {
                        RefNumber = "2023-45-197",
                        Place = "Ödehuset",
                        TypeOfCrime = "Skrot",
                        DateOfObservation = new DateTime(2023, 05, 02),
                        Observation = "Anmälaren körde förbi ödehuset och upptäckte ett antal bilar och annat skrot",
                        InvestigatorInfo = "Undersökning har gjorts och bilder har tagits",
                        InvestigatorAction = "",
                        InformerName = "Olle Pettersson",
                        InformerPhone = "0432-5255522",
                        StatusId = "S_C",
                        DepartmentId = "D03",
                        EmployeeId = "E301"
                    },
                    new Errand
                    {
                        RefNumber = "2023-45-198",
                        Place = "Restaurang Krögaren",
                        TypeOfCrime = "Buller",
                        DateOfObservation = new DateTime(2023, 06, 04),
                        Observation = "Restaurangen hade för högt ljud på så man inte kunde sova",
                        InvestigatorInfo = "Bullermätning har gjorts. Man håller sig inom riktvärden",
                        InvestigatorAction = "Anmälan är nedlagd",
                        InformerName = "Magnus Larsson",
                        InformerPhone = "0432-5545500",
                        StatusId = "S_B",
                        DepartmentId = "D04",
                        EmployeeId = "E402"
                    }
                );
                ctx.SaveChanges();
            }
        }
    }
}