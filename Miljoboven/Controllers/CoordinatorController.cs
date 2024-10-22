using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Miljoboven.Models;
using Miljoboven.Infrastructure;

namespace Miljoboven.Controllers
{
    [Authorize(Roles = "Coordinator")]
    public class CoordinatorController : Controller
    {
        private readonly IMiljobovenRepository _repository;
        private IHttpContextAccessor _contextAcc;

        public CoordinatorController(IMiljobovenRepository repository, IHttpContextAccessor cont)
        {
            _repository = repository;
            _contextAcc = cont;
        }

        public ViewResult StartCoordinator()
        {
            var userName = _contextAcc.HttpContext.User.Identity.Name;
            Employee user = _repository.GetEmployeeDetails(userName);
            ViewBag.Username = user.EmployeeName;

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
            HttpContext.Session.SetJson("CoordinatorNewErrand", errand);
            return View(errand);
        }

        public ViewResult Thanks()
        {
            var errand = HttpContext.Session.GetJson<Errand>("CoordinatorNewErrand");
            _repository.SaveErrand(errand);
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
                _repository.UpdateDepartment(id, department);
                return RedirectToAction("StartCoordinator");
            }
            return RedirectToAction("CrimeCoordinator", new { id });
        }
    }
}