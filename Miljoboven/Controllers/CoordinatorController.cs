using Microsoft.AspNetCore.Mvc;
using Miljoboven.Models;
using Miljoboven.Infrastructure;

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
            return View(_repository); ;
        }

        [HttpPost]
        public ViewResult Validate(Errand errand)
        {
            HttpContext.Session.SetJson("NewErrand", errand);
            return View("../Coordinator/Validate", errand);
        }

        public ViewResult Thanks()
        {
            var errand = HttpContext.Session.GetJson<Errand>("NewErrand");
            _repository.SaveErrand(errand);
            ViewBag.RefNumber = errand.RefNumber;
            HttpContext.Session.Remove("NewErrand");
            return View(errand);
        }

        public ViewResult ReportCrime() // Form
        {
            var newErrand = HttpContext.Session.GetJson<Errand>("NewErrand");
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
                _repository.UpdateDepartment(id, department);
                return RedirectToAction("StartCoordinator");
            }
            return RedirectToAction("CrimeCoordinator", new { id });
        }
    }
}