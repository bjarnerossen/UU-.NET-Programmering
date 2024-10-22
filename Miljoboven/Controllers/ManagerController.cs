using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Miljoboven.Models;

namespace Miljoboven.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ManagerController : Controller
    {
        private readonly IMiljobovenRepository _repository;
        private IHttpContextAccessor _contextAcc;

        public ManagerController(IMiljobovenRepository repository, IHttpContextAccessor cont)
        {
            _repository = repository;
            _contextAcc = cont;
        }

        public ViewResult StartManager()
        {
            var userId = _contextAcc.HttpContext.User.Identity.Name;
            EmployeeData details = _repository.UserData().FirstOrDefault(d => d.EmployeeId == userId);
            ViewBag.DepartmentName = details.DepartmentName;
            ViewBag.DepartmentId = details.DepartmentId;
            return View(_repository);
        }


        public ViewResult CrimeManager(int id)
        {
            var departmentId = _contextAcc.HttpContext.User.Identity.Name;
            Employee user = _repository.GetEmployeeDetails(departmentId);
            ViewBag.DepartmentId = user.DepartmentId;
            ViewBag.Username = user.EmployeeName;
            ViewBag.ID = id;
            return View(_repository);
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
                _repository.ManagerEdit(id, reason, noAction, investigator);
                return RedirectToAction("StartManager");
            }
        }
    }
}
