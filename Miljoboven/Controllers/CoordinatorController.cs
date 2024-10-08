using Microsoft.AspNetCore.Mvc;
using Miljoboven.Models;
using Miljoboven.Extensions;

namespace Miljoboven.Controllers
{
    public class CoordinatorController : Controller
    {
        private readonly IMiljobovenRepository _repository;

        public CoordinatorController(IMiljobovenRepository repository)
        {
            _repository = repository;
        }

        // Fetching errands for Investigator view
        public ViewResult StartCoordinator()
        {
            var errands = _repository.GetErrands();

            // Populate the ViewBags with statuses and departments from the repository
            ViewBag.Statuses = _repository.GetStatuses();
            ViewBag.Departments = _repository.GetDepartments();

            // Pass the errands model to the view
            return View(errands);
        }

        public ViewResult CrimeCoordinator(string errandId)
        {
            // Fetch the specific errand by its string ID
            var errand = _repository.GetErrandById(errandId);

            if (errand == null)
            {
                return View("ErrandNotFound");
            }

            // Populate the ViewBags with statuses and departments from the repository
            ViewBag.Statuses = _repository.GetStatuses();
            ViewBag.Departments = _repository.GetDepartments();

            // Pass the errand model to the view
            return View(errand);
        }

        [HttpPost]
        public ViewResult Validate(Errand errand)
        {
            ViewBag.Title = "Bekräfta - Samordnare";

            if (ModelState.IsValid) // Kontrollera att modellen är giltig
            {
                HttpContext.Session.SetJson("NewErrand", errand);
                return View("Validate", errand); // Returnera Validate-vyn
            }

            return View("../Coordinator/ReportCrime", errand);
        }

        public ViewResult ReportCrime() // Form
        {
            ViewBag.Title = "Coordinator";
            var newErrand = HttpContext.Session.GetJson<Errand>("NewErrand");
            if (newErrand == null)
            {
                return View();
            }
            else
            {
                return View("../Coordinator/Validate", newErrand);
            }
        }

        public ViewResult Thanks()
        {
            return View();
        }
    }
}