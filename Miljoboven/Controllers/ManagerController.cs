using Microsoft.AspNetCore.Mvc;
using Miljoboven.Models;
using System.Collections.Generic;

namespace Miljoboven.Controllers
{
    public class ManagerController : Controller
    {
        // Inject the repository
        private readonly IMiljobovenRepository _repository;

        public ManagerController(IMiljobovenRepository repository)
        {
            _repository = repository;
        }

        public ActionResult StartManager()
        {
            var errands = _repository.GetErrands();

            // Populate the ViewBags with statuses and departments from the repository
            ViewBag.Statuses = _repository.GetStatuses();
            ViewBag.Investigators = _repository.GetInvestigators();

            // Pass the errands model to the view
            return View(errands);
        }


        public ActionResult CrimeManager()
        {
            return View();
        }
    }
}
