using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Miljoboven.Models;

namespace Miljoboven.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ManagerController : Controller
    {
        private readonly IMiljobovenRepository repository;
        private IHttpContextAccessor contextAcc;

        public ManagerController(IMiljobovenRepository repo, IHttpContextAccessor cont)
        {
            repository = repo;
            contextAcc = cont;
        }

        [HttpGet]
        public ViewResult StartManager(string status, string investigator, string caseNumber)
        {
            ViewBag.UserName = contextAcc.HttpContext.User.Identity.Name;
            string user = contextAcc.HttpContext.User.Identity.Name;

            var errandList = repository.GetErrandListManager(user).ToList();
            if (!string.IsNullOrWhiteSpace(caseNumber))
            {
                errandList = errandList.Where(e => e.RefNumber == caseNumber).ToList();
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(status) && status != "Välj alla")
                {
                    errandList = errandList.Where(e => e.StatusName == status).ToList();
                }
                if (!string.IsNullOrWhiteSpace(investigator) && investigator != "Välj alla")
                {
                    errandList = errandList.Where(e => e.EmployeeName == investigator).ToList();
                }
            }
            if (!errandList.Any())
            {
                ViewBag.Message = $"Inga ärenden hittades för: \nStatus: {status} \nHandläggare: {investigator}";
            }
            ViewBag.ErrandList = errandList;
            ViewBag.Employee = repository.GetEmployee(user);
            ViewBag.Department = repository.GetDepartmentFromEmployee(user);
            return View(repository);
        }


        public ViewResult CrimeManager(int id)
        {
            ViewBag.UserName = contextAcc.HttpContext.User.Identity.Name;
            string user = contextAcc.HttpContext.User.Identity.Name;
            ViewBag.Employee = repository.GetEmployee(user);
            ViewBag.Department = repository.GetDepartmentFromEmployee(user);
            ViewBag.ID = id;
            return View(repository);
        }

        [HttpPost]
        public IActionResult SaveManagerChanges(int id, string reason, bool noAction, string investigator)
        {
            // Check if the investigator was not selected and no action was not taken
            if (investigator == "Välj" && noAction == false)
            {
                return RedirectToAction("CrimeManager", new { id });
            }
            else
            {
                repository.ManagerEdit(id, reason, noAction, investigator);
                return RedirectToAction("StartManager");
            }
        }
    }
}
