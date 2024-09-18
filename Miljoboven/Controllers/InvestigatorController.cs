using Microsoft.AspNetCore.Mvc;
using Miljoboven.Models;
using System.Collections.Generic;

namespace Miljoboven.Controllers
{
    public class InvestigatorController : Controller
    {

        // Inject the repository
        private readonly IMiljobovenRepository _repository;

        public InvestigatorController(IMiljobovenRepository repository)
        {
            _repository = repository;
        }

        public ViewResult StartInvestigator()
        {
            var errands = _repository.GetErrands();

            // Populate the ViewBags with statuses and departments from the repository
            ViewBag.Statuses = _repository.GetStatuses();
            ViewBag.Departments = _repository.GetDepartments();

            // Pass the errands model to the view
            return View(errands);
        }

        public ViewResult CrimeInvestigator()
        {
            return View();
        }
    }
}
