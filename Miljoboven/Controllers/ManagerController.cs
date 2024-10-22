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

        public ViewResult StartManager()
        {
	        ViewBag.UserName = contextAcc.HttpContext.User.Identity.Name;
			string user = contextAcc.HttpContext.User.Identity.Name;
			ViewBag.ErrandList = repository.GetErrandListManager(user);
			ViewBag.Employee = repository.GetEmployee(user);
			ViewBag.Department = repository.GetDepartmentFromEmployee(user);
            return View(repository);
        }


        public ViewResult CrimeManager(int id)
        {
            // var departmentId = contextAcc.HttpContext.User.Identity.Name;
            // Employee user = repository.GetEmployeeDetails(departmentId);
            // ViewBag.DepartmentId = user.DepartmentId;
            // ViewBag.Username = user.EmployeeName;
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
