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
            return View(errands);
        }

        public ViewResult CrimeCoordinator()
        {
            return View();
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