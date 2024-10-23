using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Miljoboven.Models;
using Miljoboven.Infrastructure;

namespace Miljoboven.Controllers
{
    [Authorize(Roles = "Coordinator")]
    public class CoordinatorController : Controller
    {
        private readonly IMiljobovenRepository repository;
        private IHttpContextAccessor contextAcc;

        public CoordinatorController(IMiljobovenRepository repo, IHttpContextAccessor cont)
        {
            repository = repo;
            contextAcc = cont;
        }

        [HttpGet]
        public ViewResult StartCoordinator(string status, string department, string caseNumber)
        {
            ViewBag.Username = contextAcc.HttpContext.User.Identity.Name;

            // Get the complete errand list
            var errandList = repository.GetErrandListCoordinator().ToList();

            // If a case number is provided, filter the list by the exact case number
            if (!string.IsNullOrWhiteSpace(caseNumber))
            {
                errandList = errandList.Where(e => e.RefNumber == caseNumber).ToList();
            }
            else
            {
                // Filter by status if it's not "Välj alla" or empty
                if (!string.IsNullOrWhiteSpace(status) && status != "Välj alla")
                {
                    errandList = errandList.Where(e => e.StatusName == status).ToList();
                }

                // Filter by department if it's not "Välj alla" or empty
                if (!string.IsNullOrWhiteSpace(department) && department != "Välj alla")
                {
                    errandList = errandList.Where(e => e.DepartmentName == department).ToList();
                }
            }

            // Check if the result list is empty
            if (!errandList.Any())
            {
                ViewBag.Message = $"Inga ärenden hittades för:\nAvdelning: {department} \nStatus: {status}";
            }

            // Assign the filtered list to the view
            ViewBag.ErrandList = errandList;

            return View(repository);
        }

        public ViewResult CrimeCoordinator(int id)
        {
            ViewBag.ID = id;
            return View(repository); ;
        }

        [HttpPost]
        public ViewResult Validate(Errand errand)
        {
            HttpContext.Session.SetJson("CoordinatorNewErrand", errand);
            return View(errand);
        }

        public ViewResult Thanks()
        {
            var errand = HttpContext.Session.GetJson<Errand>("CoordinatorNewErrand");
            repository.SaveErrand(errand);
            ViewBag.RefNumber = errand.RefNumber;
            HttpContext.Session.Remove("CoordinatorNewErrand");
            return View(errand);
        }

        public ViewResult ReportCrime() // Form
        {
            var newErrand = HttpContext.Session.GetJson<Errand>("CoordinatorNewErrand");
            if (newErrand == null)
            {
                return View();
            }
            else
            {
                return View(newErrand);
            }
        }

        [HttpPost]
        public IActionResult SaveDepartment(int id, string department)
        {
            if (department != null && department != "Välj" && department != "D00")
            {
                repository.UpdateDepartment(id, department);
                return RedirectToAction("StartCoordinator");
            }
            return RedirectToAction("CrimeCoordinator", new { id });
        }
    }
}