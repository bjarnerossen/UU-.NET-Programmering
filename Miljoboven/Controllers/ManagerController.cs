using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Miljoboven.Models;

namespace Miljoboven.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ManagerController : Controller
    {
        // Inject the repository
        private readonly IMiljobovenRepository _repository;

        public ManagerController(IMiljobovenRepository repository)
        {
            _repository = repository;
        }

        public ViewResult StartManager()
        {
            return View(_repository);
        }


        public ViewResult CrimeManager(int id)
        {
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
