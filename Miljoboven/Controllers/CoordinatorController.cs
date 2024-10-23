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
            var errandList = repository.GetErrandListCoordinator().ToList();

            errandList = repository.FilterErrands(errandList, status, department, caseNumber: caseNumber);

            if (!errandList.Any())
            {
                ViewBag.Message = $"Inga ärenden hittades för:\nAvdelning: {department} \nStatus: {status}";
            }

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
            ViewBag.errand = errand;
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