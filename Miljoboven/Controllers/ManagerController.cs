using Microsoft.AspNetCore.Mvc;
using Miljoboven.Models;

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
            // Case 1: No action selected (Checkbox is checked)
            if (noAction)
            {
                if (!string.IsNullOrEmpty(reason))
                {
                    _repository.ManagerEdit(id, reason, noAction, null);
                    return RedirectToAction("StartManager");
                }
            }
            // Case 2: Action selected (Assigning an investigator)
            else if (!string.IsNullOrEmpty(investigator) && investigator != "Välj")
            {
                _repository.ManagerEdit(id, null, noAction, investigator);
                return RedirectToAction("StartManager");
            }
            // Case 3: If no valid action (no reason for noAction and no valid investigator), remain on the current page
            return RedirectToAction("CrimeManager", new { id });
        }
    }
}
