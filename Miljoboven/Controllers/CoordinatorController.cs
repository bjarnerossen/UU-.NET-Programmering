using Microsoft.AspNetCore.Mvc;
using Miljoboven.Models;

namespace Miljoboven.Controllers
{
    public class CoordinatorController : Controller
    {
        private readonly IMiljobovenRepository _repository;

        public CoordinatorController(IMiljobovenRepository repository)
        {
            _repository = repository;
        }

        public ViewResult StartCoordinator()
        {
            return View(_repository);
        }

        public ViewResult CrimeCoordinator(int id)
        {
            ViewBag.ID = id;
            return View(_repository);
        }

        [HttpPost]
        public ViewResult Validate(Errand errand)
        {
            return View(errand);

        }

        public ViewResult ReportCrime() // Form
        {
            return View();
        }

        public ViewResult Thanks()
        {
            return View();
        }
    }
}