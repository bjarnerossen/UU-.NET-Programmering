using Microsoft.AspNetCore.Mvc;
using Miljoboven.Models;
using System.Collections.Generic;

namespace Miljoboven.Controllers
{
    public class CoordinatorController : Controller
    {

        // Inject the repository
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
            // Print errandId to console for debugging
            Console.WriteLine("Errand ID passed to CrimeCoordinator: " + errandId);

            // Fetch the specific errand by its string ID
            var errand = _repository.GetErrandById(errandId);

            if (errand ==  null)
            {
                return View("ErrandNotFound");
            }

            // Populate the ViewBags with statuses and departments from the repository
            ViewBag.Statuses = _repository.GetStatuses();
            ViewBag.Departments = _repository.GetDepartments();

            // Pass the errand model to the view
            return View(errand);
        }

        public ViewResult ReportCrime()
        {
            return View();
        }

        public ViewResult Thanks()
        {
            return View();
        }

        public ViewResult Validate()
        {
            return View();
        }
    }
}